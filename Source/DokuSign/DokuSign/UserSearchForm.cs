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

namespace DokuSign
{
    public partial class UserSearchForm : Form
    {
        public IDataService dataService { get; set; }

        public List<SearchUserResult> SelectedUsers { get; set; }

        public UserSearchForm()
        {
            InitializeComponent();
            SelectedUsers = new List<SearchUserResult>();
            resultsCheckedList.CheckOnClick = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (SelectedUsers != null)
            {
                resultsCheckedList.DataSource = SelectedUsers;
                resultsCheckedList.DisplayMember = "fullName";
                resultsCheckedList.ValueMember = "id";

                for (int count = 0; count < resultsCheckedList.Items.Count; count++)
                {
                    resultsCheckedList.SetItemChecked(count, true);
                }
            }
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SelectedUsers.Clear();
            foreach (SearchUserResult item in this.resultsCheckedList.CheckedItems)
            {
                SelectedUsers.Add(item);
            }
            searchText.Enabled = false;
            string _ticket = await Session.GetTikectAsync();

            List<SearchUserResult> results = await dataService.SearchUserAsync(_ticket, searchText.Text);
            List<SearchUserResult> source = new List<SearchUserResult>();

            if (this.SelectedUsers != null)
            {
                source.AddRange(SelectedUsers);
                //foreach (SearchUserResult user in this.SelectedUsers)
                //{
                //    if (results.Where(u => u.id.Equals(user.id)).Count() == 0)
                //    {
                //        results.Insert(0, user);
                //    }
                //}                
            }

            foreach (SearchUserResult user in results)
            {
                if (source.Where(u => u.id.Equals(user.id, StringComparison.OrdinalIgnoreCase)).Count() == 0)
                {
                    source.Add(user);
                }
            }

            ((ListBox)resultsCheckedList).DataSource = source;
            ((ListBox)resultsCheckedList).DisplayMember = "fullName";
            ((ListBox)resultsCheckedList).ValueMember = "id";

            if (this.SelectedUsers != null)
            {
                for (int index = 0; index < SelectedUsers.Count; index++)
                {
                    resultsCheckedList.SetItemChecked(index, true);                    
                }
            }

            searchText.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedUsers = new List<SearchUserResult>();
            foreach (SearchUserResult item in this.resultsCheckedList.CheckedItems)
            {
                SelectedUsers.Add(item);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}

