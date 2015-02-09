using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPDFReport.Models
{
    public class PatientModel
    {
        public string Name { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public int? Mrn { get; set; }
        public string EcgImage { get; set; }
    }
}