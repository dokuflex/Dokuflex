using System;
using System.Windows.Forms;
using FileImporter.ViewModels;

namespace FileImporter
{
    public partial class AdvancedForm : Form
    {
        private readonly AdvancedViewModel _viewModel;

        public AdvancedForm()
        {
            InitializeComponent();
        }

        public AdvancedForm(AdvancedViewModel viewModel)
            : this()
        {
            _viewModel = viewModel;

            InitializeControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cbxDelimiters.SelectedIndex = 1;
        }

        private void InitializeControls()
        {
            bindingSource1.DataSource = _viewModel.Fields;
        }

        private void cbxDelimiters_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (cbxDelimiters.SelectedIndex)
            {
                case 1:
                    _viewModel.Delimiter = ';';
                break;

                case 2:
                    _viewModel.Delimiter = ',';
                    break;

            }

            UpdateFieldNames();
        }

        private void UpdateFieldNames()
        {
            _viewModel.ClearSourceFieldValues();
            _viewModel.FillFieldNames();
            colSourceField.Items.Clear();
            colSourceField.Items.AddRange(_viewModel.GetFieldNames());
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (_viewModel.IsValid())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {

            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
