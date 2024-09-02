using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using dNetBm98.ColorModel;

using static dNetBm98.XColor;
using static dNetBm98.XMath;

namespace bm98_AColorPicker
{
  [DefaultEvent( "Scroll" )]
  public partial class UC_2DColorBox : UserControl
  {
    // Original Source derived from:
    // https://www.codeproject.com/Articles/9959/Adobe-Color-Picker-Clone-Part-1
    //
    // port to VS2022 as UC / .Net Framework 4.8
    // complete infrastructure rework (managing resources and restructure of call sequence)
    // adding Transparent Color handing
    // remove border drawing (not longer in PS..)

    private int m_iMarker_X = 0;
    private int m_iMarker_Y = 0;
    private bool m_bDragging = false;

    //	These variables keep track of how to fill in the content inside the box;
    private DrawStyle m_DrawStyle = DrawStyle.Hue;
    private HSB m_hsb;
    private Color m_rgb;

    private bool _resetMarkerFlag = false;
    // clipping to not overdraw
    private Region _clip;

    /// <summary>
    /// Event occurs when the marker is moved
    /// </summary>
    [Description( "Marker has moved" ), Category( "Action" )]
    public new event EventHandler<MouseEventArgs> Scroll;
    private void OnScroll( MouseEventArgs e ) => Scroll?.Invoke( this, e );


    #region Support

    /// <summary>
    /// Redraws only the content over the marker
    /// </summary>
    private void ClearMarker( Graphics g )
    {
      //	Determine the area that needs to be redrawn
      int start_x, start_y, end_x, end_y;
      HSB hsb_start = new HSB( );
      HSB hsb_end = new HSB( );

      //	Find the markers corners
      start_x = m_iMarker_X - 5;
      start_y = m_iMarker_Y - 5;
      end_x = m_iMarker_X + 5;
      end_y = m_iMarker_Y + 5;
      //	Adjust the area if part of it hangs outside the content area
      if (start_x < 0) start_x = 0;
      if (start_y < 0) start_y = 0;
      if (end_x > this.Width - 4) end_x = this.Width - 4;
      if (end_y > this.Height - 4) end_y = this.Height - 4;

      int green;
      int red;
      //	Redraw the content based on the current draw style:
      //	The code get's a little messy from here
      switch (m_DrawStyle) {
        //		  S=0,S=1,S=2,S=3.....S=100
        //	L=100
        //	L=99
        //	L=98		Drawstyle
        //	L=97		   Hue
        //	...
        //	L=0
        case DrawStyle.Hue:

          hsb_start.H = m_hsb.H; hsb_end.H = m_hsb.H; //	Hue is constant
          hsb_start.S = (double)start_x / (this.Width - 4); //	Because we're drawing horizontal lines, s will not change
          hsb_end.S = (double)end_x / (this.Width - 4);   //	from line to line

          for (int i = start_y; i <= end_y; i++)    //	For each horizontal line:
          {
            hsb_start.B = 1.0 - (double)i / (this.Height - 4);  //	Brightness (L) WILL change for each horizontal
            hsb_end.B = hsb_start.B;              //	line drawn

            using (LinearGradientBrush br = new LinearGradientBrush(
                        new Rectangle( start_x + 1, i + 2, end_x - start_x + 1, 1 ),
                        HSB.ToRgb( hsb_start ),
                        HSB.ToRgb( hsb_end ), 0, false )) {
              g.FillRectangle( br, new Rectangle( start_x + 2, i + 2, end_x - start_x + 1, 1 ) );
            }
          }

          break;
        //		  H=0,H=1,H=2,H=3.....H=360
        //	L=100
        //	L=99
        //	L=98		Drawstyle
        //	L=97		Saturation
        //	...
        //	L=0
        case DrawStyle.Saturation:

          hsb_start.S = m_hsb.S; hsb_end.S = m_hsb.S;     //	Saturation is constant
          hsb_start.B = 1.0 - (double)start_y / (this.Height - 4);  //	Because we're drawing vertical lines, L will 
          hsb_end.B = 1.0 - (double)end_y / (this.Height - 4);    //	not change from line to line

          for (int i = start_x; i <= end_x; i++)        //	For each vertical line:
          {
            hsb_start.HS = (double)i / (this.Width - 4);     //	Hue (H) WILL change for each vertical
            hsb_end.H = hsb_start.H;              //	line drawn

            using (LinearGradientBrush br = new LinearGradientBrush(
                          new Rectangle( i + 2, start_y + 1, 1, end_y - start_y + 2 ),
                          HSB.ToRgb( hsb_start ),
                          HSB.ToRgb( hsb_end ), 90, false )) {
              g.FillRectangle( br, new Rectangle( i + 2, start_y + 2, 1, end_y - start_y + 1 ) );
            }
          }
          break;
        //		  H=0,H=1,H=2,H=3.....H=360
        //	S=100
        //	S=99
        //	S=98		Drawstyle
        //	S=97		Brightness
        //	...
        //	S=0
        case DrawStyle.Brightness:

          hsb_start.B = m_hsb.B; hsb_end.B = m_hsb.B;     //	Luminance is constant
          hsb_start.S = 1.0 - (double)start_y / (this.Height - 4);  //	Because we're drawing vertical lines, S will 
          hsb_end.S = 1.0 - (double)end_y / (this.Height - 4);    //	not change from line to line

          for (int i = start_x; i <= end_x; i++)        //	For each vertical line:
          {
            hsb_start.HS = (double)i / (this.Width - 4);     //	Hue (H) WILL change for each vertical
            hsb_end.H = hsb_start.H;              //	line drawn

            using (LinearGradientBrush br = new LinearGradientBrush(
                          new Rectangle( i + 2, start_y + 1, 1, end_y - start_y + 2 ),
                          HSB.ToRgb( hsb_start ),
                          HSB.ToRgb( hsb_end ), 90, false )) {
              g.FillRectangle( br, new Rectangle( i + 2, start_y + 2, 1, end_y - start_y + 1 ) );
            }
          }

          break;
        //		  B=0,B=1,B=2,B=3.....B=100
        //	G=100
        //	G=99
        //	G=98		Drawstyle
        //	G=97		   Red
        //	...
        //	G=0
        case DrawStyle.Red:

          red = m_rgb.R;  //	Red is constant
          int start_b = ColorComp( 255 * (double)start_x / (this.Width - 4) );  //	Because we're drawing horizontal lines, B
          int end_b = ColorComp( 255 * (double)end_x / (this.Width - 4) );    //	will not change from line to line

          for (int i = start_y; i <= end_y; i++)            //	For each horizontal line:
          {
            green = ColorComp( 255 - (255 * (double)i / (this.Height - 4)) ); //	green WILL change for each horizontal line drawn

            using (LinearGradientBrush br = new LinearGradientBrush(
                          new Rectangle( start_x + 1, i + 2, end_x - start_x + 1, 1 ),
                          Color.FromArgb( red, green, start_b ),
                          Color.FromArgb( red, green, end_b ), 0, false )) {
              g.FillRectangle( br, new Rectangle( start_x + 2, i + 2, end_x - start_x + 1, 1 ) );
            }
          }

          break;
        //		  B=0,B=1,B=2,B=3.....B=100
        //	R=100
        //	R=99
        //	R=98		Drawstyle
        //	R=97		  Green
        //	...
        //	R=0
        case DrawStyle.Green:

          green = m_rgb.G;  //	Green is constant
          int start_b2 = ColorComp( 255 * (double)start_x / (this.Width - 4) ); //	Because we're drawing horizontal lines, B
          int end_b2 = ColorComp( 255 * (double)end_x / (this.Width - 4) );   //	will not change from line to line

          for (int i = start_y; i <= end_y; i++)            //	For each horizontal line:
          {
            red = ColorComp( 255 - (255 * (double)i / (this.Height - 4)) );   //	red WILL change for each horizontal line drawn

            using (LinearGradientBrush br = new LinearGradientBrush(
                            new Rectangle( start_x + 1, i + 2, end_x - start_x + 1, 1 ),
                            Color.FromArgb( red, green, start_b2 ),
                            Color.FromArgb( red, green, end_b2 ), 0, false )) {
              g.FillRectangle( br, new Rectangle( start_x + 2, i + 2, end_x - start_x + 1, 1 ) );
            }
          }

          break;
        //		  R=0,R=1,R=2,R=3.....R=100
        //	G=100
        //	G=99
        //	G=98		Drawstyle
        //	G=97		   Blue
        //	...
        //	G=0
        case DrawStyle.Blue:

          int blue = m_rgb.B;   //	Blue is constant
          int start_r = ColorComp( 255 * (double)start_x / (this.Width - 4) );  //	Because we're drawing horizontal lines, R
          int end_r = ColorComp( 255 * (double)end_x / (this.Width - 4) );    //	will not change from line to line

          for (int i = start_y; i <= end_y; i++)            //	For each horizontal line:
          {
            green = ColorComp( 255 - (255 * (double)i / (this.Height - 4)) ); //	green WILL change for each horizontal line drawn

            using (LinearGradientBrush br = new LinearGradientBrush(
                            new Rectangle( start_x + 1, i + 2, end_x - start_x + 1, 1 ),
                            Color.FromArgb( start_r, green, blue ),
                            Color.FromArgb( end_r, green, blue ), 0, false )) {
              g.FillRectangle( br, new Rectangle( start_x + 2, i + 2, end_x - start_x + 1, 1 ) );
            }
          }

          break;
      }
    }


    /// <summary>
    /// Draws the marker (circle) inside the box
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void DrawMarker( Graphics g, int x, int y ) //     *****
    {                                                   //    *  |  *
      if (x < 0) x = 0;                                 //   *   |   *
      if (x > this.Width - 4) x = this.Width - 4;       //  *    |    *
      if (y < 0) y = 0;                                 //  *    |    *
      if (y > this.Height - 4) y = this.Height - 4;     //  *----X----*
                                                        //  *    |    *
                                                        //  *    |    *
                                                        //   *   |   *
                                                        //    *  |  *
      ClearMarker( g );                                 //     *****

      m_iMarker_X = x;
      m_iMarker_Y = y;

      var _hsb = GetColor( x, y );  //	The selected color determines the color of the marker drawn over
                                                 //	it (black or white)

      Pen pen;
      { // using pen...
        if (_hsb.B < (double)200 / 255)
          pen = new Pen( Color.White );                 //	White marker if selected color is dark
        else if (_hsb.H < (double)26 / 360 || _hsb.H > (double)200 / 360)
          if (_hsb.S > (double)70 / 255)
            pen = new Pen( Color.White );
          else
            pen = new Pen( Color.Black );               //	Else use a black marker for lighter colors
        else
          pen = new Pen( Color.Black );
        g.DrawEllipse( pen, x - 3, y - 3, 10, 10 );           //	Draw the marker : 11 x 11 circle
      }
      pen.Dispose( );
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
        case DrawStyle.Transparent:
          // draw nothing
          break;
      }
    }


    /// <summary>
    /// Draws the content of the control filling in all color values with the provided Hue value.
    /// </summary>
    private void Draw_Style_Hue( Graphics g )
    {
      var hsb_start = new HSB( h: m_hsb.H, s: 0, b: 0 );
      var hsb_end = new HSB( h: m_hsb.H, s: 1, b: 0 );

      for (int i = 0; i < this.Height - 4; i++)       //	For each horizontal line in the control:
      {
        hsb_start.B = 1.0 - (double)i / (this.Height - 4);  //	Calculate luminance at this line (Hue and Saturation are constant)
        hsb_end.B = hsb_start.B;

        using (LinearGradientBrush br = new LinearGradientBrush(
                    new Rectangle( 2, 2, this.Width - 4, 1 ),
                    HSB.ToRgb( hsb_start ),
                    HSB.ToRgb( hsb_end ), 0, false )) {
          g.FillRectangle( br, new Rectangle( 2, i + 2, this.Width - 4, 1 ) );
        }
      }
    }


    /// <summary>
    /// Draws the content of the control filling in all color values with the provided Saturation value.
    /// </summary>
    private void Draw_Style_Saturation( Graphics g )
    {
      var hsb_start = new HSB( h: 0, s: m_hsb.S, b: 1 );
      var hsb_end = new HSB( h: 0, s: m_hsb.S, b: 0 );

      for (int i = 0; i < this.Width - 4; i++)    //	For each vertical line in the control:
      {
        hsb_start.HS = (double)i / (this.Width - 4); //	Calculate Hue at this line (Saturation and Luminance are constant)
        hsb_end.HS = hsb_start.HS;

        using (LinearGradientBrush br = new LinearGradientBrush(
                    new Rectangle( 2, 2, 1, this.Height - 4 ),
                    HSB.ToRgb( hsb_start ),
                    HSB.ToRgb( hsb_end ), 90, false )) {
          g.FillRectangle( br, new Rectangle( i + 2, 2, 1, this.Height - 4 ) );
        }
      }
    }


    /// <summary>
    /// Draws the content of the control filling in all color values with the provided Luminance or Brightness value.
    /// </summary>
    private void Draw_Style_Luminance( Graphics g )
    {
      var hsb_start = new HSB( h: 0, s: 1, b: m_hsb.B );
      var hsb_end = new HSB( h: 0, s: 0, b: m_hsb.B );

      for (int i = 0; i < this.Width - 4; i++)    //	For each vertical line in the control:
      {
        hsb_start.HS = (double)i / (this.Width - 4); //	Calculate Hue at this line (Saturation and Luminance are constant)
        hsb_end.HS = hsb_start.HS;

        using (LinearGradientBrush br = new LinearGradientBrush(
                  new Rectangle( 2, 2, 1, this.Height - 4 ),
                  HSB.ToRgb( hsb_start ),
                  HSB.ToRgb( hsb_end ), 90, false )) {
          g.FillRectangle( br, new Rectangle( i + 2, 2, 1, this.Height - 4 ) );
        }
      }
    }


    /// <summary>
    /// Draws the content of the control filling in all color values with the provided Red value.
    /// </summary>
    private void Draw_Style_Red( Graphics g )
    {
      int red = m_rgb.R; ;

      for (int i = 0; i < this.Height - 4; i++)       //	For each horizontal line in the control:
      {
        //	Calculate Green at this line (Red and Blue are constant)
        int green = ColorComp( 255 - (255 * (double)i / (this.Height - 4)) );

        using (LinearGradientBrush br = new LinearGradientBrush(
                  new Rectangle( 2, 2, this.Width - 4, 1 ),
                  Color.FromArgb( red, green, 0 ),
                  Color.FromArgb( red, green, 255 ), 0, false )) {
          g.FillRectangle( br, new Rectangle( 2, i + 2, this.Width - 4, 1 ) );
        }
      }
    }


    /// <summary>
    /// Draws the content of the control filling in all color values with the provided Green value.
    /// </summary>
    private void Draw_Style_Green( Graphics g )
    {
      int green = m_rgb.G; ;

      for (int i = 0; i < this.Height - 4; i++) //	For each horizontal line in the control:
      {
        //	Calculate Red at this line (Green and Blue are constant)
        int red = ColorComp( 255 - (255 * (double)i / (this.Height - 4)) );

        using (LinearGradientBrush br = new LinearGradientBrush(
                    new Rectangle( 2, 2, this.Width - 4, 1 ),
                    Color.FromArgb( red, green, 0 ),
                    Color.FromArgb( red, green, 255 ), 0, false )) {
          g.FillRectangle( br, new Rectangle( 2, i + 2, this.Width - 4, 1 ) );
        }
      }
    }


    /// <summary>
    /// Draws the content of the control filling in all color values with the provided Blue value.
    /// </summary>
    private void Draw_Style_Blue( Graphics g )
    {
      int blue = m_rgb.B; ;

      for (int i = 0; i < this.Height - 4; i++) //	For each horizontal line in the control:
      {
        //	Calculate Green at this line (Red and Blue are constant)
        int green = ColorComp( 255 - (255 * (double)i / (this.Height - 4)) );

        using (LinearGradientBrush br = new LinearGradientBrush(
                    new Rectangle( 2, 2, this.Width - 4, 1 ),
                    Color.FromArgb( 0, green, blue ),
                    Color.FromArgb( 255, green, blue ), 0, false )) {
          g.FillRectangle( br, new Rectangle( 2, i + 2, this.Width - 4, 1 ) );
        }
      }
    }


    /// <summary>
    /// Calls all the functions neccessary to redraw the entire control.
    /// </summary>
    private void Redraw_Control( Graphics g )
    {
      // if needed, reset the marker
      if (_resetMarkerFlag) {
        Reset_Marker( g );
        _resetMarkerFlag = false;
      }

      //DrawBorder( g );

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

      DrawMarker( g, m_iMarker_X, m_iMarker_Y );
    }


    /// <summary>
    /// Resets the marker position of the slider to match the controls color.  Gives the option of redrawing the slider.
    /// </summary>
    /// <param name="Redraw">Set to true if you want the function to redraw the slider after determining the best position</param>
    private void Reset_Marker( Graphics g )
    {
      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          m_iMarker_X = RoundUp( (this.Width - 4) * m_hsb.S );
          m_iMarker_Y = RoundUp( (this.Height - 4) * (1.0 - m_hsb.B) );
          break;
        case DrawStyle.Saturation:
          m_iMarker_X = RoundUp( (this.Width - 4) * m_hsb.HS );
          m_iMarker_Y = RoundUp( (this.Height - 4) * (1.0 - m_hsb.B) );
          break;
        case DrawStyle.Brightness:
          m_iMarker_X = RoundUp( (this.Width - 4) * m_hsb.HS );
          m_iMarker_Y = RoundUp( (this.Height - 4) * (1.0 - m_hsb.S) );
          break;
        case DrawStyle.Red:
          m_iMarker_X = RoundUp( (this.Width - 4) * (double)m_rgb.B / 255 );
          m_iMarker_Y = RoundUp( (this.Height - 4) * (1.0 - (double)m_rgb.G / 255) );
          break;
        case DrawStyle.Green:
          m_iMarker_X = RoundUp( (this.Width - 4) * (double)m_rgb.B / 255 );
          m_iMarker_Y = RoundUp( (this.Height - 4) * (1.0 - (double)m_rgb.R / 255) );
          break;
        case DrawStyle.Blue:
          m_iMarker_X = RoundUp( (this.Width - 4) * (double)m_rgb.R / 255 );
          m_iMarker_Y = RoundUp( (this.Height - 4) * (1.0 - (double)m_rgb.G / 255) );
          break;
        case DrawStyle.Transparent:
          m_iMarker_X = RoundUp( (this.Width - 4) * 0 );
          m_iMarker_Y = RoundUp( (this.Height - 4) * 1 );
          break;

      }

      DrawMarker( g, m_iMarker_X, m_iMarker_Y );
    }


    /// <summary>
    /// Resets the controls color (both HSL and RGB variables) based on the current marker position
    /// </summary>
    private void ResetHSLRGB( )
    {
      int red, green, blue;

      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          m_hsb.S = (double)m_iMarker_X / (this.Width - 4);
          m_hsb.B = 1.0 - (double)m_iMarker_Y / (this.Height - 4);
          m_rgb = HSB.ToRgb( m_hsb );
          break;
        case DrawStyle.Saturation:
          m_hsb.HS = (double)m_iMarker_X / (this.Width - 4);
          m_hsb.B = 1.0 - (double)m_iMarker_Y / (this.Height - 4);
          m_rgb = HSB.ToRgb( m_hsb );
          break;
        case DrawStyle.Brightness:
          m_hsb.HS = (double)m_iMarker_X / (this.Width - 4);
          m_hsb.S = 1.0 - (double)m_iMarker_Y / (this.Height - 4);
          m_rgb = HSB.ToRgb( m_hsb );
          break;
        case DrawStyle.Red:
          blue = RoundUp( 255 * (double)m_iMarker_X / (this.Width - 4) );
          green = RoundUp( 255 * (1.0 - (double)m_iMarker_Y / (this.Height - 4)) );
          m_rgb = Color.FromArgb( m_rgb.R, green, blue );
          m_hsb = HSB.FromRgb( m_rgb );
          break;
        case DrawStyle.Green:
          blue = RoundUp( 255 * (double)m_iMarker_X / (this.Width - 4) );
          red = RoundUp( 255 * (1.0 - (double)m_iMarker_Y / (this.Height - 4)) );
          m_rgb = Color.FromArgb( red, m_rgb.G, blue );
          m_hsb = HSB.FromRgb( m_rgb );
          break;
        case DrawStyle.Blue:
          red = RoundUp( 255 * (double)m_iMarker_X / (this.Width - 4) );
          green = RoundUp( 255 * (1.0 - (double)m_iMarker_Y / (this.Height - 4)) );
          m_rgb = Color.FromArgb( red, green, m_rgb.B );
          m_hsb = HSB.FromRgb( m_rgb );
          break;
        case DrawStyle.Transparent:
          // reset nothing
          break;
      }
    }



    /// <summary>
    /// Returns the graphed color at the x,y position on the control
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private HSB GetColor( int x, int y )
    {

      HSB _hsb = new HSB( );

      switch (m_DrawStyle) {
        case DrawStyle.Hue:
          _hsb.H = m_hsb.H;
          _hsb.S = (double)x / (this.Width - 4);
          _hsb.B = 1.0 - (double)y / (this.Height - 4);
          break;
        case DrawStyle.Saturation:
          _hsb.S = m_hsb.S;
          _hsb.HS = (double)x / (this.Width - 4);
          _hsb.B = 1.0 - (double)y / (this.Height - 4);
          break;
        case DrawStyle.Brightness:
          _hsb.B = m_hsb.B;
          _hsb.HS = (double)x / (this.Width - 4);
          _hsb.S = 1.0 - (double)y / (this.Height - 4);
          break;
        case DrawStyle.Red:
          _hsb = HSB.FromRgb(
            Color.FromArgb( m_rgb.R, ColorComp( 255 * (1.0 - (double)y / (this.Height - 4)) ), ColorComp( 255 * (double)x / (this.Width - 4) ) )
            );
          break;
        case DrawStyle.Green:
          _hsb = HSB.FromRgb(
            Color.FromArgb( ColorComp( 255 * (1.0 - (double)y / (this.Height - 4)) ), m_rgb.G, ColorComp( 255 * (double)x / (this.Width - 4) ) )
            );
          break;
        case DrawStyle.Blue:
          _hsb = HSB.FromRgb(
            Color.FromArgb( ColorComp( 255 * (double)x / (this.Width - 4) ), ColorComp( 255 * (1.0 - (double)y / (this.Height - 4)) ), m_rgb.B )
            );
          break;
        case DrawStyle.Transparent:
          // don't change anything
          break;
      }

      return _hsb;
    }

    #endregion

    /// <summary>
    /// cTor:
    /// </summary>
    public UC_2DColorBox( )
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
        _resetMarkerFlag = true;
        this.Refresh( );
      }
    }


    /// <summary>
    /// The HSL color of the control, changing the HSL will automatically change the RGB color for the control.
    /// </summary>
    [Browsable( false ), Description( "Set HSL value of this control" ), Category( "Appearance" )]
    public HSB HSB {
      get {
        return m_hsb;
      }
      set {
        m_hsb = value;
        m_rgb = HSB.ToRgb( m_hsb );

        //	Redraw the control based on the new color.
        _resetMarkerFlag = true;
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
        _resetMarkerFlag = true;
        this.Refresh( );
      }
    }

    #region Event Handling

    private void UC_2DColorBox_Load( object sender, EventArgs e )
    {
      _clip?.Dispose( );
      _clip = new Region( new Rectangle( 2, 2, this.Width - 4, this.Height - 4 ) );
      this.Refresh( );
    }

    private void UC_2DColorBox_Resize( object sender, EventArgs e )
    {
      _clip?.Dispose( );
      _clip = new Region( new Rectangle( 2, 2, this.Width - 4, this.Height - 4 ) );
      this.Refresh( );
    }

    private void UC_2DColorBox_Paint( object sender, PaintEventArgs e )
    {
      var save = e.Graphics.Save( );
      e.Graphics.Clip = _clip;

      Redraw_Control( e.Graphics );

      e.Graphics.Restore( save );
    }


    private void UC_2DColorBox_MouseUp( object sender, MouseEventArgs e )
    {
      if (e.Button != MouseButtons.Left)  //	Only respond to left mouse button events
        return;

      if (!m_bDragging)
        return;

      m_bDragging = false;

      int x = e.X - 2, y = e.Y - 2;
      if (x < 0) x = 0;
      if (x > this.Width - 4) x = this.Width - 4; //	Calculate marker position
      if (y < 0) y = 0;
      if (y > this.Height - 4) y = this.Height - 4;

      if (x == m_iMarker_X && y == m_iMarker_Y)   //	If the marker hasn't moved, no need to redraw it.
        return;                   //	or send a scroll notification

      using (Graphics g = this.CreateGraphics( )) {
        g.Clip = _clip;
        DrawMarker( g, x, y ); //	Redraw the marker
      }
      ResetHSLRGB( );     //	Reset the color

      OnScroll( e );//	Notify anyone who cares that the controls marker (selected color) has changed
    }

    private void UC_2DColorBox_MouseDown( object sender, MouseEventArgs e )
    {
      if (e.Button != MouseButtons.Left)  //	Only respond to left mouse button events
        return;

      m_bDragging = true;   //	Begin dragging which notifies MouseMove function that it needs to update the marker

      int x = e.X - 2, y = e.Y - 2;
      if (x < 0) x = 0;
      if (x > this.Width - 4) x = this.Width - 4; //	Calculate marker position
      if (y < 0) y = 0;
      if (y > this.Height - 4) y = this.Height - 4;

      if (x == m_iMarker_X && y == m_iMarker_Y)   //	If the marker hasn't moved, no need to redraw it.
        return;                   //	or send a scroll notification

      using (Graphics g = this.CreateGraphics( )) {
        g.Clip = _clip;
        DrawMarker( g, x, y ); //	Redraw the marker
      }
      ResetHSLRGB( );     //	Reset the color

      OnScroll( e );//	Notify anyone who cares that the controls marker (selected color) has changed
    }

    private void UC_2DColorBox_MouseMove( object sender, MouseEventArgs e )
    {
      if (!m_bDragging)   //	Only respond when the mouse is dragging the marker.
        return;

      int x = e.X - 2, y = e.Y - 2;
      if (x < 0) x = 0;
      if (x > this.Width - 4) x = this.Width - 4; //	Calculate marker position
      if (y < 0) y = 0;
      if (y > this.Height - 4) y = this.Height - 4;

      if (x == m_iMarker_X && y == m_iMarker_Y)   //	If the marker hasn't moved, no need to redraw it.
        return;                   //	or send a scroll notification

      using (Graphics g = this.CreateGraphics( )) {
        g.Clip = _clip;
        DrawMarker( g, x, y ); //	Redraw the marker
      }
      ResetHSLRGB( );     //	Reset the color

      OnScroll( e );//	Notify anyone who cares that the controls marker (selected color) has changed
    }

    #endregion

  }
}
