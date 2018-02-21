namespace Dokuflex.WinControls.UserControls
{
    partial class SignatureImageControl
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonPnl = new Telerik.WinControls.UI.RadPanel();
            this.loadBtn = new Telerik.WinControls.UI.RadButton();
            this.clearBtn = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPnl)).BeginInit();
            this.buttonPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(347, 110);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // buttonPnl
            // 
            this.buttonPnl.Controls.Add(this.clearBtn);
            this.buttonPnl.Controls.Add(this.loadBtn);
            this.buttonPnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPnl.Location = new System.Drawing.Point(0, 110);
            this.buttonPnl.Name = "buttonPnl";
            this.buttonPnl.Size = new System.Drawing.Size(347, 40);
            this.buttonPnl.TabIndex = 1;
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(157, 6);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(85, 25);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Load";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(248, 6);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(85, 25);
            this.clearBtn.TabIndex = 0;
            this.clearBtn.Text = "Clear";
            // 
            // SignatureImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonPnl);
            this.Name = "SignatureImageControl";
            this.Size = new System.Drawing.Size(347, 150);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPnl)).EndInit();
            this.buttonPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private Telerik.WinControls.UI.RadPanel buttonPnl;
        private Telerik.WinControls.UI.RadButton clearBtn;
        private Telerik.WinControls.UI.RadButton loadBtn;
    }
}
