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

namespace DokuFlex.WinForms.Common
{
    public partial class SearchForm : Form
    {
        private SearchResult _currentItem;
        private string _filterText;
        private int _fileIcon;

        private void AddToListView(SearchResult item)
        {
            var modifiedDate = DateTimeExtensions.FromUnixEpoch(item.modifiedTime);
            var size = item.size / 1024;
            var listViewItem = listView.Items.Add(item.name);
            listViewItem.ImageIndex = _fileIcon;
            listViewItem.SubItems.Add(modifiedDate.ToShortDateString());
            listViewItem.SubItems.Add(item.version.ToString());
            listViewItem.SubItems.Add(String.Format("{0} KB", size.ToString()));
            listViewItem.Tag = item;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            imageList16.Images.Add(ImageResources.FileExtensionDocSmall);
            imageList16.Images.Add(ImageResources.FileExtensionPpsSmall);
            imageList16.Images.Add(ImageResources.FileExtensionXlsSmall);
            btnFind.Image = ImageResources.SearchSmall;
            HideSearchProgress();
        }

        private void ShowSearchProgress()
        {
            progressBar.Enabled = true;
            progressPane.BringToFront();
        }

        private void HideSearchProgress()
        {
            progressBar.Enabled = false;
            progressPane.SendToBack();
        }

        public SearchResult SelectedValue
        {
            get
            {
                return _currentItem;
            }
        }

        public SearchForm()
        {
            InitializeComponent();
        }

        public SearchForm(string filterText, int fileIcon = 0)
        {
            InitializeComponent();

            _filterText = filterText;
            _fileIcon = fileIcon;
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textSearch.Text))
            {
                MessageBox.Show("Debe introducir una palabra o frase de búsqueda",
                     this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ClearResultsGrid();
            ShowSearchProgress();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                var ticket = await Session.GetTikectAsync();
                var results = await DokuFlexService.SearchAsync(ticket, textSearch.Text, _filterText, "d");        

                foreach (var item in results)
                {
                    AddToListView(item);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                HideSearchProgress();
            }
        }

        private void ClearResultsGrid()
        {
            listView.Items.Clear();
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _currentItem = e.Item.Tag as SearchResult;
            }
            else
            {
                _currentItem = null;
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

        private void textSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnFind.PerformClick();
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (_currentItem != null) btnAccept.PerformClick();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
