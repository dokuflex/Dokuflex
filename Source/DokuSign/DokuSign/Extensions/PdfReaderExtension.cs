using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace DokuSign.Extensions
{
    public static class PdfReaderExtension
    {
        public static string GetDocumentTagValue(this PdfReader pdfReader, string tag, int valueLength)
        {
            string result = String.Empty;

            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                int index = currentText.IndexOf(tag);

                if (index > -1)
                {
                    int start = index + tag.Length;

                    if (currentText.Length > start + valueLength)
                    {
                        result = currentText.Substring(start, valueLength);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
