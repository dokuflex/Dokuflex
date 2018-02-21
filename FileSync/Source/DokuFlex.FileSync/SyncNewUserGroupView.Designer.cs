namespace DokuFlex.FileSync
{
    partial class SyncNewUserGroupView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncNewUserGroupView));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lkStoreLocation = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSyncNow = new System.Windows.Forms.Button();
            this.cbxUserGroup = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(197)))), ((int)(((byte)(62)))));
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Name = "label3";
            // 
            // lkStoreLocation
            // 
            resources.ApplyResources(this.lkStoreLocation, "lkStoreLocation");
            this.lkStoreLocation.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(169)))), ((int)(((byte)(25)))));
            this.lkStoreLocation.Name = "lkStoreLocation";
            this.lkStoreLocation.TabStop = true;
            this.lkStoreLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkStoreLocation_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSyncNow);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSyncNow
            // 
            this.btnSyncNow.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnSyncNow, "btnSyncNow");
            this.btnSyncNow.Name = "btnSyncNow";
            this.btnSyncNow.UseVisualStyleBackColor = true;
            // 
            // cbxUserGroup
            // 
            this.cbxUserGroup.DisplayMember = "name";
            this.cbxUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUserGroup.FormattingEnabled = true;
            resources.ApplyResources(this.cbxUserGroup, "cbxUserGroup");
            this.cbxUserGroup.Name = "cbxUserGroup";
            this.cbxUserGroup.ValueMember = "id";
            this.cbxUserGroup.SelectedIndexChanged += new System.EventHandler(this.cbxUserGroup_SelectedIndexChanged);
            // 
            // SyncNewUserGroupView
            // 
            this.AcceptButton = this.btnSyncNow;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.cbxUserGroup);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lkStoreLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncNewUserGroupView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncNewView_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lkStoreLocation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSyncNow;
        private System.Windows.Forms.ComboBox cbxUserGroup;
    }
}