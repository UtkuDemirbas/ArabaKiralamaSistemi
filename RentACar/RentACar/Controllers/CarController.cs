using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult Detail(int aracID)
        {
            Session["aracID"] = aracID;
            return View(service.GetCar(aracID));
        }
        public ActionResult UpdateCar(int aracID)
        {
            Arac item = service.GetCar(aracID);
            Session["fotograf"] = item.Image;
            Session["aracID"] = aracID;
            return View(item);
        }
        [HttpPost]
        public ActionResult UpdateCar(Arac item, HttpPostedFileBase image1)
        {
            item.AracDurumu = true;
            item.SirketID = Convert.ToInt32(Session["sirketID"].ToString());
            item.AracID= Convert.ToInt32(Session["aracID"].ToString());

            if (image1 != null)
            {
                var image = new Bitmap(image1.InputStream);

                if (image.Width == 1280 && image.Height == 800)
                {
                    string fileName = Path.GetFileNameWithoutExtension(image1.FileName);
                    string extention = Path.GetExtension(image1.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    item.Image = "~/Css/assets/img/Cars/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Css/assets/img/Cars/"), fileName);

                    image1.SaveAs(fileName);
                    // Arabanın eski fotografını sildik.
                    System.IO.File.Delete(Server.MapPath(Session["fotograf"].ToString()));

                    service.UpdateCar(item);
                    Session["message"] = "updatecar";

                    return RedirectToAction("ListCars","Admin");
                }
                else
                {
                    ViewBag.Message = "false";
                    //ModelState.AddModelError("", "Fotoğrafın boyutu 1280x800 olmalıdır.");
                    return View();
                }
            }
            else
            {
                item.Image = Session["fotograf"].ToString();
                service.UpdateCar(item);
                Session["message"] = "updatecar";

                return RedirectToAction("ListCars", "Admin");
            }
            
        }
    }
}