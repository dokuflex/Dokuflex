// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Scan
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using DokuFlex.Scan.Data;
    using DokuFlex.Scan.Forms;
    using DokuFlex.WinForms.Common;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.WinForms.Common.Resources;
    using System.Dynamic;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Log;

    public partial class MainForm : Form
    {
        private ListViewItem _currentListViewItem;
        private List<Documentary> _documentaryTypes;
        private bool _bindingSuspended;

        private void SetImage(Image image)
        {
            this.pictureBox.Image = null;
            this.pictureBox.Image = image;
        }

        private void AddToListView(ScannedImage item)
        {
            var listViewItem = listView.Items.Add(item.DateScanned.ToString("G"));
            listViewItem.SubItems.Add(item.Name);
            listViewItem.SubItems.Add(item.Pages.ToString());
            listViewItem.SubItems.Add(item.FileType);
            listViewItem.SubItems.Add(item.Routing.DocumentaryName);
            listViewItem.SubItems.Add(item.Routing.CertificateName);
            listViewItem.SubItems.Add(item.Routing.FolderPath);
            listViewItem.Tag = item;
        }

        private void RefreshListView()
        {
            foreach (ListViewItem item in listView.Items)
            {
                var scannedItem = item.Tag as ScannedImage;

                item.Text = scannedItem.DateScanned.ToString("G");
                item.SubItems[1].Text = scannedItem.Name;
                item.SubItems[2].Text = scannedItem.Pages.ToString();
                item.SubItems[3].Text = scannedItem.FileType;
                item.SubItems[4].Text = scannedItem.Routing.DocumentaryName;
                item.SubItems[5].Text = scannedItem.Routing.CertificateName;
                item.SubItems[6].Text = scannedItem.Routing.FolderPath;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshControlsState();

            scanToolStripButton.Image = ImageResources.scanner;
            deleteToolStripButton.Image = ImageResources.Delete;
            openToolStripButton.Image = ImageResources.Picture;
            scanHistoryToolStripButton.Image = ImageResources.History;
            previewPaneToolStripButton.Image = ImageResources.WindowSplit;

            this.WindowState = FormWindowState.Maximized;
        }

        public MainForm()
        {
            InitializeComponent();

            //#region Test Data
            //var si = new ScannedImage() { Image = Image.FromFile("C:\\Users\\Emilio\\Pictures\\1367659556_1318544825_big.jpg"), FileType = ".tiff" };
            ////var ssm = new ScanRoutingManager();
            //var r = new Routing()
            //{
            //    Documentary = "058670ba-656c-4c69-b710-11c0c089f38c",
            //    DocumentaryName = "AFAFAFa"
            //};

            //if (r != null)
            //    si.Routing.Assign(r);

            //var metadata = new List<DokuField>()
            //{
            //    new DokuField(){ text = "Creado por", type = "T", mandatory = 1, order = 1 },
            //    new DokuField(){ text = "Creado", type = "F", mandatory = 1, order = 2 },
            //    new DokuField(){ text = "Modificado por", type = "T", mandatory = 1, order = 3 },
            //    new DokuField(){ text = "Modificado", type = "F", mandatory = 1, order = 4 }
            //};

            //si.Metadata.AddRange(metadata);

            //AddToListView(si);

            //si = new ScannedImage() { Image = Image.FromFile("C:\\Users\\Emilio\\Pictures\\CQRS.jpg"), FileType = ".tiff" };
            //if (r != null)
            //    si.Routing.Assign(r);

            //metadata = new List<DokuField>()
            //{
            //    new DokuField(){ text = "Creado por", type = "T", mandatory = 1, order = 1 },
            //    new DokuField(){ text = "Creado", type = "F", mandatory = 1, order = 2 },
            //    new DokuField(){ text = "Modificado por", type = "T", mandatory = 1, order = 3 },
            //    new DokuField(){ text = "Modificado", type = "F", mandatory = 1, order = 4 },
            //    new DokuField(){ text = "Fecha", type = "F", mandatory = 1, order = 5 },
            //    new DokuField(){ text = "Hora", type = "H", mandatory = 0, order = 6 },
            //    new DokuField(){ text = "Titulo", type = "N", mandatory = 1, order = 7 },
            //    new DokuField(){ text = "Descripcion", type = "M", mandatory = 0, order = 8 },
            //    new DokuField(){ text = "Categoria", type = "O", mandatory = 0, order = 9 },
            //    new DokuField(){ text = "Observaciones", type = "RT", mandatory = 0, order = 9 }
            //};

            //si.Metadata.AddRange(metadata);

            //AddToListView(si);
            //#endregion


            _documentaryTypes = new List<Documentary>();
        }

        private void scanSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new ScanSettingsForm())
            {
                form.ShowDialog();
            }
        }

        private void scanHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new ScanHistoryForm())
            {
                form.ShowDialog();
            }
        }

        private async void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var scannedImages = new List<ScannedImage>();

            try
            {
                using (var form = new NewScanForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        scannedImages.AddRange(form.ScannedImages);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ex.Message, "Doku4Invoices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            var ticket = String.Empty;

            try
            {
                ticket = await Session.GetTikectAsync();

                if (_documentaryTypes.Count == 0)
                {
                    _documentaryTypes.AddRange(await DokuFlexService.GetDocumentaryTypesAsync(ticket));
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            if (string.IsNullOrWhiteSpace(ticket)) return;

            using (var form = new ProgressForm())
            {
                if (!form.UploadFiles(ticket, scannedImages))
                    return;
            }

            try
            {
                foreach (var scannedImage in scannedImages)
                {
                    if (!string.IsNullOrWhiteSpace(scannedImage.Routing.Documentary))
                    {
                        await DokuFlexService.UpdateDocumentMetadataAsync(ticket, scannedImage.Routing.Documentary,
                            scannedImage.Routing.FileId, new DokuField[] { });

                        var documentary = _documentaryTypes.FirstOrDefault(d => d.id.Equals(scannedImage.Routing.Documentary));

                        if (documentary != null)
                        {
                            foreach (var element in documentary.elements)
                            {
                                scannedImage.Metadata.Add(DokuField.CreateNew(element));
                            }
                        }
                    }

                    AddToListView(scannedImage);
                }
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ex.Message, "Doku4Invoices", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _currentListViewItem = null;

            if (e.IsSelected)
            {

                _currentListViewItem = e.Item;
                var scannedImage = e.Item.Tag as ScannedImage;

                if (!_bindingSuspended)
                {
                    metadataControl.ApplyChanges();
                    metadataControl.BindMetadata(scannedImage.Metadata);
                }

                textDocumentaryType.Text = scannedImage.Routing.DocumentaryName;
                btnSave.Enabled = true;
            }
            else
            {
                textDocumentaryType.Text = string.Empty;
                btnSave.Enabled = false;
            }

            RefreshControlsState();
        }

        private void RefreshControlsState()
        {
            deleteToolStripMenuItem.Enabled = deleteToolStripMenuItem1.Enabled = _currentListViewItem != null;
            deleteToolStripButton.Enabled = _currentListViewItem != null;
            renameToolStripMenuItem.Enabled = renameToolStripMenuItem1.Enabled = _currentListViewItem != null;
            selectAllToolStripMenuItem.Enabled = selectAllToolStripMenuItem1.Enabled = listView.Items.Count > 0;
            sendToDokuFlexToolStripMenuItem.Enabled = sendToDokuFlexToolStripMenuItem1.Enabled = listView.SelectedItems.Count > 0;
            openToolStripMenuItem.Enabled = openToolStripButton.Enabled = _currentListViewItem != null;
            //updateMetadataToolStripMenuItem.Enabled = updateMetadataToolStripMenuItem1.Enabled = _currentListViewItem != null;
            btnSave.Enabled = _currentListViewItem != null;


            if (previewPaneToolStripMenuItem.Checked)
            {
                if (_currentListViewItem != null)
                {
                    var scannedItem = _currentListViewItem.Tag as ScannedImage;

                    SetImage(scannedItem.Image);
                }
                else
                {
                    SetImage(null);
                }
            }
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentListViewItem == null) return;

            if (MessageBox.Show("¿Desea eliminar el elemento seleccionado?", Text, MessageBoxButtons.YesNo) == DialogResult.No) return;

            var scannedImage = _currentListViewItem.Tag as ScannedImage;

            this.Cursor = Cursors.WaitCursor;

            if (!string.IsNullOrWhiteSpace(scannedImage.Path))
            {
                if (File.Exists(scannedImage.Path))
                {
                    try
                    {
#pragma warning disable SG0018 // Path traversal
                        File.Delete(scannedImage.Path);
#pragma warning restore SG0018 // Path traversal
                    }
                    catch (Exception)
                    {
                        //Silent exception
                    }
                }
            }

            try
            {
                var ticket = await Session.GetTikectAsync();
                if (await DokuFlexService.DeleteFileAsync(ticket, scannedImage.Routing.Community, scannedImage.Routing.FileId))
                {
                    metadataControl.BindMetadata(null);
                    textDocumentaryType.Text = string.Empty;
                    listView.Items.Remove(_currentListViewItem);
                    _currentListViewItem = null;
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            RefreshControlsState();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bindingSuspended = true;

            foreach (ListViewItem item in listView.Items)
            {
                item.Selected = true;
            }

            if (listView.Items.Count > 0)
                listView.FocusedItem = listView.Items[0];

            listView.Refresh();
            RefreshControlsState();
            _bindingSuspended = false;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentListViewItem == null) return;

            var scannedItem = _currentListViewItem.Tag as ScannedImage;

            using (var form = new RenameForm())
            {
                form.ImputText = scannedItem.Name;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    scannedItem.Name = form.ImputText;

                    RefreshListView();
                }
            }
        }

        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolBarToolStripMenuItem.Checked)
            {
                toolStrip.Visible = true;
            }
            else
            {
                toolStrip.Visible = false;
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked)
            {
                statusStrip.Visible = true;
            }
            else
            {
                statusStrip.Visible = false;
            }
        }

        private void previewPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (previewPaneToolStripMenuItem.Checked)
            {
                splitContainer1.Panel2Collapsed = false;

            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
            }

            previewPaneToolStripButton.Checked = previewPaneToolStripMenuItem.Checked;
            metadataControl.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void sendToDokuFlexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            metadataControl.ApplyChanges();

            if (listView.SelectedItems.Count == 0) return;

            var scannedItems = new List<ScannedImage>(listView.SelectedItems.Count);

            foreach (ListViewItem item in listView.SelectedItems)
            {
                scannedItems.Add(item.Tag as ScannedImage);
            }

            this.Cursor = Cursors.WaitCursor;

            var ticket = String.Empty;

            try
            {
                ticket = await Session.GetTikectAsync();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            if (String.IsNullOrWhiteSpace(ticket)) return;

            using (var form = new ProgressForm())
            {
                if (form.UploadFiles(ticket, scannedItems))
                {
                    //Delete uploaded files from listView
                    foreach (ListViewItem item in listView.SelectedItems)
                    {
                        var scannedImage = item.Tag as ScannedImage;

                        if (!String.IsNullOrWhiteSpace(scannedImage.Path))
                        {
                            if (File.Exists(scannedImage.Path))
                            {
                                try
                                {
                                    File.Delete(scannedImage.Path);
                                }
                                catch (Exception)
                                {
                                    //Silent exception
                                }
                            }
                        }

                        item.Remove();
                    }

                    _currentListViewItem = null;
                    metadataControl.BindMetadata(null);
                }
            }

            RefreshControlsState();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void proportionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentListViewItem == null) return;

            var scannedImage = _currentListViewItem.Tag as ScannedImage;

            if (String.IsNullOrWhiteSpace(scannedImage.Path))
            {
                scannedImage.Path = String.Format("{0}\\{1}",
                            DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory),
                            scannedImage.Name + scannedImage.FileType);
                scannedImage.Image.Save(scannedImage.Path, scannedImage.FileType.ToImageFormat());
            }

            System.Diagnostics.Process.Start(scannedImage.Path);
        }

        private void previewPaneToolStripButton_Click(object sender, EventArgs e)
        {
            previewPaneToolStripMenuItem.PerformClick();
        }

        private void scanToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = (sender as ToolStripMenuItem).ToolTipText;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var directory = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);

                if (Directory.Exists(directory))
                {
                    try
                    {
                        Directory.Delete(directory, true);
                    }
                    catch (Exception)
                    {
                        //Silent exception
                    }
                }

                directory = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.DownloadDirectory);

                if (Directory.Exists(directory))
                {
                    try
                    {
                        Directory.Delete(directory, true);
                    }
                    catch (Exception)
                    {
                        //Silent exception
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void updateMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            metadataControl.ApplyChanges();

            var scannedImages = new List<ScannedImage>();

            foreach (ListViewItem item in listView.Items)
            {
                scannedImages.Add(item.Tag as ScannedImage);
            }

            listView.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var ticket = await Session.GetTikectAsync();

                foreach (var scannedImage in scannedImages)
                {
                    await DokuFlexService.UpdateDocumentMetadataAsync(ticket, scannedImage.Routing.Documentary,
                            scannedImage.Routing.FileId, scannedImage.Metadata.ToArray());
                }
            }
            finally
            {
                listView.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new AboutBoxForm())
            {
                form.ShowDialog();
            }
        }

        private void changeCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.ChangeCredentials();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_currentListViewItem == null) return;

            metadataControl.ApplyChanges();

            if (metadataControl.HasErrors())
                MessageBox.Show("Uno o mas valores introducidos no son validos", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                var scannedImage = _currentListViewItem.Tag as ScannedImage;

                btnSave.Enabled = false;
                listView.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    var token = await Session.GetTikectAsync();

                    await DokuFlexService.UpdateDocumentMetadataAsync(token, scannedImage.Routing.Documentary,
                            scannedImage.Routing.FileId, scannedImage.Metadata.ToArray());
                }
                finally
                {
                    btnSave.Enabled = true;
                    listView.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void technicalSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:soporte@dokuflex.com");
        }
    }
}
