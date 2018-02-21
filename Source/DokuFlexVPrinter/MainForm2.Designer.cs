namespace DokuFlexVPrinter
{
    partial class MainForm2
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
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbxSignatureType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbProgressText = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.sigLocalUCtrl1 = new DokuFlexVPrinter.Controls.SigLocalUCtrl();
            this.sigPlusUCtrl1 = new DokuFlexVPrinter.Controls.SigPlusUCtrl();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // cbxSignatureType
            // 
            this.cbxSignatureType.DisplayMember = "name";
            this.cbxSignatureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSignatureType.FormattingEnabled = true;
            this.cbxSignatureType.Items.AddRange(new object[] {
            "Certificado local",
            "Certificado en linea (online)",
            "Firma biometrica"});
            this.cbxSignatureType.Location = new System.Drawing.Point(215, 14);
            this.cbxSignatureType.Name = "cbxSignatureType";
            this.cbxSignatureType.Size = new System.Drawing.Size(238, 23);
            this.cbxSignatureType.TabIndex = 29;
            this.cbxSignatureType.ValueMember = "id";
            this.cbxSignatureType.SelectedIndexChanged += new System.EventHandler(this.cbxSignatureType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 15);
            this.label9.TabIndex = 30;
            this.label9.Text = "Firmar archivo con:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbProgressText);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(889, 44);
            this.panel1.TabIndex = 38;
            // 
            // lbProgressText
            // 
            this.lbProgressText.AutoEllipsis = true;
            this.lbProgressText.Location = new System.Drawing.Point(12, 6);
            this.lbProgressText.Name = "lbProgressText";
            this.lbProgressText.Size = new System.Drawing.Size(283, 15);
            this.lbProgressText.TabIndex = 3;
            this.lbProgressText.Text = "label1";
            this.lbProgressText.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 24);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(283, 14);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(784, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(685, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 27);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "C:\\Users\\Emilio\\Pictures\\logo_dokuflex.jpg";
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(97, 509);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.sigLocalUCtrl1);
            this.pnlContainer.Controls.Add(this.sigPlusUCtrl1);
            this.pnlContainer.Location = new System.Drawing.Point(103, 54);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(779, 380);
            this.pnlContainer.TabIndex = 40;
            // 
            // sigLocalUCtrl1
            // 
            this.sigLocalUCtrl1.BackColor = System.Drawing.SystemColors.Window;
            this.sigLocalUCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigLocalUCtrl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigLocalUCtrl1.Location = new System.Drawing.Point(0, 0);
            this.sigLocalUCtrl1.Name = "sigLocalUCtrl1";
            this.sigLocalUCtrl1.Size = new System.Drawing.Size(779, 380);
            this.sigLocalUCtrl1.TabIndex = 1;
            // 
            // sigPlusUCtrl1
            // 
            this.sigPlusUCtrl1.BackColor = System.Drawing.SystemColors.Window;
            this.sigPlusUCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigPlusUCtrl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigPlusUCtrl1.Location = new System.Drawing.Point(0, 0);
            this.sigPlusUCtrl1.Name = "sigPlusUCtrl1";
            this.sigPlusUCtrl1.Size = new System.Drawing.Size(779, 380);
            this.sigPlusUCtrl1.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(784, 455);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(70, 23);
            this.btnBrowse.TabIndex = 42;
            this.btnBrowse.Text = "Navegar...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // textPath
            // 
            this.textPath.BackColor = System.Drawing.SystemColors.Window;
            this.textPath.Location = new System.Drawing.Point(103, 455);
            this.textPath.Name = "textPath";
            this.textPath.ReadOnly = true;
            this.textPath.Size = new System.Drawing.Size(675, 23);
            this.textPath.TabIndex = 41;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(100, 437);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(285, 15);
            this.label11.TabIndex = 43;
            this.label11.Text = "Guardar el archivo firmado en la siguiente ubicación:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(103, 488);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "Establecer como predeterminado";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(889, 559);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbxSignatureType);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VPrinter";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox cbxSignatureType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbProgressText;
        private Controls.SigPlusUCtrl sigPlusUCtrl1;
        private Controls.SigLocalUCtrl sigLocalUCtrl1;
        private System.Windows.Forms.Button button1;
    }
}