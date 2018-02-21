namespace DokuFlex.Common
{
    partial class FolderBrowserView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderBrowserView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnNewFolder = new System.Windows.Forms.Button();
            this.lbLoadingGroups = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.TreeViewImages = new System.Windows.Forms.ImageList(this.components);
            this.cbxGroups = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnNewFolder);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnNewFolder
            // 
            resources.ApplyResources(this.btnNewFolder, "btnNewFolder");
            this.btnNewFolder.Name = "btnNewFolder";
            this.btnNewFolder.UseVisualStyleBackColor = true;
            this.btnNewFolder.Click += new System.EventHandler(this.btnNewFolder_Click);
            // 
            // lbLoadingGroups
            // 
            resources.ApplyResources(this.lbLoadingGroups, "lbLoadingGroups");
            this.lbLoadingGroups.BackColor = System.Drawing.SystemColors.Window;
            this.lbLoadingGroups.Name = "lbLoadingGroups";
            // 
            // treeView
            // 
            resources.ApplyResources(this.treeView, "treeView");
            this.treeView.ImageList = this.TreeViewImages;
            this.treeView.Name = "treeView";
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // TreeViewImages
            // 
            this.TreeViewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeViewImages.ImageStream")));
            this.TreeViewImages.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeViewImages.Images.SetKeyName(0, "Folder16.png");
            this.TreeViewImages.Images.SetKeyName(1, "LoadingFolder16.png");
            // 
            // cbxGroups
            // 
            resources.ApplyResources(this.cbxGroups, "cbxGroups");
            this.cbxGroups.DisplayMember = "name";
            this.cbxGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroups.FormattingEnabled = true;
            this.cbxGroups.Name = "cbxGroups";
            this.cbxGroups.ValueMember = "id";
            this.cbxGroups.SelectedIndexChanged += new System.EventHandler(this.cbxGroups_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FolderBrowserView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLoadingGroups);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.cbxGroups);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FolderBrowserView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FolderBrowserView_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnNewFolder;
        private System.Windows.Forms.Label lbLoadingGroups;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ComboBox cbxGroups;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList TreeViewImages;
    }
}