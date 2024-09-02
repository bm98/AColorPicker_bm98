using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using static dNetBm98.XColor;

namespace bm98_AColorPicker
{
  /// <summary>
  /// Queue of Color Patches
  /// </summary>
  [DefaultEvent( "ColorPicked" )]
  public partial class UC_ColorQueue : UserControl
  {
    // config string separator
    private const char c_sep = '¦';

    /// <summary>
    /// Return a config string for the Queue
    /// </summary>
    /// <param name="uc">A UC_ColorQueue</param>
    /// <returns>A string</returns>
    public static string ToConfigString( UC_ColorQueue uc )
    {
      string ret = "";
      foreach (var item in uc._colorQueue) {
        ret += $"{item.ToColorS( )}{c_sep}";
      }
      return ret;
    }

    /// <summary>
    /// Fills the queue from a config string
    /// </summary>
    /// <param name="uc">A UC_ColorQueue</param>
    /// <param name="cfg">A config string</param>
    public static void FromConfigString( UC_ColorQueue uc, string cfg )
    {
      uc._colorQueue.Clear( );
      string[] e = cfg.Split( new char[] { c_sep }, StringSplitOptions.RemoveEmptyEntries );
      for (int i = 0; i < e.Length; i++) {
        Color c = e[i].FromColorS( );
        uc.EnqueuePatch( c );
      }
    }

    // *** CLASS 

    // smallest patch size
    private const int c_MinSize = 10;
    // the queue
    private Queue<Color> _colorQueue = new Queue<Color>( );
    // actual #patches 
    private int _numPatches = 10;


    [Description( "A Color patch was picked" ), Category( "Action" )]
    public event EventHandler<ColorPickedEventArgs> ColorPicked;
    private void OnColorPicked( Color picked ) => ColorPicked?.Invoke( this, new ColorPickedEventArgs( picked ) );

    /// <summary>
    /// cTor:
    /// </summary>
    public UC_ColorQueue( )
    {
      InitializeComponent( );
    }

    private void UC_ColorQueue_Load( object sender, EventArgs e )
    {
      InitFlp( );
    }

    // returns a valid number based on min Size
    private int ValidNumber( int numPatches )
    {
      // sanity
      if (numPatches < 1) return 1;

      int nItems = numPatches;

      // calc the size of an item based on numPatches
      double w = flp.ClientRectangle.Width;
      int n = (int)Math.Floor( w / numPatches );
      // LR margin is 3, B is 2
      int itemWidth = n - 6;
      if (itemWidth > flp.ClientRectangle.Height) {
        itemWidth = flp.ClientRectangle.Height;
      }
      if (itemWidth < c_MinSize) {
        // need to recalc the maximum possible
        nItems = (int)Math.Floor( w / (c_MinSize + 6) );
        if (nItems < 1) nItems = 1; // sanity..
      }

      return nItems;
    }

    // init the Layout Panel
    private void InitFlp( )
    {
      flp.SuspendLayout( );

      // clear old ones
      while (flp.Controls.Count > 0) {
        var c = flp.Controls[0];
        flp.Controls.Remove( c );
        c.Dispose( );
      }

      // size of an item 
      double w = flp.ClientRectangle.Width;
      int n = (int)Math.Floor( w / _numPatches );
      // LR margin is 3
      int itemWidth = n - 6;
      if (itemWidth > flp.ClientRectangle.Height) {
        itemWidth = flp.ClientRectangle.Height - 2;
      }

      for (int i = 0; i < _numPatches; i++) {
        Label l = new Label( ) {
          Name = $"clbl{i}",
          AutoSize = false,
          Text = "",
          Margin = new Padding( 3, 2, 3, 0 ),
          BorderStyle = BorderStyle.FixedSingle,
          Width = itemWidth,
          Height = itemWidth,
          BackColor = Color.Black,
          Cursor = Cursors.Hand,
        };
        l.Click += ( sender, e ) => { OnColorPicked( (sender as Label).BackColor ); };
        flp.Controls.Add( l );
      }
      // remove items from the queue if needed
      while (_colorQueue.Count > _numPatches) {
        _colorQueue.Dequeue( );
      }
      flp.ResumeLayout( );

      RedrawQueue( );
    }

    private void RedrawQueue( )
    {
      int idx = 0;
      foreach (var patch in _colorQueue.Reverse( )) {
        flp.Controls[idx++].BackColor = patch;
      }
    }

    /// <summary>
    /// Set the Queue Size
    /// </summary>
    [DefaultValue( 10 )]
    [Description( "Number of Patches" ), Category( "Appearance" )]
    public int NumPatches {
      get => _numPatches;
      set {
        if (value < 1) return; // just not...
        if (_numPatches != value) {
          _numPatches = ValidNumber( value ); // override if needed
          InitFlp( );
        }

      }
    }

    /// <summary>
    /// Enqueue a new Patch 
    /// </summary>
    /// <param name="color">The Color</param>
    public void EnqueuePatch( Color color )
    {
      if (_colorQueue.Contains( color )) return;

      _colorQueue.Enqueue( color );
      // handle the overflow
      if (_colorQueue.Count > _numPatches) {
        _colorQueue.Dequeue( );
      }
      RedrawQueue( );
    }

  }
}
