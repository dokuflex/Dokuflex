namespace DokuSign.Controls
{
    partial class SigBiometric
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SigBiometric));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.sigPlusNET1 = new Topaz.SigPlusNET();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemovePosition = new System.Windows.Forms.Button();
            this.savePositionsLabel = new System.Windows.Forms.Label();
            this.btnSavePosition = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.numberOfPagesUpDown = new System.Windows.Forms.NumericUpDown();
            this.pagePreview1 = new DokuSign.Controls.PagePreview();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPagesUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(18, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 160);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.sigPlusNET1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(393, 124);
            this.panel3.TabIndex = 19;
            // 
            // sigPlusNET1
            // 
            this.sigPlusNET1.BackColor = System.Drawing.Color.White;
            this.sigPlusNET1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sigPlusNET1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sigPlusNET1.ForeColor = System.Drawing.Color.Black;
            this.sigPlusNET1.Location = new System.Drawing.Point(0, 0);
            this.sigPlusNET1.Name = "sigPlusNET1";
            this.sigPlusNET1.Size = new System.Drawing.Size(393, 124);
            this.sigPlusNET1.TabIndex = 20;
            this.sigPlusNET1.Text = "sigPlusNET1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnFinalize);
            this.panel2.Controls.Add(this.btnSign);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(393, 34);
            this.panel2.TabIndex = 18;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(305, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(85, 25);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalize.Location = new System.Drawing.Point(214, 3);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(85, 25);
            this.btnFinalize.TabIndex = 16;
            this.btnFinalize.Text = "Finalizar";
            this.btnFinalize.UseVisualStyleBackColor = true;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Location = new System.Drawing.Point(123, 3);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(85, 25);
            this.btnSign.TabIndex = 15;
            this.btnSign.Text = "Firmar";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 113);
            this.label1.TabIndex = 20;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnRemovePosition
            // 
            this.btnRemovePosition.Location = new System.Drawing.Point(219, 393);
            this.btnRemovePosition.Name = "btnRemovePosition";
            this.btnRemovePosition.Size = new System.Drawing.Size(180, 25);
            this.btnRemovePosition.TabIndex = 64;
            this.btnRemovePosition.Text = "Borrar pocisión";
            this.btnRemovePosition.UseVisualStyleBackColor = true;
            this.btnRemovePosition.Click += new System.EventHandler(this.btnRemovePosition_Click);
            // 
            // savePositionsLabel
            // 
            this.savePositionsLabel.AutoEllipsis = true;
            this.savePositionsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savePositionsLabel.Location = new System.Drawing.Point(219, 351);
            this.savePositionsLabel.Name = "savePositionsLabel";
            this.savePositionsLabel.Size = new System.Drawing.Size(180, 16);
            this.savePositionsLabel.TabIndex = 63;
            this.savePositionsLabel.Text = "Página:";
            this.savePositionsLabel.Visible = false;
            // 
            // btnSavePosition
            // 
            this.btnSavePosition.Location = new System.Drawing.Point(22, 393);
            this.btnSavePosition.Name = "btnSavePosition";
            this.btnSavePosition.Size = new System.Drawing.Size(180, 25);
            this.btnSavePosition.TabIndex = 62;
            this.btnSavePosition.Text = "Guardar pocisión";
            this.btnSavePosition.UseVisualStyleBackColor = true;
            this.btnSavePosition.Click += new System.EventHandler(this.btnSavePosition_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(395, 45);
            this.label2.TabIndex = 67;
            this.label2.Text = "Haga clic sobre la imagen dentro de la página para moverla o redimencionarla.";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(19, 351);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(106, 15);
            this.label28.TabIndex = 66;
            this.label28.Text = "Número de página";
            // 
            // numberOfPagesUpDown
            // 
            this.numberOfPagesUpDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfPagesUpDown.Location = new System.Drawing.Point(131, 346);
            this.numberOfPagesUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numberOfPagesUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfPagesUpDown.Name = "numberOfPagesUpDown";
            this.numberOfPagesUpDown.Size = new System.Drawing.Size(71, 26);
            this.numberOfPagesUpDown.TabIndex = 65;
            this.numberOfPagesUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfPagesUpDown.ValueChanged += new System.EventHandler(this.numberOfPagesUpDown_ValueChanged);
            // 
            // pagePreview1
            // 
            this.pagePreview1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagePreview1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagePreview1.Location = new System.Drawing.Point(436, 3);
            this.pagePreview1.Name = "pagePreview1";
            this.pagePreview1.Size = new System.Drawing.Size(353, 428);
            this.pagePreview1.TabIndex = 68;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SigBiometric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pagePreview1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.numberOfPagesUpDown);
            this.Controls.Add(this.btnRemovePosition);
            this.Controls.Add(this.savePositionsLabel);
            this.Controls.Add(this.btnSavePosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SigBiometric";
            this.Size = new System.Drawing.Size(792, 436);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPagesUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.Button btnSign;
        private Topaz.SigPlusNET sigPlusNET1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemovePosition;
        private System.Windows.Forms.Label savePositionsLabel;
        private System.Windows.Forms.Button btnSavePosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown numberOfPagesUpDown;
        private PagePreview pagePreview1;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
