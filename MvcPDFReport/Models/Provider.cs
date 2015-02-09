using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPDFReport.Models
{
    /// <summary>
    /// Provider model
    /// </summary>
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
    }
}