namespace DokuSign
{
    partial class SigTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SigTypeForm));
            this.rbtnSigLocal = new System.Windows.Forms.RadioButton();
            this.rbtnSigOnLine = new System.Windows.Forms.RadioButton();
            this.rbtnSigBiometric = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbtnSigLocal
            // 
            this.rbtnSigLocal.AutoSize = true;
            this.rbtnSigLocal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSigLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.rbtnSigLocal.Location = new System.Drawing.Point(38, 46);
            this.rbtnSigLocal.Name = "rbtnSigLocal";
            this.rbtnSigLocal.Size = new System.Drawing.Size(104, 25);
            this.rbtnSigLocal.TabIndex = 0;
            this.rbtnSigLocal.TabStop = true;
            this.rbtnSigLocal.Tag = "0";
            this.rbtnSigLocal.Text = "Firma local";
            this.rbtnSigLocal.UseVisualStyleBackColor = true;
            // 
            // rbtnSigOnLine
            // 
            this.rbtnSigOnLine.AutoSize = true;
            this.rbtnSigOnLine.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSigOnLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.rbtnSigOnLine.Location = new System.Drawing.Point(38, 152);
            this.rbtnSigOnLine.Name = "rbtnSigOnLine";
            this.rbtnSigOnLine.Size = new System.Drawing.Size(126, 25);
            this.rbtnSigOnLine.TabIndex = 1;
            this.rbtnSigOnLine.TabStop = true;
            this.rbtnSigOnLine.Tag = "1";
            this.rbtnSigOnLine.Text = "Firma en línea";
            this.rbtnSigOnLine.UseVisualStyleBackColor = true;
            // 
            // rbtnSigBiometric
            // 
            this.rbtnSigBiometric.AutoSize = true;
            this.rbtnSigBiometric.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSigBiometric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.rbtnSigBiometric.Location = new System.Drawing.Point(38, 237);
            this.rbtnSigBiometric.Name = "rbtnSigBiometric";
            this.rbtnSigBiometric.Size = new System.Drawing.Size(146, 25);
            this.rbtnSigBiometric.TabIndex = 2;
            this.rbtnSigBiometric.TabStop = true;
            this.rbtnSigBiometric.Tag = "2";
            this.rbtnSigBiometric.Text = "Firma biométrica";
            this.rbtnSigBiometric.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 67);
            this.label1.TabIndex = 3;
            this.label1.Text = "Use un certificado del almacén de certificados de Windows para firmar los documen" +
    "tos.";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 53);
            this.label2.TabIndex = 4;
            this.label2.Text = "Use un certificado de su cuenta de DokuFlex para firmar los documentos. ";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(54, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(447, 53);
            this.label3.TabIndex = 5;
            this.label3.Text = "Use un dispositivo de firma biométrica para firmar los documentos.";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(442, 335);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(85, 25);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(503, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(24, 24);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Seleccione el modo de firma:";
            // 
            // SigTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(549, 371);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtnSigBiometric);
            this.Controls.Add(this.rbtnSigOnLine);
            this.Controls.Add(this.rbtnSigLocal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SigTypeForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modo de firma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnSigLocal;
        private System.Windows.Forms.RadioButton rbtnSigOnLine;
        private System.Windows.Forms.RadioButton rbtnSigBiometric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label label4;
    }
}