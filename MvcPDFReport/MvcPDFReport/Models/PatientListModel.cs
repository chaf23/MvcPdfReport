using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPDFReport.Models
{
    public class PatientListModel : List<PatientModel>
    {
        public string LogoImage { get; set; }
        public string EcgImage { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}