using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharpReportGenerator
{
    /// <summary>
    /// Extends the controller with functionality for rendering PDF views
    /// </summary>
    public class PdfViewController : Controller
    {
        private readonly HtmlViewRenderer _htmlViewRenderer;
        private readonly StandardPdfRenderer _standardPdfRenderer;

        public PdfViewController()
        {
            this._htmlViewRenderer = new HtmlViewRenderer();
            this._standardPdfRenderer = new StandardPdfRenderer();
        }

        protected ActionResult ViewPdf(string pageTitle, string viewName, object model, string ecgImage)
        {
            // Render the view html to a string
            var htmlText = this._htmlViewRenderer.RenderViewToString(this, viewName, model);

            // Let the html be rendered into a PDF document through iTextSharp
            byte[] buffer = _standardPdfRenderer.Render(htmlText, pageTitle, ecgImage);

            // Return the PDF as a binary stream to the client
            return new BinaryContentResult(buffer, "application/pdf");
        }
    }

    ///// <summary>
    ///// Extends the controller with functionality for rendering PDF views
    ///// </summary>
    //public class PdfViewController : Controller
    //{
    //    private readonly HtmlViewRenderer _htmlViewRenderer;
    //    private readonly StandardPdfRenderer _standardPdfRenderer;

    //    public PdfViewController()
    //    {
    //        this._htmlViewRenderer = new HtmlViewRenderer();
    //        this._standardPdfRenderer = new StandardPdfRenderer();
    //    }

    //    protected ActionResult ViewPdf(string pageTitle, string viewName, object model)
    //    {
    //        // Render the view html to a string
    //        var htmlText = this._htmlViewRenderer.RenderViewToString(this, viewName, model);
    //        Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
            
    //        //Save Pdf to local file
    //        string filePath = HostingEnvironment.MapPath("~/Content/Pdf/");
    //        FileStream fs = new FileStream(filePath + "\\pdf-" + "Test.pdf", FileMode.Create);
                
    //        PdfWriter.GetInstance(doc, fs);
    //        doc.Open();
    //        iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
    //        hw.Parse(new StringReader(htmlText));

    //        // Let the html be rendered into a PDF document through iTextSharp
    //        byte[] buffer = _standardPdfRenderer.Render(htmlText, pageTitle);

    //        // Return the PDF as a binary stream to the client
    //        return new BinaryContentResult(buffer, "application/pdf");
    //    }
    //}

    //public class CustomImageTagProcessor : iTextSharp.tool.xml.html.Image
    //{
    //    public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
    //    {
    //        IDictionary<string, string> attributes = tag.Attributes;
    //        string src;
    //        if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
    //            return new List<IElement>(1);

    //        if (string.IsNullOrEmpty(src))
    //            return new List<IElement>(1);

    //        if (src.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase))
    //        {
    //            // data:[<MIME-type>][;charset=<encoding>][;base64],<data>
    //            var base64Data = src.Substring(src.IndexOf(",") + 1);
    //            var imagedata = Convert.FromBase64String(base64Data);
    //            var image = iTextSharp.text.Image.GetInstance(imagedata);

    //            var list = new List<IElement>();
    //            var htmlPipelineContext = GetHtmlPipelineContext(ctx);
    //            list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
    //            return list;
    //        }
    //        else
    //        {
    //            return base.End(ctx, tag, currentContent);
    //        }
    //    }
    //}
}
