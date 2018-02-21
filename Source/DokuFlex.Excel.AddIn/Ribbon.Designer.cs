namespace DokuFlex.Excel.AddIn
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
            this.tabDokuFlex = this.Factory.CreateRibbonTab();
            this.groupWorksheet = this.Factory.CreateRibbonGroup();
            this.btnOpen = this.Factory.CreateRibbonButton();
            this.btnSave = this.Factory.CreateRibbonButton();
            this.groupData = this.Factory.CreateRibbonGroup();
            this.btnMetadata = this.Factory.CreateRibbonButton();
            this.groupMosUsed = this.Factory.CreateRibbonGroup();
            this.btnRecents = this.Factory.CreateRibbonButton();
            this.sbtnFavorites = this.Factory.CreateRibbonSplitButton();
            this.btnAddToFavorite = this.Factory.CreateRibbonButton();
            this.groupSearch = this.Factory.CreateRibbonGroup();
            this.btnFind = this.Factory.CreateRibbonButton();
            this.groupTools = this.Factory.CreateRibbonGroup();
            this.btnSettings = this.Factory.CreateRibbonButton();
            this.btnChangeCredentials = this.Factory.CreateRibbonButton();
            this.tabDokuFlex.SuspendLayout();
            this.groupWorksheet.SuspendLayout();
            this.groupData.SuspendLayout();
            this.groupMosUsed.SuspendLayout();
            this.groupSearch.SuspendLayout();
            this.groupTools.SuspendLayout();
            // 
            // tabDokuFlex
            // 
            this.tabDokuFlex.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabDokuFlex.Groups.Add(this.groupWorksheet);
            this.tabDokuFlex.Groups.Add(this.groupData);
            this.tabDokuFlex.Groups.Add(this.groupMosUsed);
            this.tabDokuFlex.Groups.Add(this.groupSearch);
            this.tabDokuFlex.Groups.Add(this.groupTools);
            this.tabDokuFlex.Label = "DokuFlex";
            this.tabDokuFlex.Name = "tabDokuFlex";
            // 
            // groupWorksheet
            // 
            this.groupWorksheet.Items.Add(this.btnOpen);
            this.groupWorksheet.Items.Add(this.btnSave);
            this.groupWorksheet.Label = "Libro de cálculo";
            this.groupWorksheet.Name = "groupWorksheet";
            // 
            // btnOpen
            // 
            this.btnOpen.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnOpen.Label = "Abrir";
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.ShowImage = true;
            this.btnOpen.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSave.Label = "Guardar";
            this.btnSave.Name = "btnSave";
            this.btnSave.ShowImage = true;
            this.btnSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSave_Click);
            // 
            // groupData
            // 
            this.groupData.Items.Add(this.btnMetadata);
            this.groupData.Label = "Datos";
            this.groupData.Name = "groupData";
            // 
            // btnMetadata
            // 
            this.btnMetadata.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnMetadata.Label = "Metadatos";
            this.btnMetadata.Name = "btnMetadata";
            this.btnMetadata.ShowImage = true;
            this.btnMetadata.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnMetadata_Click);
            // 
            // groupMosUsed
            // 
            this.groupMosUsed.Items.Add(this.btnRecents);
            this.groupMosUsed.Items.Add(this.sbtnFavorites);
            this.groupMosUsed.Label = "Más usados";
            this.groupMosUsed.Name = "groupMosUsed";
            // 
            // btnRecents
            // 
            this.btnRecents.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnRecents.Label = "Recientes";
            this.btnRecents.Name = "btnRecents";
            this.btnRecents.ShowImage = true;
            this.btnRecents.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRecents_Click);
            // 
            // sbtnFavorites
            // 
            this.sbtnFavorites.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.sbtnFavorites.Items.Add(this.btnAddToFavorite);
            this.sbtnFavorites.Label = "Favoritos";
            this.sbtnFavorites.Name = "sbtnFavorites";
            this.sbtnFavorites.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.sbtnFavorites_Click);
            // 
            // btnAddToFavorite
            // 
            this.btnAddToFavorite.Label = "Añadir a favoritos";
            this.btnAddToFavorite.Name = "btnAddToFavorite";
            this.btnAddToFavorite.ShowImage = true;
            this.btnAddToFavorite.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddToFavorite_Click);
            // 
            // groupSearch
            // 
            this.groupSearch.Items.Add(this.btnFind);
            this.groupSearch.Label = "Búsqueda";
            this.groupSearch.Name = "groupSearch";
            // 
            // btnFind
            // 
            this.btnFind.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnFind.Label = "Buscar";
            this.btnFind.Name = "btnFind";
            this.btnFind.ShowImage = true;
            this.btnFind.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFind_Click);
            // 
            // groupTools
            // 
            this.groupTools.Items.Add(this.btnSettings);
            this.groupTools.Label = "Herramientas";
            this.groupTools.Name = "groupTools";
            // 
            // btnSettings
            // 
            this.btnSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSettings.Label = "Configuración...";
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.ShowImage = true;
            this.btnSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSettings_Click);
            // 
            // btnChangeCredentials
            // 
            this.btnChangeCredentials.Label = "Cambiar credenciales";
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
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabDokuFlex);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tabDokuFlex.ResumeLayout(false);
            this.tabDokuFlex.PerformLayout();
            this.groupWorksheet.ResumeLayout(false);
            this.groupWorksheet.PerformLayout();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            this.groupMosUsed.ResumeLayout(false);
            this.groupMosUsed.PerformLayout();
            this.groupSearch.ResumeLayout(false);
            this.groupSearch.PerformLayout();
            this.groupTools.ResumeLayout(false);
            this.groupTools.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabDokuFlex;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupWorksheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnOpen;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupMosUsed;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton sbtnFavorites;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddToFavorite;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRecents;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupData;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMetadata;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChangeCredentials;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFind;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
