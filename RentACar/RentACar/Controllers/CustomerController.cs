using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class CustomerController : Controller
    {
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        // GET: Customer
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Kullanici user,string adress)
        {
            bool same = false;

            foreach (var item in service.ListCustomer())
            {
                if (user.TcNumarasi==item.TcNumarasi || user.Tel==item.Tel || user.Eposta==item.Eposta || user.Username==item.Username)
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

                Kullanici kullanici = new Kullanici();
                kullanici.Ad = user.Ad;
                kullanici.Soyad = user.Soyad;
                kullanici.TcNumarasi = user.TcNumarasi;
                kullanici.Eposta = user.Eposta;
                kullanici.Username = user.Username;
                kullanici.Sifre = user.Sifre;
                kullanici.Tel = user.Tel;
                kullanici.RoleID = 2;
                kullanici.AdresID = service.GetAdressID();

                service.AddUser(kullanici);
                Session["message"] = "addcustomer";
                return RedirectToAction("Login", "Login");
            }

            
        }
        public ActionResult UpdateCustomer()
        {
            Kullanici item = service.GetUser(Convert.ToInt32(Session["userID"]));
            Session["adresID"] = item.AdresID;
            return View(item);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(Kullanici user,string adress)
        {
            user.KullaniciID= Convert.ToInt32(Session["userID"]);
            bool same = false;

            foreach (var item in service.ListCustomer())
            {
                if (user.KullaniciID == item.KullaniciID)
                    continue;
                if (user.TcNumarasi == item.TcNumarasi || user.Tel == item.Tel || user.Eposta == item.Eposta || user.Username == item.Username)
                {
                    same = true;
                }
            }
            if (same)
            {
                ViewBag.Message = "false";
             //   ModelState.AddModelError("", "Değiştirmek istediğiniz bilgilerden başka bir kullanıcı bulunmaktadır.");
                return View();
            }
            else
            {
                Adres adres = new Adres();
                adres.AdresBilgileri = adress;
                adres.AdresID = Convert.ToInt32(Session["adresID"]);
                service.UpdateAdress(adres);

                service.UpdateUser(user);
                Session["message"] = "updatecustomer";
                return RedirectToAction("Quit", "Customer");
            }
            
        }
        public ActionResult Quit()
        {
            Session["userID"] = null;
            Session["pickupLocation"] = null;
            Session["returnLocation"] = null;
            Session["startDate"] = null;
            Session["endDate"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}