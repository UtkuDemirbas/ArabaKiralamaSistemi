using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACarWebService.Models
{
    public class Kullanici
    {
        public int KullaniciID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcNumarasi { get; set; }
        public string Eposta { get; set; }
        public string Username { get; set; }
        public string Sifre { get; set; }
        public string Tel { get; set; }
        public int RoleID { get; set; }
        public int AdresID { get; set; }
    }
}