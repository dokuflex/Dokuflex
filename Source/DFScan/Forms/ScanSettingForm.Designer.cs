namespace DokuFlex.Scan.Forms
{
    partial class ScanSettingForm
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
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbxScanner = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbxResolution = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxFileType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxColorFormat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.bindingSRouting = new System.Windows.Forms.BindingSource(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.cbxCertificates = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkConverToPDF = new System.Windows.Forms.CheckBox();
            this.cbxDocumentaryTypes = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lbHomologation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSRouting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true));
            this.textName.Location = new System.Drawing.Point(68, 15);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(377, 23);
            this.textName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nombre:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 318);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 46);
            this.panel1.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(239, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 253);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(426, 225);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Digitalización";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbxScanner);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(3, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(409, 52);
            this.panel3.TabIndex = 11;
            // 
            // cbxScanner
            // 
            this.cbxScanner.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource, "Scanner", true));
            this.cbxScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScanner.FormattingEnabled = true;
            this.cbxScanner.Location = new System.Drawing.Point(60, 13);
            this.cbxScanner.Name = "cbxScanner";
            this.cbxScanner.Size = new System.Drawing.Size(269, 23);
            this.cbxScanner.TabIndex = 1;
            this.cbxScanner.SelectedIndexChanged += new System.EventHandler(this.cbxScanner_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Scáner:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbxResolution);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.cbxFileType);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.cbxColorFormat);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Location = new System.Drawing.Point(2, 64);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(410, 140);
            this.panel5.TabIndex = 12;
            // 
            // cbxResolution
            // 
            this.cbxResolution.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Resolution", true));
            this.cbxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxResolution.FormatString = "N0";
            this.cbxResolution.FormattingEnabled = true;
            this.cbxResolution.Items.AddRange(new object[] {
            "200",
            "300",
            "600",
            "900",
            "1200"});
            this.cbxResolution.Location = new System.Drawing.Point(157, 61);
            this.cbxResolution.Name = "cbxResolution";
            this.cbxResolution.Size = new System.Drawing.Size(93, 23);
            this.cbxResolution.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Resolución (ppp):";
            // 
            // cbxFileType
            // 
            this.cbxFileType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource, "FileType", true));
            this.cbxFileType.DisplayMember = "DisplayName";
            this.cbxFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFileType.FormattingEnabled = true;
            this.cbxFileType.Location = new System.Drawing.Point(157, 100);
            this.cbxFileType.Name = "cbxFileType";
            this.cbxFileType.Size = new System.Drawing.Size(93, 23);
            this.cbxFileType.TabIndex = 3;
            this.cbxFileType.ValueMember = "Extension";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tipo de archivo:";
            // 
            // cbxColorFormat
            // 
            this.cbxColorFormat.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource, "ColorFormat", true));
            this.cbxColorFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxColorFormat.FormattingEnabled = true;
            this.cbxColorFormat.Items.AddRange(new object[] {
            "Color",
            "Escala de grises",
            "Blanco y negro"});
            this.cbxColorFormat.Location = new System.Drawing.Point(157, 18);
            this.cbxColorFormat.Name = "cbxColorFormat";
            this.cbxColorFormat.Size = new System.Drawing.Size(172, 23);
            this.cbxColorFormat.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Formato del color:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnBrowse);
            this.tabPage2.Controls.Add(this.textPath);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.cbxCertificates);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.chkConverToPDF);
            this.tabPage2.Controls.Add(this.cbxDocumentaryTypes);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(426, 225);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Enrutamiento";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(350, 170);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(70, 23);
            this.btnBrowse.TabIndex = 33;
            this.btnBrowse.Text = "Navegar...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textPath
            // 
            this.textPath.BackColor = System.Drawing.SystemColors.Window;
            this.textPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSRouting, "FolderPath", true));
            this.textPath.Location = new System.Drawing.Point(9, 171);
            this.textPath.Name = "textPath";
            this.textPath.ReadOnly = true;
            this.textPath.Size = new System.Drawing.Size(335, 23);
            this.textPath.TabIndex = 32;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(320, 15);
            this.label11.TabIndex = 31;
            this.label11.Text = "Guardar los archivos digitalizados en la siguiente ubicación:";
            // 
            // cbxCertificates
            // 
            this.cbxCertificates.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSRouting, "Certificate", true));
            this.cbxCertificates.DisplayMember = "text";
            this.cbxCertificates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCertificates.FormattingEnabled = true;
            this.cbxCertificates.Location = new System.Drawing.Point(113, 100);
            this.cbxCertificates.Name = "cbxCertificates";
            this.cbxCertificates.Size = new System.Drawing.Size(244, 23);
            this.cbxCertificates.TabIndex = 30;
            this.cbxCertificates.ValueMember = "id";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 15);
            this.label10.TabIndex = 29;
            this.label10.Text = "Certificado:";
            // 
            // chkConverToPDF
            // 
            this.chkConverToPDF.AutoSize = true;
            this.chkConverToPDF.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSRouting, "ConvertToPdf", true));
            this.chkConverToPDF.Location = new System.Drawing.Point(9, 60);
            this.chkConverToPDF.Name = "chkConverToPDF";
            this.chkConverToPDF.Size = new System.Drawing.Size(301, 19);
            this.chkConverToPDF.TabIndex = 28;
            this.chkConverToPDF.Text = "Convertir el documento o imágen digitalizada a PDF";
            this.chkConverToPDF.UseVisualStyleBackColor = true;
            // 
            // cbxDocumentaryTypes
            // 
            this.cbxDocumentaryTypes.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSRouting, "Documentary", true));
            this.cbxDocumentaryTypes.DisplayMember = "name";
            this.cbxDocumentaryTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDocumentaryTypes.FormattingEnabled = true;
            this.cbxDocumentaryTypes.Location = new System.Drawing.Point(113, 21);
            this.cbxDocumentaryTypes.Name = "cbxDocumentaryTypes";
            this.cbxDocumentaryTypes.Size = new System.Drawing.Size(244, 23);
            this.cbxDocumentaryTypes.TabIndex = 27;
            this.cbxDocumentaryTypes.ValueMember = "id";
            this.cbxDocumentaryTypes.SelectedIndexChanged += new System.EventHandler(this.cbxDocumentaryTypes_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 15);
            this.label9.TabIndex = 26;
            this.label9.Text = "Tipo documental:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.DataSource = this.bindingSource;
            // 
            // lbHomologation
            // 
            this.lbHomologation.AutoSize = true;
            this.lbHomologation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbHomologation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHomologation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.lbHomologation.Location = new System.Drawing.Point(332, 41);
            this.lbHomologation.Name = "lbHomologation";
            this.lbHomologation.Size = new System.Drawing.Size(113, 23);
            this.lbHomologation.TabIndex = 12;
            this.lbHomologation.Text = "Homologado";
            this.lbHomologation.Visible = false;
            // 
            // ScanSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 364);
            this.Controls.Add(this.lbHomologation);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perfil de digitalización";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanSettingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSRouting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbxScanner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbxResolution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxFileType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxColorFormat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbxDocumentaryTypes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkConverToPDF;
        private System.Windows.Forms.ComboBox cbxCertificates;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.BindingSource bindingSRouting;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lbHomologation;
    }
}