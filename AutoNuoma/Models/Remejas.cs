using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Remejas
    {
        [DisplayName("Pavadinimas")]
        [MaxLength(10)]
        [Required]
        public string pavadinimas { get; set; }
        [DisplayName("Salis")]
        [MaxLength(20)]
        [Required]
        public string salis { get; set; }
        [DisplayName("Suma")]
        [MaxLength(20)]
        [Required]
        public string suma { get; set; }
        [DisplayName("Id")]
        [MaxLength(20)]
        [Required]
        public string id { get; set; }
    }
}