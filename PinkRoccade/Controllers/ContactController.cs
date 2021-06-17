using Microsoft.AspNetCore.Mvc;
using PinkRoccade.BS.Classes;
using PinkRoccade.BS.Models;
using System.Net.Mail;

namespace PinkRoccade.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactModel contactModel)
        {
            UserModel user = SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User");

            string email = string.Empty;
            if (contactModel.Email == null)
            {
                email = user.Email;
            }
            else
            {
                email = contactModel.Email;
            }
            string subject = contactModel.Subject;
            string body = contactModel.Body;

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("db3b3cdf8c-c3e97c@inbox.mailtrap.io");
            mailMessage.Subject = subject;
            mailMessage.From = new MailAddress(email);
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.mailtrap.io");
            smtp.Port = 2525;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("836db70506df9b", "fff8be6a5c82be");
            smtp.Send(mailMessage);

            TempData["Mail Sent"] = "Mail succesvol verstuurd.";
            return View();
        }
    }
}
