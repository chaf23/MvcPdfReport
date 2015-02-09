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

        private static byte[] RenderStream(string htmlText, string pageTitle)
        {
            byte[] renderedBuffer;

            //string filePath = HostingEnvironment.MapPath("~/Content/Pdf/");
            //FileStream fs = new FileStream(filePath + "\\pdf-" + "Test.pdf", FileMode.Create);

            using (var outputMemoryStream = new MemoryStream())
            {
                using (var pdfDocument = new Document(PageSize.A4, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };
                    pdfDocument.Open();
                    using (var htmlViewReader = new StringReader(htmlText))
                    {
                        using (var htmlWorker = new HTMLWorker(pdfDocument))
                        {
                            htmlWorker.Parse(htmlViewReader);
                        }
                    }
                }

                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }

        private byte[] RenderPdf(string htmlText, string pageTitle, string ecgImage)
        {
            byte[] renderedBuffer;
            string filePath = HostingEnvironment.MapPath("~/Content/Pdf/");

            using (var outputMemoryStream = new FileStream(filePath + "\\pdf-" + "Test.pdf", FileMode.Create))
            {
                using (var pdfDocument = new Document(PageSize.A4, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };
                    pdfDocument.Open();
                    using (var htmlViewReader = new StringReader(htmlText))
                    {
                        using (var htmlWorker = new HTMLWorker(pdfDocument))
                        {
                            htmlWorker.Parse(htmlViewReader);
                        }
                    }
                    pdfDocument.Add(ConvertBase64ToElement(ecgImage));
                }

                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }

        private IElement ConvertBase64ToElement(string base64String)
        {
            Image gif = null;

            try
            {
                //  Convert base64string to bytes array
                Byte[] bytes = Convert.FromBase64String(base64String);
                gif = iTextSharp.text.Image.GetInstance(bytes);

            }
            catch (DocumentException dex)
            {
                //log exception here
            }
            catch (IOException ioex)
            {
                //log exception here
            }
            if (gif != null)
            {
                gif.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                gif.Border = iTextSharp.text.Rectangle.NO_BORDER;
                gif.BorderColor = iTextSharp.text.BaseColor.WHITE;
                gif.ScaleToFit(170f, 100f);
            }

            return gif;
        }
    }
}
