using System;
using System.IO;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace iTextSharpReportGenerator
{
    /// <summary>
    /// This class is responsible for rendering a html text string to a PDF 
    /// document using the html renderer of iTextSharp
    /// </summary>
    public class StandardPdfRenderer
    {
        private const int HorizontalMargin = 40;
        private const int VerticalMargin = 40;

        public byte[] Render(string htmlText, string pageTitle, string ecgImage)
        {
            return RenderPdf(htmlText, pageTitle, ecgImage);
        }

        //private static byte[] RenderStream(string htmlText, string pageTitle)
        //{
        //    byte[] renderedBuffer;

        //    using (var outputMemoryStream = new MemoryStream())
        //    {
        //        using (var pdfDocument = new Document(PageSize.A4, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
        //        {
        //            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
        //            pdfWriter.CloseStream = false;
        //            pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };
        //            pdfDocument.Open();
        //            using (var htmlViewReader = new StringReader(htmlText))
        //            {
        //                using (var htmlWorker = new HTMLWorker(pdfDocument))
        //                {
        //                    htmlWorker.Parse(htmlViewReader);
        //                }
        //            }
        //        }

        //        renderedBuffer = new byte[outputMemoryStream.Position];
        //        outputMemoryStream.Position = 0;
        //        outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
        //    }

        //    return renderedBuffer;
        //}

        private byte[] RenderPdf(string htmlText, string pageTitle, string ecgImage)
        {
            byte[] renderedBuffer;
            string filePath = HostingEnvironment.MapPath("~/Content/Pdf/");

            using (var outputMemoryStream = new FileStream(filePath + "\\pdf-" + "Test.pdf", FileMode.Create))
            {
                using (var doc = new Document(PageSize.A4))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(doc, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    doc.Open();
                    DrawPdfPage drawPdfPage = new DrawPdfPage();
                    drawPdfPage.Generate(pdfWriter, ecgImage);
                }

                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }
    }
}
