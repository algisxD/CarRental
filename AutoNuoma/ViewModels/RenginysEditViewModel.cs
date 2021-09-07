using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoNuoma.ViewModels
{
    public class RenginysEditViewModel
    {
        [DisplayName("Pavadinimas")]
        [Required]
        public string pavadinimas { get; set; }

        [DisplayName("Tipas")]
        [Required]
        public string tipas { get; set; }

        [DisplayName("Bilieto kaina")]
        [Required]
        public string bilietoKaina { get; set; }

        [DisplayName("ID")]
        public string id { get; set; }

        //Arena
        [DisplayName("Arena")]
        [Required]
        public string fk_arena { get; set; }

        //Arenu sąrašas pasirinkimui
        public IList<SelectListItem> ArenaList { get; set; }
    }
}