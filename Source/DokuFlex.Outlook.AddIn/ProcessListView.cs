using DokuFlex.Windows.Common.Services;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.WinForms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuFlex.Outlook.AddIn
{
    public partial class ProcessListView : Form
    {
        public ProcessListView()
        {
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var ticket = await Session.GetTikectAsync();
                var results = await DataServiceFactory.Create().ListProcessesAsync(ticket);

                foreach (var item in results)
                {
                    if (item.active == 1)
                        AddToListView(item);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void AddToListView(Process item)
        {
            var listViewItem = listView.Items.Add(item.title);
            listViewItem.SubItems.Add(item.appCategory.datModified);
            listViewItem.SubItems.Add(item.version.ToString());
            listViewItem.Tag = item;
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                SelectedItem = e.Item.Tag as Process;
            }
            else
            {
                SelectedItem = null;
            }
        }

        public Process SelectedItem { get; private set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
