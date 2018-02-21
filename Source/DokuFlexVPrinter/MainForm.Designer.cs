namespace DokuFlexVPrinter
{
    partial class MainForm
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
            this.cbxSignatureType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxCertificates = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBioSign = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSignatureType
            // 
            this.cbxSignatureType.DisplayMember = "name";
            this.cbxSignatureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSignatureType.FormattingEnabled = true;
            this.cbxSignatureType.Items.AddRange(new object[] {
            "Certificado",
            "Firma biometrica"});
            this.cbxSignatureType.Location = new System.Drawing.Point(150, 26);
            this.cbxSignatureType.Name = "cbxSignatureType";
            this.cbxSignatureType.Size = new System.Drawing.Size(206, 23);
            this.cbxSignatureType.TabIndex = 0;
            this.cbxSignatureType.ValueMember = "id";
            this.cbxSignatureType.SelectedIndexChanged += new System.EventHandler(this.cbxSignatureType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 15);
            this.label9.TabIndex = 28;
            this.label9.Text = "Firmar archivo con:";
            // 
            // cbxCertificates
            // 
            this.cbxCertificates.DisplayMember = "text";
            this.cbxCertificates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCertificates.FormattingEnabled = true;
            this.cbxCertificates.Location = new System.Drawing.Point(245, 75);
            this.cbxCertificates.Name = "cbxCertificates";
            this.cbxCertificates.Size = new System.Drawing.Size(187, 23);
            this.cbxCertificates.TabIndex = 1;
            this.cbxCertificates.ValueMember = "id";
            this.cbxCertificates.SelectedIndexChanged += new System.EventHandler(this.cbxCertificates_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(204, 15);
            this.label10.TabIndex = 31;
            this.label10.Text = "Firmar el archivo con este certificado:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(362, 241);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(70, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Navegar...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textPath
            // 
            this.textPath.BackColor = System.Drawing.SystemColors.Window;
            this.textPath.Location = new System.Drawing.Point(38, 242);
            this.textPath.Name = "textPath";
            this.textPath.ReadOnly = true;
            this.textPath.Size = new System.Drawing.Size(318, 23);
            this.textPath.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 224);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(301, 15);
            this.label11.TabIndex = 34;
            this.label11.Text = "Guardar los archivos firmados en la siguiente ubicación:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 315);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 55);
            this.panel1.TabIndex = 37;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 11);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(274, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(386, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(300, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(38, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Establecer como predeterminada";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnBioSign
            // 
            this.btnBioSign.Enabled = false;
            this.btnBioSign.Location = new System.Drawing.Point(38, 178);
            this.btnBioSign.Name = "btnBioSign";
            this.btnBioSign.Size = new System.Drawing.Size(201, 23);
            this.btnBioSign.TabIndex = 3;
            this.btnBioSign.Text = "Firmar el archivo biométricamente";
            this.btnBioSign.UseVisualStyleBackColor = true;
            this.btnBioSign.Click += new System.EventHandler(this.btnBioSign_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(35, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Proteger el contenido con contraseña:";
            // 
            // textPassword
            // 
            this.textPassword.Enabled = false;
            this.textPassword.Location = new System.Drawing.Point(245, 126);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(187, 23);
            this.textPassword.TabIndex = 2;
            this.textPassword.UseSystemPasswordChar = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(478, 370);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.btnBioSign);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxCertificates);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxSignatureType);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DokuFlex VPrinter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxSignatureType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxCertificates;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBioSign;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}