namespace DokuFlex.WinForms.Common
{
    partial class MetadataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetadataForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxDocumentaryTypes = new System.Windows.Forms.ComboBox();
            this.metadataControl = new DokuFlex.WinForms.Common.Controls.MetadataControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.cbxDocumentaryTypes);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxDocumentaryTypes
            // 
            resources.ApplyResources(this.cbxDocumentaryTypes, "cbxDocumentaryTypes");
            this.cbxDocumentaryTypes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxDocumentaryTypes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxDocumentaryTypes.DisplayMember = "name";
            this.cbxDocumentaryTypes.FormattingEnabled = true;
            this.cbxDocumentaryTypes.Name = "cbxDocumentaryTypes";
            this.cbxDocumentaryTypes.ValueMember = "id";
            this.cbxDocumentaryTypes.SelectedIndexChanged += new System.EventHandler(this.cbxDocumentaryTypes_SelectedIndexChanged);
            // 
            // metadataControl
            // 
            resources.ApplyResources(this.metadataControl, "metadataControl");
            this.metadataControl.Name = "metadataControl";
            // 
            // MetadataForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metadataControl);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MetadataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.MetadataControl metadataControl;
        private System.Windows.Forms.ComboBox cbxDocumentaryTypes;
        private System.Windows.Forms.Button btnSave;
    }
}