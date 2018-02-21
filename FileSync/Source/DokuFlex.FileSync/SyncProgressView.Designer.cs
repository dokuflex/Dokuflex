namespace DokuFlex.FileSync
{
    partial class SyncProgressView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncProgressView));
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lklSyncPath = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // progress
            // 
            resources.ApplyResources(this.progress, "progress");
            this.progress.Name = "progress";
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lklSyncPath
            // 
            resources.ApplyResources(this.lklSyncPath, "lklSyncPath");
            this.lklSyncPath.AutoEllipsis = true;
            this.lklSyncPath.Name = "lklSyncPath";
            this.lklSyncPath.TabStop = true;
            this.lklSyncPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklSyncPath_LinkClicked);
            // 
            // SyncProgressView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.lklSyncPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncProgressView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncProgressView_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lklSyncPath;
    }
}