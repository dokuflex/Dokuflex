namespace DokuFlexVPrinter.Controls
{
    partial class SigLocalUCtrl
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
            this.chkSigVisible = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Locationtext = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Contacttext = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Reasontext = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCertificates = new System.Windows.Forms.ComboBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.multiSigChkBx = new System.Windows.Forms.CheckBox();
            this.sigPanel2 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.sigImgBox = new System.Windows.Forms.PictureBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.custSigText = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.sigLocationUCtrl1 = new DokuFlexVPrinter.Controls.SigLocationUCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.sigPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sigImgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // chkSigVisible
            // 
            this.chkSigVisible.AutoSize = true;
            this.chkSigVisible.Location = new System.Drawing.Point(329, 65);
            this.chkSigVisible.Name = "chkSigVisible";
            this.chkSigVisible.Size = new System.Drawing.Size(92, 19);
            this.chkSigVisible.TabIndex = 41;
            this.chkSigVisible.Text = "Firma visible";
            this.chkSigVisible.UseVisualStyleBackColor = true;
            this.chkSigVisible.CheckedChanged += new System.EventHandler(this.chkVisibleSignature_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 15);
            this.label13.TabIndex = 40;
            this.label13.Text = "Ubicación:";
            // 
            // Locationtext
            // 
            this.Locationtext.Location = new System.Drawing.Point(93, 115);
            this.Locationtext.Name = "Locationtext";
            this.Locationtext.Size = new System.Drawing.Size(209, 23);
            this.Locationtext.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 15);
            this.label12.TabIndex = 38;
            this.label12.Text = "Contacto:";
            // 
            // Contacttext
            // 
            this.Contacttext.Location = new System.Drawing.Point(93, 86);
            this.Contacttext.Name = "Contacttext";
            this.Contacttext.Size = new System.Drawing.Size(209, 23);
            this.Contacttext.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 15);
            this.label11.TabIndex = 36;
            this.label11.Text = "Rasón:";
            // 
            // Reasontext
            // 
            this.Reasontext.Location = new System.Drawing.Point(93, 144);
            this.Reasontext.Name = "Reasontext";
            this.Reasontext.Size = new System.Drawing.Size(209, 23);
            this.Reasontext.TabIndex = 35;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(635, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(26, 25);
            this.btnBrowse.TabIndex = 43;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 15);
            this.label10.TabIndex = 44;
            this.label10.Text = "Contraseña:";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(93, 43);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(161, 23);
            this.passwordBox.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 45;
            this.label1.Text = "Certificado:";
            // 
            // cbxCertificates
            // 
            this.cbxCertificates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCertificates.FormattingEnabled = true;
            this.cbxCertificates.Location = new System.Drawing.Point(93, 14);
            this.cbxCertificates.Name = "cbxCertificates";
            this.cbxCertificates.Size = new System.Drawing.Size(536, 23);
            this.cbxCertificates.TabIndex = 47;
            this.cbxCertificates.SelectedIndexChanged += new System.EventHandler(this.cbxCertificates_SelectedIndexChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // multiSigChkBx
            // 
            this.multiSigChkBx.AutoSize = true;
            this.multiSigChkBx.Checked = true;
            this.multiSigChkBx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.multiSigChkBx.Location = new System.Drawing.Point(22, 383);
            this.multiSigChkBx.Name = "multiSigChkBx";
            this.multiSigChkBx.Size = new System.Drawing.Size(594, 19);
            this.multiSigChkBx.TabIndex = 51;
            this.multiSigChkBx.Text = "Habilitar multi-firma (esto permitirá que firme el documento varias veces sin inv" +
    "alidar la firma de más edad)";
            this.multiSigChkBx.UseVisualStyleBackColor = true;
            // 
            // sigPanel2
            // 
            this.sigPanel2.Controls.Add(this.label26);
            this.sigPanel2.Controls.Add(this.clearBtn);
            this.sigPanel2.Controls.Add(this.sigImgBox);
            this.sigPanel2.Controls.Add(this.browseBtn);
            this.sigPanel2.Controls.Add(this.custSigText);
            this.sigPanel2.Controls.Add(this.label27);
            this.sigPanel2.Enabled = false;
            this.sigPanel2.Location = new System.Drawing.Point(22, 173);
            this.sigPanel2.Name = "sigPanel2";
            this.sigPanel2.Size = new System.Drawing.Size(280, 201);
            this.sigPanel2.TabIndex = 52;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 7);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(112, 15);
            this.label26.TabIndex = 49;
            this.label26.Text = "Texto personalizado";
            // 
            // clearBtn
            // 
            this.clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearBtn.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearBtn.Location = new System.Drawing.Point(138, 176);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(62, 22);
            this.clearBtn.TabIndex = 48;
            this.clearBtn.Text = "Limpiar";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // sigImgBox
            // 
            this.sigImgBox.BackColor = System.Drawing.SystemColors.Window;
            this.sigImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sigImgBox.Location = new System.Drawing.Point(12, 101);
            this.sigImgBox.Name = "sigImgBox";
            this.sigImgBox.Size = new System.Drawing.Size(256, 69);
            this.sigImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sigImgBox.TabIndex = 40;
            this.sigImgBox.TabStop = false;
            // 
            // browseBtn
            // 
            this.browseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseBtn.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseBtn.Location = new System.Drawing.Point(206, 176);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(62, 22);
            this.browseBtn.TabIndex = 43;
            this.browseBtn.Text = "Navegar";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // custSigText
            // 
            this.custSigText.Location = new System.Drawing.Point(12, 25);
            this.custSigText.Multiline = true;
            this.custSigText.Name = "custSigText";
            this.custSigText.Size = new System.Drawing.Size(256, 55);
            this.custSigText.TabIndex = 41;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(9, 83);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(106, 15);
            this.label27.TabIndex = 44;
            this.label27.Text = "Imagen de la firma";
            // 
            // sigLocationUCtrl1
            // 
            this.sigLocationUCtrl1.Enabled = false;
            this.sigLocationUCtrl1.Location = new System.Drawing.Point(329, 86);
            this.sigLocationUCtrl1.Name = "sigLocationUCtrl1";
            this.sigLocationUCtrl1.Size = new System.Drawing.Size(460, 288);
            this.sigLocationUCtrl1.TabIndex = 53;
            // 
            // SigLocalUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.sigLocationUCtrl1);
            this.Controls.Add(this.sigPanel2);
            this.Controls.Add(this.multiSigChkBx);
            this.Controls.Add(this.cbxCertificates);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSigVisible);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Locationtext);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Contacttext);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Reasontext);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SigLocalUCtrl";
            this.Size = new System.Drawing.Size(792, 410);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.sigPanel2.ResumeLayout(false);
            this.sigPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sigImgBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSigVisible;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Locationtext;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Contacttext;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Reasontext;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCertificates;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox multiSigChkBx;
        private System.Windows.Forms.Panel sigPanel2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.PictureBox sigImgBox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox custSigText;
        private System.Windows.Forms.Label label27;
        private SigLocationUCtrl sigLocationUCtrl1;
    }
}
