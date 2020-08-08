using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACarWebService.Models
{
    public class Filtre
    {
        public string Marka { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string AracTipi { get; set; }
        public string AracSinifi { get; set; }
        public string YakitTuru { get; set; }
        public string AracYili { get; set; }
    }
}