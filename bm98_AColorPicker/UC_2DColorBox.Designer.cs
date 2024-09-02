namespace bm98_AColorPicker
{
  partial class UC_2DColorBox
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
      // UC_2DColorBox
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.DoubleBuffered = true;
      this.Name = "UC_2DColorBox";
      this.Size = new System.Drawing.Size(260, 260);
      this.Load += new System.EventHandler(this.UC_2DColorBox_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.UC_2DColorBox_Paint);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UC_2DColorBox_MouseDown);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UC_2DColorBox_MouseMove);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UC_2DColorBox_MouseUp);
      this.Resize += new System.EventHandler(this.UC_2DColorBox_Resize);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
