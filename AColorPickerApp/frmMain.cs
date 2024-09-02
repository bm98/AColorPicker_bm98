/******************************************************************/
/*****                                                        *****/
/*****     Project:           Adobe Color Picker Clone 1      *****/
/*****     Filename:          frmMain.cs                      *****/
/*****     Original Author:   Danny Blanchard                 *****/
/*****                        - scrabcakes@gmail.com          *****/
/*****     Updates:	                                          *****/
/*****      3/28/2005 - Initial Version : Danny Blanchard     *****/
/*****                                                        *****/
/******************************************************************/

// bm98 added Stored Color Settings

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using bm98_AColorPicker;

namespace adobe_color_picker_clone_part_1
{
  /// <summary>
  /// This is just an example form, you probably wouldn't want to use this in your own project.
  /// </summary>
  public class frmMain : System.Windows.Forms.Form
  {
    #region Class Variables

    private System.Windows.Forms.Label m_lbl_CurrentlySelectedColorLabel;
    private System.Windows.Forms.Label m_lbl_ClickDescription;
    private System.Windows.Forms.Label m_lbl_SelectedColor;
    private System.Windows.Forms.Label m_lbl_HexColor;
    private DrawStyle m_eDrawStyle = DrawStyle.Hue;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion

    #region Constructors / Destructors

    public frmMain( )
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent( );

      //	Display the color hex value
      string red = Convert.ToString( m_lbl_SelectedColor.BackColor.R, 16 );
      if (red.Length < 2) red = "0" + red;
      string green = Convert.ToString( m_lbl_SelectedColor.BackColor.G, 16 );
      if (green.Length < 2) green = "0" + green;
      string blue = Convert.ToString( m_lbl_SelectedColor.BackColor.B, 16 );
      if (blue.Length < 2) blue = "0" + blue;

      m_lbl_HexColor.Text = "#" + red.ToUpper( ) + green.ToUpper( ) + blue.ToUpper( );
      m_lbl_HexColor.Update( );
    }


    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if (disposing) {
        if (components != null) {
          components.Dispose( );
        }
      }
      base.Dispose( disposing );
    }


    #endregion

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.m_lbl_SelectedColor = new System.Windows.Forms.Label( );
      this.m_lbl_CurrentlySelectedColorLabel = new System.Windows.Forms.Label( );
      this.m_lbl_HexColor = new System.Windows.Forms.Label( );
      this.m_lbl_ClickDescription = new System.Windows.Forms.Label( );
      this.SuspendLayout( );
      // 
      // m_lbl_SelectedColor
      // 
      this.m_lbl_SelectedColor.BackColor = System.Drawing.Color.Red;
      this.m_lbl_SelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_lbl_SelectedColor.Location = new System.Drawing.Point( 8, 8 );
      this.m_lbl_SelectedColor.Name = "m_lbl_SelectedColor";
      this.m_lbl_SelectedColor.Size = new System.Drawing.Size( 80, 56 );
      this.m_lbl_SelectedColor.TabIndex = 0;
      this.m_lbl_SelectedColor.Click += new System.EventHandler( this.m_lbl_SelectedColor_Click );
      // 
      // m_lbl_CurrentlySelectedColorLabel
      // 
      this.m_lbl_CurrentlySelectedColorLabel.Location = new System.Drawing.Point( 96, 8 );
      this.m_lbl_CurrentlySelectedColorLabel.Name = "m_lbl_CurrentlySelectedColorLabel";
      this.m_lbl_CurrentlySelectedColorLabel.Size = new System.Drawing.Size( 136, 24 );
      this.m_lbl_CurrentlySelectedColorLabel.TabIndex = 1;
      this.m_lbl_CurrentlySelectedColorLabel.Text = "Currently Selected Color:";
      // 
      // m_lbl_HexColor
      // 
      this.m_lbl_HexColor.Location = new System.Drawing.Point( 96, 40 );
      this.m_lbl_HexColor.Name = "m_lbl_HexColor";
      this.m_lbl_HexColor.Size = new System.Drawing.Size( 136, 24 );
      this.m_lbl_HexColor.TabIndex = 2;
      this.m_lbl_HexColor.Text = "#xxxxxx";
      // 
      // m_lbl_ClickDescription
      // 
      this.m_lbl_ClickDescription.Location = new System.Drawing.Point( 8, 72 );
      this.m_lbl_ClickDescription.Name = "m_lbl_ClickDescription";
      this.m_lbl_ClickDescription.Size = new System.Drawing.Size( 344, 24 );
      this.m_lbl_ClickDescription.TabIndex = 3;
      this.m_lbl_ClickDescription.Text = "(Click on color to open the color picker dialog box.)";
      // 
      // frmMain
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.ClientSize = new System.Drawing.Size( 360, 94 );
      this.Controls.Add( this.m_lbl_ClickDescription );
      this.Controls.Add( this.m_lbl_HexColor );
      this.Controls.Add( this.m_lbl_CurrentlySelectedColorLabel );
      this.Controls.Add( this.m_lbl_SelectedColor );
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMain";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Adobe Color Picker Clone Part 1 Test Form";
      this.ResumeLayout( false );

    }
    #endregion

    #region Application Code

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main( )
    {
      Application.Run( new frmMain( ) );
    }


    #endregion

    #region Events

    frmAColorPicker ColorDialog = new frmAColorPicker( Color.Red, "Picker Test" );

    private void m_lbl_SelectedColor_Click( object sender, System.EventArgs e )
    {
      ColorDialog.DrawStyle = m_eDrawStyle;
      ColorDialog.PrimaryColor = m_lbl_SelectedColor.BackColor;
      ColorDialog.SettingsSupport( SaveSettings, LoadSettings );

      if (ColorDialog.ShowDialog( this ) == DialogResult.OK) {
        //	Display the color
        m_lbl_SelectedColor.BackColor = ColorDialog.PrimaryColor;

        //	Display the color hex value
        string red = Convert.ToString( m_lbl_SelectedColor.BackColor.R, 16 );
        if (red.Length < 2) red = "0" + red;
        string green = Convert.ToString( m_lbl_SelectedColor.BackColor.G, 16 );
        if (green.Length < 2) green = "0" + green;
        string blue = Convert.ToString( m_lbl_SelectedColor.BackColor.B, 16 );
        if (blue.Length < 2) blue = "0" + blue;

        m_lbl_HexColor.Text = "#" + red.ToUpper( ) + green.ToUpper( ) + blue.ToUpper( );
        m_lbl_HexColor.Update( );

        m_eDrawStyle = ColorDialog.DrawStyle;
      }
    }

    // bm98 added Stored Color Settings
    private string LoadSettings( )
    {
      AppSettings.Instance.Reload( );
      return AppSettings.Instance.StoredColors;
    }
    private void SaveSettings( string settings )
    {
      AppSettings.Instance.Reload( );
      AppSettings.Instance.StoredColors = settings;
      AppSettings.Instance.Save( );
    }

    #endregion
  }
}
