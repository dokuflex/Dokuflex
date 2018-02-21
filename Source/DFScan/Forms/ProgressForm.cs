// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Scan.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using DokuFlex.Scan.Data;
    using DokuFlex.WinForms.Common.Resources;
    using System.Threading;
    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;
    using System.IO;

    public partial class ProgressForm : Form
    {
        private bool _asyncTaskStarted;
        private List<ScannedImage> _scannedImages;
        private string _ticket;

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _asyncTaskStarted = true;

            try
            {
                foreach (var scannedImage in _scannedImages)
                {
                    if (string.IsNullOrWhiteSpace(scannedImage.Path))
                    {
                        scannedImage.Path = String.Format("{0}\\{1}",
                            DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory),
                            scannedImage.Name + scannedImage.FileType);

                        try
                        {
                            scannedImage.Image.Save(scannedImage.Path, scannedImage.FileType.ToImageFormat());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("{0}\n\n{1}", ErrorMessages.AsyncTaskError, ex.Message),
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            this.DialogResult = DialogResult.Cancel;
                            break;
                        }
                    }

                    var result = await DokuFlexService.UploadAsync(_ticket,
                            scannedImage.Routing.Community, string.Empty,
                            scannedImage.Routing.Folder, string.Empty, string.Empty,
                            true, scannedImage.Routing.Certificate, "scan",
                            scannedImage.Routing.ConvertToPdf, new FileInfo(scannedImage.Path));

                    scannedImage.Routing.FileId = result.nodeId;
                }

                if (DialogResult == DialogResult.None)
                    this.DialogResult = DialogResult.OK;
            }
            finally
            {
                _asyncTaskStarted = false;
                this.Close();
            }
        }

        public ProgressForm()
        {
            InitializeComponent();
        }

        public bool UploadFiles(string ticket, List<ScannedImage> items)
        {
            _ticket = ticket;
            _scannedImages = items;
            return this.ShowDialog() == DialogResult.OK;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _asyncTaskStarted;
        }
    }
}
