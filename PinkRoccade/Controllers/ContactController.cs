using Microsoft.AspNetCore.Mvc;
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
            string email = contactModel.Email;
            string subject = contactModel.Subject;
            string body = contactModel.Body;

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.From = new MailAddress("db3b3cdf8c-c3e97c@inbox.mailtrap.io");
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.mailtrap.io");
            smtp.Port = 2525;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("ad3f4fd6510412", "948abe13d2a8e6");
            smtp.Send(mailMessage);
            ViewBag["Success"] = "The project has been added";
            ViewBag.message = "Bedankt voor uw mail. We nemen zo spoedig contact met u op.";

            return View();
        }
    }
}
