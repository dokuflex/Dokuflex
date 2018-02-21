namespace DokuFlex.Word.AddIn
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
            this.groupDocument = this.Factory.CreateRibbonGroup();
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
            this.btnChangeCrendetials = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnSendToSign = this.Factory.CreateRibbonButton();
            this.tabDokuFlex.SuspendLayout();
            this.groupDocument.SuspendLayout();
            this.groupData.SuspendLayout();
            this.groupMosUsed.SuspendLayout();
            this.groupSearch.SuspendLayout();
            this.groupTools.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabDokuFlex
            // 
            this.tabDokuFlex.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabDokuFlex.Groups.Add(this.groupDocument);
            this.tabDokuFlex.Groups.Add(this.groupData);
            this.tabDokuFlex.Groups.Add(this.groupMosUsed);
            this.tabDokuFlex.Groups.Add(this.groupSearch);
            this.tabDokuFlex.Groups.Add(this.groupTools);
            this.tabDokuFlex.Groups.Add(this.group1);
            this.tabDokuFlex.Label = "DokuFlex";
            this.tabDokuFlex.Name = "tabDokuFlex";
            // 
            // groupDocument
            // 
            this.groupDocument.Items.Add(this.btnOpen);
            this.groupDocument.Items.Add(this.btnSave);
            this.groupDocument.Label = "Documento";
            this.groupDocument.Name = "groupDocument";
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
            // btnChangeCrendetials
            // 
            this.btnChangeCrendetials.Label = "Cambiar credenciales";
            this.btnChangeCrendetials.Name = "btnChangeCrendetials";
            this.btnChangeCrendetials.ShowImage = true;
            this.btnChangeCrendetials.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnChangeCrendetials_Click);
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnSendToSign);
            this.group1.Label = "eSignature";
            this.group1.Name = "group1";
            // 
            // btnSendToSign
            // 
            this.btnSendToSign.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSendToSign.Label = "Enviar para firmar";
            this.btnSendToSign.Name = "btnSendToSign";
            this.btnSendToSign.ShowImage = true;
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            // 
            // Ribbon.OfficeMenu
            // 
            this.OfficeMenu.Items.Add(this.btnChangeCrendetials);
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tabDokuFlex);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tabDokuFlex.ResumeLayout(false);
            this.tabDokuFlex.PerformLayout();
            this.groupDocument.ResumeLayout(false);
            this.groupDocument.PerformLayout();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            this.groupMosUsed.ResumeLayout(false);
            this.groupMosUsed.PerformLayout();
            this.groupSearch.ResumeLayout(false);
            this.groupSearch.PerformLayout();
            this.groupTools.ResumeLayout(false);
            this.groupTools.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabDokuFlex;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupDocument;
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
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChangeCrendetials;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFind;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSendToSign;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
