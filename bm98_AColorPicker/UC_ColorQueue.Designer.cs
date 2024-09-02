namespace bm98_AColorPicker
{
  partial class UC_ColorQueue
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
      this.flp = new System.Windows.Forms.FlowLayoutPanel();
      this.lCol0 = new System.Windows.Forms.Label();
      this.flp.SuspendLayout();
      this.SuspendLayout();
      // 
      // flp
      // 
      this.flp.Controls.Add(this.lCol0);
      this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flp.Location = new System.Drawing.Point(0, 0);
      this.flp.Name = "flp";
      this.flp.Size = new System.Drawing.Size(365, 38);
      this.flp.TabIndex = 0;
      this.flp.WrapContents = false;
      // 
      // lCol0
      // 
      this.lCol0.BackColor = System.Drawing.Color.Red;
      this.lCol0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lCol0.Location = new System.Drawing.Point(3, 0);
      this.lCol0.Name = "lCol0";
      this.lCol0.Size = new System.Drawing.Size(32, 32);
      this.lCol0.TabIndex = 0;
      // 
      // UC_ColorQueue
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.flp);
      this.Name = "UC_ColorQueue";
      this.Size = new System.Drawing.Size(365, 38);
      this.Load += new System.EventHandler(this.UC_ColorQueue_Load);
      this.flp.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flp;
    private System.Windows.Forms.Label lCol0;
  }
}
