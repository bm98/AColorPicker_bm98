using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using dNetBm98.ColorModel;

using static dNetBm98.XMath;
using static dNetBm98.XColor;

namespace bm98_AColorPicker
{
  /// <summary>
  /// Drawing Style of the Color Display
  /// </summary>
  public enum DrawStyle
  {
    Hue,
    Saturation,
    Brightness,
    Red,
    Green,
    Blue,
    Transparent
  }

  /// <summary>
  /// Color Picker Form
  /// </summary>
  public partial class frmAColorPicker : Form
  {
    // Original Source derived from:
    // https://www.codeproject.com/Articles/9959/Adobe-Color-Picker-Clone-Part-1
    //
    // port to VS2022 as UC / .Net Framework 4.8
    // complete infrastructure rework (managing resources and restructure of call sequence)
    // adding Transparent Color handing
    // adding a color patches queue and save to Settings


    private Color c_TransparentRGB = Color.FromArgb( 128, 204, 93, 91 ); //  - something; just to have some impression - use a not likely color

    private HSB m_hsb;
    private Color m_rgb;
    private CMYK m_cmyk;

    // support Settings
    private Action<string> _saveSettingsMethod = null;
    private Func<string> _loadSettingsCallback = null;

    #region Support

    // convert a Color to a hex number (rrggbb)
    private void WriteHexData( Color rgb )
    {
      m_txt_Hex.Text = rgb.ToColorHrgb( ).Replace( "#", "" ); // without #
    }


    // convert a hex string to Color
    private Color ParseHexData( string hex_data )
    {
      return ("#" + hex_data).FromColorHrgb( );
    }

    // fill all textboxes from main color variables
    private void UpdateTextBoxes( )
    {
      m_txt_Hue.Text = RoundUp( m_hsb.H ).ToString( );
      m_txt_Sat.Text = RoundUp( m_hsb.S * 100 ).ToString( );
      m_txt_Black.Text = RoundUp( m_hsb.B * 100 ).ToString( );
      m_txt_Cyan.Text = RoundUp( m_cmyk.C * 100 ).ToString( );
      m_txt_Magenta.Text = RoundUp( m_cmyk.M * 100 ).ToString( );
      m_txt_Yellow.Text = RoundUp( m_cmyk.Y * 100 ).ToString( );
      m_txt_K.Text = RoundUp( m_cmyk.K * 100 ).ToString( );
      m_txt_Red.Text = m_rgb.R.ToString( );
      m_txt_Green.Text = m_rgb.G.ToString( );
      m_txt_Blue.Text = m_rgb.B.ToString( );

      WriteHexData( m_rgb );
    }

    // Update GUI parts from from main color variables
    private void UpdateFromMainColor( )
    {
      UpdateTextBoxes( );

      m_lbl_Primary_Color.BackColor = m_rgb;
      m_lbl_Primary_Color.Update( );
    }

    // Update the main RGB field and its dependencies
    private void UpdateRGB( Color color, bool updateBig, bool updateThin )
    {
      if (color.A < 255) {
        // opacity not 100% - set Transparent mode
        DrawStyle = DrawStyle.Transparent;
      }
      else if (DrawStyle == DrawStyle.Transparent) {
        // Opaque and still Transparent mode - reset to Hue
        DrawStyle = DrawStyle.Hue;
      }

      m_rgb = color;
      m_hsb = HSB.FromRgb( m_rgb );
      m_cmyk = CMYK.FromRgb( m_rgb );

      UpdateFromMainColor( );

      if (updateBig) m_ctrl_BigBox.HSB = m_hsb;
      if (updateThin) m_ctrl_ThinBox.HSB = m_hsb;
    }

    // Update the main HSB field and its dependencies
    private void UpdateHSB( HSB hSB, bool updateBig, bool updateThin )
    {
      m_hsb = hSB;
      m_rgb = HSB.ToRgb( m_hsb );
      m_cmyk = CMYK.FromRgb( m_rgb );

      UpdateFromMainColor( );

      if (updateBig) m_ctrl_BigBox.HSB = m_hsb;
      if (updateThin) m_ctrl_ThinBox.HSB = m_hsb;
    }

    #endregion

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="starting_color">The initial color of the Picker</param>
    /// <param name="title">The Dialog Title</param>
    public frmAColorPicker( Color starting_color, string title )
    {
      InitializeComponent( );

      UpdateRGB( starting_color, true, true );
      m_lbl_Secondary_Color.BackColor = starting_color;

      // seems to not propagate as expected
      m_ctrl_BigBox.BackColor = this.BackColor;
      m_ctrl_ThinBox.BackColor = this.BackColor;

      this.Text = title;
      m_rbtn_Hue.Checked = true;
    }

    #region Events

    #region General Events

    // called when newly shown
    private void frmAColorPicker_Shown( object sender, EventArgs e )
    {
      // load patches
      string setting = _loadSettingsCallback?.Invoke( );
      if (!string.IsNullOrEmpty( setting )) {
        UC_ColorQueue.FromConfigString( colQueue, setting );
      }
      UpdateRGB( PrimaryColor, true, true );
      m_txt_Hex.Focus( );
      m_txt_Hex.SelectAll( );
    }

    // called when loaded
    private void frmColorPicker_Load( object sender, System.EventArgs e )
    {
    }

    // OK Button, leave dialog
    private void m_cmd_OK_Click( object sender, System.EventArgs e )
    {
      // store last pick as patch and save to Settings
      if (this.DrawStyle != DrawStyle.Transparent) {
        // cannot store Transparent color
        colQueue.EnqueuePatch( PrimaryColor );
        // save patches
        _saveSettingsMethod?.Invoke( UC_ColorQueue.ToConfigString( colQueue ) );
      }

      this.DialogResult = DialogResult.OK;
      this.Hide( );
    }


    // Cancel Button, leave dialog
    private void m_cmd_Cancel_Click( object sender, System.EventArgs e )
    {
      this.DialogResult = DialogResult.Cancel;
      this.Hide( );
    }

    // picked a color patch
    private void colQueue_ColorPicked( object sender, ColorPickedEventArgs e )
    {
      UpdateRGB( e.PickedColor, true, true );
    }

    #endregion

    #region Color picker area and slider

    private void m_ctrl_BigBox_Scroll( object sender, MouseEventArgs e )
    {
      UpdateHSB( m_ctrl_BigBox.HSB, false, true );
    }

    private void m_ctrl_ThinBox_Scroll( object sender, MouseEventArgs e )
    {
      UpdateHSB( m_ctrl_ThinBox.HSB, true, false );
    }

    #endregion

    #region Hex Box (m_txt_Hex)

    private void m_txt_Hex_Leave( object sender, EventArgs e )
    {
      string text = m_txt_Hex.Text.ToUpper( );
      bool has_illegal_chars = false;

      if (text.Length <= 0)
        has_illegal_chars = true;
      foreach (char letter in text) {
        if (!char.IsNumber( letter )) {
          if (letter >= 'A' && letter <= 'F')
            continue;
          has_illegal_chars = true;
          break;
        }
      }

      if (has_illegal_chars) {
        MessageBox.Show( "Hex must be a hex value between 0x000000 and 0xFFFFFF" );
        WriteHexData( m_rgb );
        return;
      }

      UpdateRGB( ParseHexData( text ), true, true );
    }

    #endregion

    #region Color Boxes

    private void m_lbl_Primary_Color_Click( object sender, EventArgs e )
    {
      UpdateRGB( m_lbl_Primary_Color.BackColor, true, true );
    }

    private void m_lbl_Secondary_Color_Click( object sender, EventArgs e )
    {
      UpdateRGB( m_lbl_Secondary_Color.BackColor, true, true );
    }

    #endregion

    #region Radio Buttons

    private void m_rbtn_Hue_CheckedChanged( object sender, EventArgs e )
    {
      if (m_rbtn_Hue.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Hue;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Hue;
      }
    }


    private void m_rbtn_Sat_CheckedChanged( object sender, EventArgs e )
    {
      if (m_rbtn_Sat.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Saturation;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Saturation;
      }
    }


    private void m_rbtn_Black_CheckedChanged( object sender, EventArgs e )
    {
      if (m_rbtn_Black.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Brightness;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Brightness;
      }
    }


    private void m_rbtn_Red_CheckedChanged( object sender, EventArgs e )
    {
      if (m_rbtn_Red.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Red;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Red;
      }
    }


    private void m_rbtn_Green_CheckedChanged( object sender, EventArgs e )
    {
      if (m_rbtn_Green.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Green;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Green;
      }
    }


    private void m_rbtn_Blue_CheckedChanged( object sender, EventArgs e )
    {
      if (m_rbtn_Blue.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Blue;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Blue;
      }
    }

    private void rbTransparent_CheckedChanged( object sender, EventArgs e )
    {
      if (rbTransparent.Checked) {
        m_ctrl_ThinBox.DrawStyle = DrawStyle.Transparent;
        m_ctrl_BigBox.DrawStyle = DrawStyle.Transparent;
      }
    }

    #endregion

    #region Text Boxes

    // text validation fields (local to this region)
    private string _editText = "";
    private bool _skipUpdate = false;

    // generic enter
    private void m_txt_Enter( object sender, EventArgs e )
    {
      if (!(sender is TextBox)) return;
      TextBox tx = (TextBox)sender;
      _editText = tx.Text;
      _skipUpdate = false;
    }

    // generic validating
    private void m_txt_ValidatingNumber( object sender, CancelEventArgs e )
    {
      if (!(sender is TextBox)) return;
      TextBox tx = (TextBox)sender;
      if (_editText == tx.Text) {
        // has not changed - don't update values
        _skipUpdate = true;
      }
      else {
        // text has changed - look for errors
        bool valid_chars = (tx.Text.Length > 0) && tx.Text.All( c => char.IsNumber( c ) );
        if (!valid_chars) {
          MessageBox.Show( $"'{tx.Text}' is not a valid entry for this field" );
          UpdateTextBoxes( );

          e.Cancel = true;
        }
        // else pass as Validated
      }
    }


    private void m_txt_Hue_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Hue.Text;

      var hsl = m_hsb;
      int hue = int.Parse( text ); // assuming it is only numbers...

      if ((hue < 0) || (hue >= 360)) {
        m_txt_Hue.Text = "0"; hsl.H = 0.0;
      }
      else {
        hsl.H = hue;
      }
      UpdateHSB( hsl, true, true );
    }


    private void m_txt_Sat_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Sat.Text;

      var hsl = m_hsb;
      int sat = int.Parse( text );

      if (sat < 0) {
        m_txt_Sat.Text = "0"; hsl.S = 0.0;
      }
      else if (sat > 100) {
        m_txt_Sat.Text = "100"; hsl.S = 1.0;
      }
      else {
        hsl.S = (double)sat / 100;
      }

      UpdateHSB( hsl, true, true );
    }


    private void m_txt_Black_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Black.Text;

      var hsl = m_hsb;
      int lum = int.Parse( text );

      if (lum < 0) {
        m_txt_Black.Text = "0"; hsl.B = 0.0;
      }
      else if (lum > 100) {
        m_txt_Black.Text = "100"; hsl.B = 1.0;
      }
      else {
        hsl.B = (double)lum / 100;
      }

      UpdateHSB( hsl, true, true );
    }


    private void m_txt_Red_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Red.Text;

      Color rgb = m_rgb;
      int red = int.Parse( text );

      if (red < 0) {
        m_txt_Sat.Text = "0"; rgb = Color.FromArgb( 0, m_rgb.G, m_rgb.B );
      }
      else if (red > 255) {
        m_txt_Sat.Text = "255"; rgb = Color.FromArgb( 255, m_rgb.G, m_rgb.B );
      }
      else {
        rgb = Color.FromArgb( red, m_rgb.G, m_rgb.B );
      }

      UpdateRGB( rgb, true, true );
    }


    private void m_txt_Green_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Green.Text;

      Color rgb = m_rgb;
      int green = int.Parse( text );

      if (green < 0) {
        m_txt_Green.Text = "0"; rgb = Color.FromArgb( m_rgb.R, 0, m_rgb.B );
      }
      else if (green > 255) {
        m_txt_Green.Text = "255"; rgb = Color.FromArgb( m_rgb.R, 255, m_rgb.B );
      }
      else {
        rgb = Color.FromArgb( m_rgb.R, green, m_rgb.B );
      }

      UpdateRGB( rgb, true, true );
    }


    private void m_txt_Blue_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Blue.Text;

      Color rgb = m_rgb;
      int blue = int.Parse( text );

      if (blue < 0) {
        m_txt_Blue.Text = "0"; rgb = Color.FromArgb( m_rgb.R, m_rgb.G, 0 );
      }
      else if (blue > 255) {
        m_txt_Blue.Text = "255"; rgb = Color.FromArgb( m_rgb.R, m_rgb.G, 255 );
      }
      else {
        rgb = Color.FromArgb( m_rgb.R, m_rgb.G, blue );
      }

      UpdateRGB( rgb, true, true );
    }


    private void m_txt_Cyan_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Cyan.Text;

      var cmyk = m_cmyk;
      int cyan = int.Parse( text );

      if (cyan < 0) {
        m_txt_Cyan.Text = "0"; cmyk.C = 0.0;
      }
      else if (cyan > 100) {
        m_txt_Cyan.Text = "100"; cmyk.C = 1.0;
      }
      else {
        cmyk.C = (double)cyan / 100;
      }

      UpdateRGB( CMYK.ToRgb( cmyk ), true, true );
    }


    private void m_txt_Magenta_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Magenta.Text;

      var cmyk = m_cmyk;
      int magenta = int.Parse( text );

      if (magenta < 0) {
        m_txt_Magenta.Text = "0"; cmyk.M = 0.0;
      }
      else if (magenta > 100) {
        m_txt_Magenta.Text = "100"; cmyk.M = 1.0;
      }
      else {
        cmyk.M = (double)magenta / 100;
      }

      UpdateRGB( CMYK.ToRgb( cmyk ), true, true );
    }


    private void m_txt_Yellow_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_Yellow.Text;

      var cmyk = m_cmyk;
      int yellow = int.Parse( text );

      if (yellow < 0) {
        m_txt_Yellow.Text = "0"; cmyk.Y = 0.0;
      }
      else if (yellow > 100) {
        m_txt_Yellow.Text = "100"; cmyk.Y = 1.0;
      }
      else {
        cmyk.Y = (double)yellow / 100;
      }

      UpdateRGB( CMYK.ToRgb( cmyk ), true, true );
    }


    private void m_txt_K_Validated( object sender, EventArgs e )
    {
      if (_skipUpdate) { _skipUpdate = false; return; } // don't do anything if it has not changed

      string text = m_txt_K.Text;

      var cmyk = m_cmyk;
      int key = int.Parse( text );

      if (key < 0) {
        m_txt_K.Text = "0"; cmyk.K = 0.0;
      }
      else if (key > 100) {
        m_txt_K.Text = "100"; cmyk.K = 1.0;
      }
      else {
        cmyk.K = (double)key / 100;
      }

      UpdateRGB( CMYK.ToRgb( cmyk ), true, true );
    }


    #endregion

    #endregion

    #region Public Methods

    /// <summary>
    /// Get;Set: The Dialog Title
    /// </summary>
    public string Title {
      get => this.Text;
      set => this.Text = value;
    }

    /// <summary>
    /// Get;Set; The intial or picked Color
    /// </summary>
    public Color PrimaryColor {
      get {
        if (DrawStyle == DrawStyle.Transparent) return Color.Transparent;
        return m_rgb;
      }
      set {
        Color col = value;
        if (value == Color.Transparent) {
          col = c_TransparentRGB;
          DrawStyle = DrawStyle.Transparent;
        }
        UpdateRGB( col, true, true );
        m_lbl_Primary_Color.BackColor = col;
        m_lbl_Secondary_Color.BackColor = col;
      }
    }

    /// <summary>
    /// The kind of color picking
    /// </summary>
    public DrawStyle DrawStyle {
      get {
        if (m_rbtn_Hue.Checked)
          return DrawStyle.Hue;
        else if (m_rbtn_Sat.Checked)
          return DrawStyle.Saturation;
        else if (m_rbtn_Black.Checked)
          return DrawStyle.Brightness;
        else if (m_rbtn_Red.Checked)
          return DrawStyle.Red;
        else if (m_rbtn_Green.Checked)
          return DrawStyle.Green;
        else if (m_rbtn_Blue.Checked)
          return DrawStyle.Blue;
        else if (rbTransparent.Checked)
          return DrawStyle.Transparent;
        else
          return DrawStyle.Hue;
      }
      set {
        switch (value) {
          case DrawStyle.Hue:
            m_rbtn_Hue.Checked = true;
            break;
          case DrawStyle.Saturation:
            m_rbtn_Sat.Checked = true;
            break;
          case DrawStyle.Brightness:
            m_rbtn_Black.Checked = true;
            break;
          case DrawStyle.Red:
            m_rbtn_Red.Checked = true;
            break;
          case DrawStyle.Green:
            m_rbtn_Green.Checked = true;
            break;
          case DrawStyle.Blue:
            m_rbtn_Blue.Checked = true;
            break;
          case DrawStyle.Transparent:
            rbTransparent.Checked = true;
            break;
          default:
            m_rbtn_Hue.Checked = true;
            break;
        }
      }
    }

    /// <summary>
    /// Support Save and Load of Settings
    /// </summary>
    /// <param name="saveMethod">Setting Save method</param>
    /// <param name="loadCallback">Setting Load function</param>
    public void SettingsSupport( Action<string> saveMethod, Func<string> loadCallback )
    {
      _saveSettingsMethod = saveMethod;
      _loadSettingsCallback = loadCallback;
    }

    #endregion


  }
}
