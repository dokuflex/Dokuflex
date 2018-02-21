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
    using System.Windows.Forms;

    using DokuFlex.Scan.Data;

    public partial class ScanSettingsForm : Form
    {
        private ScanSettingsManager _settingsManager;
        private ListViewItem _currentListViewItem;

        private void RefreshListView()
        {
            listView.Items.Clear();

            foreach (var item in _settingsManager.Settings)
            {
                var name = item.IsDefault ? string.Format("{0} (Predet.)", item.Name) : item.Name;

                var listViewItem = listView.Items.Add(item.Scanner);
                listViewItem.SubItems.Add(name);
                listViewItem.SubItems.Add(item.ColorFormat);
                listViewItem.SubItems.Add(item.FileType);
                listViewItem.SubItems.Add(item.Resolution.ToString());
                listViewItem.SubItems.Add(item.Routing.DocumentaryName);
                listViewItem.SubItems.Add(item.Routing.CertificateName);
                listViewItem.SubItems.Add(item.Routing.FolderPath);
                listViewItem.Tag = item;
            }
        }

        private void RefreshControlsState()
        {
            btnDelete.Enabled = _currentListViewItem != null;
            btnEdit.Enabled = _currentListViewItem != null;
            btnSetAsDefault.Enabled = _currentListViewItem != null && (_currentListViewItem.Tag as ScanSetting).IsDefault == false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshListView();
            RefreshControlsState();
        }        

        public ScanSettingsForm()
        {
            InitializeComponent();

            _settingsManager = new ScanSettingsManager();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var scanSetting = new ScanSetting();

            using (var form = new ScanSettingForm())
            {
                form.BindToControls(scanSetting);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _settingsManager.Add(scanSetting);

                    RefreshListView();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var scanSetting = _currentListViewItem.Tag as ScanSetting;

            using (var form = new ScanSettingForm())
            {
                form.BindToControls(scanSetting);
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshListView();
                }
            }
        }

        private void ScanSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                _settingsManager.SaveToXml();
            }
        }

        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            if (_currentListViewItem != null)
            {
                foreach (var item in _settingsManager.Settings)
                {
                    item.IsDefault = false;
                }

                (_currentListViewItem.Tag as ScanSetting).IsDefault = true;
                _currentListViewItem.Selected = true;
            }

            RefreshListView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentListViewItem != null)
            {
                _settingsManager.Remove(_currentListViewItem.Tag as ScanSetting);

                listView.Items.Remove(_currentListViewItem);
                _currentListViewItem = null;
            }

            RefreshControlsState();
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _currentListViewItem = e.Item;
            }
            else
            {
                _currentListViewItem = null;
            }

            RefreshControlsState();
        }
    }
}
