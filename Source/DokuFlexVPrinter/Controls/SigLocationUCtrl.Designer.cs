namespace DokuFlexVPrinter.Controls
{
    partial class SigLocationUCtrl
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
            this.sigPanel1 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.pagePreviewPanel = new System.Windows.Forms.Panel();
            this.sigPicture = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.numberOfPagesUpDown = new System.Windows.Forms.NumericUpDown();
            this.sigPanel1.SuspendLayout();
            this.previewPanel.SuspendLayout();
            this.pagePreviewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sigPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPagesUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // sigPanel1
            // 
            this.sigPanel1.Controls.Add(this.label33);
            this.sigPanel1.Controls.Add(this.previewPanel);
            this.sigPanel1.Controls.Add(this.label28);
            this.sigPanel1.Controls.Add(this.numberOfPagesUpDown);
            this.sigPanel1.Location = new System.Drawing.Point(3, 3);
            this.sigPanel1.Name = "sigPanel1";
            this.sigPanel1.Size = new System.Drawing.Size(402, 251);
            this.sigPanel1.TabIndex = 49;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.MediumBlue;
            this.label33.Location = new System.Drawing.Point(3, 3);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(145, 71);
            this.label33.TabIndex = 55;
            this.label33.Text = "Arrastrar para cambiar el tamaño de la firma en el cuadro de vista previa o use l" +
    "os controles.";
            // 
            // previewPanel
            // 
            this.previewPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPanel.Controls.Add(this.pagePreviewPanel);
            this.previewPanel.Location = new System.Drawing.Point(154, 3);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(240, 240);
            this.previewPanel.TabIndex = 46;
            // 
            // pagePreviewPanel
            // 
            this.pagePreviewPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pagePreviewPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pagePreviewPanel.Controls.Add(this.sigPicture);
            this.pagePreviewPanel.Location = new System.Drawing.Point(21, 53);
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
            this.sigPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sigPicture.TabIndex = 0;
            this.sigPicture.TabStop = false;
            this.sigPicture.Move += new System.EventHandler(this.sigPicture_Move);
            this.sigPicture.Resize += new System.EventHandler(this.sigPicture_Resize);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(14, 94);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 13);
            this.label28.TabIndex = 45;
            this.label28.Text = "Página";
            // 
            // numberOfPagesUpDown
            // 
            this.numberOfPagesUpDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfPagesUpDown.Location = new System.Drawing.Point(60, 88);
            this.numberOfPagesUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numberOfPagesUpDown.Name = "numberOfPagesUpDown";
            this.numberOfPagesUpDown.Size = new System.Drawing.Size(71, 26);
            this.numberOfPagesUpDown.TabIndex = 35;
            this.numberOfPagesUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfPagesUpDown.ValueChanged += new System.EventHandler(this.numberOfPagesUpDown_ValueChanged);
            // 
            // SigLocationUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sigPanel1);
            this.Name = "SigLocationUCtrl";
            this.Size = new System.Drawing.Size(409, 256);
            this.sigPanel1.ResumeLayout(false);
            this.sigPanel1.PerformLayout();
            this.previewPanel.ResumeLayout(false);
            this.pagePreviewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sigPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPagesUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sigPanel1;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Panel pagePreviewPanel;
        private System.Windows.Forms.PictureBox sigPicture;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown numberOfPagesUpDown;
    }
}
