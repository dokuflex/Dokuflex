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
    using System.Linq;
    using System.Windows.Forms;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;

    public partial class MetadataForm : Form
    {
        private string _ticket;
        private string _fileId;
        private Documentary _docType;
        private List<Documentary> _docTypes;

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Cursor = Cursors.WaitCursor;

            try
            {
                _docTypes = await DokuFlexService.GetDocumentaryTypesAsync(_ticket);
                cbxDocumentaryTypes.DataSource = new BindingList<Documentary>(_docTypes);

                if (!String.IsNullOrWhiteSpace(_fileId))
                {
                    var docMetadata = await DokuFlexService.GetDocumentMetadadaAsync(_ticket, _fileId);
                    var targetDocType = _docTypes.FirstOrDefault(d => d.id.Equals(docMetadata.docType));

                    if (targetDocType == null) return;

                    targetDocType.elements.Clear();
                    targetDocType.elements.AddRange(docMetadata.elements);

                    cbxDocumentaryTypes.SelectedValue = docMetadata.docType;
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public string DocumentType
        {
            get
            {
                return _docType.id;
            }
        }

        public List<DokuField> Metadata
        {
            get
            {
                return _docType.elements;
            }
        }

        public MetadataForm()
        {
            InitializeComponent();
        }

        public MetadataForm(string ticket, string fileId, string title = "")
        {
            InitializeComponent();

            this.Text = String.Format("{0} - Metadatos", title);

            _docType = null;
            _ticket = ticket;
            _fileId = fileId;
        }

        private void cbxDocumentaryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _docType = cbxDocumentaryTypes.SelectedItem as Documentary;

            metadataControl.ApplyChanges();

            if (_docType != null)
            {
                metadataControl.BindMetadata(_docType.elements);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_docType == null)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                metadataControl.ApplyChanges();

                if (metadataControl.HasErrors())
                    MessageBox.Show("Uno o mas valores introducidos no son validos", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }
    }
}
