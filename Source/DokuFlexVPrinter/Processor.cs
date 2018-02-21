using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrinterPlusPlusSDK;
using System.Windows.Forms;
using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using System.Drawing;
using System.Drawing.Imaging;

namespace DokuFlexVPrinter
{
    public class Processor : IProcessor
    {
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

                    Console.WriteLine(pageFilePath);
                }
            }
        }

        public ProcessResult Process(string key, string psFilename)
        {
            var fileName = DateTime.Now.ToString("s").Replace(":", String.Empty);
            var dirName = Path.Combine(System.IO.Path.GetTempPath(), fileName);

            if (!Directory.Exists(dirName))
            {
                try
                {
                    Directory.CreateDirectory(dirName);
                }
                catch (Exception)
                {
                    MessageBox.Show(ErrorMessages.ApplicationError);
                    return new ProcessResult();
                }
            }

            var outputPdfFile = String.Format("{0}\\{1}.pdf", dirName, fileName);

            ConvertPsToPdf(psFilename, outputPdfFile);
            ConvertPdfToPng(outputPdfFile, dirName);

            using (var form = new MainForm2(psFilename, outputPdfFile, dirName))
            {
                form.ShowDialog();
            }      

            return new ProcessResult();
        }
    }
}
