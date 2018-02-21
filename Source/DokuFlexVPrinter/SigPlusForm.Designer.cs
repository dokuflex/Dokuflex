namespace DokuFlexVPrinter
{
    partial class SigPlusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SigPlusForm));
            this.cmdSign = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEncryption = new System.Windows.Forms.ComboBox();
            this.cboCompression = new System.Windows.Forms.ComboBox();
            this.cmdSaveImage = new System.Windows.Forms.Button();
            this.cmdSaveSig = new System.Windows.Forms.Button();
            this.cmdLoadSig = new System.Windows.Forms.Button();
            this.cboPenWidth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sigPlusNET1 = new Topaz.SigPlusNET();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSign
            // 
            this.cmdSign.Location = new System.Drawing.Point(12, 154);
            this.cmdSign.Name = "cmdSign";
            this.cmdSign.Size = new System.Drawing.Size(75, 23);
            this.cmdSign.TabIndex = 1;
            this.cmdSign.Text = "Firmar";
            this.cmdSign.UseVisualStyleBackColor = true;
            this.cmdSign.Click += new System.EventHandler(this.cmdSign_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(93, 154);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(75, 23);
            this.cmdStop.TabIndex = 2;
            this.cmdStop.Text = "Detener";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(176, 154);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 3;
            this.cmdClear.Text = "Limpiar";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(339, 154);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Cerrar";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboEncryption);
            this.groupBox1.Controls.Add(this.cboCompression);
            this.groupBox1.Location = new System.Drawing.Point(431, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 49);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compression / Encryption";
            // 
            // cboEncryption
            // 
            this.cboEncryption.FormattingEnabled = true;
            this.cboEncryption.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cboEncryption.Location = new System.Drawing.Point(83, 19);
            this.cboEncryption.Name = "cboEncryption";
            this.cboEncryption.Size = new System.Drawing.Size(58, 21);
            this.cboEncryption.TabIndex = 6;
            this.cboEncryption.Text = "0";
            // 
            // cboCompression
            // 
            this.cboCompression.FormattingEnabled = true;
            this.cboCompression.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cboCompression.Location = new System.Drawing.Point(6, 19);
            this.cboCompression.Name = "cboCompression";
            this.cboCompression.Size = new System.Drawing.Size(58, 21);
            this.cboCompression.TabIndex = 6;
            this.cboCompression.Text = "0";
            // 
            // cmdSaveImage
            // 
            this.cmdSaveImage.Location = new System.Drawing.Point(257, 154);
            this.cmdSaveImage.Name = "cmdSaveImage";
            this.cmdSaveImage.Size = new System.Drawing.Size(75, 23);
            this.cmdSaveImage.TabIndex = 6;
            this.cmdSaveImage.Text = "Guardar imagen";
            this.cmdSaveImage.UseVisualStyleBackColor = true;
            this.cmdSaveImage.Click += new System.EventHandler(this.cmdSaveImage_Click);
            // 
            // cmdSaveSig
            // 
            this.cmdSaveSig.Location = new System.Drawing.Point(518, 65);
            this.cmdSaveSig.Name = "cmdSaveSig";
            this.cmdSaveSig.Size = new System.Drawing.Size(61, 23);
            this.cmdSaveSig.TabIndex = 7;
            this.cmdSaveSig.Text = "Save Sig";
            this.cmdSaveSig.UseVisualStyleBackColor = true;
            this.cmdSaveSig.Click += new System.EventHandler(this.cmdSaveSig_Click);
            // 
            // cmdLoadSig
            // 
            this.cmdLoadSig.Location = new System.Drawing.Point(518, 94);
            this.cmdLoadSig.Name = "cmdLoadSig";
            this.cmdLoadSig.Size = new System.Drawing.Size(60, 23);
            this.cmdLoadSig.TabIndex = 8;
            this.cmdLoadSig.Text = "Load Sig";
            this.cmdLoadSig.UseVisualStyleBackColor = true;
            this.cmdLoadSig.Click += new System.EventHandler(this.cmdLoadSig_Click);
            // 
            // cboPenWidth
            // 
            this.cboPenWidth.FormattingEnabled = true;
            this.cboPenWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cboPenWidth.Location = new System.Drawing.Point(441, 81);
            this.cboPenWidth.Name = "cboPenWidth";
            this.cboPenWidth.Size = new System.Drawing.Size(58, 21);
            this.cboPenWidth.TabIndex = 9;
            this.cboPenWidth.Text = "1";
            this.cboPenWidth.SelectedIndexChanged += new System.EventHandler(this.cboPenWidth_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(438, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pen Width";
            // 
            // sigPlusNET1
            // 
            this.sigPlusNET1.BackColor = System.Drawing.Color.White;
            this.sigPlusNET1.ForeColor = System.Drawing.Color.Black;
            this.sigPlusNET1.Location = new System.Drawing.Point(12, 12);
            this.sigPlusNET1.Name = "sigPlusNET1";
            this.sigPlusNET1.Size = new System.Drawing.Size(402, 136);
            this.sigPlusNET1.TabIndex = 11;
            this.sigPlusNET1.Text = "sigPlusNET1";
            // 
            // SigPlusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 185);
            this.Controls.Add(this.sigPlusNET1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPenWidth);
            this.Controls.Add(this.cmdLoadSig);
            this.Controls.Add(this.cmdSaveSig);
            this.Controls.Add(this.cmdSaveImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdSign);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SigPlusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firma biometrica";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSign;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboEncryption;
        private System.Windows.Forms.ComboBox cboCompression;
        private System.Windows.Forms.Button cmdSaveImage;
        private System.Windows.Forms.Button cmdSaveSig;
        private System.Windows.Forms.Button cmdLoadSig;
        private System.Windows.Forms.ComboBox cboPenWidth;
        private System.Windows.Forms.Label label1;
        private Topaz.SigPlusNET sigPlusNET1;
    }
}

