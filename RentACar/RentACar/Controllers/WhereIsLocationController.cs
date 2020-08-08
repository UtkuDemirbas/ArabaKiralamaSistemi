using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class WhereIsLocationController : Controller
    {
        // GET: WhereIsLocation
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult WhereIsLocation()
        {
            List<string> havaalanlari = new List<string>();
            string dosyayolu = @"C:/Users/utkuf/OneDrive/Masaüstü/RentACar/RentACar/havaalanlari/havaalanlari.txt";
            FileStream fs = new FileStream(dosyayolu, FileMode.Open, FileAccess.Read);

            StreamReader sw = new StreamReader(fs);

            string havaalani = sw.ReadLine();
            while (havaalani != null)
            {
                string[] tam = havaalani.Split('-');
                havaalanlari.Add(tam[0]);
                havaalani = sw.ReadLine();
            }
            return View(havaalanlari);
        }
        [HttpPost]
        public ActionResult WhereIsLocation(string pickupLocation, string returnLocation, DateTime startDate, DateTime endDate)
        {
            if (startDate == endDate)
            {
                Session["message"] = "date";
                return RedirectToAction("WhereIsLocation", "WhereIsLocation");
            }
            else
            {
                Session["pickupLocation"] = pickupLocation;
                Session["returnLocation"] = returnLocation;
                Session["startDate"] = startDate;
                Session["endDate"] = endDate;

                return RedirectToAction("ListAllCar", "Home");
            }
            
        }
    }
}