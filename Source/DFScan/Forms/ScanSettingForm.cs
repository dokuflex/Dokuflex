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
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Saraff.Twain;

    using DokuFlex.Scan.Data;
    using DokuFlex.Windows.Common.Log;
    using DokuFlex.WinForms.Common;
    using System.Threading.Tasks;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;
    using Saraff.IoC;

    public partial class ScanSettingForm : Form
    {
        private readonly Twain32 _twain;
        private readonly ServiceContainer _container;
        private string _ticket;
        private UserGroup _community;
        private FileFolder _folder;

        private void DisplayScanners()
        {
            cbxScanner.Items.Clear();

            _twain.OpenDSM();

            if (_twain.SourcesCount > 0)
            {
                for (int index = 0; index < _twain.SourcesCount; index++)
                {
                    cbxScanner.Items.Add(_twain.GetSourceProductName(index));
                }
            }
        }

        private void DisplayColorFormats()
        {
            cbxColorFormat.Items.Clear();

            try
            {
                var _pixelTypes = _twain.Capabilities.PixelType.Get();

                for (int index = 0; index < _pixelTypes.Count; index++)
                {
                    if (_pixelTypes[index].ToString() == "RGB")
                    {
                        cbxColorFormat.Items.Add("Color");
                    }

                    if (_pixelTypes[index].ToString() == "Gray")
                    {
                        cbxColorFormat.Items.Add("Escala de grises");
                    }

                    if (_pixelTypes[index].ToString() == "BW")
                    {
                        cbxColorFormat.Items.Add("Blanco y negro");
                    }
                }
            }
            catch (TwainException ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ErrorMessages.PixelsNotAviableError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayResolutions()
        {
            cbxResolution.Items.Clear();

            try
            {
                Twain32.Enumeration resolutions = _twain.Capabilities.XResolution.Get();

                if (resolutions.Count > 0)
                {
                    for (int index = 0; index < resolutions.Count; index++)
                    {
                        if (float.Parse(resolutions[index].ToString()) >= 200)
                        {
                            cbxResolution.Items.Add(resolutions[index].ToString());
                        }
                    }
                }
            }
            catch (TwainException ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ErrorMessages.ResolutionsNotAviableError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFileTypes()
        {
            // Populate the combo box using an array as DataSource.
            var fileTypes = new ArrayList();
            fileTypes.Add(new FileType("TIFF", ".tiff"));
            fileTypes.Add(new FileType("BMP", ".bmp"));
            fileTypes.Add(new FileType("JPG", ".jpg"));
            fileTypes.Add(new FileType("PNG", ".png"));

            cbxFileType.Items.Clear();
            cbxFileType.DataSource = fileTypes;
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    DisplayScanners();
        //    DisplayFileTypes();

        //    this.Cursor = Cursors.WaitCursor;

        //    try
        //    {
        //        var documentaryList = new List<Documentary>()
        //        {
        //            new Documentary(){id = Guid.NewGuid().ToString("N"), name = "Prueba 1", homologation = 1},
        //            new Documentary(){id = Guid.NewGuid().ToString("N"), name = "Prueba 2", homologation = 0}
        //        };

        //        var certificateList = new List<Certificate>()
        //        {
        //            new Certificate(){id = Guid.NewGuid().ToString("N"), text = "Certificado 1"},
        //            new Certificate(){id = Guid.NewGuid().ToString("N"), text = "Certificado 2"},
        //        };

        //        cbxDocumentaryTypes.DataSource = new BindingList<Documentary>(documentaryList);
        //        cbxCertificates.DataSource = new BindingList<Certificate>(certificateList);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("{0}\n\n{1}", ErrorMessages.AsyncTaskError, ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }

        //    bindingSource.ResetBindings(true);
        //}

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DisplayScanners();
            DisplayFileTypes();

            this.Cursor = Cursors.WaitCursor;

            Task<string> theTask = Session.GetTikectAsync();

            try
            {
                _ticket = await theTask;

                if (String.IsNullOrWhiteSpace(_ticket))
                {
                    this.Close();
                    return;
                }

                var documentaryList = await DokuFlexService.GetDocumentaryTypesAsync(_ticket);
                var certificateList = await DokuFlexService.ListCertificatesAsync(_ticket);

                cbxDocumentaryTypes.DataSource = new BindingList<Documentary>(documentaryList);
                cbxCertificates.DataSource = new BindingList<Certificate>(certificateList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}", ErrorMessages.AsyncTaskError, ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            bindingSource.ResetBindings(true);
        }

        public ScanSettingForm()
        {
            InitializeComponent();

            _container = new ServiceContainer();
            _container.Bind(typeof(IStreamProvider), typeof(SaraffStreamProvider));
            _twain = _container.CreateInstance<Twain32>();
            bindingSource.DataSource = typeof(ScanSetting);
            bindingSRouting.DataSource = typeof(Routing);
        }

        public void BindToControls(ScanSetting scanSetting)
        {
            bindingSource.DataSource = scanSetting;
            bindingSRouting.DataSource = scanSetting.Routing;

            _folder = new FileFolder()
            {
                id = scanSetting.Routing.Folder
            };

            _community = new UserGroup()
            {
                id = scanSetting.Routing.Community,
                name = scanSetting.Routing.CommunityName
            };
        }

        private void cbxScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _twain.CloseDataSource();
                _twain.SourceIndex = cbxScanner.SelectedIndex;
                _twain.OpenDataSource();

            }
            catch (TwainException ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ErrorMessages.ScannerCannotByActivate, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DisplayColorFormats();
            DisplayResolutions();
        }

        private void ScanSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.HasErrors())
                {
                    e.Cancel = true;
                }
                else
                {
                    var documentary = cbxDocumentaryTypes.SelectedItem as Documentary;
                    var routing = bindingSRouting.Current as Routing;
                    routing.DocumentaryName = cbxDocumentaryTypes.Text;
                    routing.Homologation = documentary.homologation;
                    routing.CertificateName = cbxCertificates.Text;
                    routing.Community = _community.id;
                    routing.CommunityName = _community.name;
                    routing.Folder = _folder.id;

                    bindingSRouting.EndEdit();
                    bindingSource.EndEdit();
                    _twain.CloseDSM();
                }
            }
            else
            {
                bindingSRouting.CancelEdit();
                bindingSource.CancelEdit();
                _twain.CloseDSM();
            }
        }

        private bool HasErrors()
        {
            var result = false;

            if (String.IsNullOrWhiteSpace(textName.Text))
            {
                errorProvider.SetError(textName, ErrorMessages.InvalidProfileNameError);
                result = true;
            }
            else
            {
                errorProvider.SetError(textName, String.Empty);
            }

            if (cbxScanner.SelectedItem == null)
            {
                errorProvider.SetError(cbxScanner, ErrorMessages.InvalidScannerError);
                result = true;
            }
            else
            {
                errorProvider.SetError(cbxScanner, String.Empty);
            }

            if (cbxResolution.SelectedItem == null)
            {
                errorProvider.SetError(cbxResolution, ErrorMessages.InvalidResolutionError);
                result = true;
            }
            else
            {
                errorProvider.SetError(cbxResolution, String.Empty);
            }

            if (cbxColorFormat.SelectedItem == null)
            {
                errorProvider.SetError(cbxColorFormat, ErrorMessages.InvalidPixelFormat);
                result = true;
            }
            else
            {
                errorProvider.SetError(cbxColorFormat, String.Empty);
            }

            if (cbxDocumentaryTypes.SelectedValue == null)
            {
                errorProvider.SetError(cbxDocumentaryTypes, ErrorMessages.InvalidDocumentaryTypeError);
                result = true;
            }
            else
            {
                errorProvider.SetError(cbxDocumentaryTypes, String.Empty);
            }

            if (String.IsNullOrWhiteSpace(textPath.Text))
            {
                errorProvider.SetError(textPath, ErrorMessages.InvalidFolderError);
                result = true;
            }
            else
            {
                errorProvider.SetError(textPath, String.Empty);
            }

            var documentary = cbxDocumentaryTypes.SelectedItem as Documentary;

            if (documentary.homologation == 1)
            {
                if (cbxCertificates.SelectedItem == null)
                {
                    errorProvider.SetError(cbxCertificates, "El tipo documental seleccionado requiere un certificado");
                    result = true;
                }
                else
                {
                    errorProvider.SetError(cbxCertificates, String.Empty);
                }

                if (chkConverToPDF.Checked)
                {
                    errorProvider.SetError(chkConverToPDF, String.Empty);
                }
                else
                {
                    errorProvider.SetError(chkConverToPDF, "El tipo documental seleccionado requiere que las digitalizaciones se archiven como PDF");
                    result = true;
                }

                if (String.Compare(cbxColorFormat.Text, "Blanco y negro") == 0)
                {
                    errorProvider.SetError(cbxColorFormat, String.Empty);
                }
                else
                {
                    errorProvider.SetError(cbxColorFormat, "El tipo documental seleccionado requiere que el formato de color sea Blanco y negro");
                    result = true;
                }

                if (String.Compare(cbxFileType.Text, "TIFF") == 0)
                {
                    errorProvider.SetError(cbxFileType, String.Empty);
                }
                else
                {
                    errorProvider.SetError(cbxFileType, "El tipo documental seleccionado requiere que el tipo de archivo sea TIFF");
                    result = true;
                }
            }

            return result;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var form = new BrowseFolderForm(_ticket))
            {
                if (form.ShowFolderBrowserDialog())
                {
                    _community = form.Group;
                    _folder = form.Folder;

                    textPath.Text = form.FullPath;
                }
            }
        }

        private void cbxDocumentaryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipoDocumental = cbxDocumentaryTypes.SelectedItem as Documentary;

            if (tipoDocumental != null && tipoDocumental.homologation == 1)
            {
                lbHomologation.Visible = true;
            }
            else
            {
                lbHomologation.Visible = false;
            }
        }
    }
}
