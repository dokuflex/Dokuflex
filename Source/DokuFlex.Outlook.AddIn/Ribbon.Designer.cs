namespace DokuFlex.Outlook.AddIn
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupMail = this.Factory.CreateRibbonGroup();
            this.btnEMailAdd = this.Factory.CreateRibbonButton();
            this.btnMailAttachAdd = this.Factory.CreateRibbonButton();
            this.groupTasks = this.Factory.CreateRibbonGroup();
            this.btnTaskAdd = this.Factory.CreateRibbonButton();
            this.groupTools = this.Factory.CreateRibbonGroup();
            this.btnSettings = this.Factory.CreateRibbonButton();
            this.btnChangeCredentials = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.groupMail.SuspendLayout();
            this.groupTasks.SuspendLayout();
            this.groupTools.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupMail);
            this.tab1.Groups.Add(this.groupTasks);
            this.tab1.Groups.Add(this.groupTools);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // groupMail
            // 
            this.groupMail.Items.Add(this.btnEMailAdd);
            this.groupMail.Items.Add(this.btnMailAttachAdd);
            resources.ApplyResources(this.groupMail, "groupMail");
            this.groupMail.Name = "groupMail";
            // 
            // btnEMailAdd
            // 
            this.btnEMailAdd.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.btnEMailAdd, "btnEMailAdd");
            this.btnEMailAdd.Name = "btnEMailAdd";
            this.btnEMailAdd.ShowImage = true;
            this.btnEMailAdd.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnEMailAdd_Click);
            // 
            // btnMailAttachAdd
            // 
            this.btnMailAttachAdd.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.btnMailAttachAdd, "btnMailAttachAdd");
            this.btnMailAttachAdd.Name = "btnMailAttachAdd";
            this.btnMailAttachAdd.ShowImage = true;
            this.btnMailAttachAdd.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnMailAttachAdd_Click);
            // 
            // groupTasks
            // 
            this.groupTasks.Items.Add(this.btnTaskAdd);
            resources.ApplyResources(this.groupTasks, "groupTasks");
            this.groupTasks.Name = "groupTasks";
            // 
            // btnTaskAdd
            // 
            this.btnTaskAdd.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.btnTaskAdd, "btnTaskAdd");
            this.btnTaskAdd.Name = "btnTaskAdd";
            this.btnTaskAdd.ShowImage = true;
            this.btnTaskAdd.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnTaskAdd_Click);
            // 
            // groupTools
            // 
            this.groupTools.Items.Add(this.btnSettings);
            resources.ApplyResources(this.groupTools, "groupTools");
            this.groupTools.Name = "groupTools";
            // 
            // btnSettings
            // 
            this.btnSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.ShowImage = true;
            this.btnSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSettings_Click);
            // 
            // btnChangeCredentials
            // 
            resources.ApplyResources(this.btnChangeCredentials, "btnChangeCredentials");
            this.btnChangeCredentials.Name = "btnChangeCredentials";
            this.btnChangeCredentials.ShowImage = true;
            this.btnChangeCredentials.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnChangeCredentials_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            // 
            // Ribbon.OfficeMenu
            // 
            this.OfficeMenu.Items.Add(this.btnChangeCredentials);
            this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            resources.ApplyResources(this, "$this");
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupMail.ResumeLayout(false);
            this.groupMail.PerformLayout();
            this.groupTasks.ResumeLayout(false);
            this.groupTasks.PerformLayout();
            this.groupTools.ResumeLayout(false);
            this.groupTools.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupMail;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMailAttachAdd;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupTasks;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTaskAdd;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChangeCredentials;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnEMailAdd;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
