
using System;
using System.Drawing;

using static bm98_AColorPicker.Extensions;

namespace bm98_AColorPicker
{
  // Original Source derived from:
  // https://www.codeproject.com/Articles/9959/Adobe-Color-Picker-Clone-Part-1
  //
  // port to VS2022 as UC / .Net Framework 4.8
  // correcting the color space to HSB/HSV which PS uses
  // correcting the Hue property for degrees supporting the original scaled mode as HS property
  // rework color conversions (were not matching reference material)
  // Add true HSL colorspace / not used so far
  // Renamed from proprietary 'Adobe' to PickerColors

  /// <summary>
  /// Provides HSB/HSV, HSL, and CMYK objects
  /// Provides Conversions in RGB, HSB/HSV, HSL, CMYK colorspace 
  /// </summary>
  public static class PickerColors
  {

    #region HSB / HSV

    /// <summary>
    /// HSB color model
    ///  H(ue):        0..<360°
    ///  HS(caled):    0..1 (Hue / 360)
    ///  S(aturation): 0..1
    ///  B(rightness): 0..1
    /// </summary>
    public struct HSB
    {
      /// <summary>
      /// An Empty HSL (set to 0) 
      /// </summary>
      public static HSB Empty => new HSB( 0, 0, 0 );

      double _h;
      double _s;
      double _b;

      /// <summary>
      /// cTor:
      /// </summary>
      public HSB( double h = 0, double s = 0, double b = 0 )
      {
        _h = h;
        _s = s;
        _b = b;
      }

      /// <summary>
      /// cTor: Copy
      /// </summary>
      /// <param name="other"></param>
      public HSB( HSB other )
      {
        _h = other._h;
        _s = other._s;
        _b = other._b;
      }

      /// <summary>
      /// Hue Part
      /// </summary>
      public double H {
        get { return _h; }
        set {
          _h = value >= 360.0 ? 0.0 : value < 0.0 ? 0.0 : value;   // clamp before 360
        }
      }
      /// <summary>
      /// Scaled Hue Part (0..<360.0 -> 0..<1.0)
      /// </summary>
      public double HS {
        get { return _h / 360.0; }
        set {
          var hs = value >= 1.0 ? 0.0 : value < 0.0 ? 0.0 : value; // clamp before 360
          _h = hs * 360.0;
        }
      }

      /// <summary>
      /// Saturation Part
      /// </summary>
      public double S {
        get { return _s; }
        set {
          _s = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
        }
      }

      /// <summary>
      /// Brightness Part
      /// </summary>
      public double B {
        get { return _b; }
        set {
          _b = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
        }
      }

      /// <summary>
      /// String representation of this object
      /// </summary>
      /// <returns>A String</returns>
      public override string ToString( )
      {
        return $"({_h:##0},{_s:0.00},{_b:0.00})";
      }
    }


    /// <summary> 
    /// Converts a colour from HSB to RGB 
    /// </summary> 
    /// <param name="hsb">The HSB value</param> 
    /// <returns>A Color structure containing the equivalent RGB values</returns> 
    public static Color HSB_to_RGB( HSB hsb )
    {
      /*
       * https://www.rapidtables.com/convert/color/hsv-to-rgb.html

        When 0 ≤ H < 360, 0 ≤ S ≤ 1 and 0 ≤ L ≤ 1:

            C = B × S

            X = C × (1 - |(H / 60°) mod 2 - 1|)

            m = B - C

            R',G',B' = (C,X,0) | (X,C,0) | (0,C,X) | (0,X,C) | (X,0,C) | (C,0,X) for H intervals of 60°

           (R,G,B) = ((R'+m)×255, (G'+m)×255,(B'+m)×255)

       */

      double C = hsb.B * hsb.S;

      double X = C * (1 - Math.Abs( (hsb.H / 60) % 2 - 1 ));

      double m = hsb.B - C;

      double r = 0, g = 0, b = 0;
      if ((0 <= hsb.H) && (hsb.H < 60)) { r = C; g = X; b = 0; }
      else if ((60 <= hsb.H) && (hsb.H < 120)) { r = X; g = C; b = 0; }
      else if ((120 <= hsb.H) && (hsb.H < 180)) { r = 0; g = C; b = X; }
      else if ((180 <= hsb.H) && (hsb.H < 240)) { r = 0; g = X; b = C; }
      else if ((240 <= hsb.H) && (hsb.H < 300)) { r = X; g = 0; b = C; }
      else if ((300 <= hsb.H) && (hsb.H < 360)) { r = C; g = 0; b = X; }

      Color c = Color.FromArgb( RoundByte( (r + m) * 255 ), RoundByte( (g + m) * 255 ), RoundByte( (b + m) * 255 ) );

      return c;
    }

    /// <summary> 
    /// Converts RGB to HSB
    /// </summary> 
    /// <param name="c">A Color to convert</param> 
    /// <returns>An HSB value</returns> 
    public static HSB RGB_to_HSB( Color c )
    {
      /*
       * https://www.rapidtables.com/convert/color/rgb-to-hsv.html
       * 
       * The R,G,B values are divided by 255 to change the range from 0..255 to 0..1:

            R' = R/255
            G' = G/255
            B' = B/255

            Cmax = max(R', G', B')
            Cmin = min(R', G', B')
            Δ = Cmax - Cmin

            Hue calculation:
             H=
                0                    , Δ=0
                60 x ( (G'-B')/Δ % 6 ) , Cmax=R
                60 x ( (B'-R')/ + 2 )  , Cmax=G
                60 x ( (R'-G')/ + 4 )  , Cmax=B

            Saturation calculation:
             S=
               0        , Cmax=0
               Δ/Cmax   , Cmax!=0   
              
            Brightness calculation actually B in HSB:
              V = Cmax
       */
      double r = c.R / 255.0;
      double g = c.G / 255.0;
      double b = c.B / 255.0;

      double Cmax = Math.Max( r, Math.Max( g, b ) );
      double Cmin = Math.Min( r, Math.Min( g, b ) ); ;
      double delta = Cmax - Cmin;

      var hsb = new HSB( );
      if (delta == 0) { hsb.H = 0; }
      else if (Cmax == r) { hsb.H = 60 * (((g - b) / delta) % 6); }
      else if (Cmax == g) { hsb.H = 60 * (((b - r) / delta + 2) % 6); }
      else if (Cmax == b) { hsb.H = 60 * (((r - g) / delta + 4) % 6); }

      if (Cmax == 0) { hsb.S = 0; }
      else { hsb.S = delta / Cmax; }

      hsb.B = Cmax;

      return hsb;
    }

    #endregion


    #region HSL

    /// <summary>
    /// HSB color model
    ///  H(ue):        0..<360°
    ///  HS(caled):    0..1 (Hue / 360)
    ///  S(aturation): 0..1
    ///  L(ightness ):  0..1
    /// </summary>
    public struct HSL
    {
      /// <summary>
      /// An Empty HSL (set to 0) 
      /// </summary>
      public static HSL Empty => new HSL( 0, 0, 0 );

      double _h;
      double _s;
      double _l;

      /// <summary>
      /// cTor:
      /// </summary>
      public HSL( double h = 0, double s = 0, double l = 0 )
      {
        _h = h;
        _s = s;
        _l = l;
      }

      /// <summary>
      /// cTor: Copy
      /// </summary>
      /// <param name="other"></param>
      public HSL( HSL other )
      {
        _h = other._h;
        _s = other._s;
        _l = other._l;
      }

      /// <summary>
      /// Hue Part
      /// </summary>
      public double H {
        get { return _h; }
        set {
          _h = value >= 360.0 ? 0.0 : value < 0.0 ? 0.0 : value;   // clamp before 360
        }
      }
      /// <summary>
      /// Scaled Hue Part (0..<360.0 -> 0..<1.0)
      /// </summary>
      public double HS {
        get { return _h / 360.0; }
        set {
          var hs = value >= 1.0 ? 0.0 : value < 0.0 ? 0.0 : value; // clamp before 360
          _h = hs * 360.0;
        }
      }

      /// <summary>
      /// Saturation Part
      /// </summary>
      public double S {
        get { return _s; }
        set {
          _s = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
        }
      }

      /// <summary>
      /// Lightness Part
      /// </summary>
      public double L {
        get { return _l; }
        set {
          _l = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
        }
      }

      /// <summary>
      /// String representation of this object
      /// </summary>
      /// <returns>A String</returns>
      public override string ToString( )
      {
        return $"({_h:##0},{_s:0.00},{_l:0.00})";
      }
    }

    /// <summary> 
    /// Converts a colour from HSL to RGB 
    /// </summary> 
    /// <remarks>Adapted from the algorithm in Foley and Van-Dam</remarks> 
    /// <param name="hsl">The HSL value</param> 
    /// <returns>A Color structure containing the equivalent RGB values</returns> 
    public static Color HSL_to_RGB( HSL hsl )
    {
      /*
       * https://www.rapidtables.com/convert/color/hsl-to-rgb.html

        When 0 ≤ H < 360, 0 ≤ S ≤ 1 and 0 ≤ L ≤ 1:

            C = (1 - |2L - 1|) × S

            X = C × (1 - |(H / 60°) mod 2 - 1|)

            m = L - C/2

            R',G',B' = (C,X,0) | (X,C,0) | (0,C,X) | (0,X,C) | (X,0,C) | (C,0,X) for H intervals of 60°

           (R,G,B) = ((R'+m)×255, (G'+m)×255,(B'+m)×255)

       */

      double C = (1 - Math.Abs( 2 * hsl.L - 1 )) * hsl.S;

      double X = C * (1 - Math.Abs( (hsl.H / 60) % 2 - 1 ));

      double m = hsl.L - C / 2;

      double r = 0, g = 0, b = 0;
      if ((0 <= hsl.H) && (hsl.H < 60)) { r = C; g = X; b = 0; }
      else if ((60 <= hsl.H) && (hsl.H < 120)) { r = X; g = C; b = 0; }
      else if ((120 <= hsl.H) && (hsl.H < 180)) { r = 0; g = C; b = X; }
      else if ((180 <= hsl.H) && (hsl.H < 240)) { r = 0; g = X; b = C; }
      else if ((240 <= hsl.H) && (hsl.H < 300)) { r = X; g = 0; b = C; }
      else if ((300 <= hsl.H) && (hsl.H < 360)) { r = C; g = 0; b = X; }

      Color c = Color.FromArgb( RoundByte( (r + m) * 255 ), RoundByte( (g + m) * 255 ), RoundByte( (b + m) * 255 ) );

      return c;
    }


    /// <summary> 
    /// Converts RGB to HSL
    /// </summary> 
    /// <param name="c">A Color to convert</param> 
    /// <returns>An HSL value</returns> 
    public static HSL RGB_to_HSL( Color c )
    {
      /*
       * https://www.rapidtables.com/convert/color/rgb-to-hsl.html
       * 
       * The R,G,B values are divided by 255 to change the range from 0..255 to 0..1:

            R' = R/255
            G' = G/255
            B' = B/255

            Cmax = max(R', G', B')
            Cmin = min(R', G', B')
            Δ = Cmax - Cmin

            Hue calculation:
             H=
                0                    , Δ=0
                60 x ( (G'-B')/Δ % 6 ) , Cmax=R
                60 x ( (B'-R')/ + 2 )  , Cmax=G
                60 x ( (R'-G')/ + 4 )  , Cmax=B

            Saturation calculation:
             S=
               0            , Cmax=0
               Δ/(1-|2L-1|) , Cmax!=0   
              
            Lightness calculation:
              L = (Cmax + Cmin) / 2
       */
      double r = c.R / 255.0;
      double g = c.G / 255.0;
      double b = c.B / 255.0;

      double Cmax = Math.Max( r, Math.Max( g, b ) );
      double Cmin = Math.Min( r, Math.Min( g, b ) ); ;
      double delta = Cmax - Cmin;

      var hsl = new HSL( );
      if (delta == 0) { hsl.H = 0; }
      else if (Cmax == r) { hsl.H = 60 * (((g - b) / delta) % 6); }
      else if (Cmax == g) { hsl.H = 60 * (((b - r) / delta + 2) % 6); }
      else if (Cmax == b) { hsl.H = 60 * (((r - g) / delta + 4) % 6); }

      hsl.L = (Cmax + Cmin) / 2;

      if (Cmax == 0) { hsl.S = 0; }
      else { hsl.S = delta / (1 - Math.Abs( 2 * hsl.L - 1 )); }

      return hsl;
    }

    #endregion


    #region CMYK

    /// <summary>
    /// CMYK ColorSpace
    /// </summary>
    public struct CMYK
    {
      /// <summary>
      /// An Empty CMYK (set to 0) 
      /// </summary>
      public static CMYK Empty => new CMYK( 0, 0, 0, 0 );

      double _c;
      double _m;
      double _y;
      double _k;

      /// <summary>
      /// cTor:
      /// </summary>
      public CMYK( double c = 0, double m = 0, double y = 0, double k = 0 )
      {
        _c = c;
        _m = m;
        _y = y;
        _k = k;
      }


      /// <summary>
      /// Cyan Part
      /// </summary>
      public double C {
        get { return _c; }
        set {
          _c = value;
          _c = _c > 1 ? 1 : _c < 0 ? 0 : _c;
        }
      }

      /// <summary>
      /// Magenta Part
      /// </summary>
      public double M {
        get { return _m; }
        set {
          _m = value;
          _m = _m > 1 ? 1 : _m < 0 ? 0 : _m;
        }
      }

      /// <summary>
      /// Yellow Part
      /// </summary>
      public double Y {
        get { return _y; }
        set {
          _y = value;
          _y = _y > 1 ? 1 : _y < 0 ? 0 : _y;
        }
      }

      /// <summary>
      /// Key (Blackness) Part
      /// </summary>
      public double K {
        get { return _k; }
        set {
          _k = value;
          _k = _k > 1 ? 1 : _k < 0 ? 0 : _k;
        }
      }

      /// <summary>
      /// String representation of this object
      /// </summary>
      /// <returns>A String</returns>
      public override string ToString( )
      {
        return $"({_c:0.00},{_m:0.00},{_y:0.00},{_k:0.00})";
      }

    }

    /// <summary>
    /// Converts RGB to CMYK
    /// </summary>
    /// <param name="c">A color to convert.</param>
    /// <returns>A CMYK object</returns>
    public static CMYK RGB_to_CMYK( Color c )
    {
      CMYK _cmyk = new CMYK( );
      double low = 1.0;

      _cmyk.C = (double)(255 - c.R) / 255;
      if (low > _cmyk.C)
        low = _cmyk.C;

      _cmyk.M = (double)(255 - c.G) / 255;
      if (low > _cmyk.M)
        low = _cmyk.M;

      _cmyk.Y = (double)(255 - c.B) / 255;
      if (low > _cmyk.Y)
        low = _cmyk.Y;

      if (low > 0.0) {
        _cmyk.K = low;
      }

      return _cmyk;
    }


    /// <summary>
    /// Converts CMYK to RGB
    /// </summary>
    /// <param name="_cmyk">A color to convert</param>
    /// <returns>A Color object</returns>
    public static Color CMYK_to_RGB( CMYK _cmyk )
    {
      int red, green, blue;

      red = RoundByte( 255 - (255 * _cmyk.C) );
      green = RoundByte( 255 - (255 * _cmyk.M) );
      blue = RoundByte( 255 - (255 * _cmyk.Y) );

      return Color.FromArgb( red, green, blue );
    }

    #endregion

    /// <summary> 
    /// Sets the absolute brightness of a colour 
    /// </summary> 
    /// <param name="c">Original colour</param> 
    /// <param name="brightness">The luminance level to impose</param> 
    /// <returns>an adjusted colour</returns> 
    public static Color SetBrightness( Color c, double brightness )
    {
      HSB hsb = RGB_to_HSB( c );
      hsb.B = brightness;
      return HSB_to_RGB( hsb );
    }


    /// <summary> 
    /// Modifies an existing brightness level 
    /// </summary> 
    /// <remarks> 
    /// To reduce brightness use a number smaller than 1. To increase brightness use a number larger than 1 
    /// </remarks> 
    /// <param name="c">The original colour</param> 
    /// <param name="brightness">The luminance delta</param> 
    /// <returns>An adjusted colour</returns> 
    public static Color ModifyBrightness( Color c, double brightness )
    {
      HSB hsb = RGB_to_HSB( c );
      hsb.B *= brightness;
      return HSB_to_RGB( hsb );
    }


    /// <summary> 
    /// Sets the absolute saturation level 
    /// </summary> 
    /// <remarks>Accepted values 0-1</remarks> 
    /// <param name="c">An original colour</param> 
    /// <param name="Saturation">The saturation value to impose</param> 
    /// <returns>An adjusted colour</returns> 
    public static Color SetSaturation( Color c, double Saturation )
    {
      HSB hsb = RGB_to_HSB( c );
      hsb.S = Saturation;
      return HSB_to_RGB( hsb );
    }


    /// <summary> 
    /// Modifies an existing Saturation level 
    /// </summary> 
    /// <remarks> 
    /// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger than 1 
    /// </remarks> 
    /// <param name="c">The original colour</param> 
    /// <param name="Saturation">The saturation delta</param> 
    /// <returns>An adjusted colour</returns> 
    public static Color ModifySaturation( Color c, double Saturation )
    {
      HSB hsb = RGB_to_HSB( c );
      hsb.S *= Saturation;
      return HSB_to_RGB( hsb );
    }


    /// <summary> 
    /// Sets the absolute Hue level 
    /// </summary> 
    /// <remarks>Accepted values 0-1</remarks> 
    /// <param name="c">An original colour</param> 
    /// <param name="Hue">The Hue value to impose</param> 
    /// <returns>An adjusted colour</returns> 
    public static Color SetHue( Color c, double Hue )
    {
      HSB hsb = RGB_to_HSB( c );
      hsb.H = Hue;
      return HSB_to_RGB( hsb );
    }


    /// <summary> 
    /// Modifies an existing Hue level 
    /// </summary> 
    /// <remarks> 
    /// To reduce Hue use a number smaller than 1. To increase Hue use a number larger than 1 
    /// </remarks> 
    /// <param name="c">The original colour</param> 
    /// <param name="Hue">The Hue delta</param> 
    /// <returns>An adjusted colour</returns> 
    public static Color ModifyHue( Color c, double Hue )
    {
      HSB hsb = RGB_to_HSB( c );
      hsb.H *= Hue;
      return HSB_to_RGB( hsb );
    }


  }
}
