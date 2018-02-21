//=======================================================================================
// PulsarSoft Inc.
//=======================================================================================
// EL SOFTWARE SE ENTREGA "TAL CUAL", SIN GARANTÍA DE NINGÚN TIPO, EXPRESAS O IMPLÍCITAS,
// INCLUYENDO PERO NO LIMITADAS A LAS GARANTÍAS DE COMERCIALIZACIÓN, APTITUD PARA UN
// PROPÓSITO PARTICULAR Y NO INFRACCIÓN. EN NINGÚN CASO, LOS AUTORES O TITULARES DEL
// COPYRIGHT SERÁN RESPONSABLES POR CUALQUIER RECLAMACIÓN, DAÑO U OTRA RESPONSABILIDAD,
// YA SEA EN UNA ACCIÓN DE CONTRATO, AGRAVIO O CUALQUIER OTRA FORMA, QUE SURJAN DE O EN
// CONEXION CON EL SOFTWARE O EL USO U OTROS TRATOS EN EL SOFTWARE.
//=======================================================================================
// Copyright (c) PulsarSoft Inc. Reservados todos los derechos.
// Este código es liberado bajo los términos de la licencia Apache v2.0,
// vea el archivo de texto licencia-es.txt para más información.
//=======================================================================================

namespace DokuFlex.Outlook.AddIn
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    using DokuFlex.Windows.Common;
    using DokuFlex.WinForms.Common;
    using DokuFlex.WinForms.Common.Resources;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;

    public partial class UploadEmailView : Form
    {
        private UploadEMailPresenter _presenter;

        private void DisplayAttachments()
        {
            var attachments = _presenter.GetAttachments();

            chkAttachedList.Items.Clear();

            foreach (var attach in attachments)
            {
                chkAttachedList.Items.Add(attach.DisplayName, true);
            }
        }

        private void DisableUIControls()
        {
            chkIncludeMessage.Enabled = false;
            chkAttachedList.Enabled = false;
            btnBrowse.Enabled = false;
            btnStart.Enabled = false;
        }

        private void EnableUIControls()
        {
            chkIncludeMessage.Enabled = true;
            chkAttachedList.Enabled = true;
            btnBrowse.Enabled = true;
            btnStart.Enabled = true;
        }

        private void UpdateProgressInfo(int step, int total)
        {
            lbUploading.Visible = true;
            lbUploading.Text = string.Format("Subiendo {0} de {1}", step, total);
            pbUploading.Enabled = true;
            pbUploading.Visible = true;
        }

        private void HideUploadProgressInfo()
        {
            lbUploading.Visible = false;
            pbUploading.Enabled = false;
            pbUploading.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DisplayAttachments();

            emailPicture.Image = ImageResources.EmailPicture;
            lbReceivedTime.Text = _presenter.ReceivedTime;
            lbFrom.Text = _presenter.From;
            lbSubject.Text = _presenter.Subject;
            this.Text = string.Format("{0} - {1}", _presenter.Subject, "Correo");
            btnStart.Enabled = btnBrowse.Enabled = chkAttachedList.CheckedIndices.Count > 0;
        }

        public UploadEmailView()
        {
            InitializeComponent();

            _presenter = new UploadEMailPresenter();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (_presenter.BrowseForFolder())
            {
                lbLocationPath.Text = _presenter.FullPath;
            }
        }

        private void UploadEmailView_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (chkAttachedList.CheckedItems.Count == 0)
            {
                MessageBox.Show("No hay archivos seleccionados para enviar a DokuFlex", Globals.ThisAddIn.Application.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            var counter = 0;
            var linkedFileId = String.Empty;

            DisableUIControls();

            try
            {
                var ticket = await Session.GetTikectAsync();
                var fileId = String.Empty;

                foreach (object itemChecked in chkAttachedList.CheckedItems)
                {
                    var attachName = itemChecked.ToString();

                    UpdateProgressInfo(++counter, chkAttachedList.CheckedItems.Count);

                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        if (chkIncludeMessage.Checked)
                        {
                            fileId = await _presenter.UploadAttachAsync(attachName, linkedFileId, true);
                        }
                        else
                        {
                            fileId = await _presenter.UploadAttachAsync(attachName, linkedFileId, false);
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                    using (var form = new MetadataForm(ticket, String.Empty, attachName))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            await DokuFlexService.UpdateDocumentMetadataAsync(ticket, form.DocumentType,
                                        fileId, form.Metadata.ToArray());
                        }
                    }

                    if (String.IsNullOrWhiteSpace(linkedFileId))
                    {
                        linkedFileId = fileId;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            finally
            {
                EnableUIControls();
            }
        }

        private void llbSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        private void chkAttachments_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkAttachedList.Items.Count; i++)
            {
                chkAttachedList.SetItemChecked(i, chkAttachments.Checked);
            }
        }

        private void btnAssociateWithProcess_Click(object sender, EventArgs e)
        {
            using (var form = new ProcessListView())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _presenter.ProcessId = form.SelectedItem?.id;
                    lblProcess.Text = form.SelectedItem?.title;

                    btnBrowse.Enabled = form.SelectedItem == null;
                    btnStart.Enabled = form.SelectedItem != null;
                }
            }
        }
    }
}
