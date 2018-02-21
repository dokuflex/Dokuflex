

namespace DokuFlex.Outlook.AddIn
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Threading.Tasks;
    using System.Threading;

    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Services;
   

    public partial class UpdateTaskView : Form
    {
        private UpdateTaskPresenter _presenter;
        private bool _taskCancelled;
        private string _ticket;
        private UserGroup _group;
        private Category _category;
        private bool _dialogShown;
        private List<string> _attachmentsId;
        private List<Category> _categories;

        private void DisplayAttachments()
        {
            var attachments = _presenter.GetAttachments();

            chkAttachedList.Items.Clear();

            foreach (var attach in attachments)
            {
                chkAttachedList.Items.Add(attach.DisplayName, false);
            }
        }

        private void TaskAsyncExceptionHandle(AggregateException e)
        {
            MessageBox.Show(string.Format("{1}\n{2}", e.Message, e.InnerException.Message),
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            _taskCancelled = false;

            this.Cursor = Cursors.Default;
            //this.DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        private void DisplayGroupsAsyncBegin()
        {
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<UserGroup>>.Factory.StartNew(() => _presenter.GetUserGroups(_ticket));

            task.ContinueWith(t => DisplayGroupsAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayGroupsAsyncEnd(IList<UserGroup> groups)
        {
            if (_taskCancelled) return;

            cbxGroups.DataSource = new BindingList<UserGroup>(groups);
            cbxGroups.SelectedIndex = 0;
        }

        private void DisplayProjectsAsyncBegin()
        {
            this.Cursor = Cursors.WaitCursor;

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<Project>>.Factory.StartNew(() => DokuFlexService.ListProjects(_ticket, _group.id));

            task.ContinueWith(t => DisplayProjectsAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayProjectsAsyncEnd(IList<Project> projects)
        {
            if (_taskCancelled) return;

            projects.Insert(0, new Project() { title = String.Empty});

            cbxProjects.DataSource = new BindingList<Project>(projects);

            this.Cursor = Cursors.Default;
        }

        private void DisplayCategoriesAsyncBegin(string projectId)
        {
            this.Cursor = Cursors.WaitCursor;

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<Category>>.Factory.StartNew(() => _presenter.ListCategories(_ticket, _group.id, projectId, "TASK_CATEGORY"));

            task.ContinueWith(t => DisplayCategoriesAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayCategoriesAsyncEnd(IList<Category> categories)
        {
            if (_taskCancelled) return;

            _categories.Clear();
            _categories.AddRange(categories);

            var categoriesOnly = _categories.Where(c => c.parentId.Equals("0")).ToList();

            cbxCategories.DataSource = new BindingList<Category>(categoriesOnly);
            cbxCategories.SelectedIndex = 0;

            this.Cursor = Cursors.Default;
        }

        private void DisplayCategoriesStatusAsyncBegin(string projectId)
        {
            this.Cursor = Cursors.WaitCursor;

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<Category>>.Factory.StartNew(() => _presenter.ListCategories(_ticket, _group.id, projectId,"STATUS_CATEGORY"));

            task.ContinueWith(t => DisplayCategoriesStatusAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayCategoriesStatusAsyncEnd(IList<Category> categories)
        {
            if (_taskCancelled) return;

            cbxStatus.DataSource = new BindingList<Category>(categories);
            cbxStatus.SelectedIndex = 0;

            this.Cursor = Cursors.Default;
        }

        private void UploadAttach()
        {
            foreach (object itemChecked in chkAttachedList.CheckedItems)
            {
                var nodeId = _presenter.UploadAttach(itemChecked.ToString(), _ticket, _group.id);

                if (!String.IsNullOrWhiteSpace(nodeId))
                {
                    _attachmentsId.Add(nodeId);
                }
            }          
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            textTaskName.Text = _presenter.Subject;
            textDescription.Text = _presenter.MessageBody;
            DisplayAttachments();

            DisplayGroupsAsyncBegin();

            if (chkAttachedList.CheckedIndices.Count > 0)
            {
                lbAttachHint.Visible = true;
            }
        }

        public UpdateTaskView()
        {
            InitializeComponent();
        }

        public UpdateTaskView(string ticket)
        {
            InitializeComponent();

            _dialogShown = true;
            _ticket = ticket;
            _categories = new List<Category>();
            _attachmentsId = new List<string>();
            _presenter = new UpdateTaskPresenter();
        }

        private void UpdateTaskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dialogShown)
            {
                e.Cancel = true;
                return;
            }        
        }

        private void ClearControlValidation()
        {
            errorProvider.SetError(textTaskName, String.Empty);
            errorProvider.SetError(dtpEndDate, String.Empty);
            errorProvider.SetError(cbxGroups, String.Empty);
            errorProvider.SetError(textFolderPath, String.Empty);
        }

        private bool ContainsErrors()
        {
            var hasError = false;

            if (String.IsNullOrWhiteSpace(textTaskName.Text))
            {
                errorProvider.SetError(textTaskName, "El nombre de tarea no es valido");

                hasError = true;
            }

            var daysBetween = dtpEndDate.Value.Subtract(dtpStartDate.Value).Days;

            if (daysBetween < 0)
            {
                errorProvider.SetError(dtpEndDate, "La fecha de finalización no es valida");

                hasError = true;
            }

            if (cbxGroups.SelectedItem == null)
            {
                errorProvider.SetError(cbxGroups, "La comunidad no es valida");

                hasError = true;
            }

            if (cbxCategories.SelectedItem == null)
            {
                errorProvider.SetError(cbxCategories, "La categoría no es valida");

                hasError = true;
            }

            if (chkAttachedList.CheckedIndices.Count > 0 && String.IsNullOrWhiteSpace(textFolderPath.Text))
            {
                errorProvider.SetError(textFolderPath, "La carpeta de destino no es valida");

                hasError = true;
            }

            return hasError;
        }

        private void cbxGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            _group = cbxGroups.SelectedItem as UserGroup;

            if (_group != null)
            {
                DisplayProjectsAsyncBegin();
            }          
        }

        private void cbxCategories_SelectedValueChanged(object sender, EventArgs e)
        {
            _category = cbxCategories.SelectedItem as Category;

            if (_category == null)
            {
                cbxSubCategories.DataSource = new BindingList<Category>(new List<Category>());
            }
            else
            {
                var subCategories = _categories.Where(c => c.parentId.Equals(_category.id)).ToList();

                cbxSubCategories.DataSource = new BindingList<Category>(subCategories);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (_presenter.BrowseForFolders(_ticket, _group.id))
            {
                textFolderPath.Text = _presenter.FolderPath;
            }
        }

        private void chkAttachedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAttachedList.CheckedIndices.Count > 0)
            {
                lbAttachHint.Visible = true;
                btnBrowse.Enabled = true;
            }
            else
            {
                lbAttachHint.Visible = false;
                btnBrowse.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ContainsErrors()) return;

            var startDate = dtpStartDate.Value;
            var startTime = dtpStartTime.Value.TimeOfDay;
            var newStartDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hours, startTime.Minutes, startTime.Seconds).ToUnixEpoch();
            var endDate = dtpEndDate.Value;
            var endTime = dtpEndTime.Value.TimeOfDay;
            var newEndDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hours, endTime.Minutes, endTime.Seconds).ToUnixEpoch();
            var projectId = cbxProjects.SelectedValue != null ? cbxProjects.SelectedValue.ToString() : String.Empty;
            var taskResult = _presenter.UpdateTask(_ticket, _group.id, textTaskName.Text, newStartDateTime, newEndDateTime, textDescription.Text,
                    _category.text, cbxSubCategories.Text, cbxStatus.Text, projectId);

            this.Cursor = Cursors.WaitCursor;

            try
            {
                UploadAttach();

                foreach (var item in _attachmentsId)
                {
                    _presenter.LinkFileToTask(_ticket, taskResult.id, item);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            using (var form = new UpdateTaskDlg())
            {
                form.ShowTaskResultDlg(taskResult.fullNumber, taskResult.url);
            }

            _dialogShown = false;
            _taskCancelled = true;

            this.Close();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            var startDate = dtpStartDate.Value.AddDays(3);
            dtpEndDate.Value = startDate;
        }

        private void chkAttachments_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkAttachedList.Items.Count; i++)
            {
                chkAttachedList.SetItemChecked(i, chkAttachments.Checked);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _dialogShown = false;
            _taskCancelled = true;

            this.Close();
        }

        private void cbxProjects_SelectedValueChanged(object sender, EventArgs e)
        {
            var projectId = cbxProjects.SelectedValue;

            if (projectId != null)
            {
                DisplayCategoriesStatusAsyncBegin(projectId.ToString());
                DisplayCategoriesAsyncBegin(projectId.ToString());            
            }
            else
            {
                DisplayCategoriesStatusAsyncBegin(String.Empty);
                DisplayCategoriesAsyncBegin(String.Empty);               
            }
        }
    }
}
