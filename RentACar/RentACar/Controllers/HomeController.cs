using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        // GET: Home
        public ActionResult ListAllCar()
        {
            if (Session["pickupLocation"] == null || Session["returnLocation"] == null)
            {
                return RedirectToAction("WhereIsLocation", "WhereIsLocation");
            }
            else
            {
                if (!service.ListCar(Session["pickupLocation"].ToString(), Session["returnLocation"].ToString()).Any())
                {
                    ModelState.AddModelError("", "Şuan uygun kiralık araç bulunmamaktadır.");
                }
                return View(service.ListCar(Session["pickupLocation"].ToString(), Session["returnLocation"].ToString()));
            }
        }

        [HttpPost]
        public ActionResult CarFilter(string marka, int min, int max, string aracTipi, string aracSinifi, string yakitTuru, string aracYili)
        {
            string pickupLocation = Session["pickupLocation"].ToString();
            string returnLocation = Session["returnLocation"].ToString();
            Filtre filter = new Filtre();
            if (!(marka == "Seçiniz"))
                filter.Marka = marka;
            filter.Min = min;
            filter.Max = max;
            if (!(aracTipi == "Seçiniz"))
                filter.AracTipi = aracTipi;
            if (!(aracSinifi == "Seçiniz"))
                filter.AracSinifi = aracSinifi;
            if (!(yakitTuru == "Seçiniz"))
                filter.YakitTuru = yakitTuru;
            if (!(aracYili == "Seçiniz"))
                filter.AracYili = aracYili;

            return View("ListAllCar", service.ListCarFilter(pickupLocation, returnLocation, filter));
        }
    }
}