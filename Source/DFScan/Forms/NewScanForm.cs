//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Scan.Forms
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.Drawing.Imaging;
    using Saraff.Twain;
    using DataMatrix.net;

    using DokuFlex.WinForms.Common.Resources;
    using DokuFlex.Windows.Common.Log;
    using DokuFlex.Scan.Data;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.Windows.Common;
    using System.Diagnostics;
    using Saraff.IoC;

    public partial class NewScanForm : Form
    {
        private readonly Twain32 _twain;
        private readonly ScanSettingsManager _settingsManager;
        private readonly List<Image> _images;
        private bool _previewing;
        private int _imageIndex;
        private ScanSetting _scanSetting;
        private readonly List<ScannedImage> _scannedItems;
        private readonly ServiceContainer _container;
        private bool _closingWindow;

        private void DisplayScannerSettings()
        {
            cbxScannerSettings.DataSource = new BindingList<string>(_settingsManager.GetSettingNames().ToList());
            cbxScannerSettings.SelectedItem = _settingsManager.GetDefaultSettingName();
        }

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
            rbtnRGB.Enabled = rbtnRGB.Checked = false;
            rbtnGray.Enabled = rbtnGray.Checked = false;
            rbtnBW.Enabled = rbtnBW.Checked = false;

            try
            {
                var _pixelTypes = _twain.Capabilities.PixelType.Get();

                for (int index = 0; index < _pixelTypes.Count; index++)
                {
                    if (_pixelTypes[index].ToString() == "RGB")
                    {
                        rbtnRGB.Enabled = true;
                        rbtnRGB.Checked = true;
                    }

                    if (_pixelTypes[index].ToString() == "Gray")
                    {
                        rbtnGray.Enabled = true;
                    }

                    if (_pixelTypes[index].ToString() == "BW")
                    {
                        rbtnBW.Enabled = true;
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
                var resolutions = _twain.Capabilities.XResolution.Get();

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

                if (cbxResolution.Items.Count > 0)
                {
                    cbxResolution.SelectedIndex = 0;
                }
            }
            catch (TwainException ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ErrorMessages.ResolutionsNotAviableError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayScannerCapability()
        {
            if ((_twain.IsCapSupported(TwCap.Duplex) & TwQC.GetCurrent) != 0)
            {
                var _duplexCapValue = (TwDX)_twain.GetCurrentCap(TwCap.Duplex);
                chkDuplex.Enabled = _duplexCapValue != TwDX.None;
            }
            else
                chkDuplex.Enabled = false;

            try
            {
                _twain.SetCap(TwCap.FeederEnabled, false);
                chkADF.Enabled = true;
            }
            catch
            {
                chkADF.Enabled = false;
            }
        }

        private void RefreshControlsState()
        {
            upDownPageBreak.Enabled = rbtnTIFF.Checked && !chkBarCode.Checked;
            chkBarCode.Enabled = rbtnTIFF.Checked;

            //Nav button states
            btnFirst.Enabled = btnPrevius.Enabled = _imageIndex > 0;
            btnNext.Enabled = btnLast.Enabled = _imageIndex < _images.Count - 1;

            textPart.Text = _images.Count > 0 ? (_imageIndex + 1).ToString() : _imageIndex.ToString();
            textTotal.Text = _images.Count.ToString();
        }

        private void ResetControlsState()
        {
            chkDuplex.Enabled = chkDuplex.Checked = false;
            chkADF.Enabled = chkADF.Checked = false;

            rbtnRGB.Enabled = rbtnRGB.Checked = false;
            rbtnGray.Enabled = rbtnGray.Checked = false;
            rbtnBW.Enabled = rbtnBW.Checked = false;
        }

        private void SetImage(Image image)
        {
            this.pictureBox.Image = null;
            this.pictureBox.Image = image;
        }

        private ImageCodecInfo GetCodecForString(string type)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < info.Length; i++)
            {
                string EnumName = type.ToString();
                if (info[i].FormatDescription.Equals(EnumName))
                {
                    return info[i];
                }
            }

            return null;

        }

        public Bitmap ConvertToBitonal(Bitmap original)
        {
            Bitmap source = null;

            // If original bitmap is not already in 32 BPP, ARGB format, then convert
            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }
            else
            {
                source = original;
            }

            // Lock source bitmap in memory
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // Copy image data to binary array
            int imageSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

            // Unlock source bitmap
            source.UnlockBits(sourceData);

            // Create destination bitmap
            Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Create destination buffer
            imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int pixelValue = 128;
            int height = source.Height;
            int width = source.Width;
            int threshold = 500;

            // Iterate lines
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                pixelValue = 128;

                // Iterate pixels
                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] + sourceBuffer[sourceIndex + 3];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte)pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }

            // Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);

            // Unlock destination bitmap
            destination.UnlockBits(destinationData);

            // Return
            return destination;
        }

        public Image CreateMultipageTiff(Image[] bmp, string path)
        {
            if (bmp != null)
            {
                try
                {
                    var codecInfo = GetCodecForString("TIFF");

                    for (int i = 0; i < bmp.Length; i++)
                    {
                        if (bmp[i] == null) break;
                        bmp[i] = (Image)ConvertToBitonal((Bitmap)bmp[i]);
                    }

                    if (bmp.Length == 1)
                    {

                        EncoderParameters iparams = new EncoderParameters(1);
                        Encoder iparam = Encoder.Compression;
                        EncoderParameter iparamPara = new EncoderParameter(iparam, (long)(EncoderValue.CompressionCCITT4));
                        iparams.Param[0] = iparamPara;
                        bmp[0].Save(path, codecInfo, iparams);

                        return bmp[0];
                    }
                    else
                        if (bmp.Length > 1)
                        {

                            Encoder saveEncoder;
                            Encoder compressionEncoder;
                            EncoderParameter SaveEncodeParam;
                            EncoderParameter CompressionEncodeParam;
                            EncoderParameters EncoderParams = new EncoderParameters(2);

                            saveEncoder = Encoder.SaveFlag;
                            compressionEncoder = Encoder.Compression;

                            // Save the first page (frame).
                            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                            CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                            EncoderParams.Param[0] = CompressionEncodeParam;
                            EncoderParams.Param[1] = SaveEncodeParam;
                            bmp[0].Save(path, codecInfo, EncoderParams);


                            for (int i = 1; i < bmp.Length; i++)
                            {
                                if (bmp[i] == null) break;

                                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                                CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                                EncoderParams.Param[0] = CompressionEncodeParam;
                                EncoderParams.Param[1] = SaveEncodeParam;
                                bmp[0].SaveAdd(bmp[i], EncoderParams);

                            }

                            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                            EncoderParams.Param[0] = SaveEncodeParam;
                            bmp[0].SaveAdd(EncoderParams);

                            return bmp[0];
                        }
                }
                catch (Exception ex)
                {
                    LogFactory.CreateLog().LogError(ex);
                    MessageBox.Show(ErrorMessages.CreateMultipageTiffError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return null;
        }

        private void CreateScannedImagesList()
        {
            _scannedItems.Clear();

            var nameIndex = 0;

            if (rbtnTIFF.Checked)
            {

                var pageIndex = 0;
                var pageCount = int.Parse(upDownPageBreak.Value.ToString());
                var imageList = (List<Image>)null;

                switch (chkBarCode.Checked)
                {
                    case true:
                        var dataMatrix = new DmtxImageDecoder();

                        for (int i = 0; i < _images.Count; i++)
                        {
                            if (pageIndex == 0)
                            {
                                imageList = new List<Image>();
                            }

                            using (var bmp = new Bitmap(_images[i]))
                            {
                                var decodeInfo = dataMatrix.DecodeImage(bmp);

                                if (decodeInfo.Count > 0 &&
                                    String.Compare(decodeInfo[0], "DOKUFLEX") == 0)
                                {
                                    var uploadDir = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);

                                    if (!Directory.Exists(uploadDir))
                                    {
                                        Directory.CreateDirectory(uploadDir);
                                    }

                                    var scannedItem = new ScannedImage();
                                    scannedItem.Name = String.Format("{0}_{1}", scannedItem.Name, ++nameIndex);
                                    scannedItem.FileType = ".tiff";
                                    scannedItem.Pages = pageIndex;
                                    scannedItem.Path = String.Format("{0}\\{1}", uploadDir,
                                    scannedItem.Name + ".tiff");
                                    scannedItem.Image = CreateMultipageTiff(imageList.ToArray(), scannedItem.Path);

                                    if (_scanSetting != null)
                                    {
                                        scannedItem.Routing.Assign(_scanSetting.Routing);
                                    }

                                    _scannedItems.Add(scannedItem);

                                    pageIndex = 0;
                                }
                                else
                                {
                                    imageList.Add(_images[i]);
                                    pageIndex++;
                                }
                            }

                        }
                        break;

                    default:
                        for (int i = 0; i < _images.Count; i++)
                        {
                            if (pageIndex == 0)
                            {
                                imageList = new List<Image>();
                            }

                            if (pageCount == 0)
                            {
                                imageList.Add(_images[i]);
                                pageIndex++;
                                continue;
                            }

                            if (pageIndex >= pageCount)
                            {
                                var uploadDir = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);

                                if (!Directory.Exists(uploadDir))
                                {
                                    Directory.CreateDirectory(uploadDir);
                                }

                                var scannedItem = new ScannedImage();
                                scannedItem.Name = String.Format("{0}_{1}", scannedItem.Name, ++nameIndex);
                                scannedItem.FileType = ".tiff";
                                scannedItem.Pages = imageList.Count;
                                scannedItem.Path = String.Format("{0}\\{1}", uploadDir,
                                scannedItem.Name + ".tiff");
                                scannedItem.Image = CreateMultipageTiff(imageList.ToArray(), scannedItem.Path);

                                if (_scanSetting != null)
                                {
                                    scannedItem.Routing.Assign(_scanSetting.Routing);
                                }

                                _scannedItems.Add(scannedItem);

                                pageIndex = 0;
                            }
                            else
                            {
                                imageList.Add(_images[i]);
                                pageIndex++;
                            }
                        }
                        break;
                }

                if (pageIndex > 0)
                {
                    var uploadDir = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);

                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var scannedItem = new ScannedImage();
                    scannedItem.Name = String.Format("{0}_{1}", scannedItem.Name, ++nameIndex);
                    scannedItem.FileType = ".tiff";
                    scannedItem.Pages = imageList.Count;
                    scannedItem.Path = String.Format("{0}\\{1}", uploadDir,
                            scannedItem.Name + ".tiff");
                    scannedItem.Image = CreateMultipageTiff(imageList.ToArray(), scannedItem.Path);

                    if (_scanSetting != null)
                    {
                        scannedItem.Routing.Assign(_scanSetting.Routing);
                    }

                    _scannedItems.Add(scannedItem);
                }
            }
            else
            {
                var fileType = String.Empty;

                if (rbtnBMP.Checked)
                {
                    fileType = ".bmp";
                }

                if (rbtnJPG.Checked)
                {
                    fileType = ".jpeg";
                }

                if (rbtnPNG.Checked)
                {
                    fileType = ".png";
                }

                for (int i = 0; i < _images.Count; i++)
                {
                    var scannedItem = new ScannedImage();
                    scannedItem.Name = String.Format("{0}_{1}", scannedItem.Name, ++nameIndex);
                    scannedItem.Image = _images[i];
                    scannedItem.FileType = fileType;

                    if (_scanSetting != null)
                    {
                        scannedItem.Routing.Assign(_scanSetting.Routing);
                    }

                    _scannedItems.Add(scannedItem);
                }
            }
        }

        private bool HasErrors()
        {
            var result = false;

            if (String.IsNullOrWhiteSpace(cbxScannerSettings.Text))
            {
                errorProvider.SetError(cbxScannerSettings, "El perfil no es valido");
                result = true;
            }
            else
            {
                errorProvider.SetError(cbxScannerSettings, String.Empty);
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

            if (_scanSetting == null)
            {
                errorProvider.SetError(rbtnTIFF, "No hay perfil seleccionado");
                return true;
            }
            else
            {
                errorProvider.SetError(cbxScanner, String.Empty);
            }

            var documentary = _scanSetting.Routing.Homologation;

            if (_scanSetting.Routing.Homologation == 1)
            {
                if (rbtnBW.Checked)
                {
                    errorProvider.SetError(rbtnBW, String.Empty);
                }
                else
                {
                    errorProvider.SetError(rbtnBW, "El perfil seleccionado requiere que el formato de color sea Blanco y negro");
                    result = true;
                }

                if (rbtnTIFF.Checked)
                {
                    errorProvider.SetError(rbtnTIFF, String.Empty);
                }
                else
                {
                    errorProvider.SetError(rbtnTIFF, "El perfil seleccionado requiere que el tipo de archivo sea TIFF");
                    result = true;
                }
            }

            return result;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            btnScan.Image = ImageResources.ScannerLarge;
            btnPreview.Image = ImageResources.PreviewLarge;
            btnFirst.Image = ImageResources.NavFirstSmall;
            btnPrevius.Image = ImageResources.NavPreviousSmall;
            btnNext.Image = ImageResources.NavNextSmall;
            btnLast.Image = ImageResources.NavLastSmall;

            ResetControlsState();
            RefreshControlsState();
            DisplayScanners();
            DisplayScannerSettings();
        }

        public List<ScannedImage> ScannedImages
        {
            get
            {
                return _scannedItems;
            }
        }

        public NewScanForm()
        {
            InitializeComponent();

            _container = new ServiceContainer();
            _container.Bind(typeof(IStreamProvider), typeof(SaraffStreamProvider));
            _twain = _container.CreateInstance<Twain32>();
            _twain.EndXfer += EndXfer;
            _twain.AcquireCompleted += AcquireCompleted;
            _imageIndex = 0;
            _images = new List<Image>();
            _scannedItems = new List<ScannedImage>();
            _settingsManager = new ScanSettingsManager();
            _previewing = false;
            _closingWindow = false;
        }

        private void AcquireCompleted(object sender, EventArgs e)
        {
            if (_closingWindow) return;

            this.Cursor = Cursors.Default;

            if (!_previewing)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            btnScan.Enabled = true;
            btnPreview.Enabled = true;
            btnCancel.Enabled = true;
            RefreshControlsState();
        }

        private void EndXfer(object sender, Twain32.EndXferEventArgs e)
        {
            if (_closingWindow) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (e.Image != null)
                {
                    _images.Add(e.Image);
                    _imageIndex = _images.Count - 1;
                    SetImage(e.Image);
                }
            }
            catch (Exception ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ErrorMessages.AsyncTaskError, "Doku4Invoices", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
                MessageBox.Show(ErrorMessages.ResolutionsNotAviableError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DisplayScannerCapability();
            DisplayResolutions();
            DisplayColorFormats();
            RefreshControlsState();
        }

        private void rbtnTIFF_CheckedChanged(object sender, EventArgs e)
        {
            RefreshControlsState();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (HasErrors()) return;

            SetImage(null);
            _images.Clear();

            try
            {
                if (cbxResolution.SelectedItem != null)
                {
                    var resolution = float.Parse(cbxResolution.SelectedItem.ToString());
                    _twain.Capabilities.XResolution.Set(resolution);
                    _twain.Capabilities.YResolution.Set(resolution);
                }

                if (rbtnRGB.Checked)
                {
                    _twain.Capabilities.PixelType.Set(Saraff.Twain.TwPixelType.RGB);
                }

                if (rbtnBW.Checked)
                {
                    _twain.Capabilities.PixelType.Set(Saraff.Twain.TwPixelType.BW);
                }

                if (rbtnGray.Checked)
                {
                    _twain.Capabilities.PixelType.Set(Saraff.Twain.TwPixelType.Gray);
                }

                if (chkDuplex.Enabled)
                {
                    if (chkDuplex.Checked)
                    {
                        if ((_twain.IsCapSupported(TwCap.FeederEnabled) & TwQC.Set) != 0)
                        {
                            _twain.SetCap(TwCap.FeederEnabled, true);

                            if ((_twain.IsCapSupported(TwCap.XferCount) & TwQC.Set) != 0)
                            {
                                _twain.SetCap(TwCap.XferCount, (short)-1);
                            }

                            if ((_twain.IsCapSupported(TwCap.DuplexEnabled) & TwQC.Set) != 0)
                            {
                                _twain.SetCap(TwCap.DuplexEnabled, true);
                            }
                        }
                    }
                }

                if (chkADF.Enabled)
                {
                    if (chkADF.Checked)
                        _twain.SetCap(TwCap.FeederEnabled, true);
                    else
                        _twain.SetCap(TwCap.FeederEnabled, false);
                }

                _previewing = false;
                _twain.Acquire();
            }
            catch (TwainException ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.WaitCursor;

            btnScan.Enabled = false;
            btnPreview.Enabled = false;
            btnCancel.Enabled = false;
            RefreshControlsState();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (HasErrors()) return;

            SetImage(null);
            _images.Clear();

            try
            {
                if (cbxResolution.SelectedItem != null)
                {
                    var resolution = float.Parse(cbxResolution.SelectedItem.ToString());
                    _twain.Capabilities.XResolution.Set(resolution);
                    _twain.Capabilities.YResolution.Set(resolution);
                }

                if (rbtnRGB.Checked)
                {
                    _twain.Capabilities.PixelType.Set(Saraff.Twain.TwPixelType.RGB);
                }

                if (rbtnBW.Checked)
                {
                    _twain.Capabilities.PixelType.Set(Saraff.Twain.TwPixelType.BW);
                }

                if (rbtnGray.Checked)
                {
                    _twain.Capabilities.PixelType.Set(Saraff.Twain.TwPixelType.Gray);
                }

                if (chkDuplex.Enabled)
                {
                    if (chkDuplex.Checked)
                    {
                        if ((_twain.IsCapSupported(TwCap.FeederEnabled) & TwQC.Set) != 0)
                        {
                            _twain.SetCap(TwCap.FeederEnabled, true);

                            if ((_twain.IsCapSupported(TwCap.XferCount) & TwQC.Set) != 0)
                            {
                                _twain.SetCap(TwCap.XferCount, (short)-1);
                            }

                            if ((_twain.IsCapSupported(TwCap.DuplexEnabled) & TwQC.Set) != 0)
                            {
                                _twain.SetCap(TwCap.DuplexEnabled, true);
                            }
                        }
                    }
                }

                if (chkADF.Enabled)
                {
                    if (chkADF.Checked)
                        _twain.SetCap(TwCap.FeederEnabled, true);
                    else
                        _twain.SetCap(TwCap.FeederEnabled, false);
                }

                _previewing = true;
                _twain.Acquire();
            }
            catch (TwainException ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.WaitCursor;

            btnScan.Enabled = false;
            btnPreview.Enabled = false;
            btnCancel.Enabled = false;
            RefreshControlsState();
        }

        private void btnPrevius_Click(object sender, EventArgs e)
        {
            _imageIndex--;
            SetImage(_images[_imageIndex]);
            RefreshControlsState();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            _imageIndex = 0;
            SetImage(_images[0]);
            RefreshControlsState();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _imageIndex++;
            SetImage(_images[_imageIndex]);
            RefreshControlsState();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            _imageIndex = _images.Count - 1;
            SetImage(_images[_images.Count - 1]);
            RefreshControlsState();
        }

        private void cbxScannerSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxScannerSettings.SelectedItem == null) return;

            _scanSetting = _settingsManager.GetSettingByName(cbxScannerSettings.SelectedItem.ToString());

            lbHomologation.Visible = _scanSetting.Routing.Homologation == 1;

            cbxScanner.SelectedItem = _scanSetting.Scanner;
            cbxResolution.SelectedItem = _scanSetting.Resolution.ToString();

            switch (_scanSetting.ColorFormat)
            {
                case "Color":
                    rbtnRGB.Checked = true;
                    break;

                case "Escala de grises":
                    rbtnGray.Checked = true;
                    break;

                case "Blanco y negro":
                    rbtnBW.Checked = true;
                    break;

                default:
                    break;
            }

            switch (_scanSetting.FileType)
            {
                case ".tiff":
                    rbtnTIFF.Checked = true;
                    break;

                case ".jpeg":
                    rbtnJPG.Checked = true;
                    break;

                case ".bmp":
                    rbtnBMP.Checked = true;
                    break;

                case ".png":
                    rbtnPNG.Checked = true;
                    break;

                default:
                    break;
            }
        }

        private void NewScanForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (this.DialogResult == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    CreateScannedImagesList();
                }
                catch (Exception ex)
                {
                    LogFactory.CreateLog().LogError(ex);
                    MessageBox.Show(ErrorMessages.AsyncTaskError, "Doku4Invoices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                _closingWindow = true;
            }

            _twain.CloseDSM();
        }

        private void btnPrintBarCode_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "DOKUFLEX_DTMX.png";
            p.StartInfo.Verb = "Print";
            p.Start();
        }
    }
}
