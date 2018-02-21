using System;
using System.Windows.Forms;

namespace DokuFlex.FileSync
{
    public partial class SyncStatusView : Form
    {
        private void LoadNotSyncItems()
        {
            var items = SyncTableManager.GetNotSyncItems();

            foreach (var item in items)
            {
                var listViewItem = new ListViewItem()
                {
                    Text = item.Name
                };

                listViewItem.SubItems.Add(item.Path);
                listViewItem.SubItems.Add(item.SyncStatusText);
                listView1.Items.Add(listViewItem);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadNotSyncItems();
        }

        public SyncStatusView()
        {
            InitializeComponent();
        }
    }
}
