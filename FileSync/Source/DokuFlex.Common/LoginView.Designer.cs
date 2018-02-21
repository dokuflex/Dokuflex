namespace DokuFlex.Common
{
    partial class LoginView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.pbLoging = new System.Windows.Forms.ProgressBar();
            this.lbLoging = new System.Windows.Forms.Label();
            this.lbDokuFlexDotCom = new System.Windows.Forms.Label();
            this.LoginPicture = new System.Windows.Forms.PictureBox();
            this.lbDokuFlex = new System.Windows.Forms.Label();
            this.lbSigninAddress = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.llbForgotMe = new System.Windows.Forms.LinkLabel();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.textEmailAddress = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.LoginPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLoging
            // 
            resources.ApplyResources(this.pbLoging, "pbLoging");
            this.pbLoging.Name = "pbLoging";
            this.pbLoging.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // lbLoging
            // 
            this.lbLoging.AutoEllipsis = true;
            resources.ApplyResources(this.lbLoging, "lbLoging");
            this.lbLoging.Name = "lbLoging";
            // 
            // lbDokuFlexDotCom
            // 
            resources.ApplyResources(this.lbDokuFlexDotCom, "lbDokuFlexDotCom");
            this.lbDokuFlexDotCom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            this.lbDokuFlexDotCom.Name = "lbDokuFlexDotCom";
            // 
            // LoginPicture
            // 
            resources.ApplyResources(this.LoginPicture, "LoginPicture");
            this.LoginPicture.Name = "LoginPicture";
            this.LoginPicture.TabStop = false;
            // 
            // lbDokuFlex
            // 
            resources.ApplyResources(this.lbDokuFlex, "lbDokuFlex");
            this.lbDokuFlex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.lbDokuFlex.Name = "lbDokuFlex";
            // 
            // lbSigninAddress
            // 
            resources.ApplyResources(this.lbSigninAddress, "lbSigninAddress");
            this.lbSigninAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            this.lbSigninAddress.Name = "lbSigninAddress";
            // 
            // btnLogin
            // 
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // llbForgotMe
            // 
            resources.ApplyResources(this.llbForgotMe, "llbForgotMe");
            this.llbForgotMe.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbForgotMe.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.llbForgotMe.Name = "llbForgotMe";
            this.llbForgotMe.TabStop = true;
            this.llbForgotMe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbForgotMe_LinkClicked);
            // 
            // chkRememberMe
            // 
            resources.ApplyResources(this.chkRememberMe, "chkRememberMe");
            this.chkRememberMe.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // textEmailAddress
            // 
            resources.ApplyResources(this.textEmailAddress, "textEmailAddress");
            this.textEmailAddress.Name = "textEmailAddress";
            // 
            // textPassword
            // 
            resources.ApplyResources(this.textPassword, "textPassword");
            this.textPassword.Name = "textPassword";
            this.textPassword.UseSystemPasswordChar = true;
            // 
            // LoginView
            // 
            this.AcceptButton = this.btnLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pbLoging);
            this.Controls.Add(this.lbLoging);
            this.Controls.Add(this.lbDokuFlexDotCom);
            this.Controls.Add(this.LoginPicture);
            this.Controls.Add(this.lbDokuFlex);
            this.Controls.Add(this.lbSigninAddress);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.llbForgotMe);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.textEmailAddress);
            this.Controls.Add(this.textPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.LoginPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbLoging;
        private System.Windows.Forms.Label lbLoging;
        private System.Windows.Forms.Label lbDokuFlexDotCom;
        private System.Windows.Forms.PictureBox LoginPicture;
        private System.Windows.Forms.Label lbDokuFlex;
        private System.Windows.Forms.Label lbSigninAddress;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel llbForgotMe;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.TextBox textEmailAddress;
        private System.Windows.Forms.TextBox textPassword;
    }
}