namespace DokuFlex.WinForms.Common
{
    partial class OpenFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFileForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.TreeViewImages = new System.Windows.Forms.ImageList(this.components);
            this.lbFolderEmptyInfo = new System.Windows.Forms.Label();
            this.lbLoadingGroups = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListViewImages32 = new System.Windows.Forms.ImageList(this.components);
            this.ListViewImages16 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxView = new System.Windows.Forms.ComboBox();
            this.cbxGroups = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxFileExtensions = new System.Windows.Forms.ComboBox();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lbFolderEmptyInfo);
            this.splitContainer.Panel2.Controls.Add(this.lbLoadingGroups);
            this.splitContainer.Panel2.Controls.Add(this.listView);
            // 
            // treeView
            // 
            resources.ApplyResources(this.treeView, "treeView");
            this.treeView.ImageList = this.TreeViewImages;
            this.treeView.Name = "treeView";
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
            // lbFolderEmptyInfo
            // 
            resources.ApplyResources(this.lbFolderEmptyInfo, "lbFolderEmptyInfo");
            this.lbFolderEmptyInfo.BackColor = System.Drawing.SystemColors.Window;
            this.lbFolderEmptyInfo.Name = "lbFolderEmptyInfo";
            // 
            // lbLoadingGroups
            // 
            resources.ApplyResources(this.lbLoadingGroups, "lbLoadingGroups");
            this.lbLoadingGroups.BackColor = System.Drawing.SystemColors.Window;
            this.lbLoadingGroups.Name = "lbLoadingGroups";
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.SystemColors.Window;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colModified});
            this.listView.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listView, "listView");
            this.listView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView.FullRowSelect = true;
            this.listView.LargeImageList = this.ListViewImages32;
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.ShowItemToolTips = true;
            this.listView.SmallImageList = this.ListViewImages16;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // colTitle
            // 
            resources.ApplyResources(this.colTitle, "colTitle");
            // 
            // colModified
            // 
            resources.ApplyResources(this.colModified, "colModified");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.metadataToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // metadataToolStripMenuItem
            // 
            this.metadataToolStripMenuItem.Name = "metadataToolStripMenuItem";
            resources.ApplyResources(this.metadataToolStripMenuItem, "metadataToolStripMenuItem");
            this.metadataToolStripMenuItem.Click += new System.EventHandler(this.metadataToolStripMenuItem_Click);
            // 
            // ListViewImages32
            // 
            this.ListViewImages32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImages32.ImageStream")));
            this.ListViewImages32.TransparentColor = System.Drawing.Color.Transparent;
            this.ListViewImages32.Images.SetKeyName(0, "file_extension_3gp.png");
            this.ListViewImages32.Images.SetKeyName(1, "file_extension_7z.png");
            this.ListViewImages32.Images.SetKeyName(2, "file_extension_ace.png");
            this.ListViewImages32.Images.SetKeyName(3, "file_extension_ai.png");
            this.ListViewImages32.Images.SetKeyName(4, "file_extension_aif.png");
            this.ListViewImages32.Images.SetKeyName(5, "file_extension_aiff.png");
            this.ListViewImages32.Images.SetKeyName(6, "file_extension_amr.png");
            this.ListViewImages32.Images.SetKeyName(7, "file_extension_asf.png");
            this.ListViewImages32.Images.SetKeyName(8, "file_extension_asx.png");
            this.ListViewImages32.Images.SetKeyName(9, "file_extension_bat.png");
            this.ListViewImages32.Images.SetKeyName(10, "file_extension_bin.png");
            this.ListViewImages32.Images.SetKeyName(11, "file_extension_bmp.png");
            this.ListViewImages32.Images.SetKeyName(12, "file_extension_bup.png");
            this.ListViewImages32.Images.SetKeyName(13, "file_extension_cab.png");
            this.ListViewImages32.Images.SetKeyName(14, "file_extension_cbr.png");
            this.ListViewImages32.Images.SetKeyName(15, "file_extension_cda.png");
            this.ListViewImages32.Images.SetKeyName(16, "file_extension_cdl.png");
            this.ListViewImages32.Images.SetKeyName(17, "file_extension_cdr.png");
            this.ListViewImages32.Images.SetKeyName(18, "file_extension_chm.png");
            this.ListViewImages32.Images.SetKeyName(19, "file_extension_dat.png");
            this.ListViewImages32.Images.SetKeyName(20, "file_extension_divx.png");
            this.ListViewImages32.Images.SetKeyName(21, "file_extension_dll.png");
            this.ListViewImages32.Images.SetKeyName(22, "file_extension_dmg.png");
            this.ListViewImages32.Images.SetKeyName(23, "file_extension_doc.png");
            this.ListViewImages32.Images.SetKeyName(24, "file_extension_dss.png");
            this.ListViewImages32.Images.SetKeyName(25, "file_extension_dvf.png");
            this.ListViewImages32.Images.SetKeyName(26, "file_extension_dwg.png");
            this.ListViewImages32.Images.SetKeyName(27, "file_extension_eml.png");
            this.ListViewImages32.Images.SetKeyName(28, "file_extension_eps.png");
            this.ListViewImages32.Images.SetKeyName(29, "file_extension_exe.png");
            this.ListViewImages32.Images.SetKeyName(30, "file_extension_fla.png");
            this.ListViewImages32.Images.SetKeyName(31, "file_extension_flv.png");
            this.ListViewImages32.Images.SetKeyName(32, "file_extension_gif.png");
            this.ListViewImages32.Images.SetKeyName(33, "file_extension_gz.png");
            this.ListViewImages32.Images.SetKeyName(34, "file_extension_hqx.png");
            this.ListViewImages32.Images.SetKeyName(35, "file_extension_htm.png");
            this.ListViewImages32.Images.SetKeyName(36, "file_extension_html.png");
            this.ListViewImages32.Images.SetKeyName(37, "file_extension_ifo.png");
            this.ListViewImages32.Images.SetKeyName(38, "file_extension_indd.png");
            this.ListViewImages32.Images.SetKeyName(39, "file_extension_iso.png");
            this.ListViewImages32.Images.SetKeyName(40, "file_extension_jar.png");
            this.ListViewImages32.Images.SetKeyName(41, "file_extension_jpeg.png");
            this.ListViewImages32.Images.SetKeyName(42, "file_extension_jpg.png");
            this.ListViewImages32.Images.SetKeyName(43, "file_extension_lnk.png");
            this.ListViewImages32.Images.SetKeyName(44, "file_extension_log.png");
            this.ListViewImages32.Images.SetKeyName(45, "file_extension_m4a.png");
            this.ListViewImages32.Images.SetKeyName(46, "file_extension_m4b.png");
            this.ListViewImages32.Images.SetKeyName(47, "file_extension_m4p.png");
            this.ListViewImages32.Images.SetKeyName(48, "file_extension_m4v.png");
            this.ListViewImages32.Images.SetKeyName(49, "file_extension_mcd.png");
            this.ListViewImages32.Images.SetKeyName(50, "file_extension_mdb.png");
            this.ListViewImages32.Images.SetKeyName(51, "file_extension_mid.png");
            this.ListViewImages32.Images.SetKeyName(52, "file_extension_mov.png");
            this.ListViewImages32.Images.SetKeyName(53, "file_extension_mp2.png");
            this.ListViewImages32.Images.SetKeyName(54, "file_extension_mp4.png");
            this.ListViewImages32.Images.SetKeyName(55, "file_extension_mpeg.png");
            this.ListViewImages32.Images.SetKeyName(56, "file_extension_mpg.png");
            this.ListViewImages32.Images.SetKeyName(57, "file_extension_msi.png");
            this.ListViewImages32.Images.SetKeyName(58, "file_extension_mswmm.png");
            this.ListViewImages32.Images.SetKeyName(59, "file_extension_ogg.png");
            this.ListViewImages32.Images.SetKeyName(60, "file_extension_pdf.png");
            this.ListViewImages32.Images.SetKeyName(61, "file_extension_png.png");
            this.ListViewImages32.Images.SetKeyName(62, "file_extension_pps.png");
            this.ListViewImages32.Images.SetKeyName(63, "file_extension_ps.png");
            this.ListViewImages32.Images.SetKeyName(64, "file_extension_psd.png");
            this.ListViewImages32.Images.SetKeyName(65, "file_extension_pst.png");
            this.ListViewImages32.Images.SetKeyName(66, "file_extension_ptb.png");
            this.ListViewImages32.Images.SetKeyName(67, "file_extension_pub.png");
            this.ListViewImages32.Images.SetKeyName(68, "file_extension_qbb.png");
            this.ListViewImages32.Images.SetKeyName(69, "file_extension_qbw.png");
            this.ListViewImages32.Images.SetKeyName(70, "file_extension_qxd.png");
            this.ListViewImages32.Images.SetKeyName(71, "file_extension_ram.png");
            this.ListViewImages32.Images.SetKeyName(72, "file_extension_rar.png");
            this.ListViewImages32.Images.SetKeyName(73, "file_extension_rm.png");
            this.ListViewImages32.Images.SetKeyName(74, "file_extension_rmvb.png");
            this.ListViewImages32.Images.SetKeyName(75, "file_extension_rtf.png");
            this.ListViewImages32.Images.SetKeyName(76, "file_extension_sea.png");
            this.ListViewImages32.Images.SetKeyName(77, "file_extension_ses.png");
            this.ListViewImages32.Images.SetKeyName(78, "file_extension_sit.png");
            this.ListViewImages32.Images.SetKeyName(79, "file_extension_sitx.png");
            this.ListViewImages32.Images.SetKeyName(80, "file_extension_ss.png");
            this.ListViewImages32.Images.SetKeyName(81, "file_extension_swf.png");
            this.ListViewImages32.Images.SetKeyName(82, "file_extension_tgz.png");
            this.ListViewImages32.Images.SetKeyName(83, "file_extension_thm.png");
            this.ListViewImages32.Images.SetKeyName(84, "file_extension_tif.png");
            this.ListViewImages32.Images.SetKeyName(85, "file_extension_tmp.png");
            this.ListViewImages32.Images.SetKeyName(86, "file_extension_torrent.png");
            this.ListViewImages32.Images.SetKeyName(87, "file_extension_ttf.png");
            this.ListViewImages32.Images.SetKeyName(88, "file_extension_txt.png");
            this.ListViewImages32.Images.SetKeyName(89, "file_extension_vcd.png");
            this.ListViewImages32.Images.SetKeyName(90, "file_extension_vob.png");
            this.ListViewImages32.Images.SetKeyName(91, "file_extension_wav.png");
            this.ListViewImages32.Images.SetKeyName(92, "file_extension_wma.png");
            this.ListViewImages32.Images.SetKeyName(93, "file_extension_wmv.png");
            this.ListViewImages32.Images.SetKeyName(94, "file_extension_wps.png");
            this.ListViewImages32.Images.SetKeyName(95, "file_extension_xls.png");
            this.ListViewImages32.Images.SetKeyName(96, "file_extension_xpi.png");
            this.ListViewImages32.Images.SetKeyName(97, "file_extension_zip.png");
            // 
            // ListViewImages16
            // 
            this.ListViewImages16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImages16.ImageStream")));
            this.ListViewImages16.TransparentColor = System.Drawing.Color.Transparent;
            this.ListViewImages16.Images.SetKeyName(0, "file_extension_3gp.png");
            this.ListViewImages16.Images.SetKeyName(1, "file_extension_7z.png");
            this.ListViewImages16.Images.SetKeyName(2, "file_extension_ace.png");
            this.ListViewImages16.Images.SetKeyName(3, "file_extension_ai.png");
            this.ListViewImages16.Images.SetKeyName(4, "file_extension_aif.png");
            this.ListViewImages16.Images.SetKeyName(5, "file_extension_aiff.png");
            this.ListViewImages16.Images.SetKeyName(6, "file_extension_amr.png");
            this.ListViewImages16.Images.SetKeyName(7, "file_extension_asf.png");
            this.ListViewImages16.Images.SetKeyName(8, "file_extension_asx.png");
            this.ListViewImages16.Images.SetKeyName(9, "file_extension_bat.png");
            this.ListViewImages16.Images.SetKeyName(10, "file_extension_bin.png");
            this.ListViewImages16.Images.SetKeyName(11, "file_extension_bmp.png");
            this.ListViewImages16.Images.SetKeyName(12, "file_extension_bup.png");
            this.ListViewImages16.Images.SetKeyName(13, "file_extension_cab.png");
            this.ListViewImages16.Images.SetKeyName(14, "file_extension_cbr.png");
            this.ListViewImages16.Images.SetKeyName(15, "file_extension_cda.png");
            this.ListViewImages16.Images.SetKeyName(16, "file_extension_cdl.png");
            this.ListViewImages16.Images.SetKeyName(17, "file_extension_cdr.png");
            this.ListViewImages16.Images.SetKeyName(18, "file_extension_chm.png");
            this.ListViewImages16.Images.SetKeyName(19, "file_extension_dat.png");
            this.ListViewImages16.Images.SetKeyName(20, "file_extension_divx.png");
            this.ListViewImages16.Images.SetKeyName(21, "file_extension_dll.png");
            this.ListViewImages16.Images.SetKeyName(22, "file_extension_dmg.png");
            this.ListViewImages16.Images.SetKeyName(23, "file_extension_doc.png");
            this.ListViewImages16.Images.SetKeyName(24, "file_extension_dss.png");
            this.ListViewImages16.Images.SetKeyName(25, "file_extension_dvf.png");
            this.ListViewImages16.Images.SetKeyName(26, "file_extension_dwg.png");
            this.ListViewImages16.Images.SetKeyName(27, "file_extension_eml.png");
            this.ListViewImages16.Images.SetKeyName(28, "file_extension_eps.png");
            this.ListViewImages16.Images.SetKeyName(29, "file_extension_exe.png");
            this.ListViewImages16.Images.SetKeyName(30, "file_extension_fla.png");
            this.ListViewImages16.Images.SetKeyName(31, "file_extension_flv.png");
            this.ListViewImages16.Images.SetKeyName(32, "file_extension_gif.png");
            this.ListViewImages16.Images.SetKeyName(33, "file_extension_gz.png");
            this.ListViewImages16.Images.SetKeyName(34, "file_extension_hqx.png");
            this.ListViewImages16.Images.SetKeyName(35, "file_extension_htm.png");
            this.ListViewImages16.Images.SetKeyName(36, "file_extension_html.png");
            this.ListViewImages16.Images.SetKeyName(37, "file_extension_ifo.png");
            this.ListViewImages16.Images.SetKeyName(38, "file_extension_indd.png");
            this.ListViewImages16.Images.SetKeyName(39, "file_extension_iso.png");
            this.ListViewImages16.Images.SetKeyName(40, "file_extension_jar.png");
            this.ListViewImages16.Images.SetKeyName(41, "file_extension_jpeg.png");
            this.ListViewImages16.Images.SetKeyName(42, "file_extension_jpg.png");
            this.ListViewImages16.Images.SetKeyName(43, "file_extension_lnk.png");
            this.ListViewImages16.Images.SetKeyName(44, "file_extension_log.png");
            this.ListViewImages16.Images.SetKeyName(45, "file_extension_m4a.png");
            this.ListViewImages16.Images.SetKeyName(46, "file_extension_m4b.png");
            this.ListViewImages16.Images.SetKeyName(47, "file_extension_m4p.png");
            this.ListViewImages16.Images.SetKeyName(48, "file_extension_m4v.png");
            this.ListViewImages16.Images.SetKeyName(49, "file_extension_mcd.png");
            this.ListViewImages16.Images.SetKeyName(50, "file_extension_mdb.png");
            this.ListViewImages16.Images.SetKeyName(51, "file_extension_mid.png");
            this.ListViewImages16.Images.SetKeyName(52, "file_extension_mov.png");
            this.ListViewImages16.Images.SetKeyName(53, "file_extension_mp2.png");
            this.ListViewImages16.Images.SetKeyName(54, "file_extension_mp4.png");
            this.ListViewImages16.Images.SetKeyName(55, "file_extension_mpeg.png");
            this.ListViewImages16.Images.SetKeyName(56, "file_extension_mpg.png");
            this.ListViewImages16.Images.SetKeyName(57, "file_extension_msi.png");
            this.ListViewImages16.Images.SetKeyName(58, "file_extension_mswmm.png");
            this.ListViewImages16.Images.SetKeyName(59, "file_extension_ogg.png");
            this.ListViewImages16.Images.SetKeyName(60, "file_extension_pdf.png");
            this.ListViewImages16.Images.SetKeyName(61, "file_extension_png.png");
            this.ListViewImages16.Images.SetKeyName(62, "file_extension_pps.png");
            this.ListViewImages16.Images.SetKeyName(63, "file_extension_ps.png");
            this.ListViewImages16.Images.SetKeyName(64, "file_extension_psd.png");
            this.ListViewImages16.Images.SetKeyName(65, "file_extension_pst.png");
            this.ListViewImages16.Images.SetKeyName(66, "file_extension_ptb.png");
            this.ListViewImages16.Images.SetKeyName(67, "file_extension_pub.png");
            this.ListViewImages16.Images.SetKeyName(68, "file_extension_qbb.png");
            this.ListViewImages16.Images.SetKeyName(69, "file_extension_qbw.png");
            this.ListViewImages16.Images.SetKeyName(70, "file_extension_qxd.png");
            this.ListViewImages16.Images.SetKeyName(71, "file_extension_ram.png");
            this.ListViewImages16.Images.SetKeyName(72, "file_extension_rar.png");
            this.ListViewImages16.Images.SetKeyName(73, "file_extension_rm.png");
            this.ListViewImages16.Images.SetKeyName(74, "file_extension_rmvb.png");
            this.ListViewImages16.Images.SetKeyName(75, "file_extension_rtf.png");
            this.ListViewImages16.Images.SetKeyName(76, "file_extension_sea.png");
            this.ListViewImages16.Images.SetKeyName(77, "file_extension_ses.png");
            this.ListViewImages16.Images.SetKeyName(78, "file_extension_sit.png");
            this.ListViewImages16.Images.SetKeyName(79, "file_extension_sitx.png");
            this.ListViewImages16.Images.SetKeyName(80, "file_extension_ss.png");
            this.ListViewImages16.Images.SetKeyName(81, "file_extension_swf.png");
            this.ListViewImages16.Images.SetKeyName(82, "file_extension_tgz.png");
            this.ListViewImages16.Images.SetKeyName(83, "file_extension_thm.png");
            this.ListViewImages16.Images.SetKeyName(84, "file_extension_tif.png");
            this.ListViewImages16.Images.SetKeyName(85, "file_extension_tmp.png");
            this.ListViewImages16.Images.SetKeyName(86, "file_extension_torrent.png");
            this.ListViewImages16.Images.SetKeyName(87, "file_extension_ttf.png");
            this.ListViewImages16.Images.SetKeyName(88, "file_extension_txt.png");
            this.ListViewImages16.Images.SetKeyName(89, "file_extension_vcd.png");
            this.ListViewImages16.Images.SetKeyName(90, "file_extension_vob.png");
            this.ListViewImages16.Images.SetKeyName(91, "file_extension_wav.png");
            this.ListViewImages16.Images.SetKeyName(92, "file_extension_wma.png");
            this.ListViewImages16.Images.SetKeyName(93, "file_extension_wmv.png");
            this.ListViewImages16.Images.SetKeyName(94, "file_extension_wps.png");
            this.ListViewImages16.Images.SetKeyName(95, "file_extension_xls.png");
            this.ListViewImages16.Images.SetKeyName(96, "file_extension_xpi.png");
            this.ListViewImages16.Images.SetKeyName(97, "file_extension_zip.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbxView);
            this.panel1.Controls.Add(this.cbxGroups);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbxView
            // 
            resources.ApplyResources(this.cbxView, "cbxView");
            this.cbxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxView.FormattingEnabled = true;
            this.cbxView.Items.AddRange(new object[] {
            resources.GetString("cbxView.Items"),
            resources.GetString("cbxView.Items1"),
            resources.GetString("cbxView.Items2"),
            resources.GetString("cbxView.Items3")});
            this.cbxView.Name = "cbxView";
            this.cbxView.SelectedValueChanged += new System.EventHandler(this.cbxView_SelectedValueChanged);
            // 
            // cbxGroups
            // 
            resources.ApplyResources(this.cbxGroups, "cbxGroups");
            this.cbxGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGroups.DisplayMember = "name";
            this.cbxGroups.FormattingEnabled = true;
            this.cbxGroups.Name = "cbxGroups";
            this.cbxGroups.ValueMember = "id";
            this.cbxGroups.SelectedValueChanged += new System.EventHandler(this.cbxGroups_SelectedValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxFileExtensions);
            this.panel2.Controls.Add(this.textFileName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOpen);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // cbxFileExtensions
            // 
            resources.ApplyResources(this.cbxFileExtensions, "cbxFileExtensions");
            this.cbxFileExtensions.DisplayMember = "DisplayName";
            this.cbxFileExtensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFileExtensions.FormattingEnabled = true;
            this.cbxFileExtensions.Name = "cbxFileExtensions";
            this.cbxFileExtensions.ValueMember = "Extension";
            this.cbxFileExtensions.SelectedIndexChanged += new System.EventHandler(this.cbxFileExtensions_SelectedIndexChanged);
            // 
            // textFileName
            // 
            resources.ApplyResources(this.textFileName, "textFileName");
            this.textFileName.Name = "textFileName";
            this.textFileName.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // OpenFileForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenFileForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenFileView_FormClosing);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxGroups;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbxFileExtensions;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Label lbLoadingGroups;
        private System.Windows.Forms.ImageList TreeViewImages;
        private System.Windows.Forms.ImageList ListViewImages32;
        private System.Windows.Forms.ImageList ListViewImages16;
        private System.Windows.Forms.Label lbFolderEmptyInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colModified;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metadataToolStripMenuItem;
    }
}