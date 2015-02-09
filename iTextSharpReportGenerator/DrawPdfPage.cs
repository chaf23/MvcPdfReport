using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharpReportGenerator
{
    public class DrawPdfPage
    {
        private int YPos { get; set; }

        public void Generate(PdfWriter pdfWriter, string base64String)
        {
            var cb = pdfWriter.DirectContent;
            DocumentHeader(cb);
            YPos = 527;
            for (int cnt = 0; cnt < 3; cnt++)
            {
                cb.AddImage(ConvertBase64ToElement(base64String));
                LineTimeAndLead(cb);
            }

        }

        private void DocumentHeader(PdfContentByte cb)
        {
            //Vertical Line
            cb.MoveTo(296, 632);
            cb.LineTo(296, 842);
            cb.Stroke();
            //Horizontal Line
            cb.MoveTo(0, 632);
            cb.LineTo(595, 632);
            cb.Stroke();

            //Date:
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 24);

            cb.BeginText();
            var text = "Date:" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            cb.ShowTextAligned(0, text, 50, 650, 0);
            cb.EndText();
        }

        private Image ConvertBase64ToElement(string base64String)
        {
            Image img = null;

            try
            {
                //  Convert base64string to bytes array
                Byte[] bytes = Convert.FromBase64String(base64String);
                img = iTextSharp.text.Image.GetInstance(bytes);

            }
            catch (DocumentException dex)
            {
                //log exception here
            }
            catch (IOException ioex)
            {
                //log exception here
            }
            if (img != null)
            {
                img.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                img.Border = iTextSharp.text.Rectangle.NO_BORDER;
                img.BorderColor = iTextSharp.text.BaseColor.WHITE;
                img.ScaleToFit(595f, 104f);
                img.SetAbsolutePosition(50, YPos);
            }

            return img;
        }

        private void LineTimeAndLead(PdfContentByte cb)
        {
            YPos -= 1;
            //Top Border Line
            cb.MoveTo(0, YPos);
            cb.LineTo(595, YPos);
            cb.Stroke();

            //Time: Lead: Text
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(bf, 24);
            YPos -= 58;
            cb.BeginText();
            var text = "Lead:";
            cb.ShowTextAligned(0, text, 320, YPos, 0);
            cb.EndText();
            cb.BeginText();
            text = "Time:";
            cb.ShowTextAligned(0, text, 50, YPos, 0);
            cb.EndText();

            //Bottom Border Line
            YPos -= 46;
            cb.MoveTo(0, YPos);
            cb.LineTo(595, YPos);
            cb.Stroke();

            YPos -= 106;
        }
    }
}
