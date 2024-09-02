namespace bm98_AColorPicker
{
  partial class UC_VerticalColorSlider
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if (disposing && (components != null)) {
        components.Dispose( );
      }
      base.Dispose( disposing );
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.SuspendLayout();
      // 
      // UC_VerticalColorSlider
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.DoubleBuffered = true;
      this.Name = "UC_VerticalColorSlider";
      this.Size = new System.Drawing.Size(40, 264);
      this.Load += new System.EventHandler(this.UC_VerticalColorSlider_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.UC_VerticalColorSlider_Paint);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UC_VerticalColorSlider_MouseDown);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UC_VerticalColorSlider_MouseMove);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UC_VerticalColorSlider_MouseUp);
      this.Resize += new System.EventHandler(this.UC_VerticalColorSlider_Resize);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
