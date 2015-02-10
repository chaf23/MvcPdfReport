using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharpReportGenerator
{
    public class DrawPdfPage
    {
        private int YPos { get; set; }
        
        private int XBeginningPos { get; set; }
        private int XEndPos { get; set; }
        private int YBeginningPos { get; set; }
        private int YEndPost { get; set; }

        public DrawPdfPage()
        {
            //Page Margins
            XBeginningPos = 40;
            YBeginningPos = 40;
            XEndPos = 555;
            YEndPost = 802;
        }

        public void Generate(Document doc, PdfWriter pdfWriter, string base64String)
        {
            var cb = pdfWriter.DirectContent;
            DocumentHeader(cb);

            for (var cnt = 0; cnt < 30; cnt++)
            {
                if (YPos < YBeginningPos)
                {
                    doc.NewPage();
                    DocumentHeader(cb);
                }

                cb.AddImage(ConvertBase64ToElement(base64String));
                LineTimeAndLead(cb);

            }

        }

        private void DocumentHeader(PdfContentByte cb)
        {
            YPos = 678;

            //Vertical Line
            cb.MoveTo(296, YPos);
            cb.LineTo(296, YEndPost);
            cb.Stroke();

            //Horizontal Line
            cb.MoveTo(XBeginningPos, YPos);
            cb.LineTo(XEndPos, YPos);
            cb.Stroke();
            var text = "Date:" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            var textAlignedPdf = new TextAlignedPdfConfig
                                            {
                                                Alignment = 0,
                                                Text = text,
                                                X = 50,
                                                Y = YPos += 18,
                                                Rotation = 0
                                            };

            PdfText(cb, textAlignedPdf);

            YPos -= 123;
        }

        private Image ConvertBase64ToElement(string base64String)
        {
            Image img = null;

            try
            {
                //  Convert base64string to bytes array
                var bytes = Convert.FromBase64String(base64String);
                img = Image.GetInstance(bytes);

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
                //Scale to size
                img.ScaleAbsolute(XEndPos - XBeginningPos, 104);
                //XY Coordinates
                img.SetAbsolutePosition(XBeginningPos, YPos);
            }

            return img;
        }

        private void LineTimeAndLead(PdfContentByte cb)
        {
            YPos -= 1;

            //Top Border Line
            cb.MoveTo(XBeginningPos, YPos);
            cb.LineTo(XEndPos, YPos);
            cb.Stroke();

            //Time: & Lead: Text
            TimeLeadText(cb);

            //Bottom Border Line
            YPos -= 46;
            cb.MoveTo(XBeginningPos, YPos);
            cb.LineTo(XEndPos, YPos);
            cb.Stroke();

            YPos -= 106;
        }

        private void TimeLeadText(PdfContentByte cb)
        {
            YPos -= 58;
            var textAlignedPdf = new TextAlignedPdfConfig
            {
                Alignment = 0,
                Rotation = 0,
                Y = YPos
            };

            var text = "Lead:";
            textAlignedPdf.Text = text;
            textAlignedPdf.X = 320;
            PdfText(cb, textAlignedPdf);

            text = "Time:";
            textAlignedPdf.Text = text;
            textAlignedPdf.X = 50;
            PdfText(cb, textAlignedPdf);
        }

        private static void PdfText(PdfContentByte cb, TextAlignedPdfConfig parms)
        {
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(bf, 18);

            cb.BeginText();
            cb.ShowTextAligned(parms.Alignment, parms.Text, parms.X, parms.Y, parms.Rotation);
            cb.EndText();
        }

        private class TextAlignedPdfConfig
        {
            public int Alignment { get; set; }
            public string Text { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
            public float Rotation { get; set; }
        }
    }
}
