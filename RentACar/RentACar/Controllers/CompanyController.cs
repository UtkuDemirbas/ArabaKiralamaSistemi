using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCompany(Sirket user, string adress)
        {
            bool same = false;

            foreach (var item in service.ListCompany())
            {
                if (user.SirketTel == item.SirketTel || user.Username == item.Username || user.SirketAdi==item.SirketAdi)
                {
                    same = true;
                }
            }
            if (same)
            {
                ViewBag.message = "false";
                return View();
            }
            else
            {
                Adres adres = new Adres();

                adres.AdresBilgileri = adress;
                service.AddAdress(adres);

                Sirket kullanici = new Sirket();
                kullanici.SirketAdi = user.SirketAdi;
                kullanici.SirketTel = user.SirketTel;
                kullanici.Username = user.Username;
                kullanici.Sifre = user.Sifre;
                kullanici.AdresID = service.GetAdressID();

                service.AddCompany(kullanici);
                ViewBag.message = "true";
                return RedirectToAction("ListCars","Admin");
            }
        }
        public ActionResult UpdateCompany(string adress)
        {
            Sirket sirket = service.GetCompany(Convert.ToInt32(Session["sirketID"]));
            Session["adresID"] = sirket.AdresID;
            return View(sirket);
        }
        [HttpPost]
        public ActionResult UpdateCompany(Sirket sirket,string adress)
        {
            sirket.SirketID = Convert.ToInt32(Session["sirketID"]);
            bool same = false;

            foreach (var item in service.ListCompany())
            {
                if (sirket.SirketID == item.SirketID)
                    continue;
                if (sirket.SirketTel == item.SirketTel ||  sirket.Username == item.Username || sirket.SirketAdi==item.SirketAdi)
                {
                    same = true;
                }
            }
            if (same)
            {
                ViewBag.message = "false";
               // ModelState.AddModelError("", "Değiştirmek istediğiniz bilgilerden başka bir kullanıcı bulunmaktadır.");
                return View();
            }
            else
            {
                Adres adres = new Adres();
                adres.AdresBilgileri = adress;
                adres.AdresID = Convert.ToInt32(Session["adresID"]);
                service.UpdateAdress(adres);

                service.UpdateCompany(sirket);
                Session["message"] = "updatecompany";
                return RedirectToAction("Quit", "Admin");
            }
        }
        public ActionResult TakeDeliveryOf(int kiralikAracID,int aracID)
        {
            service.DeleteRentACar(kiralikAracID, aracID);
            Session["message"] = "delete";
            return RedirectToAction("ListCars","Admin");
        }
    }
}