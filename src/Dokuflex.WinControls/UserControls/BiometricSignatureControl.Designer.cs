namespace Dokuflex.WinControls.UserControls
{
    partial class BiometricSignatureControl
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.clearButton = new Telerik.WinControls.UI.RadButton();
            this.finalizeButton = new Telerik.WinControls.UI.RadButton();
            this.signButton = new Telerik.WinControls.UI.RadButton();
            this.sigPlusNET = new Topaz.SigPlusNET();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalizeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signButton)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.signButton);
            this.radPanel1.Controls.Add(this.finalizeButton);
            this.radPanel1.Controls.Add(this.clearButton);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel1.Location = new System.Drawing.Point(0, 242);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(476, 41);
            this.radPanel1.TabIndex = 0;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(275, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(85, 25);
            this.clearButton.TabIndex = 0;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // finalizeButton
            // 
            this.finalizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.finalizeButton.Location = new System.Drawing.Point(366, 3);
            this.finalizeButton.Name = "finalizeButton";
            this.finalizeButton.Size = new System.Drawing.Size(90, 25);
            this.finalizeButton.TabIndex = 0;
            this.finalizeButton.Text = "Finalize";
            this.finalizeButton.Click += new System.EventHandler(this.finalizeButton_Click);
            // 
            // signButton
            // 
            this.signButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.signButton.Location = new System.Drawing.Point(184, 3);
            this.signButton.Name = "signButton";
            this.signButton.Size = new System.Drawing.Size(85, 25);
            this.signButton.TabIndex = 1;
            this.signButton.Text = "Sign";
            this.signButton.Click += new System.EventHandler(this.signButton_Click);
            // 
            // sigPlusNET
            // 
            this.sigPlusNET.BackColor = System.Drawing.Color.White;
            this.sigPlusNET.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sigPlusNET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigPlusNET.ForeColor = System.Drawing.Color.Black;
            this.sigPlusNET.Location = new System.Drawing.Point(0, 0);
            this.sigPlusNET.Name = "sigPlusNET";
            this.sigPlusNET.Size = new System.Drawing.Size(476, 242);
            this.sigPlusNET.TabIndex = 21;
            this.sigPlusNET.Text = "sigPlusNET1";
            // 
            // BiometricSignatureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sigPlusNET);
            this.Controls.Add(this.radPanel1);
            this.Name = "BiometricSignatureControl";
            this.Size = new System.Drawing.Size(476, 283);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clearButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalizeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton signButton;
        private Telerik.WinControls.UI.RadButton finalizeButton;
        private Telerik.WinControls.UI.RadButton clearButton;
        private Topaz.SigPlusNET sigPlusNET;
    }
}
