using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharpReportGenerator
{
    public class EcgImageSnippet
    {
        public void CreatePDF(string base64Image)
        {
            //path you want to store PDF 
            string pdfPath = string.Format(@"C:\PDF\{0}.pdf", DateTime.Now.ToString("yyyy-MM-dd hhmmss"));

            using (FileStream msReport = new FileStream(pdfPath, FileMode.Create))
            {
                //step 1
                using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 140f, 30f))
                {

                    // step 2
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);

                    //open the stream 
                    pdfDoc.Open();

                    iTextSharp.text.Image gif = null;

                    string base64String = base64Image;
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
                    gif.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                    gif.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    gif.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    gif.ScaleToFit(170f, 100f);

                    pdfDoc.Add(gif);

                    pdfDoc.Close();

                }
            }
        }
    }
}