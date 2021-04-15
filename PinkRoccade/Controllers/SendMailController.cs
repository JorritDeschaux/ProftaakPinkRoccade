using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PinkRoccade.BS.Models;
using PinkRoccade.BS.Classes;

namespace PinkRoccade.Controllers
{
    public partial class SendMailController : Controller
    {
        // GET: SendMail
        public IActionResult Index()
        {
            return View("SendMail");
        }

        public IActionResult Sender(MailModel mailModel) {
            IFormFile file = Request.Form.Files[0];

            BinaryReader br = new BinaryReader(file.OpenReadStream());
            Byte[] byteImage = br.ReadBytes((Int32) file.Length);
            string Base64string = Convert.ToBase64String(byteImage, 0, byteImage.Length);

            string mailContent = $"{mailModel.body} <img src='data:image/png;base64,{Base64string}' /> ";

            MailHelper.SendMail(mailModel.fromMail, "Mailbox@Pinkrocadde.nl", mailModel.subject, mailContent);

            return View("SendMail");
        }
    }
}