using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class RezervationController : Controller
    {
        // GET: Rezervation
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult Rezervation(int aracID)
        {
            Arac car = service.GetCar(aracID);
            Rezervasyon rez = new Rezervasyon();

            rez.AracID = Convert.ToInt32(Session["aracID"].ToString());
            rez.KullaniciID = Convert.ToInt32(Session["userID"].ToString());
            rez.AracMarka = car.AracMarka;
            rez.AracModel = car.AracModel;
            rez.Image = car.Image;
            rez.AlisTarihi = Convert.ToDateTime(Session["startDate"]);
            rez.VerisTarihi = Convert.ToDateTime(Session["endDate"]);
            rez.AlisYeri = Session["pickupLocation"].ToString();
            rez.VerisYeri = Session["returnLocation"].ToString();

            service.Rezervation(rez);
            Session["message"] = "rez";
            return RedirectToAction("MyRezervation","Profile");
        }
        public ActionResult DeleteRezervation(int rezervasyonID,int aracID)
        {
            service.DeleteRezervation(rezervasyonID, aracID);
            Session["message"] = "delrez";
            return RedirectToAction("MyRezervation","Profile");
        }
    }
}