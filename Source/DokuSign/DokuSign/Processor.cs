using System;
using System.IO;
using System.Collections.Generic;
using PrinterPlusPlusSDK;
using System.Windows.Forms;
using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using System.Drawing;
using System.Drawing.Imaging;
using DokuFlex.Windows.Common;
using System.Globalization;
using System.Threading;

namespace DokuSign
{
    public class Processor : IProcessor
    {
        private void SetUILanguage()
        {
            var uiLanguage = ConfigurationManager.GetValue("UILanguage");

            switch (uiLanguage)
            {
                case "Spanish":

                case "Español":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                    break;

                default:
                    break;
            }
        }

        private void ConvertPsToPdf(string inputFile, string outputFile)
        {
            GhostscriptVersionInfo gv = GhostscriptVersionInfo.GetLastInstalledVersion();

            using (GhostscriptProcessor processor = new GhostscriptProcessor(gv, true))
            {
                List<string> switches = new List<string>();
                switches.Add("-empty");
                switches.Add("-dQUIET");
                switches.Add("-dSAFER");
                switches.Add("-dBATCH");
                switches.Add("-dNOPAUSE");
                switches.Add("-dNOPROMPT");
                switches.Add("-sDEVICE=pdfwrite");
                switches.Add("-dCompatibilityLevel=1.4");
                switches.Add("-sOutputFile=" + outputFile);
                switches.Add("-c");
                switches.Add("-f");
                switches.Add(inputFile.ToString());

                processor.StartProcessing(switches.ToArray(), null);
            }
        }

        private void ConvertPdfToPng(string inputPdfPath, string outputPath)
        {
            int desired_x_dpi = 96;
            int desired_y_dpi = 96;

            GhostscriptVersionInfo gv = GhostscriptVersionInfo.GetLastInstalledVersion();

            using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(inputPdfPath, gv, false);

                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                {
                    string pageFilePath = Path.Combine(outputPath, "Page-" + pageNumber.ToString() + ".png");

                    Image img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                    img.Save(pageFilePath, ImageFormat.Png);
                }
            }
        }

        public ProcessResult Process(string key, string psFilename)
        {
            SetUILanguage();

            var fileName = DateTime.Now.ToString("s").Replace(":", String.Empty);
            var documentDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

            if (!Directory.Exists(documentDir))
            {
                try
                {
                    Directory.CreateDirectory(documentDir);
                }
                catch (Exception)
                {
                    MessageBox.Show(ErrorMessages.ApplicationError);
                    return new ProcessResult();
                }
            }

            var outputPdf = String.Format("{0}\\{1}.pdf", documentDir, fileName);

            try
            {
                ConvertPsToPdf(psFilename, outputPdf);
                ConvertPdfToPng(outputPdf, documentDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing documents", "Doku4Signatures", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new ProcessResult(ex);
            }

            int signatureIndex = -1;
            int.TryParse(ConfigurationManager.GetValue(Constants.SignatureType), out signatureIndex);

            if (signatureIndex == -1)
                using (var sigTypeForm = new SigTypeForm())
                {
                    sigTypeForm.SignatureIndex = signatureIndex;

                    if (sigTypeForm.ShowDialog() == DialogResult.OK)
                    {
                        signatureIndex = sigTypeForm.SignatureIndex;
                        ConfigurationManager.SetValue(Constants.SignatureType, signatureIndex.ToString());
                        ConfigurationManager.Save();
                    }

                }

            try
            {
                using (var form = new MainForm(psFilename, outputPdf, documentDir))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                return new ProcessResult(ex);
            }

            return new ProcessResult();
        }
    }
}
