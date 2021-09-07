using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Organizacija
    {
        [DisplayName("Salis")]
        public string salis { get; set; }
        [DisplayName("Pavadinimas")]
        [Required]
        public string pavadinimas { get; set; }

        [DisplayName("Id")]
        [Required]
        public string id { get; set; }
    }
}