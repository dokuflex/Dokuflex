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

namespace DokuFlex.WinForms.Common
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;

    public partial class TransferProgressForm : Form
    {
        private string _fileId;

        private bool _taskAsyncStarted;
        private long _modifiedTime;
        private bool _result = false;

        private void TaskAsyncExceptionHandle(AggregateException e)
        {
            _taskAsyncStarted = false;

            MessageBox.Show(string.Format("{0}\n\n{1}",
                ErrorMessages.AsyncTaskError, ErrorMessages.RepositoryNotFoundError),
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UploadFileAsyncBegin(string ticket, string groupId, string folderId,
            string fileId, string filePath, bool saveAsNewVersion)
        {
            _taskAsyncStarted = true;

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<UploadResult>.Factory.StartNew(() => DokuFlexService.Upload(ticket,
                groupId, fileId, folderId, String.Empty, String.Empty, saveAsNewVersion,
                String.Empty, "plugin", false, new FileInfo(filePath)));

            task.ContinueWith(t => UploadFileAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void UploadFileAsyncEnd(UploadResult result)
        {
            _taskAsyncStarted = false;

            if (result == null)
            {
                MessageBox.Show(ErrorMessages.AsyncTaskError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                _fileId = result.nodeId;
                _modifiedTime = result.modifiedTime;
                _result = true;
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }

        private void DownloadFileAsyncBegin(string ticket, string fileId, string filePath)
        {
            _taskAsyncStarted = true;

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<bool>.Factory.StartNew(() => DokuFlexService.Download(ticket, fileId, filePath));

            task.ContinueWith(t => DownloadFileAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DownloadFileAsyncEnd(bool downloaded)
        {
            _taskAsyncStarted = false;

            if (downloaded)
            {
                _result = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(ErrorMessages.AsyncTaskError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }

            this.Close();
        }

        public string FileId
        {
            get
            {
                return _fileId;
            }
        }

        public long ModifiedTime
        {
            get
            {
                return _modifiedTime;
            }
        }

        public bool UploadFile(string ticket, string groupId, string folderId,
            string fileId, string filePath, bool saveAsNewVersion)
        {
            UploadFileAsyncBegin(ticket, groupId, folderId,
            fileId, filePath, saveAsNewVersion);
            this.ShowDialog();
            return _result;
        }

        public bool DownloadFile(string ticket, string fileId, string filePath)
        {
            DownloadFileAsyncBegin(ticket, fileId, filePath);
            this.ShowDialog();
            return _result;
        }

        public TransferProgressForm()
        {
            InitializeComponent();

            _fileId = string.Empty;
        }

        private void TransferFileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _taskAsyncStarted;
        }
    }
}
