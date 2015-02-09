using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPDFReport.Models
{
    /// <summary>
    /// ProviderList model which extends Provider collection
    /// </summary>
    public class ProviderList : List<Provider>
    {
        public string LogoImage { get; set; }
        public string EcgImage { get; set; }
    }
}