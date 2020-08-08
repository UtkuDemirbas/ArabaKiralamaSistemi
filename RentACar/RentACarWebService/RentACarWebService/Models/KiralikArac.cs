using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACarWebService.Models
{
    public class KiralikArac
    {
        public int KiralikAracID { get; set; }
        public int AracID { get; set; }
        public int KullaniciID { get; set; }
        public string AracMarka { get; set; }
        public string AracModel { get; set; }
        public string Image { get; set; }
        public DateTime KiralanmaZamani { get; set; }
        public int VerilisKm { get; set; }
        public int SonKilometre { get; set; }
        public decimal Ucret { get; set; }
        public int KacGun { get; set; }
        public string AlisYeri { get; set; }
        public string VerisYeri { get; set; }


    }
}