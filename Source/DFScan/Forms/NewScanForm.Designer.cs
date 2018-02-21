namespace DokuFlex.Scan.Forms
{
    partial class NewScanForm
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
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbHomologation = new System.Windows.Forms.Label();
            this.cbxScannerSettings = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbtnRGB = new System.Windows.Forms.RadioButton();
            this.rbtnGray = new System.Windows.Forms.RadioButton();
            this.rbtnBW = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDuplex = new System.Windows.Forms.CheckBox();
            this.chkADF = new System.Windows.Forms.CheckBox();
            this.cbxScanner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxResolution = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnPrintBarCode = new System.Windows.Forms.Button();
            this.upDownPageBreak = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkBarCode = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbtnPNG = new System.Windows.Forms.RadioButton();
            this.rbtnTIFF = new System.Windows.Forms.RadioButton();
            this.rbtnBMP = new System.Windows.Forms.RadioButton();
            this.rbtnJPG = new System.Windows.Forms.RadioButton();
            this.previewPane = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textTotal = new System.Windows.Forms.TextBox();
            this.textPart = new System.Windows.Forms.TextBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevius = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.rightPane = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayout.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownPageBreak)).BeginInit();
            this.previewPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.rightPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.panel3, 0, 0);
            this.tableLayout.Controls.Add(this.panel5, 0, 1);
            this.tableLayout.Controls.Add(this.panel6, 0, 2);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 3;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.99739F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.00956F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.99305F));
            this.tableLayout.Size = new System.Drawing.Size(267, 567);
            this.tableLayout.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbHomologation);
            this.panel3.Controls.Add(this.cbxScannerSettings);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(259, 78);
            this.panel3.TabIndex = 0;
            // 
            // lbHomologation
            // 
            this.lbHomologation.AutoSize = true;
            this.lbHomologation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbHomologation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHomologation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.lbHomologation.Location = new System.Drawing.Point(130, 39);
            this.lbHomologation.Name = "lbHomologation";
            this.lbHomologation.Size = new System.Drawing.Size(113, 23);
            this.lbHomologation.TabIndex = 13;
            this.lbHomologation.Text = "Homologado";
            this.lbHomologation.Visible = false;
            // 
            // cbxScannerSettings
            // 
            this.cbxScannerSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScannerSettings.FormattingEnabled = true;
            this.cbxScannerSettings.Location = new System.Drawing.Point(52, 13);
            this.cbxScannerSettings.Name = "cbxScannerSettings";
            this.cbxScannerSettings.Size = new System.Drawing.Size(191, 23);
            this.cbxScannerSettings.TabIndex = 9;
            this.cbxScannerSettings.SelectedIndexChanged += new System.EventHandler(this.cbxScannerSettings_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Perfil:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbtnRGB);
            this.panel5.Controls.Add(this.rbtnGray);
            this.panel5.Controls.Add(this.rbtnBW);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.chkDuplex);
            this.panel5.Controls.Add(this.chkADF);
            this.panel5.Controls.Add(this.cbxScanner);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.cbxResolution);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 89);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(259, 247);
            this.panel5.TabIndex = 2;
            // 
            // rbtnRGB
            // 
            this.rbtnRGB.AutoSize = true;
            this.rbtnRGB.Location = new System.Drawing.Point(12, 161);
            this.rbtnRGB.Name = "rbtnRGB";
            this.rbtnRGB.Size = new System.Drawing.Size(54, 19);
            this.rbtnRGB.TabIndex = 7;
            this.rbtnRGB.TabStop = true;
            this.rbtnRGB.Text = "Color";
            this.rbtnRGB.UseVisualStyleBackColor = true;
            // 
            // rbtnGray
            // 
            this.rbtnGray.AutoSize = true;
            this.rbtnGray.Location = new System.Drawing.Point(122, 136);
            this.rbtnGray.Name = "rbtnGray";
            this.rbtnGray.Size = new System.Drawing.Size(106, 19);
            this.rbtnGray.TabIndex = 6;
            this.rbtnGray.TabStop = true;
            this.rbtnGray.Text = "Escala de grises";
            this.rbtnGray.UseVisualStyleBackColor = true;
            // 
            // rbtnBW
            // 
            this.rbtnBW.AutoSize = true;
            this.rbtnBW.Location = new System.Drawing.Point(12, 136);
            this.rbtnBW.Name = "rbtnBW";
            this.rbtnBW.Size = new System.Drawing.Size(104, 19);
            this.rbtnBW.TabIndex = 5;
            this.rbtnBW.TabStop = true;
            this.rbtnBW.Text = "Blanco y negro";
            this.rbtnBW.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Formato del color";
            // 
            // chkDuplex
            // 
            this.chkDuplex.AutoSize = true;
            this.chkDuplex.Location = new System.Drawing.Point(79, 67);
            this.chkDuplex.Name = "chkDuplex";
            this.chkDuplex.Size = new System.Drawing.Size(62, 19);
            this.chkDuplex.TabIndex = 3;
            this.chkDuplex.Text = "Duplex";
            this.chkDuplex.UseVisualStyleBackColor = true;
            this.chkDuplex.Visible = false;
            // 
            // chkADF
            // 
            this.chkADF.AutoSize = true;
            this.chkADF.Location = new System.Drawing.Point(12, 67);
            this.chkADF.Name = "chkADF";
            this.chkADF.Size = new System.Drawing.Size(48, 19);
            this.chkADF.TabIndex = 2;
            this.chkADF.Text = "ADF";
            this.chkADF.UseVisualStyleBackColor = true;
            // 
            // cbxScanner
            // 
            this.cbxScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScanner.FormattingEnabled = true;
            this.cbxScanner.Location = new System.Drawing.Point(12, 38);
            this.cbxScanner.Name = "cbxScanner";
            this.cbxScanner.Size = new System.Drawing.Size(231, 23);
            this.cbxScanner.TabIndex = 1;
            this.cbxScanner.SelectedIndexChanged += new System.EventHandler(this.cbxScanner_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Scáner";
            // 
            // cbxResolution
            // 
            this.cbxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxResolution.Location = new System.Drawing.Point(115, 196);
            this.cbxResolution.Name = "cbxResolution";
            this.cbxResolution.Size = new System.Drawing.Size(113, 23);
            this.cbxResolution.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Resolución (ppp):";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnPrintBarCode);
            this.panel6.Controls.Add(this.upDownPageBreak);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.chkBarCode);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.rbtnPNG);
            this.panel6.Controls.Add(this.rbtnTIFF);
            this.panel6.Controls.Add(this.rbtnBMP);
            this.panel6.Controls.Add(this.rbtnJPG);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(4, 343);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(259, 220);
            this.panel6.TabIndex = 3;
            // 
            // btnPrintBarCode
            // 
            this.btnPrintBarCode.Enabled = false;
            this.btnPrintBarCode.Location = new System.Drawing.Point(21, 184);
            this.btnPrintBarCode.Name = "btnPrintBarCode";
            this.btnPrintBarCode.Size = new System.Drawing.Size(152, 23);
            this.btnPrintBarCode.TabIndex = 17;
            this.btnPrintBarCode.Text = "Imprimir código de barra.";
            this.btnPrintBarCode.UseVisualStyleBackColor = true;
            this.btnPrintBarCode.Visible = false;
            this.btnPrintBarCode.Click += new System.EventHandler(this.btnPrintBarCode_Click);
            // 
            // upDownPageBreak
            // 
            this.upDownPageBreak.Location = new System.Drawing.Point(146, 104);
            this.upDownPageBreak.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownPageBreak.Name = "upDownPageBreak";
            this.upDownPageBreak.Size = new System.Drawing.Size(67, 23);
            this.upDownPageBreak.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Salto de página:";
            // 
            // chkBarCode
            // 
            this.chkBarCode.AutoSize = true;
            this.chkBarCode.Enabled = false;
            this.chkBarCode.Location = new System.Drawing.Point(21, 150);
            this.chkBarCode.Name = "chkBarCode";
            this.chkBarCode.Size = new System.Drawing.Size(222, 19);
            this.chkBarCode.TabIndex = 6;
            this.chkBarCode.Text = "Salto de página por código de barras.";
            this.chkBarCode.UseVisualStyleBackColor = true;
            this.chkBarCode.Visible = false;
            this.chkBarCode.CheckedChanged += new System.EventHandler(this.rbtnTIFF_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Tipo de archivo";
            // 
            // rbtnPNG
            // 
            this.rbtnPNG.AutoSize = true;
            this.rbtnPNG.Location = new System.Drawing.Point(124, 49);
            this.rbtnPNG.Name = "rbtnPNG";
            this.rbtnPNG.Size = new System.Drawing.Size(49, 19);
            this.rbtnPNG.TabIndex = 3;
            this.rbtnPNG.Text = "PNG";
            this.rbtnPNG.UseVisualStyleBackColor = true;
            this.rbtnPNG.CheckedChanged += new System.EventHandler(this.rbtnTIFF_CheckedChanged);
            // 
            // rbtnTIFF
            // 
            this.rbtnTIFF.AutoSize = true;
            this.rbtnTIFF.Checked = true;
            this.rbtnTIFF.Location = new System.Drawing.Point(181, 49);
            this.rbtnTIFF.Name = "rbtnTIFF";
            this.rbtnTIFF.Size = new System.Drawing.Size(47, 19);
            this.rbtnTIFF.TabIndex = 4;
            this.rbtnTIFF.TabStop = true;
            this.rbtnTIFF.Text = "TIFF";
            this.rbtnTIFF.UseVisualStyleBackColor = true;
            this.rbtnTIFF.CheckedChanged += new System.EventHandler(this.rbtnTIFF_CheckedChanged);
            // 
            // rbtnBMP
            // 
            this.rbtnBMP.AutoSize = true;
            this.rbtnBMP.Location = new System.Drawing.Point(66, 49);
            this.rbtnBMP.Name = "rbtnBMP";
            this.rbtnBMP.Size = new System.Drawing.Size(50, 19);
            this.rbtnBMP.TabIndex = 2;
            this.rbtnBMP.Text = "BMP";
            this.rbtnBMP.UseVisualStyleBackColor = true;
            this.rbtnBMP.CheckedChanged += new System.EventHandler(this.rbtnTIFF_CheckedChanged);
            // 
            // rbtnJPG
            // 
            this.rbtnJPG.AutoSize = true;
            this.rbtnJPG.Location = new System.Drawing.Point(12, 49);
            this.rbtnJPG.Name = "rbtnJPG";
            this.rbtnJPG.Size = new System.Drawing.Size(44, 19);
            this.rbtnJPG.TabIndex = 1;
            this.rbtnJPG.Text = "JPG";
            this.rbtnJPG.UseVisualStyleBackColor = true;
            this.rbtnJPG.CheckedChanged += new System.EventHandler(this.rbtnTIFF_CheckedChanged);
            // 
            // previewPane
            // 
            this.previewPane.Controls.Add(this.pictureBox);
            this.previewPane.Controls.Add(this.panel2);
            this.previewPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPane.Location = new System.Drawing.Point(267, 0);
            this.previewPane.Name = "previewPane";
            this.previewPane.Size = new System.Drawing.Size(387, 567);
            this.previewPane.TabIndex = 7;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(387, 519);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textTotal);
            this.panel2.Controls.Add(this.textPart);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnPrevius);
            this.panel2.Controls.Add(this.btnFirst);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 519);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(387, 48);
            this.panel2.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(191, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "/";
            // 
            // textTotal
            // 
            this.textTotal.BackColor = System.Drawing.SystemColors.Window;
            this.textTotal.Location = new System.Drawing.Point(209, 12);
            this.textTotal.Name = "textTotal";
            this.textTotal.ReadOnly = true;
            this.textTotal.Size = new System.Drawing.Size(60, 23);
            this.textTotal.TabIndex = 3;
            this.textTotal.Text = "0";
            // 
            // textPart
            // 
            this.textPart.BackColor = System.Drawing.SystemColors.Window;
            this.textPart.Location = new System.Drawing.Point(125, 12);
            this.textPart.Name = "textPart";
            this.textPart.ReadOnly = true;
            this.textPart.Size = new System.Drawing.Size(60, 23);
            this.textPart.TabIndex = 2;
            this.textPart.Text = "0";
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(331, 12);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(50, 25);
            this.btnLast.TabIndex = 5;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(275, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 25);
            this.btnNext.TabIndex = 4;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevius
            // 
            this.btnPrevius.Location = new System.Drawing.Point(69, 12);
            this.btnPrevius.Name = "btnPrevius";
            this.btnPrevius.Size = new System.Drawing.Size(50, 25);
            this.btnPrevius.TabIndex = 1;
            this.btnPrevius.UseVisualStyleBackColor = true;
            this.btnPrevius.Click += new System.EventHandler(this.btnPrevius_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(13, 12);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(50, 25);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // rightPane
            // 
            this.rightPane.Controls.Add(this.btnCancel);
            this.rightPane.Controls.Add(this.btnPreview);
            this.rightPane.Controls.Add(this.btnScan);
            this.rightPane.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPane.Location = new System.Drawing.Point(654, 0);
            this.rightPane.Name = "rightPane";
            this.rightPane.Size = new System.Drawing.Size(114, 567);
            this.rightPane.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(6, 530);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(6, 113);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(100, 90);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "&Vista previa";
            this.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(6, 17);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(100, 90);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "&Digitalizar";
            this.btnScan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // NewScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(768, 567);
            this.Controls.Add(this.previewPane);
            this.Controls.Add(this.tableLayout);
            this.Controls.Add(this.rightPane);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewScanForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva digitalización";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewScanForm_FormClosing);
            this.tableLayout.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownPageBreak)).EndInit();
            this.previewPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.rightPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbxResolution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel previewPane;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbxScannerSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxScanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDuplex;
        private System.Windows.Forms.CheckBox chkADF;
        private System.Windows.Forms.RadioButton rbtnRGB;
        private System.Windows.Forms.RadioButton rbtnGray;
        private System.Windows.Forms.RadioButton rbtnBW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtnPNG;
        private System.Windows.Forms.RadioButton rbtnTIFF;
        private System.Windows.Forms.RadioButton rbtnBMP;
        private System.Windows.Forms.RadioButton rbtnJPG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel rightPane;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textTotal;
        private System.Windows.Forms.TextBox textPart;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevius;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.NumericUpDown upDownPageBreak;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkBarCode;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lbHomologation;
        private System.Windows.Forms.Button btnPrintBarCode;
    }
}