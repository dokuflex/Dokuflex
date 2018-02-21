namespace DokuFlex.WinForms.Common
{
    partial class TransferProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferProgressForm));
            this.pbTransfering = new System.Windows.Forms.ProgressBar();
            this.lbTransfering = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbTransfering
            // 
            resources.ApplyResources(this.pbTransfering, "pbTransfering");
            this.pbTransfering.Name = "pbTransfering";
            this.pbTransfering.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // lbTransfering
            // 
            resources.ApplyResources(this.lbTransfering, "lbTransfering");
            this.lbTransfering.Name = "lbTransfering";
            // 
            // TransferProgressForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbTransfering);
            this.Controls.Add(this.lbTransfering);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransferFileView_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbTransfering;
        private System.Windows.Forms.Label lbTransfering;
    }
}