namespace DokuSign.Controls
{
    partial class SigLocal
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
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.certificateList = new System.Windows.Forms.ComboBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MultiSignature = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(93, 167);
            this.passwordBox.MaxLength = 30;
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(200, 23);
            this.passwordBox.TabIndex = 6;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // certificateList
            // 
            this.certificateList.DataSource = this.bindingSource;
            this.certificateList.DisplayMember = "DisplayName";
            this.certificateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.certificateList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.certificateList.FormattingEnabled = true;
            this.certificateList.Location = new System.Drawing.Point(93, 115);
            this.certificateList.Name = "certificateList";
            this.certificateList.Size = new System.Drawing.Size(640, 23);
            this.certificateList.TabIndex = 3;
            this.certificateList.ValueMember = "Certificate";
            this.certificateList.SelectedIndexChanged += new System.EventHandler(this.certificateList_SelectedIndexChanged);
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(DokuSign.CertificateInfo);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Certificado";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(718, 55);
            this.label3.TabIndex = 7;
            this.label3.Text = "Para comenzar eliga un certificado del almacén de certificados local de su PC, es" +
    "criba la contraseña asociada al certificado si se requiere.";
            // 
            // MultiSignature
            // 
            this.MultiSignature.AutoSize = true;
            this.MultiSignature.Location = new System.Drawing.Point(93, 221);
            this.MultiSignature.Name = "MultiSignature";
            this.MultiSignature.Size = new System.Drawing.Size(455, 19);
            this.MultiSignature.TabIndex = 8;
            this.MultiSignature.Text = "Multi-firma (Marque este casilla si el documento debe permitir más de una firma)";
            this.MultiSignature.UseVisualStyleBackColor = true;
            // 
            // SigLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MultiSignature);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.certificateList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SigLocal";
            this.Size = new System.Drawing.Size(750, 350);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.ComboBox certificateList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox MultiSignature;
        private System.Windows.Forms.BindingSource bindingSource;

    }
}
