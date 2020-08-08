using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCar(Arac item,HttpPostedFileBase image1)
        {
            var image = new Bitmap(image1.InputStream);

            if(image.Width==1280 && image.Height == 800)
            {
                string fileName = Path.GetFileNameWithoutExtension(image1.FileName);
                string extention = Path.GetExtension(image1.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                item.Image = "~/Css/assets/img/Cars/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Css/assets/img/Cars/"), fileName);
      
                image1.SaveAs(fileName);
                item.AracDurumu = true;
                item.SirketID = Convert.ToInt32(Session["sirketID"].ToString());

                service.AddCar(item);

                ViewBag.Message = "true";

                return View();
            }
            else
            {
                ViewBag.Message = "false";
               // ModelState.AddModelError("", "Fotoğrafın boyutu 1280x800 olmalıdır.");
                return View();
            }
            
        }
        
        
        public ActionResult ListCars()
        {
            if (!service.ListCompanyCar(Convert.ToInt32(Session["sirketID"])).Any())
            {
                ModelState.AddModelError("", "Şirketinize ait herhangi araç bulunmamaktadır.");
            }
            return View(service.ListCompanyCar(Convert.ToInt32(Session["sirketID"])));
        }
        public ActionResult Detail(int aracID)
        {
            Session["aracID"] = aracID;
            return View(service.GetCar(aracID));
        }
        public ActionResult Quit()
        {
            Session["sirketID"] = null;
            return RedirectToAction("LoginCompany", "Login");
        }
    }
}