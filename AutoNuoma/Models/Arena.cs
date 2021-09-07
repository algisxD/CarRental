using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Arena
    {
        [DisplayName("Pavadinimas")]
        [Required]
        public string pavadinimas { get; set; }


        [DisplayName("Salis")]
        [Required]
        public string salis { get; set; }


        [DisplayName("Adresas")]
        [Required]
        public string adresas { get; set; }

        [DisplayName("Vietu skaicius")]
        [Required]
        public string vietuSkaicius { get; set; }


        [DisplayName("Id")]
        [Required]
        public string id { get; set; }
    }
}