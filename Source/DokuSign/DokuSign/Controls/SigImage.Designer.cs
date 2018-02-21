namespace DokuSign.Controls
{
    partial class SigImage
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
            this.components = new System.ComponentModel.Container();
            this.label26 = new System.Windows.Forms.Label();
            this.custSigText = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.sigImgBox = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sigImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(0, 7);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(112, 15);
            this.label26.TabIndex = 68;
            this.label26.Text = "Texto personalizado";
            // 
            // custSigText
            // 
            this.custSigText.Location = new System.Drawing.Point(3, 25);
            this.custSigText.Multiline = true;
            this.custSigText.Name = "custSigText";
            this.custSigText.Size = new System.Drawing.Size(255, 55);
            this.custSigText.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(122, 189);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(62, 22);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Borrar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // sigImgBox
            // 
            this.sigImgBox.BackColor = System.Drawing.SystemColors.Window;
            this.sigImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sigImgBox.Location = new System.Drawing.Point(3, 111);
            this.sigImgBox.Name = "sigImgBox";
            this.sigImgBox.Size = new System.Drawing.Size(255, 72);
            this.sigImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sigImgBox.TabIndex = 63;
            this.sigImgBox.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(190, 189);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(62, 22);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Navegar";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(0, 93);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(142, 15);
            this.label27.TabIndex = 65;
            this.label27.Text = "Imagen de la firma digital";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SigImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label26);
            this.Controls.Add(this.custSigText);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.sigImgBox);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label27);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SigImage";
            this.Size = new System.Drawing.Size(263, 218);
            ((System.ComponentModel.ISupportInitialize)(this.sigImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox custSigText;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox sigImgBox;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
