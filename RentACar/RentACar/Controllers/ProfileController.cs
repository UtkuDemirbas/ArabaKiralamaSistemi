using RentACar.RentACarWebService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult MyRentACar()
        {
            if (!service.GetRentACar(Convert.ToInt32(Session["userID"])).Any())
            {
                ModelState.AddModelError("", "Şuan için kiralık aracınız bulunmamaktadır.");
            }
            return View(service.GetRentACar(Convert.ToInt32(Session["userID"].ToString())));
        }
        public ActionResult MyRezervation()
        {
            if (!service.GetRezervation(Convert.ToInt32(Session["userID"])).Any())
            {
                ModelState.AddModelError("", "Sizin için herhangi bir rezervasyon bulunmamaktadır.");
            }
            return View(service.GetRezervation(Convert.ToInt32(Session["userID"].ToString())));

        }
    }
}