namespace FileImporter
{
    partial class AdvancedForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDelimiters = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colDokuField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceField = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colIsFilePath = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnAccept = new System.Windows.Forms.Button();
            this.chkFirstRowIsHeader = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delimitador de campo:";
            // 
            // cbxDelimiters
            // 
            this.cbxDelimiters.FormattingEnabled = true;
            this.cbxDelimiters.Items.AddRange(new object[] {
            "Tabulador",
            "Punto y Coma",
            "Coma",
            "Espacio"});
            this.cbxDelimiters.Location = new System.Drawing.Point(127, 15);
            this.cbxDelimiters.Name = "cbxDelimiters";
            this.cbxDelimiters.Size = new System.Drawing.Size(112, 21);
            this.cbxDelimiters.TabIndex = 1;
            this.cbxDelimiters.SelectedIndexChanged += new System.EventHandler(this.cbxDelimiters_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDokuField,
            this.colSourceField,
            this.colIsFilePath});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 117);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(405, 192);
            this.dataGridView1.TabIndex = 5;
            // 
            // colDokuField
            // 
            this.colDokuField.DataPropertyName = "DokuField";
            this.colDokuField.HeaderText = "DokuField";
            this.colDokuField.Name = "colDokuField";
            this.colDokuField.Width = 130;
            // 
            // colSourceField
            // 
            this.colSourceField.DataPropertyName = "SourceField";
            this.colSourceField.HeaderText = "Campo";
            this.colSourceField.Name = "colSourceField";
            this.colSourceField.Width = 140;
            // 
            // colIsFilePath
            // 
            this.colIsFilePath.DataPropertyName = "IsFilePath";
            this.colIsFilePath.HeaderText = "Es la ruta";
            this.colIsFilePath.Name = "colIsFilePath";
            this.colIsFilePath.ReadOnly = true;
            this.colIsFilePath.Width = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Información del campo";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.BtnAccept);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 319);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 40);
            this.panel1.TabIndex = 7;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(332, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(85, 25);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancelar";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnAccept
            // 
            this.BtnAccept.Location = new System.Drawing.Point(241, 3);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(85, 25);
            this.BtnAccept.TabIndex = 0;
            this.BtnAccept.Text = "Aceptar";
            this.BtnAccept.UseVisualStyleBackColor = true;
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // chkFirstRowIsHeader
            // 
            this.chkFirstRowIsHeader.AutoSize = true;
            this.chkFirstRowIsHeader.Checked = true;
            this.chkFirstRowIsHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirstRowIsHeader.Enabled = false;
            this.chkFirstRowIsHeader.Location = new System.Drawing.Point(12, 62);
            this.chkFirstRowIsHeader.Name = "chkFirstRowIsHeader";
            this.chkFirstRowIsHeader.Size = new System.Drawing.Size(235, 17);
            this.chkFirstRowIsHeader.TabIndex = 8;
            this.chkFirstRowIsHeader.Text = "Primera fila contiene nombres de los campos";
            this.chkFirstRowIsHeader.UseVisualStyleBackColor = true;
            // 
            // AdvancedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 359);
            this.Controls.Add(this.chkFirstRowIsHeader);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbxDelimiters);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de archivo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDelimiters;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnAccept;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox chkFirstRowIsHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDokuField;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSourceField;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsFilePath;
    }
}