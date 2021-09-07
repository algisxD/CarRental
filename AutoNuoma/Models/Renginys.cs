using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Renginys
    {
        [DisplayName("Pavadinimas")]
        public string pavadinimas { get; set; }

        [DisplayName("Tipas")]
        public string tipas { get; set; }

        [DisplayName("Bilieto kaina")]
        public string bilietoKaina { get; set; }

        [DisplayName("ID")]
        public string id { get; set; }

        //Arena
        [DisplayName("Arena")]
        public virtual Arena arena { get; set; }

    }
}