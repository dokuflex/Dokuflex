namespace DokuSign
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.progresslabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.addSignerButton = new System.Windows.Forms.Button();
            this.sendToSignText = new System.Windows.Forms.TextBox();
            this.sendToSignLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sigBiometric1 = new DokuSign.Controls.SigBiometric();
            this.sigLocal1 = new DokuSign.Controls.SigLocal();
            this.sigOnline1 = new DokuSign.Controls.SigOnline();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progresslabel);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 564);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 37);
            this.panel1.TabIndex = 0;
            // 
            // progresslabel
            // 
            this.progresslabel.AutoSize = true;
            this.progresslabel.Location = new System.Drawing.Point(12, 6);
            this.progresslabel.Name = "progresslabel";
            this.progresslabel.Size = new System.Drawing.Size(38, 15);
            this.progresslabel.TabIndex = 3;
            this.progresslabel.Text = "label3";
            this.progresslabel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Enabled = false;
            this.progressBar.Location = new System.Drawing.Point(15, 21);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(264, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(712, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Aceptar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(793, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.addSignerButton);
            this.panel2.Controls.Add(this.sendToSignText);
            this.panel2.Controls.Add(this.sendToSignLabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnBrowse);
            this.panel2.Controls.Add(this.folderPath);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 489);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(884, 75);
            this.panel2.TabIndex = 3;
            // 
            // addSignerButton
            // 
            this.addSignerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addSignerButton.Location = new System.Drawing.Point(795, 38);
            this.addSignerButton.Name = "addSignerButton";
            this.addSignerButton.Size = new System.Drawing.Size(75, 23);
            this.addSignerButton.TabIndex = 5;
            this.addSignerButton.Text = "+";
            this.addSignerButton.UseVisualStyleBackColor = true;
            this.addSignerButton.Click += new System.EventHandler(this.addSignerButton_Click);
            // 
            // sendToSignText
            // 
            this.sendToSignText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendToSignText.Location = new System.Drawing.Point(114, 39);
            this.sendToSignText.Name = "sendToSignText";
            this.sendToSignText.ReadOnly = true;
            this.sendToSignText.Size = new System.Drawing.Size(675, 23);
            this.sendToSignText.TabIndex = 4;
            // 
            // sendToSignLabel
            // 
            this.sendToSignLabel.AutoSize = true;
            this.sendToSignLabel.Location = new System.Drawing.Point(3, 42);
            this.sendToSignLabel.Name = "sendToSignLabel";
            this.sendToSignLabel.Size = new System.Drawing.Size(100, 15);
            this.sendToSignLabel.TabIndex = 3;
            this.sendToSignLabel.Text = "Enviar para firmar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Carpeta de destino";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(795, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Navegar";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // folderPath
            // 
            this.folderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderPath.Location = new System.Drawing.Point(115, 6);
            this.folderPath.Name = "folderPath";
            this.folderPath.ReadOnly = true;
            this.folderPath.Size = new System.Drawing.Size(674, 23);
            this.folderPath.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sigBiometric1);
            this.panel4.Controls.Add(this.sigLocal1);
            this.panel4.Controls.Add(this.sigOnline1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(884, 489);
            this.panel4.TabIndex = 3;
            // 
            // sigBiometric1
            // 
            this.sigBiometric1.BackColor = System.Drawing.SystemColors.Window;
            this.sigBiometric1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigBiometric1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigBiometric1.Location = new System.Drawing.Point(0, 0);
            this.sigBiometric1.Name = "sigBiometric1";
            this.sigBiometric1.NumberOfPages = 1;
            this.sigBiometric1.Size = new System.Drawing.Size(884, 489);
            this.sigBiometric1.TabIndex = 3;
            // 
            // sigLocal1
            // 
            this.sigLocal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigLocal1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigLocal1.Location = new System.Drawing.Point(0, 0);
            this.sigLocal1.Name = "sigLocal1";
            this.sigLocal1.Size = new System.Drawing.Size(884, 489);
            this.sigLocal1.TabIndex = 2;
            // 
            // sigOnline1
            // 
            this.sigOnline1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigOnline1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigOnline1.Location = new System.Drawing.Point(0, 0);
            this.sigOnline1.Name = "sigOnline1";
            this.sigOnline1.Size = new System.Drawing.Size(884, 489);
            this.sigOnline1.TabIndex = 4;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(884, 601);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doku4Signatures";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private Controls.SigLocal sigLocal1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.Label progresslabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Controls.SigBiometric sigBiometric1;
        private Controls.SigOnline sigOnline1;
        private System.Windows.Forms.Label sendToSignLabel;
        private System.Windows.Forms.TextBox sendToSignText;
        private System.Windows.Forms.Button addSignerButton;
    }
}