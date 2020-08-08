using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACarWebService.Models
{
    public class Rezervasyon
    {
        public int RezervasyonID { get; set; }
        public int AracID { get; set; }
        public int KullaniciID { get; set; }
        public string AracMarka { get; set; }
        public string AracModel { get; set; }
        public string Image { get; set; }
        public DateTime AlisTarihi { get; set; }
        public DateTime VerisTarihi { get; set; }
        public string AlisYeri { get; set; }
        public string VerisYeri { get; set; }


    }
}