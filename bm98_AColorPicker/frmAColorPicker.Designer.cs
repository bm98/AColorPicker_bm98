using System.Windows.Forms;

namespace bm98_AColorPicker
{
  partial class frmAColorPicker
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.m_lbl_Key_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Yellow_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Magenta_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Cyan_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Black_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Saturation_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Hue_Symbol = new System.Windows.Forms.Label();
      this.m_lbl_Secondary_Color = new System.Windows.Forms.Label();
      this.m_lbl_Primary_Color = new System.Windows.Forms.Label();
      this.m_lbl_K = new System.Windows.Forms.Label();
      this.m_lbl_Yellow = new System.Windows.Forms.Label();
      this.m_lbl_Magenta = new System.Windows.Forms.Label();
      this.m_lbl_Cyan = new System.Windows.Forms.Label();
      this.m_lbl_HexPound = new System.Windows.Forms.Label();
      this.m_rbtn_Blue = new System.Windows.Forms.RadioButton();
      this.m_rbtn_Green = new System.Windows.Forms.RadioButton();
      this.m_rbtn_Red = new System.Windows.Forms.RadioButton();
      this.m_rbtn_Black = new System.Windows.Forms.RadioButton();
      this.m_rbtn_Sat = new System.Windows.Forms.RadioButton();
      this.m_rbtn_Hue = new System.Windows.Forms.RadioButton();
      this.m_txt_Hex = new System.Windows.Forms.TextBox();
      this.m_txt_K = new System.Windows.Forms.TextBox();
      this.m_txt_Yellow = new System.Windows.Forms.TextBox();
      this.m_txt_Magenta = new System.Windows.Forms.TextBox();
      this.m_txt_Cyan = new System.Windows.Forms.TextBox();
      this.m_txt_Blue = new System.Windows.Forms.TextBox();
      this.m_txt_Green = new System.Windows.Forms.TextBox();
      this.m_txt_Red = new System.Windows.Forms.TextBox();
      this.m_txt_Black = new System.Windows.Forms.TextBox();
      this.m_txt_Sat = new System.Windows.Forms.TextBox();
      this.m_txt_Hue = new System.Windows.Forms.TextBox();
      this.m_cmd_Cancel = new System.Windows.Forms.Button();
      this.m_cmd_OK = new System.Windows.Forms.Button();
      this.m_pbx_BlankBox = new System.Windows.Forms.PictureBox();
      this.rbTransparent = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.colQueue = new bm98_AColorPicker.UC_ColorQueue();
      this.m_ctrl_ThinBox = new bm98_AColorPicker.UC_VerticalColorSlider();
      this.m_ctrl_BigBox = new bm98_AColorPicker.UC_2DColorBox();
      ((System.ComponentModel.ISupportInitialize)(this.m_pbx_BlankBox)).BeginInit();
      this.SuspendLayout();
      // 
      // m_lbl_Key_Symbol
      // 
      this.m_lbl_Key_Symbol.AutoSize = true;
      this.m_lbl_Key_Symbol.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Key_Symbol.Location = new System.Drawing.Point(496, 263);
      this.m_lbl_Key_Symbol.Name = "m_lbl_Key_Symbol";
      this.m_lbl_Key_Symbol.Size = new System.Drawing.Size(19, 17);
      this.m_lbl_Key_Symbol.TabIndex = 46;
      this.m_lbl_Key_Symbol.Text = "%";
      // 
      // m_lbl_Yellow_Symbol
      // 
      this.m_lbl_Yellow_Symbol.AutoSize = true;
      this.m_lbl_Yellow_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Yellow_Symbol.Location = new System.Drawing.Point(496, 239);
      this.m_lbl_Yellow_Symbol.Name = "m_lbl_Yellow_Symbol";
      this.m_lbl_Yellow_Symbol.Size = new System.Drawing.Size(19, 16);
      this.m_lbl_Yellow_Symbol.TabIndex = 87;
      this.m_lbl_Yellow_Symbol.Text = "%";
      // 
      // m_lbl_Magenta_Symbol
      // 
      this.m_lbl_Magenta_Symbol.AutoSize = true;
      this.m_lbl_Magenta_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Magenta_Symbol.Location = new System.Drawing.Point(496, 214);
      this.m_lbl_Magenta_Symbol.Name = "m_lbl_Magenta_Symbol";
      this.m_lbl_Magenta_Symbol.Size = new System.Drawing.Size(19, 16);
      this.m_lbl_Magenta_Symbol.TabIndex = 86;
      this.m_lbl_Magenta_Symbol.Text = "%";
      // 
      // m_lbl_Cyan_Symbol
      // 
      this.m_lbl_Cyan_Symbol.AutoSize = true;
      this.m_lbl_Cyan_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Cyan_Symbol.Location = new System.Drawing.Point(496, 190);
      this.m_lbl_Cyan_Symbol.Name = "m_lbl_Cyan_Symbol";
      this.m_lbl_Cyan_Symbol.Size = new System.Drawing.Size(19, 16);
      this.m_lbl_Cyan_Symbol.TabIndex = 85;
      this.m_lbl_Cyan_Symbol.Text = "%";
      // 
      // m_lbl_Black_Symbol
      // 
      this.m_lbl_Black_Symbol.AutoSize = true;
      this.m_lbl_Black_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Black_Symbol.Location = new System.Drawing.Point(402, 161);
      this.m_lbl_Black_Symbol.Name = "m_lbl_Black_Symbol";
      this.m_lbl_Black_Symbol.Size = new System.Drawing.Size(19, 16);
      this.m_lbl_Black_Symbol.TabIndex = 84;
      this.m_lbl_Black_Symbol.Text = "%";
      // 
      // m_lbl_Saturation_Symbol
      // 
      this.m_lbl_Saturation_Symbol.AutoSize = true;
      this.m_lbl_Saturation_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Saturation_Symbol.Location = new System.Drawing.Point(402, 134);
      this.m_lbl_Saturation_Symbol.Name = "m_lbl_Saturation_Symbol";
      this.m_lbl_Saturation_Symbol.Size = new System.Drawing.Size(19, 16);
      this.m_lbl_Saturation_Symbol.TabIndex = 83;
      this.m_lbl_Saturation_Symbol.Text = "%";
      // 
      // m_lbl_Hue_Symbol
      // 
      this.m_lbl_Hue_Symbol.AutoSize = true;
      this.m_lbl_Hue_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_lbl_Hue_Symbol.Location = new System.Drawing.Point(402, 107);
      this.m_lbl_Hue_Symbol.Name = "m_lbl_Hue_Symbol";
      this.m_lbl_Hue_Symbol.Size = new System.Drawing.Size(14, 18);
      this.m_lbl_Hue_Symbol.TabIndex = 82;
      this.m_lbl_Hue_Symbol.Text = "°";
      // 
      // m_lbl_Secondary_Color
      // 
      this.m_lbl_Secondary_Color.Location = new System.Drawing.Point(443, 119);
      this.m_lbl_Secondary_Color.Name = "m_lbl_Secondary_Color";
      this.m_lbl_Secondary_Color.Size = new System.Drawing.Size(60, 34);
      this.m_lbl_Secondary_Color.TabIndex = 81;
      this.m_lbl_Secondary_Color.Click += new System.EventHandler(this.m_lbl_Secondary_Color_Click);
      // 
      // m_lbl_Primary_Color
      // 
      this.m_lbl_Primary_Color.Location = new System.Drawing.Point(443, 85);
      this.m_lbl_Primary_Color.Name = "m_lbl_Primary_Color";
      this.m_lbl_Primary_Color.Size = new System.Drawing.Size(60, 34);
      this.m_lbl_Primary_Color.TabIndex = 80;
      this.m_lbl_Primary_Color.Click += new System.EventHandler(this.m_lbl_Primary_Color_Click);
      // 
      // m_lbl_K
      // 
      this.m_lbl_K.Location = new System.Drawing.Point(438, 264);
      this.m_lbl_K.Name = "m_lbl_K";
      this.m_lbl_K.Size = new System.Drawing.Size(16, 16);
      this.m_lbl_K.TabIndex = 79;
      this.m_lbl_K.Text = "K:";
      // 
      // m_lbl_Yellow
      // 
      this.m_lbl_Yellow.Location = new System.Drawing.Point(438, 240);
      this.m_lbl_Yellow.Name = "m_lbl_Yellow";
      this.m_lbl_Yellow.Size = new System.Drawing.Size(16, 16);
      this.m_lbl_Yellow.TabIndex = 78;
      this.m_lbl_Yellow.Text = "Y:";
      // 
      // m_lbl_Magenta
      // 
      this.m_lbl_Magenta.Location = new System.Drawing.Point(438, 216);
      this.m_lbl_Magenta.Name = "m_lbl_Magenta";
      this.m_lbl_Magenta.Size = new System.Drawing.Size(16, 16);
      this.m_lbl_Magenta.TabIndex = 77;
      this.m_lbl_Magenta.Text = "M:";
      // 
      // m_lbl_Cyan
      // 
      this.m_lbl_Cyan.Location = new System.Drawing.Point(438, 192);
      this.m_lbl_Cyan.Name = "m_lbl_Cyan";
      this.m_lbl_Cyan.Size = new System.Drawing.Size(16, 16);
      this.m_lbl_Cyan.TabIndex = 76;
      this.m_lbl_Cyan.Text = "C:";
      // 
      // m_lbl_HexPound
      // 
      this.m_lbl_HexPound.Location = new System.Drawing.Point(328, 274);
      this.m_lbl_HexPound.Name = "m_lbl_HexPound";
      this.m_lbl_HexPound.Size = new System.Drawing.Size(16, 14);
      this.m_lbl_HexPound.TabIndex = 72;
      this.m_lbl_HexPound.Text = "#";
      // 
      // m_rbtn_Blue
      // 
      this.m_rbtn_Blue.Location = new System.Drawing.Point(324, 237);
      this.m_rbtn_Blue.Name = "m_rbtn_Blue";
      this.m_rbtn_Blue.Size = new System.Drawing.Size(35, 24);
      this.m_rbtn_Blue.TabIndex = 99;
      this.m_rbtn_Blue.Text = "B:";
      this.m_rbtn_Blue.CheckedChanged += new System.EventHandler(this.m_rbtn_Blue_CheckedChanged);
      // 
      // m_rbtn_Green
      // 
      this.m_rbtn_Green.Location = new System.Drawing.Point(324, 212);
      this.m_rbtn_Green.Name = "m_rbtn_Green";
      this.m_rbtn_Green.Size = new System.Drawing.Size(35, 24);
      this.m_rbtn_Green.TabIndex = 99;
      this.m_rbtn_Green.Text = "G:";
      this.m_rbtn_Green.CheckedChanged += new System.EventHandler(this.m_rbtn_Green_CheckedChanged);
      // 
      // m_rbtn_Red
      // 
      this.m_rbtn_Red.Location = new System.Drawing.Point(324, 187);
      this.m_rbtn_Red.Name = "m_rbtn_Red";
      this.m_rbtn_Red.Size = new System.Drawing.Size(35, 24);
      this.m_rbtn_Red.TabIndex = 99;
      this.m_rbtn_Red.Text = "R:";
      this.m_rbtn_Red.CheckedChanged += new System.EventHandler(this.m_rbtn_Red_CheckedChanged);
      // 
      // m_rbtn_Black
      // 
      this.m_rbtn_Black.Location = new System.Drawing.Point(324, 157);
      this.m_rbtn_Black.Name = "m_rbtn_Black";
      this.m_rbtn_Black.Size = new System.Drawing.Size(35, 24);
      this.m_rbtn_Black.TabIndex = 99;
      this.m_rbtn_Black.Text = "B:";
      this.m_rbtn_Black.CheckedChanged += new System.EventHandler(this.m_rbtn_Black_CheckedChanged);
      // 
      // m_rbtn_Sat
      // 
      this.m_rbtn_Sat.Location = new System.Drawing.Point(324, 132);
      this.m_rbtn_Sat.Name = "m_rbtn_Sat";
      this.m_rbtn_Sat.Size = new System.Drawing.Size(35, 24);
      this.m_rbtn_Sat.TabIndex = 99;
      this.m_rbtn_Sat.Text = "S:";
      this.m_rbtn_Sat.CheckedChanged += new System.EventHandler(this.m_rbtn_Sat_CheckedChanged);
      // 
      // m_rbtn_Hue
      // 
      this.m_rbtn_Hue.Location = new System.Drawing.Point(324, 107);
      this.m_rbtn_Hue.Name = "m_rbtn_Hue";
      this.m_rbtn_Hue.Size = new System.Drawing.Size(35, 24);
      this.m_rbtn_Hue.TabIndex = 99;
      this.m_rbtn_Hue.Text = "H:";
      this.m_rbtn_Hue.CheckedChanged += new System.EventHandler(this.m_rbtn_Hue_CheckedChanged);
      // 
      // m_txt_Hex
      // 
      this.m_txt_Hex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Hex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Hex.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Hex.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Hex.Location = new System.Drawing.Point(344, 270);
      this.m_txt_Hex.Name = "m_txt_Hex";
      this.m_txt_Hex.Size = new System.Drawing.Size(72, 21);
      this.m_txt_Hex.TabIndex = 0;
      this.m_txt_Hex.Leave += new System.EventHandler(this.m_txt_Hex_Leave);
      // 
      // m_txt_K
      // 
      this.m_txt_K.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_K.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_K.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_K.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_K.Location = new System.Drawing.Point(455, 262);
      this.m_txt_K.Name = "m_txt_K";
      this.m_txt_K.Size = new System.Drawing.Size(35, 21);
      this.m_txt_K.TabIndex = 10;
      this.m_txt_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_K.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_K.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_K.Validated += new System.EventHandler(this.m_txt_K_Validated);
      // 
      // m_txt_Yellow
      // 
      this.m_txt_Yellow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Yellow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Yellow.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Yellow.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Yellow.Location = new System.Drawing.Point(455, 237);
      this.m_txt_Yellow.Name = "m_txt_Yellow";
      this.m_txt_Yellow.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Yellow.TabIndex = 9;
      this.m_txt_Yellow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Yellow.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Yellow.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Yellow.Validated += new System.EventHandler(this.m_txt_Yellow_Validated);
      // 
      // m_txt_Magenta
      // 
      this.m_txt_Magenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Magenta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Magenta.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Magenta.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Magenta.Location = new System.Drawing.Point(455, 212);
      this.m_txt_Magenta.Name = "m_txt_Magenta";
      this.m_txt_Magenta.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Magenta.TabIndex = 8;
      this.m_txt_Magenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Magenta.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Magenta.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Magenta.Validated += new System.EventHandler(this.m_txt_Magenta_Validated);
      // 
      // m_txt_Cyan
      // 
      this.m_txt_Cyan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Cyan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Cyan.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Cyan.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Cyan.Location = new System.Drawing.Point(455, 187);
      this.m_txt_Cyan.Name = "m_txt_Cyan";
      this.m_txt_Cyan.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Cyan.TabIndex = 7;
      this.m_txt_Cyan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Cyan.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Cyan.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Cyan.Validated += new System.EventHandler(this.m_txt_Cyan_Validated);
      // 
      // m_txt_Blue
      // 
      this.m_txt_Blue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Blue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Blue.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Blue.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Blue.Location = new System.Drawing.Point(361, 237);
      this.m_txt_Blue.Name = "m_txt_Blue";
      this.m_txt_Blue.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Blue.TabIndex = 6;
      this.m_txt_Blue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Blue.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Blue.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Blue.Validated += new System.EventHandler(this.m_txt_Blue_Validated);
      // 
      // m_txt_Green
      // 
      this.m_txt_Green.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Green.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Green.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Green.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Green.Location = new System.Drawing.Point(361, 212);
      this.m_txt_Green.Name = "m_txt_Green";
      this.m_txt_Green.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Green.TabIndex = 5;
      this.m_txt_Green.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Green.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Green.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Green.Validated += new System.EventHandler(this.m_txt_Green_Validated);
      // 
      // m_txt_Red
      // 
      this.m_txt_Red.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Red.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Red.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Red.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Red.Location = new System.Drawing.Point(361, 187);
      this.m_txt_Red.Name = "m_txt_Red";
      this.m_txt_Red.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Red.TabIndex = 4;
      this.m_txt_Red.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Red.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Red.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Red.Validated += new System.EventHandler(this.m_txt_Red_Validated);
      // 
      // m_txt_Black
      // 
      this.m_txt_Black.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Black.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Black.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Black.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Black.Location = new System.Drawing.Point(361, 157);
      this.m_txt_Black.Name = "m_txt_Black";
      this.m_txt_Black.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Black.TabIndex = 3;
      this.m_txt_Black.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Black.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Black.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Black.Validated += new System.EventHandler(this.m_txt_Black_Validated);
      // 
      // m_txt_Sat
      // 
      this.m_txt_Sat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Sat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Sat.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Sat.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Sat.Location = new System.Drawing.Point(361, 132);
      this.m_txt_Sat.Name = "m_txt_Sat";
      this.m_txt_Sat.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Sat.TabIndex = 2;
      this.m_txt_Sat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Sat.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Sat.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Sat.Validated += new System.EventHandler(this.m_txt_Sat_Validated);
      // 
      // m_txt_Hue
      // 
      this.m_txt_Hue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.m_txt_Hue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_txt_Hue.Font = new System.Drawing.Font("Cascadia Mono", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_txt_Hue.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.m_txt_Hue.Location = new System.Drawing.Point(361, 107);
      this.m_txt_Hue.Name = "m_txt_Hue";
      this.m_txt_Hue.Size = new System.Drawing.Size(35, 21);
      this.m_txt_Hue.TabIndex = 1;
      this.m_txt_Hue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_txt_Hue.Enter += new System.EventHandler(this.m_txt_Enter);
      this.m_txt_Hue.Validating += new System.ComponentModel.CancelEventHandler(this.m_txt_ValidatingNumber);
      this.m_txt_Hue.Validated += new System.EventHandler(this.m_txt_Hue_Validated);
      // 
      // m_cmd_Cancel
      // 
      this.m_cmd_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cmd_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.m_cmd_Cancel.Location = new System.Drawing.Point(446, 12);
      this.m_cmd_Cancel.Name = "m_cmd_Cancel";
      this.m_cmd_Cancel.Size = new System.Drawing.Size(72, 25);
      this.m_cmd_Cancel.TabIndex = 12;
      this.m_cmd_Cancel.Text = "Cancel";
      this.m_cmd_Cancel.Click += new System.EventHandler(this.m_cmd_Cancel_Click);
      // 
      // m_cmd_OK
      // 
      this.m_cmd_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.m_cmd_OK.Location = new System.Drawing.Point(361, 12);
      this.m_cmd_OK.Name = "m_cmd_OK";
      this.m_cmd_OK.Size = new System.Drawing.Size(72, 25);
      this.m_cmd_OK.TabIndex = 11;
      this.m_cmd_OK.Text = "Ok";
      this.m_cmd_OK.Click += new System.EventHandler(this.m_cmd_OK_Click);
      // 
      // m_pbx_BlankBox
      // 
      this.m_pbx_BlankBox.BackColor = System.Drawing.Color.Black;
      this.m_pbx_BlankBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.m_pbx_BlankBox.Location = new System.Drawing.Point(442, 84);
      this.m_pbx_BlankBox.Name = "m_pbx_BlankBox";
      this.m_pbx_BlankBox.Size = new System.Drawing.Size(62, 70);
      this.m_pbx_BlankBox.TabIndex = 48;
      this.m_pbx_BlankBox.TabStop = false;
      // 
      // rbTransparent
      // 
      this.rbTransparent.AutoSize = true;
      this.rbTransparent.Location = new System.Drawing.Point(324, 76);
      this.rbTransparent.Name = "rbTransparent";
      this.rbTransparent.Size = new System.Drawing.Size(86, 17);
      this.rbTransparent.TabIndex = 99;
      this.rbTransparent.Text = "Transparent";
      this.rbTransparent.CheckedChanged += new System.EventHandler(this.rbTransparent_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(457, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 13);
      this.label1.TabIndex = 92;
      this.label1.Text = "new";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(453, 157);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(44, 13);
      this.label2.TabIndex = 92;
      this.label2.Text = "current";
      // 
      // colQueue
      // 
      this.colQueue.BackColor = System.Drawing.Color.Transparent;
      this.colQueue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.colQueue.Location = new System.Drawing.Point(13, 303);
      this.colQueue.Name = "colQueue";
      this.colQueue.NumPatches = 16;
      this.colQueue.Size = new System.Drawing.Size(502, 33);
      this.colQueue.TabIndex = 90;
      this.colQueue.TabStop = false;
      this.colQueue.ColorPicked += new System.EventHandler<bm98_AColorPicker.ColorPickedEventArgs>(this.colQueue_ColorPicked);
      // 
      // m_ctrl_ThinBox
      // 
      this.m_ctrl_ThinBox.BackColor = System.Drawing.Color.Transparent;
      this.m_ctrl_ThinBox.DrawStyle = bm98_AColorPicker.DrawStyle.Hue;
      this.m_ctrl_ThinBox.Location = new System.Drawing.Point(273, 25);
      this.m_ctrl_ThinBox.Name = "m_ctrl_ThinBox";
      this.m_ctrl_ThinBox.RGB = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.m_ctrl_ThinBox.Size = new System.Drawing.Size(40, 264);
      this.m_ctrl_ThinBox.TabIndex = 89;
      this.m_ctrl_ThinBox.TabStop = false;
      this.m_ctrl_ThinBox.Scroll += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.m_ctrl_ThinBox_Scroll);
      // 
      // m_ctrl_BigBox
      // 
      this.m_ctrl_BigBox.BackColor = System.Drawing.Color.Transparent;
      this.m_ctrl_BigBox.CausesValidation = false;
      this.m_ctrl_BigBox.DrawStyle = bm98_AColorPicker.DrawStyle.Hue;
      this.m_ctrl_BigBox.Location = new System.Drawing.Point(12, 27);
      this.m_ctrl_BigBox.Name = "m_ctrl_BigBox";
      this.m_ctrl_BigBox.RGB = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.m_ctrl_BigBox.Size = new System.Drawing.Size(260, 260);
      this.m_ctrl_BigBox.TabIndex = 88;
      this.m_ctrl_BigBox.TabStop = false;
      this.m_ctrl_BigBox.Scroll += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.m_ctrl_BigBox_Scroll);
      // 
      // frmAColorPicker
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
      this.CancelButton = this.m_cmd_Cancel;
      this.ClientSize = new System.Drawing.Size(530, 348);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.rbTransparent);
      this.Controls.Add(this.colQueue);
      this.Controls.Add(this.m_ctrl_ThinBox);
      this.Controls.Add(this.m_ctrl_BigBox);
      this.Controls.Add(this.m_lbl_Key_Symbol);
      this.Controls.Add(this.m_lbl_Yellow_Symbol);
      this.Controls.Add(this.m_lbl_Magenta_Symbol);
      this.Controls.Add(this.m_lbl_Cyan_Symbol);
      this.Controls.Add(this.m_lbl_Black_Symbol);
      this.Controls.Add(this.m_lbl_Saturation_Symbol);
      this.Controls.Add(this.m_lbl_Hue_Symbol);
      this.Controls.Add(this.m_lbl_Secondary_Color);
      this.Controls.Add(this.m_lbl_Primary_Color);
      this.Controls.Add(this.m_lbl_K);
      this.Controls.Add(this.m_lbl_Yellow);
      this.Controls.Add(this.m_lbl_Magenta);
      this.Controls.Add(this.m_lbl_Cyan);
      this.Controls.Add(this.m_lbl_HexPound);
      this.Controls.Add(this.m_rbtn_Blue);
      this.Controls.Add(this.m_rbtn_Green);
      this.Controls.Add(this.m_rbtn_Red);
      this.Controls.Add(this.m_rbtn_Black);
      this.Controls.Add(this.m_rbtn_Sat);
      this.Controls.Add(this.m_rbtn_Hue);
      this.Controls.Add(this.m_txt_Hex);
      this.Controls.Add(this.m_txt_K);
      this.Controls.Add(this.m_txt_Yellow);
      this.Controls.Add(this.m_txt_Magenta);
      this.Controls.Add(this.m_txt_Cyan);
      this.Controls.Add(this.m_txt_Blue);
      this.Controls.Add(this.m_txt_Green);
      this.Controls.Add(this.m_txt_Red);
      this.Controls.Add(this.m_txt_Black);
      this.Controls.Add(this.m_txt_Sat);
      this.Controls.Add(this.m_txt_Hue);
      this.Controls.Add(this.m_cmd_Cancel);
      this.Controls.Add(this.m_cmd_OK);
      this.Controls.Add(this.m_pbx_BlankBox);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAColorPicker";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "A Color Picker";
      this.Load += new System.EventHandler(this.frmColorPicker_Load);
      this.Shown += new System.EventHandler(this.frmAColorPicker_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.m_pbx_BlankBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label m_lbl_Key_Symbol;
    private System.Windows.Forms.Label m_lbl_Yellow_Symbol;
    private System.Windows.Forms.Label m_lbl_Magenta_Symbol;
    private System.Windows.Forms.Label m_lbl_Cyan_Symbol;
    private System.Windows.Forms.Label m_lbl_Black_Symbol;
    private System.Windows.Forms.Label m_lbl_Saturation_Symbol;
    private System.Windows.Forms.Label m_lbl_Hue_Symbol;
    private System.Windows.Forms.Label m_lbl_Secondary_Color;
    private System.Windows.Forms.Label m_lbl_Primary_Color;
    private System.Windows.Forms.Label m_lbl_K;
    private System.Windows.Forms.Label m_lbl_Yellow;
    private System.Windows.Forms.Label m_lbl_Magenta;
    private System.Windows.Forms.Label m_lbl_Cyan;
    private System.Windows.Forms.Label m_lbl_HexPound;
    private System.Windows.Forms.RadioButton m_rbtn_Blue;
    private System.Windows.Forms.RadioButton m_rbtn_Green;
    private System.Windows.Forms.RadioButton m_rbtn_Red;
    private System.Windows.Forms.RadioButton m_rbtn_Black;
    private System.Windows.Forms.RadioButton m_rbtn_Sat;
    private System.Windows.Forms.RadioButton m_rbtn_Hue;
    private System.Windows.Forms.TextBox m_txt_Hex;
    private System.Windows.Forms.TextBox m_txt_K;
    private System.Windows.Forms.TextBox m_txt_Yellow;
    private System.Windows.Forms.TextBox m_txt_Magenta;
    private System.Windows.Forms.TextBox m_txt_Cyan;
    private System.Windows.Forms.TextBox m_txt_Blue;
    private System.Windows.Forms.TextBox m_txt_Green;
    private System.Windows.Forms.TextBox m_txt_Red;
    private System.Windows.Forms.TextBox m_txt_Black;
    private System.Windows.Forms.TextBox m_txt_Sat;
    private System.Windows.Forms.TextBox m_txt_Hue;
    private System.Windows.Forms.Button m_cmd_Cancel;
    private System.Windows.Forms.Button m_cmd_OK;
    private System.Windows.Forms.PictureBox m_pbx_BlankBox;
    private UC_2DColorBox m_ctrl_BigBox;
    private UC_VerticalColorSlider m_ctrl_ThinBox;
    private UC_ColorQueue colQueue;
    private System.Windows.Forms.RadioButton rbTransparent;
    private Label label1;
    private Label label2;
  }
}