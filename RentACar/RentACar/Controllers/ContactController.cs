using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string Ad,string email,string website,string konu,string messagee)
        {
            try
            {
                var body = new StringBuilder();

                body.AppendLine("Ad & Soyad: " + Ad);
                body.AppendLine("Email Adresi: " + email);
                body.AppendLine("Konu: " + konu);
                body.AppendLine("Mesaj: " + messagee);

                var fromAddress = new MailAddress("demirbas.utku1910@gmail.com");
                var toAddress = new MailAddress("demirbas.utku1910@gmail.com");
                const string subject = "Utku Demirbaş-CarRental";


                using (var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "10021998ud"),
                    Timeout = 20000
                })

                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body.ToString() })
                {
                        smtp.Send(message);
                }

                Session["message"] = "mail";


                return RedirectToAction("Contact", "Contact");
            }
            catch (Exception)
            {
                Session["message"] = "error";
                return RedirectToAction("Contact", "Contact");
            }  
        }
    }
}