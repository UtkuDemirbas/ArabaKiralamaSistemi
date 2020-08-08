using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class RentACarController : Controller
    {
        // GET: RentACar
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();

        
        public ActionResult RentACar(int aracID, int rezervasyon)
        {
            if (rezervasyon != 0)
            {
                Arac car = service.GetCar(aracID);
                Rezervasyon rez = service.GetRezervationID(rezervasyon);

                KiralikArac arac = new KiralikArac();
                arac.AracID = car.AracID;
                arac.KullaniciID = Convert.ToInt32(Session["userID"].ToString());
                arac.AracMarka = car.AracMarka;
                arac.AracModel = car.AracModel;
                arac.Image = car.Image;
                arac.KiralanmaZamani = DateTime.Now;
                arac.VerilisKm = car.AnlikKm;
                arac.AlisYeri = rez.AlisYeri;
                arac.VerisYeri = rez.VerisYeri;
                TimeSpan kalangun = rez.VerisTarihi - rez.AlisTarihi;
                arac.KacGun = Convert.ToInt32(kalangun.TotalDays);
                arac.Ucret = Convert.ToInt32(kalangun.TotalDays) * car.Fiyat;
                arac.SonKilometre = (Convert.ToInt32(kalangun.TotalDays) * car.GunlukSinirKm)+arac.VerilisKm;

                service.DeleteRezervation(rezervasyon,aracID);
                service.RentACar(arac);
                Session["message"] = "rent";
                return RedirectToAction("MyRentACar", "Profile");
            }
            else
            {
                Arac car = service.GetCar(Convert.ToInt32(Session["aracID"].ToString()));
                KiralikArac arac = new KiralikArac();
                arac.AracID = car.AracID;
                arac.KullaniciID = Convert.ToInt32(Session["userID"].ToString());
                arac.AracMarka = car.AracMarka;
                arac.AracModel = car.AracModel;
                arac.Image = car.Image;
                arac.KiralanmaZamani = DateTime.Now;
                arac.VerilisKm = car.AnlikKm;
                arac.AlisYeri = Session["pickupLocation"].ToString();
                arac.VerisYeri = Session["returnLocation"].ToString();
                TimeSpan kalangun = Convert.ToDateTime(Session["endDate"]) - Convert.ToDateTime(Session["startDate"]);
                arac.KacGun = Convert.ToInt32(kalangun.TotalDays);
                arac.Ucret = Convert.ToInt32(kalangun.TotalDays) * car.Fiyat;
                arac.SonKilometre = Convert.ToInt32(kalangun.TotalDays) * car.GunlukSinirKm;

                service.RentACar(arac);

                Session["message"] = "rent";
                return RedirectToAction("MyRentACar", "Profile");
            }
        }
    }
}