using System;
using System.Drawing;

namespace bm98_AColorPicker
{
  /// <summary>
  /// Color Picked EventArgs
  /// </summary>
  public class ColorPickedEventArgs : EventArgs
  {
    /// <summary>
    /// The picked Color
    /// </summary>
    public Color PickedColor { get; set; }

    /// <summary>
    /// cTor:
    /// </summary>
    public ColorPickedEventArgs( Color picked )
    {
      PickedColor = picked;
    }

  }
}
