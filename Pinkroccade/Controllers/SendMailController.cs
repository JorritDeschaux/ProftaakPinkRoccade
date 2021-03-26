using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinkroccade.Models;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Net.Mail;

namespace Pinkroccade.Controllers
{
    public partial class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult Index()
        {
            return View("SendMail");
        }
        public ActionResult Sender(MailModel mailModel)
        {
            HttpPostedFileBase file = Request.Files[0];

            BinaryReader br = new BinaryReader(file.InputStream);
            Byte[] byteImage = br.ReadBytes((Int32)file.ContentLength);
            string Base64string = Convert.ToBase64String(byteImage, 0, byteImage.Length);

            string mailContent = $"{mailModel.body} <img src='data:image/png;base64,{Base64string}' /> ";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailModel.fromMail);
            mail.To.Add(new MailAddress("Mailbox@Pinkroccade.nl"));
            mail.Subject = mailModel.subject;
            mail.IsBodyHtml = true;
            mail.Body = mailContent;

            MailHelper.smtpClient().Send(mail);
            return View("SendMail");
        }
    }
}