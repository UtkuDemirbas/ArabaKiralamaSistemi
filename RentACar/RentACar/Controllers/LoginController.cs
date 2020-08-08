using RentACar.RentACarWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        RentACarWebService.RentACarWebServiceSoapClient service = new RentACarWebServiceSoapClient();
        public ActionResult Login()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Login(string username,string sifre)
        {
            int id;
            id=service.Login(username,sifre);
            if (id != -1)
            {
                Session["userID"] = id;
                if (Session["notLogin"] != null)
                {
                    Session["notLogin"] = null;
                    return RedirectToAction("ListAllCar", "Home");
                }
                else
                {
                    Session["message"] = "login";
                    return RedirectToAction("WhereIsLocation", "WhereIsLocation");
                }
            }
            else
            {
                ViewBag.Message= "Lütfen Bilgilerinizi Kontrol Ediniz";
                return View();
            }
            
        }
        public ActionResult LoginCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginCompany(string username,string sifre)
        {
            int id;
            id = service.LoginCompany(username, sifre);
            if (id != -1)
            {
                Session["sirketID"] = id;
                Session["message"] = "login";
                return RedirectToAction("ListCars", "Admin");
            }
            else
            {
                ViewBag.Message = "Lutfen Bilgilerinizi Kontrol Ediniz";
                return View();
            }
               
        }
    }
}