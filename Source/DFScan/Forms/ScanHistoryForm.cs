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

namespace DokuFlex.Scan.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.WinForms.Common;
    using DokuFlex.Windows.Common.Services;
    using System.IO;
    using System.Diagnostics;
    using DokuFlex.Windows.Common;

    public partial class ScanHistoryForm : Form
    {
        private ListViewItem _currentListViewItem;
        private string _ticket;
        private string _downloadDir;

        private void AddToListView(ScanHistory item)
        {
            var date = item.date.FromUnixEpoch().ToString("G");

            var listViewItem = listView.Items.Add(date);
            listViewItem.SubItems.Add(item.name);
            listViewItem.Tag = item;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Directory.Exists(_downloadDir))
            {
                try
                {
                    Directory.CreateDirectory(_downloadDir);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dtpStartDate.Value = DateTime.Now;
            HideProcessingPane();
            HidePreviewNoSupportedPane();
        }

        private void ShowProcessingPane()
        {
            processingPane.BringToFront();
            processingPane.Show();
        }

        private void HideProcessingPane()
        {
            processingPane.SendToBack();
            processingPane.Hide();
        }

        public ScanHistoryForm()
        {
            InitializeComponent();

            _downloadDir = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.DownloadDirectory);
        }

        private async void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            var startDate = dtpStartDate.Value.ToUnixEpoch();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                _ticket = await Session.GetTikectAsync();

                var elements = await DokuFlexService.GetScanHistoryAsync(_ticket, startDate);

                listView.Items.Clear();

                foreach (var element in elements)
                {
                    if (String.IsNullOrWhiteSpace(element.id))
                        continue;

                    AddToListView(element);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _currentListViewItem = e.Item;

                var history = e.Item.Tag as ScanHistory;

                if (String.IsNullOrWhiteSpace(history.path))
                {
                    history.path = String.Format("{0}\\{1}", _downloadDir, history.name);

                    ShowProcessingPane();
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        var docMetadata = await DokuFlexService.GetDocumentMetadadaAsync(_ticket, history.id);
                        metadataControl.BindMetadata(docMetadata.elements);
                        await DokuFlexService.DownloadAsync(_ticket, history.id, history.path);
                    }
                    finally
                    {
                        HideProcessingPane();
                        this.Cursor = Cursors.Default;
                    }
                }

                webBrowser.Navigate(history.path);

                //ShowPreviewNoSupportedPane(history.path);
            }
            else
            {
                _currentListViewItem = null;
            }
        }

        private void ShowPreviewNoSupportedPane(string path)
        {
            PreviewNoSupportedPane.Show();
            PreviewNoSupportedPane.BringToFront();
            PreviewNoSupportedPane.Tag = path;
        }

        private void HidePreviewNoSupportedPane()
        {
            PreviewNoSupportedPane.Hide();
            PreviewNoSupportedPane.SendToBack();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PreviewNoSupportedPane.Tag.ToString());
        }
    }
}
