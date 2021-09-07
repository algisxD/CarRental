using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Zaidimas
    {
        [DisplayName("Pavadinimas")]
        [Required]
        public string pavadinimas { get; set; }
        [DisplayName("Zanras")]
        [MaxLength(20)]
        [Required]
        public string zanras { get; set; }
        [DisplayName("Versija")]
        [MaxLength(20)]
        [Required]
        public string versija { get; set; }
        [DisplayName("Kurejas")]
        [MaxLength(20)]
        [Required]
        public string kurejas { get; set; }
        [DisplayName("Id")]
        [MaxLength(20)]
        [Required]
        public string id { get; set; }
    }
}