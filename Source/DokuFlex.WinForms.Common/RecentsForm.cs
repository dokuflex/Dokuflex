//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.WinForms.Common
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

    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.WinForms.Common.Resources;

    public partial class RecentsForm : Form
    {
        private RecentFile _recent;
        private string _ticket;
        private string _filterText;
        private int _fileIcon;

        public void ShowMetadata(string fileId, string fileName)
        {
            using (var form = new MetadataForm(_ticket, fileId, fileName))
            {
                form.ShowDialog();
            }
        }

        private void AddToListView(RecentFile item)
        {
            var modifiedDate = DateTimeExtensions.FromUnixEpoch(item.modifiedTime);
            var size = item.size / 1024;
            var listViewItem = listView.Items.Add(item.title);
            listViewItem.ImageIndex = _fileIcon;
            listViewItem.SubItems.Add(modifiedDate.ToShortDateString());
            listViewItem.SubItems.Add(item.version.ToString());
            listViewItem.SubItems.Add(String.Format("{0} KB", size.ToString()));
            listViewItem.Tag = item;

            var currentDate = DateTime.Today;

            if (modifiedDate.Year == currentDate.Year)
            {
                if (modifiedDate.Month == currentDate.Month)
                {
                    if (modifiedDate.Day == currentDate.Day)
                    {
                        listViewItem.Group = listView.Groups["Today"];
                    }
                    else
                        if (modifiedDate.Day == currentDate.Day - 1)
                        {
                            listViewItem.Group = listView.Groups["Yesterday"];
                        }
                        else
                        {
                            listViewItem.Group = listView.Groups["ThisMonth"];
                        }
                }
                else
                {
                    listViewItem.Group = listView.Groups["OlderThanThisMonth"];
                }
            }
            else
            {
                listViewItem.Group = listView.Groups["OlderThanThisMonth"];
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            imageList16.Images.Add(ImageResources.FileExtensionDocSmall);
            imageList16.Images.Add(ImageResources.FileExtensionPpsSmall);
            imageList16.Images.Add(ImageResources.FileExtensionXlsSmall);

            this.Cursor = Cursors.WaitCursor;

            try
            {
                var recents = await DokuFlexService.GetRecentDocumentsAsync(_ticket, _filterText);

                foreach (var item in recents)
                {
                    AddToListView(item);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public RecentFile SelectedValue
        {
            get
            {
                return _recent;
            }
        }

        public RecentsForm()
        {
            InitializeComponent();
        }

        public RecentsForm(string ticket, string filterText, int fileIcon = 0)
        {
            InitializeComponent();

            _ticket = ticket;
            _filterText = filterText;
            _fileIcon = fileIcon;
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _recent = e.Item.Tag as RecentFile;
            }
            else
            {
                _recent = null;
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView.Columns[e.Column].Tag == null)
            {
                listView.Columns[e.Column].Tag = SortOrder.Ascending;
                listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
            }
            else
            {
                var sortOrder = listView.Columns[e.Column].Tag.ToString();

                switch (sortOrder)
                {
                    case "Ascending":
                        listView.Columns[e.Column].Tag = SortOrder.Descending;
                        listView.ListViewItemSorter = new ListViewItemReverseComparer(e.Column);
                        break;

                    case "Descending":
                        listView.Columns[e.Column].Tag = SortOrder.Ascending;
                        listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
                        break;

                    default:
                        break;
                }
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_recent != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_recent != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void metadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_recent != null)
            {
                ShowMetadata(_recent.id, _recent.name);
            }
        }
    }
}
