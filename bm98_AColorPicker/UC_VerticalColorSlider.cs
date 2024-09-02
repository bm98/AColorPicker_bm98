using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using dNetBm98.ColorModel;

using static dNetBm98.XColor;
using static dNetBm98.XMath;

namespace bm98_AColorPicker
{
  [DefaultEvent( "Scroll" )]
  public partial class UC_VerticalColorSlider : UserControl
  {
    // Original Source derived from:
    // https://www.codeproject.com/Articles/9959/Adobe-Color-Picker-Clone-Part-1
    //
    // port to VS2022 as UC / .Net Framework 4.8
    // complete infrastructure rework (managing resources and restructure of call sequence)
    // adding Transparent Color handing
    // remove border drawing (not longer in PS..)

    //	Slider properties
    private int m_iMarker_Start_Y = 0;
    private bool m_bDragging = false;

    //	These variables keep track of how to fill in the content inside the box;
    private DrawStyle m_DrawStyle = DrawStyle.Hue;
    private HSB m_hsb;
    private Color m_rgb;

    private bool _resetSliderFlag = false;

    /// <summary>
    /// Event occurs when the vertical slider is moved
    /// </summary>
    [Description( "Marker has moved" ), Category( "Action" )]
    public new event EventHandler<MouseEventArgs> Scroll;
    private void OnScroll( MouseEventArgs e ) => Scroll?.Invoke( this, e );

    #region Support

    /// <summary>
    /// Redraws the background over the slider area on both sides of the control
    /// </summary>
    private void ClearSlider( Graphics g )
    {
      using (Brush brush = new SolidBrush( this.BackColor )) { // SystemBrushes.Control;
        g.FillRectangle( brush, 0, 0, 8, this.Height );       //	clear left hand slider
        g.FillRectangle( brush, this.Width - 8, 0, 8, this.Height );  //	clear right hand slider
      }
    }


    /// <summary>
    /// Draws the slider arrows on both sides of the control.
    /// </summary>
    /// <param name="position">position value of the slider, lowest being at the bottom.  The range
    /// is between 0 and the controls height-9.  The values will be adjusted if too large/small</param>
    /// <param name="Unconditional">If Unconditional is true, the slider is drawn, otherwise some logic 
    /// is performed to determine is drawing is really neccessary.</param>
    private void DrawSlider( Graphics g, int position, bool Unconditional )
    {
      if (position < 0) position = 0;
      if (position > this.Height - 9) position = this.Height - 9;

      if (m_iMarker_Start_Y == position && !Unconditional)  //	If the marker position hasn't changed
        return;                       //	since the last time it was drawn and we don't HAVE to redraw
                                      //	then exit procedure

      m_iMarker_Start_Y = position; //	Update the controls marker position

      this.ClearSlider( g );    //	Remove old slider

      using (Pen pencil = new Pen( Color.FromArgb( 116, 114, 106 ) )) {  //	Same gray color Photoshop uses
        Brush brush = Brushes.White;

        Point[] arrow = new Point[7];               //	 GGG
        arrow[0] = new Point( 1, position );        //	G   G
        arrow[1] = new Point( 3, position );        //	G    G
        arrow[2] = new Point( 7, position + 4 );    //	G     G
        arrow[3] = new Point( 3, position + 8 );    //	G      G
        arrow[4] = new Point( 1, position + 8 );    //	G     G
        arrow[5] = new Point( 0, position + 7 );    //	G    G
        arrow[6] = new Point( 0, position + 1 );    //	G   G
                                                    //	 GGG

        g.FillPolygon( brush, arrow );  //	Fill left arrow with white
        g.DrawPolygon( pencil, arrow ); //	Draw left arrow border with gray

        //	    GGG
        arrow[0] = new Point( this.Width - 2, position );     //	   G   G
        arrow[1] = new Point( this.Width - 4, position );     //	  G    G
        arrow[2] = new Point( this.Width - 8, position + 4 ); //	 G     G
        arrow[3] = new Point( this.Width - 4, position + 8 ); //	G      G
        arrow[4] = new Point( this.Width - 2, position + 8 ); //	 G     G
        arrow[5] = new Point( this.Width - 1, position + 7 ); //	  G    G
        arrow[6] = new Point( this.Width - 1, position + 1 ); //	   G   G
                                                              //	    GGG

        g.FillPolygon( brush, arrow );  //	Fill right arrow with white
        g.DrawPolygon( pencil, arrow ); //	Draw right arrow border with gray
      }
    }

    /// <summary>
    /// Evaluates the DrawStyle of the control and calls the appropriate
    /// drawing function for content
    /// </summary>
    private void DrawContent( Graphics g )
    {
      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          Draw_Style_Hue( g );
          break;
        case DrawStyle.Saturation:
          Draw_Style_Saturation( g );
          break;
        case DrawStyle.Brightness:
          Draw_Style_Luminance( g );
          break;
        case DrawStyle.Red:
          Draw_Style_Red( g );
          break;
        case DrawStyle.Green:
          Draw_Style_Green( g );
          break;
        case DrawStyle.Blue:
          Draw_Style_Blue( g );
          break;
      }
    }


    #region Draw_Style_X - Content drawing functions

    //	The following functions do the real work of the control, drawing the primary content (the area between the slider)
    //	

    /// <summary>
    /// Fills in the content of the control showing all values of Hue (from 0 to 360)
    /// </summary>
    private void Draw_Style_Hue( Graphics g )
    {
      var _hsb = new HSB( 0, 1, 1 ); //	S and L will both be at 100% for this DrawStyle

      for (int i = 0; i < this.Height - 8; i++) //	i represents the current line of pixels we want to draw horizontally
      {
        _hsb.HS = 1.0 - (double)i / (this.Height - 8);     //	H (hue) is based on the current vertical position
        using (Pen pen = new Pen( HSB.ToRgb( _hsb ) )) {            //	Get the Color for this line
          g.DrawLine( pen, 11, i + 4, this.Width - 11, i + 4 ); //	Draw the line and loop back for next line
        }
      }
    }


    /// <summary>
    /// Fills in the content of the control showing all values of Saturation (0 to 100%) for the given
    /// Hue and Luminance.
    /// </summary>
    private void Draw_Style_Saturation( Graphics g )
    {
      var _hsb = new HSB( m_hsb ); //	Use the H and L values of the current color (m_hsl)

      for (int i = 0; i < this.Height - 8; i++) //	i represents the current line of pixels we want to draw horizontally
      {
        _hsb.S = 1.0 - (double)i / (this.Height - 8);     //	S (Saturation) is based on the current vertical position
        using (Pen pen = new Pen( HSB.ToRgb( _hsb ) )) {  //	Get the Color for this line
          g.DrawLine( pen, 11, i + 4, this.Width - 11, i + 4 ); //	Draw the line and loop back for next line
        }
      }
    }


    /// <summary>
    /// Fills in the content of the control showing all values of Luminance (0 to 100%) for the given
    /// Hue and Saturation.
    /// </summary>
    private void Draw_Style_Luminance( Graphics g )
    {
      var _hsb = new HSB( m_hsb ); //	Use the H and S values of the current color (m_hsl)

      for (int i = 0; i < this.Height - 8; i++) //	i represents the current line of pixels we want to draw horizontally
      {
        _hsb.B = 1.0 - (double)i / (this.Height - 8);     //	L (Luminance) is based on the current vertical position
        using (Pen pen = new Pen( HSB.ToRgb( _hsb ) )) {  //	Get the Color for this line
          g.DrawLine( pen, 11, i + 4, this.Width - 11, i + 4 ); //	Draw the line and loop back for next line
        }
      }
    }


    /// <summary>
    /// Fills in the content of the control showing all values of Red (0 to 255) for the given
    /// Green and Blue.
    /// </summary>
    private void Draw_Style_Red( Graphics g )
    {
      for (int i = 0; i < this.Height - 8; i++) //	i represents the current line of pixels we want to draw horizontally
      {
        int red = 255 - ColorComp( 255 * (double)i / (this.Height - 8) ); //	red is based on the current vertical position
        using (Pen pen = new Pen( Color.FromArgb( red, m_rgb.G, m_rgb.B ) )) { //	Get the Color for this line
          g.DrawLine( pen, 11, i + 4, this.Width - 11, i + 4 );     //	Draw the line and loop back for next line
        }
      }
    }


    /// <summary>
    /// Fills in the content of the control showing all values of Green (0 to 255) for the given
    /// Red and Blue.
    /// </summary>
    private void Draw_Style_Green( Graphics g )
    {
      for (int i = 0; i < this.Height - 8; i++) //	i represents the current line of pixels we want to draw horizontally
      {
        int green = 255 - ColorComp( 255 * (double)i / (this.Height - 8) ); //	green is based on the current vertical position
        using (Pen pen = new Pen( Color.FromArgb( m_rgb.R, green, m_rgb.B ) )) { //	Get the Color for this line
          g.DrawLine( pen, 11, i + 4, this.Width - 11, i + 4 );     //	Draw the line and loop back for next line
        }
      }
    }


    /// <summary>
    /// Fills in the content of the control showing all values of Blue (0 to 255) for the given
    /// Red and Green.
    /// </summary>
    private void Draw_Style_Blue( Graphics g )
    {
      for (int i = 0; i < this.Height - 8; i++) //	i represents the current line of pixels we want to draw horizontally
      {
        int blue = 255 - ColorComp( 255 * (double)i / (this.Height - 8) );  //	green is based on the current vertical position
        using (Pen pen = new Pen( Color.FromArgb( m_rgb.R, m_rgb.G, blue ) )) {  //	Get the Color for this line
          g.DrawLine( pen, 11, i + 4, this.Width - 11, i + 4 );     //	Draw the line and loop back for next line
        }
      }
    }

    #endregion

    /// <summary>
    /// Calls all the functions neccessary to redraw the entire control.
    /// </summary>
    private void Redraw_Control( Graphics g )
    {
      if (_resetSliderFlag) {
        Reset_Slider( g );
        _resetSliderFlag = false;
      }

      DrawSlider( g, m_iMarker_Start_Y, true );

      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          Draw_Style_Hue( g );
          break;
        case DrawStyle.Saturation:
          Draw_Style_Saturation( g );
          break;
        case DrawStyle.Brightness:
          Draw_Style_Luminance( g );
          break;
        case DrawStyle.Red:
          Draw_Style_Red( g );
          break;
        case DrawStyle.Green:
          Draw_Style_Green( g );
          break;
        case DrawStyle.Blue:
          Draw_Style_Blue( g );
          break;
        case DrawStyle.Transparent:
          // draw nothing
          break;
      }
    }


    /// <summary>
    /// Resets the vertical position of the slider to match the controls color.  Gives the option of redrawing the slider.
    /// </summary>
    /// <param name="Redraw">Set to true if you want the function to redraw the slider after determining the best position</param>
    private void Reset_Slider( Graphics g )
    {
      //	The position of the marker (slider) changes based on the current drawstyle:
      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * m_hsb.HS );
          break;
        case DrawStyle.Saturation:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * m_hsb.S );
          break;
        case DrawStyle.Brightness:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * m_hsb.B );
          break;
        case DrawStyle.Red:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * (double)m_rgb.R / 255 );
          break;
        case DrawStyle.Green:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * (double)m_rgb.G / 255 );
          break;
        case DrawStyle.Blue:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * (double)m_rgb.B / 255 );
          break;
        case DrawStyle.Transparent:
          m_iMarker_Start_Y = (this.Height - 8) - RoundUp( (this.Height - 8) * 0 );
          break;
      }

      DrawSlider( g, m_iMarker_Start_Y, true );
    }


    /// <summary>
    /// Resets the controls color (both HSB and RGB variables) based on the current slider position
    /// </summary>
    private void ResetHSBRGB( )
    {
      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          m_hsb.HS = 1.0 - (double)m_iMarker_Start_Y / (this.Height - 9);
          m_rgb = HSB.ToRgb( m_hsb );
          break;
        case DrawStyle.Saturation:
          m_hsb.S = 1.0 - (double)m_iMarker_Start_Y / (this.Height - 9);
          m_rgb = HSB.ToRgb( m_hsb );
          break;
        case DrawStyle.Brightness:
          m_hsb.B = 1.0 - (double)m_iMarker_Start_Y / (this.Height - 9);
          m_rgb = HSB.ToRgb( m_hsb );
          break;
        case DrawStyle.Red:
          m_rgb = Color.FromArgb( 255 - ColorComp( 255 * (double)m_iMarker_Start_Y / (this.Height - 9) ), m_rgb.G, m_rgb.B );
          m_hsb = HSB.FromRgb( m_rgb );
          break;
        case DrawStyle.Green:
          m_rgb = Color.FromArgb( m_rgb.R, 255 - ColorComp( 255 * (double)m_iMarker_Start_Y / (this.Height - 9) ), m_rgb.B );
          m_hsb = HSB.FromRgb( m_rgb );
          break;
        case DrawStyle.Blue:
          m_rgb = Color.FromArgb( m_rgb.R, m_rgb.G, 255 - ColorComp( 255 * (double)m_iMarker_Start_Y / (this.Height - 9) ) );
          m_hsb = HSB.FromRgb( m_rgb );
          break;
        case DrawStyle.Transparent:
          // leave them alone
          break;
      }
    }

    #endregion

    /// <summary>
    /// cTor:
    /// </summary>
    public UC_VerticalColorSlider( )
    {
      InitializeComponent( );

      //	Initialize Colors
      m_hsb = new HSB( 1.0, 1.0, 1.0 );
      m_rgb = HSB.ToRgb( m_hsb );
      m_DrawStyle = DrawStyle.Hue;
    }


    /// <summary>
    /// The drawstyle of the contol (Hue, Saturation, Brightness, Red, Green or Blue)
    /// </summary>
    [Description( "Set DrawStyle of this control" ), Category( "Behavior" )]
    public DrawStyle DrawStyle {
      get {
        return m_DrawStyle;
      }
      set {
        m_DrawStyle = value;
        this.Enabled = (m_DrawStyle != DrawStyle.Transparent);

        //	Redraw the control based on the new DrawStyle
        _resetSliderFlag = true;
        this.Refresh( );
      }
    }


    /// <summary>
    /// The HSB color of the control, changing the HSB will automatically change the RGB color for the control.
    /// </summary>
    [Browsable( false ), Description( "Set HSB value of this control" ), Category( "Appearance" )]
    public HSB HSB {
      get {
        return m_hsb;
      }
      set {
        m_hsb = value;
        m_rgb = HSB.ToRgb( m_hsb );

        //	Redraw the control based on the new color.
        _resetSliderFlag = true;
        this.Refresh( );
      }
    }


    /// <summary>
    /// The RGB color of the control, changing the RGB will automatically change the HSL color for the control.
    /// </summary>
    [Description( "Set RGB value of this control" ), Category( "Appearance" )]
    public Color RGB {
      get {
        return m_rgb;
      }
      set {
        m_rgb = value;
        m_hsb = HSB.FromRgb( m_rgb );

        //	Redraw the control based on the new color.
        _resetSliderFlag = true;
        this.Refresh( );
      }
    }

    #region Event Handling

    private void UC_VerticalColorSlider_Load( object sender, EventArgs e )
    {
      this.Refresh( );
    }

    private void UC_VerticalColorSlider_Resize( object sender, EventArgs e )
    {
      this.Refresh( );
    }

    private void UC_VerticalColorSlider_Paint( object sender, PaintEventArgs e )
    {
      Redraw_Control( e.Graphics );
    }


    private void UC_VerticalColorSlider_MouseDown( object sender, MouseEventArgs e )
    {
      if (e.Button != MouseButtons.Left)  //	Only respond to left mouse button events
        return;

      m_bDragging = true;   //	Begin dragging which notifies MouseMove function that it needs to update the marker

      int y;
      y = e.Y;
      y -= 4;                     //	Calculate slider position
      if (y < 0) y = 0;
      if (y > this.Height - 9) y = this.Height - 9;

      if (y == m_iMarker_Start_Y)         //	If the slider hasn't moved, no need to redraw it.
        return;                   //	or send a scroll notification

      using (Graphics g = this.CreateGraphics( )) {
        DrawSlider( g, y, false ); //	Redraw the slider
      }
      ResetHSBRGB( );     //	Reset the color

      OnScroll( e );//	Notify anyone who cares that the controls marker (selected color) has changed
    }

    private void UC_VerticalColorSlider_MouseMove( object sender, MouseEventArgs e )
    {
      if (!m_bDragging)   //	Only respond when the mouse is dragging the marker.
        return;

      int y;
      y = e.Y;
      y -= 4;                     //	Calculate slider position
      if (y < 0) y = 0;
      if (y > this.Height - 9) y = this.Height - 9;

      if (y == m_iMarker_Start_Y)         //	If the slider hasn't moved, no need to redraw it.
        return;                   //	or send a scroll notification

      using (Graphics g = this.CreateGraphics( )) {
        DrawSlider( g, y, false ); //	Redraw the slider
      }
      ResetHSBRGB( );     //	Reset the color

      OnScroll( e );//	Notify anyone who cares that the controls marker (selected color) has changed
    }

    private void UC_VerticalColorSlider_MouseUp( object sender, MouseEventArgs e )
    {
      if (e.Button != MouseButtons.Left)  //	Only respond to left mouse button events
        return;

      m_bDragging = false;

      int y;
      y = e.Y;
      y -= 4;                     //	Calculate slider position
      if (y < 0) y = 0;
      if (y > this.Height - 9) y = this.Height - 9;

      if (y == m_iMarker_Start_Y)         //	If the slider hasn't moved, no need to redraw it.
        return;                   //	or send a scroll notification

      using (Graphics g = this.CreateGraphics( )) {
        DrawSlider( g, y, false ); //	Redraw the slider
      }
      ResetHSBRGB( );     //	Reset the color

      OnScroll( e );//	Notify anyone who cares that the controls marker (selected color) has changed
    }

    #endregion

  }
}
