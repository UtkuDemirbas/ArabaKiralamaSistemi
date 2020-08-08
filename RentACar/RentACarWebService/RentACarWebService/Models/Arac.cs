using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACarWebService.Models
{
    public  class Arac
    {
        public int AracID { get; set; }
        public string AracMarka { get; set; }
        public string AracModel { get; set; }
        public int Yil { get; set; }
        public int GerekenEhliyetYasi { get; set; }
        public int GunlukSinirKm { get; set; }
        public int AnlikKm { get; set; }
        public bool Airbag { get; set; }
        public int BagajHacmi { get; set; }
        public int KoltukSayisi { get; set; }
        public string AracTipi { get; set; }
        public bool Klima { get; set; }
        public string YakitTuru { get; set; }
        public string AracSinifi { get; set; }
        public string VitesTuru { get; set; }
        public decimal Fiyat { get; set; }
        public bool AracDurumu { get; set; }
        public string Aciklama { get; set; }
        public string Image { get; set; }
        public int SirketID { get; set; }
    }
}