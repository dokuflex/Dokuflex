namespace DokuFlex.Outlook.AddIn
{
    partial class UploadEmailView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadEmailView));
            this.llbSettings = new System.Windows.Forms.LinkLabel();
            this.lbUploading = new System.Windows.Forms.Label();
            this.pbUploading = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLocationPath = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkAttachedList = new System.Windows.Forms.CheckedListBox();
            this.chkIncludeMessage = new System.Windows.Forms.CheckBox();
            this.emailPicture = new System.Windows.Forms.PictureBox();
            this.lbDokuFlexDotCom = new System.Windows.Forms.Label();
            this.lbFrom = new System.Windows.Forms.Label();
            this.lbSubject = new System.Windows.Forms.Label();
            this.lbReceivedTime = new System.Windows.Forms.Label();
            this.lbDokuFlex = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.chkAttachments = new System.Windows.Forms.CheckBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.btnAssociateWithProcess = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.emailPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // llbSettings
            // 
            this.llbSettings.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            resources.ApplyResources(this.llbSettings, "llbSettings");
            this.llbSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbSettings.LinkColor = System.Drawing.Color.DarkGray;
            this.llbSettings.Name = "llbSettings";
            this.llbSettings.TabStop = true;
            this.llbSettings.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            this.llbSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbSettings_LinkClicked);
            // 
            // lbUploading
            // 
            resources.ApplyResources(this.lbUploading, "lbUploading");
            this.lbUploading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbUploading.Name = "lbUploading";
            // 
            // pbUploading
            // 
            resources.ApplyResources(this.pbUploading, "pbUploading");
            this.pbUploading.Name = "pbUploading";
            this.pbUploading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Name = "label3";
            // 
            // lbLocationPath
            // 
            this.lbLocationPath.AutoEllipsis = true;
            resources.ApplyResources(this.lbLocationPath, "lbLocationPath");
            this.lbLocationPath.Name = "lbLocationPath";
            // 
            // btnStart
            // 
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkAttachedList
            // 
            this.chkAttachedList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkAttachedList.CheckOnClick = true;
            this.chkAttachedList.FormattingEnabled = true;
            resources.ApplyResources(this.chkAttachedList, "chkAttachedList");
            this.chkAttachedList.Name = "chkAttachedList";
            // 
            // chkIncludeMessage
            // 
            resources.ApplyResources(this.chkIncludeMessage, "chkIncludeMessage");
            this.chkIncludeMessage.Checked = true;
            this.chkIncludeMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeMessage.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkIncludeMessage.Name = "chkIncludeMessage";
            this.chkIncludeMessage.UseVisualStyleBackColor = true;
            // 
            // emailPicture
            // 
            resources.ApplyResources(this.emailPicture, "emailPicture");
            this.emailPicture.Name = "emailPicture";
            this.emailPicture.TabStop = false;
            // 
            // lbDokuFlexDotCom
            // 
            resources.ApplyResources(this.lbDokuFlexDotCom, "lbDokuFlexDotCom");
            this.lbDokuFlexDotCom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            this.lbDokuFlexDotCom.Name = "lbDokuFlexDotCom";
            // 
            // lbFrom
            // 
            this.lbFrom.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this.lbFrom, "lbFrom");
            this.lbFrom.Name = "lbFrom";
            // 
            // lbSubject
            // 
            this.lbSubject.AutoEllipsis = true;
            resources.ApplyResources(this.lbSubject, "lbSubject");
            this.lbSubject.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbSubject.Name = "lbSubject";
            // 
            // lbReceivedTime
            // 
            resources.ApplyResources(this.lbReceivedTime, "lbReceivedTime");
            this.lbReceivedTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbReceivedTime.Name = "lbReceivedTime";
            // 
            // lbDokuFlex
            // 
            resources.ApplyResources(this.lbDokuFlex, "lbDokuFlex");
            this.lbDokuFlex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.lbDokuFlex.Name = "lbDokuFlex";
            // 
            // lbTitle
            // 
            resources.ApplyResources(this.lbTitle, "lbTitle");
            this.lbTitle.Name = "lbTitle";
            // 
            // chkAttachments
            // 
            resources.ApplyResources(this.chkAttachments, "chkAttachments");
            this.chkAttachments.Checked = true;
            this.chkAttachments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttachments.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkAttachments.Name = "chkAttachments";
            this.chkAttachments.UseVisualStyleBackColor = true;
            this.chkAttachments.CheckedChanged += new System.EventHandler(this.chkAttachments_CheckedChanged);
            // 
            // lblProcess
            // 
            this.lblProcess.AutoEllipsis = true;
            resources.ApplyResources(this.lblProcess, "lblProcess");
            this.lblProcess.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblProcess.Name = "lblProcess";
            // 
            // btnAssociateWithProcess
            // 
            this.btnAssociateWithProcess.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAssociateWithProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.btnAssociateWithProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            resources.ApplyResources(this.btnAssociateWithProcess, "btnAssociateWithProcess");
            this.btnAssociateWithProcess.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAssociateWithProcess.Name = "btnAssociateWithProcess";
            this.btnAssociateWithProcess.UseVisualStyleBackColor = true;
            this.btnAssociateWithProcess.Click += new System.EventHandler(this.btnAssociateWithProcess_Click);
            // 
            // UploadEmailView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.btnAssociateWithProcess);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.chkAttachments);
            this.Controls.Add(this.llbSettings);
            this.Controls.Add(this.lbUploading);
            this.Controls.Add(this.pbUploading);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbLocationPath);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.chkAttachedList);
            this.Controls.Add(this.chkIncludeMessage);
            this.Controls.Add(this.emailPicture);
            this.Controls.Add(this.lbDokuFlexDotCom);
            this.Controls.Add(this.lbFrom);
            this.Controls.Add(this.lbSubject);
            this.Controls.Add(this.lbReceivedTime);
            this.Controls.Add(this.lbDokuFlex);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadEmailView";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UploadEmailView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.emailPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llbSettings;
        private System.Windows.Forms.Label lbUploading;
        private System.Windows.Forms.ProgressBar pbUploading;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbLocationPath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckedListBox chkAttachedList;
        private System.Windows.Forms.CheckBox chkIncludeMessage;
        private System.Windows.Forms.PictureBox emailPicture;
        private System.Windows.Forms.Label lbDokuFlexDotCom;
        private System.Windows.Forms.Label lbFrom;
        private System.Windows.Forms.Label lbSubject;
        private System.Windows.Forms.Label lbReceivedTime;
        private System.Windows.Forms.Label lbDokuFlex;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.CheckBox chkAttachments;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Button btnAssociateWithProcess;
    }
}