namespace Dokuflex.WinControls
{
    partial class PdfSignerForm
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.signatureTypeDdList = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.settingsPanel = new Telerik.WinControls.UI.RadPanel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.selectFolderBtn = new Telerik.WinControls.UI.RadButton();
            this.selectRecipientsBtn = new Telerik.WinControls.UI.RadButton();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.cancelBtn = new Telerik.WinControls.UI.RadButton();
            this.pdfViewer = new Telerik.WinControls.UI.RadPdfViewer();
            this.radCheckedDropDownList1 = new Telerik.WinControls.UI.RadCheckedDropDownList();
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.doneBtn = new Telerik.WinControls.UI.RadButton();
            this.signPageBtn = new Telerik.WinControls.UI.RadButton();
            this.removeSignatureBtn = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.pageNumberLabel = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.signatureTypeDdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectFolderBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectRecipientsBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cancelBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckedDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doneBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signPageBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeSignatureBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageNumberLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.doneBtn);
            this.radPanel1.Controls.Add(this.radTextBox1);
            this.radPanel1.Controls.Add(this.radCheckedDropDownList1);
            this.radPanel1.Controls.Add(this.selectRecipientsBtn);
            this.radPanel1.Controls.Add(this.selectFolderBtn);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(777, 78);
            this.radPanel1.TabIndex = 0;
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.pageNumberLabel);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.removeSignatureBtn);
            this.radPanel2.Controls.Add(this.signPageBtn);
            this.radPanel2.Controls.Add(this.radLabel3);
            this.radPanel2.Controls.Add(this.settingsPanel);
            this.radPanel2.Controls.Add(this.signatureTypeDdList);
            this.radPanel2.Controls.Add(this.radLabel2);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.radPanel2.Location = new System.Drawing.Point(0, 78);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(358, 335);
            this.radPanel2.TabIndex = 1;
            // 
            // signatureTypeDdList
            // 
            this.signatureTypeDdList.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.signatureTypeDdList.Font = new System.Drawing.Font("Segoe UI", 10F);
            radListDataItem1.Text = "Local Certificate";
            radListDataItem2.Text = "Online Certificate";
            radListDataItem3.Text = "Image from PC";
            radListDataItem4.Text = "Biometric signature";
            this.signatureTypeDdList.Items.Add(radListDataItem1);
            this.signatureTypeDdList.Items.Add(radListDataItem2);
            this.signatureTypeDdList.Items.Add(radListDataItem3);
            this.signatureTypeDdList.Items.Add(radListDataItem4);
            this.signatureTypeDdList.Location = new System.Drawing.Point(12, 37);
            this.signatureTypeDdList.Name = "signatureTypeDdList";
            this.signatureTypeDdList.Size = new System.Drawing.Size(235, 23);
            this.signatureTypeDdList.TabIndex = 1;
            this.signatureTypeDdList.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.signatureTypeDdList_SelectedIndexChanged);
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.radLabel2.Location = new System.Drawing.Point(12, 6);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(114, 25);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "Signature type";
            // 
            // radPanel3
            // 
            this.radPanel3.Controls.Add(this.pdfViewer);
            this.radPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel3.Location = new System.Drawing.Point(358, 78);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(419, 335);
            this.radPanel3.TabIndex = 2;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsPanel.Location = new System.Drawing.Point(12, 185);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(321, 144);
            this.settingsPanel.TabIndex = 3;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.radLabel3.Location = new System.Drawing.Point(12, 154);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(66, 25);
            this.radLabel3.TabIndex = 4;
            this.radLabel3.Text = "Settings";
            // 
            // selectFolderBtn
            // 
            this.selectFolderBtn.Location = new System.Drawing.Point(12, 12);
            this.selectFolderBtn.Name = "selectFolderBtn";
            this.selectFolderBtn.Size = new System.Drawing.Size(72, 24);
            this.selectFolderBtn.TabIndex = 5;
            this.selectFolderBtn.Text = "Folder...";
            // 
            // selectRecipientsBtn
            // 
            this.selectRecipientsBtn.Location = new System.Drawing.Point(12, 42);
            this.selectRecipientsBtn.Name = "selectRecipientsBtn";
            this.selectRecipientsBtn.Size = new System.Drawing.Size(72, 24);
            this.selectRecipientsBtn.TabIndex = 7;
            this.selectRecipientsBtn.Text = "Recipients...";
            // 
            // radPanel4
            // 
            this.radPanel4.Controls.Add(this.cancelBtn);
            this.radPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel4.Location = new System.Drawing.Point(0, 413);
            this.radPanel4.Name = "radPanel4";
            this.radPanel4.Size = new System.Drawing.Size(777, 40);
            this.radPanel4.TabIndex = 3;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(680, 6);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(85, 25);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "Cancel";
            // 
            // pdfViewer
            // 
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.EnableThumbnails = false;
            this.pdfViewer.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.Size = new System.Drawing.Size(419, 335);
            this.pdfViewer.TabIndex = 0;
            this.pdfViewer.Text = "radPdfViewer1";
            this.pdfViewer.ThumbnailsScaleFactor = 0.15F;
            // 
            // radCheckedDropDownList1
            // 
            this.radCheckedDropDownList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radCheckedDropDownList1.Location = new System.Drawing.Point(90, 44);
            this.radCheckedDropDownList1.Name = "radCheckedDropDownList1";
            this.radCheckedDropDownList1.Size = new System.Drawing.Size(605, 20);
            this.radCheckedDropDownList1.TabIndex = 8;
            // 
            // radTextBox1
            // 
            this.radTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radTextBox1.Location = new System.Drawing.Point(90, 14);
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(605, 20);
            this.radTextBox1.TabIndex = 9;
            // 
            // doneBtn
            // 
            this.doneBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.doneBtn.Location = new System.Drawing.Point(701, 14);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(64, 50);
            this.doneBtn.TabIndex = 10;
            this.doneBtn.Text = "Done!";
            // 
            // signPageBtn
            // 
            this.signPageBtn.Location = new System.Drawing.Point(12, 109);
            this.signPageBtn.Name = "signPageBtn";
            this.signPageBtn.Size = new System.Drawing.Size(110, 24);
            this.signPageBtn.TabIndex = 5;
            this.signPageBtn.Text = "Sign page";
            this.signPageBtn.Click += new System.EventHandler(this.signPageBtn_Click);
            // 
            // removeSignatureBtn
            // 
            this.removeSignatureBtn.Location = new System.Drawing.Point(137, 109);
            this.removeSignatureBtn.Name = "removeSignatureBtn";
            this.removeSignatureBtn.Size = new System.Drawing.Size(110, 24);
            this.removeSignatureBtn.TabIndex = 6;
            this.removeSignatureBtn.Text = "Remove signature";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.radLabel1.Location = new System.Drawing.Point(12, 78);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(44, 25);
            this.radLabel1.TabIndex = 7;
            this.radLabel1.Text = "Page";
            // 
            // pageNumberLabel
            // 
            this.pageNumberLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.pageNumberLabel.Location = new System.Drawing.Point(228, 78);
            this.pageNumberLabel.Name = "pageNumberLabel";
            this.pageNumberLabel.Size = new System.Drawing.Size(105, 25);
            this.pageNumberLabel.TabIndex = 8;
            this.pageNumberLabel.Text = "Page number";
            // 
            // PdfSignerForm
            // 
            this.AcceptButton = this.doneBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(777, 453);
            this.Controls.Add(this.radPanel3);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radPanel4);
            this.Name = "PdfSignerForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PdfSignerForm";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.signatureTypeDdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingsPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectFolderBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectRecipientsBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cancelBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckedDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doneBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signPageBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeSignatureBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageNumberLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadDropDownList signatureTypeDdList;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadPanel settingsPanel;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadButton selectRecipientsBtn;
        private Telerik.WinControls.UI.RadButton selectFolderBtn;
        private Telerik.WinControls.UI.RadPanel radPanel4;
        private Telerik.WinControls.UI.RadButton cancelBtn;
        private Telerik.WinControls.UI.RadPdfViewer pdfViewer;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadCheckedDropDownList radCheckedDropDownList1;
        private Telerik.WinControls.UI.RadButton doneBtn;
        private Telerik.WinControls.UI.RadButton signPageBtn;
        private Telerik.WinControls.UI.RadButton removeSignatureBtn;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel pageNumberLabel;
    }
}
