namespace DokuSign.Controls
{
    partial class PagePreview
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previewPanel = new System.Windows.Forms.Panel();
            this.pagePreviewPanel = new System.Windows.Forms.Panel();
            this.sigPicture = new System.Windows.Forms.PictureBox();
            this.previewPanel.SuspendLayout();
            this.pagePreviewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sigPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // previewPanel
            // 
            this.previewPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPanel.Controls.Add(this.pagePreviewPanel);
            this.previewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPanel.Location = new System.Drawing.Point(0, 0);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(353, 457);
            this.previewPanel.TabIndex = 4;
            // 
            // pagePreviewPanel
            // 
            this.pagePreviewPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pagePreviewPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pagePreviewPanel.Controls.Add(this.sigPicture);
            this.pagePreviewPanel.Location = new System.Drawing.Point(3, 77);
            this.pagePreviewPanel.Name = "pagePreviewPanel";
            this.pagePreviewPanel.Size = new System.Drawing.Size(200, 72);
            this.pagePreviewPanel.TabIndex = 1;
            // 
            // sigPicture
            // 
            this.sigPicture.BackColor = System.Drawing.SystemColors.Window;
            this.sigPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sigPicture.Location = new System.Drawing.Point(27, 38);
            this.sigPicture.Name = "sigPicture";
            this.sigPicture.Size = new System.Drawing.Size(50, 20);
            this.sigPicture.TabIndex = 0;
            this.sigPicture.TabStop = false;
            this.sigPicture.Move += new System.EventHandler(this.sigPicture_Move);
            this.sigPicture.Resize += new System.EventHandler(this.sigPicture_Resize);
            // 
            // PagePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PagePreview";
            this.Size = new System.Drawing.Size(353, 457);
            this.previewPanel.ResumeLayout(false);
            this.pagePreviewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sigPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Panel pagePreviewPanel;
        private System.Windows.Forms.PictureBox sigPicture;
    }
}
