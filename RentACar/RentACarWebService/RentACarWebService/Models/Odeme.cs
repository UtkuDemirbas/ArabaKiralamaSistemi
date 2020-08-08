using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACarWebService.Models
{
    public class Odeme
    {
        public int OdemeID { get; set; }
        public decimal Tutar { get; set; }
        public int KullaniciID { get; set; }

    }
}