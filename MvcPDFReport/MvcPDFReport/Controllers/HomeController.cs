using System;
using System.Web.Mvc;
using MvcPDFReport.Models;
using iTextSharpReportGenerator;

namespace MvcPDFReport.Controllers
{
    /// <summary>
    /// Extends the PdfViewController with functionality for rendering PDF views
    /// </summary>
    public class HomeController : PdfViewController
    {
        public ActionResult PrintProviders(PatientModel patient)
        {
            //var providerList = CreateCustomerList();
            var ecgImageList = CreateEcgImageList(patient);
            FillImageUrl(ecgImageList, "Bears.jpg", patient.EcgImage);
            return ViewPdf("RHYTHM STRIPS", "PdfReport", ecgImageList, patient.EcgImage);
        }

        #region Private Methods

        private void FillImageUrl(PatientListModel providerList, string logoName, string ecgImage)
        {
            if (Request.Url == null) return;
            var url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            providerList.LogoImage = url + "Content/" + logoName;
            providerList.EcgImage = ecgImage;
        }

        private PatientListModel CreateEcgImageList(PatientModel patient)
        {
            return new PatientListModel()
                       {
                           new PatientModel { Address = patient.Address, Dob = patient.Dob, Mrn = patient.Mrn, Name = patient.Name}
                       };
        }

        //private ProviderList CreateCustomerList()
        //{
        //    return new ProviderList()
        //               {
        //                   new Provider {Id = 1, Name = "Inexis Consulting", Address = "Colombo 4", Place = "Sri Lanka"},
        //                   new Provider {Id = 2, Name = "Microsoft", Address = "Washington", Place = "USA"},
        //                   new Provider {Id = 3, Name = "IBM", Address = "Armonk, New York", Place = "USA"},
        //                   new Provider {Id = 4, Name = "HP", Address = "California", Place = "USA"},
        //                   new Provider {Id = 5, Name = "Novell", Address = "Provo,Utah", Place = "USA"},
        //                   new Provider {Id = 6, Name = "Google", Address = "California", Place = "USA"},
        //                   new Provider {Id = 7, Name = "Oracle", Address = "Redwood City", Place = "USA"},
        //                   new Provider {Id = 8, Name = "Apple", Address = "California", Place = "USA"},
        //                   new Provider {Id = 9, Name = "SAP", Address = "Walldorf", Place = "Germany"},
        //               };
        //}

        #endregion
    }
}
