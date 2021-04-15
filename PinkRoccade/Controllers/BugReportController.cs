
using Microsoft.AspNetCore.Mvc;
using PinkRoccade.BS.Classes;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public partial class BugReportController : Controller
    {
        // GET: BugReport
        public IActionResult Index()
        {
            return View("BugReport");
        }

        public IActionResult Sender(MailModel mailModel)
        {
            //HttpPostedFileBase file = Request.Files[0];

            //BinaryReader br = new BinaryReader(file.InputStream);
            //Byte[] byteImage = br.ReadBytes((Int32)file.ContentLength);
            //string Base64string = Convert.ToBase64String(byteImage, 0, byteImage.Length);

            string mailContent = $"{mailModel.body}";

            MailHelper.SendMail(mailModel.fromMail, "Mailbox@Pinkrocadde.nl", mailModel.subject, mailContent);
            return View("BugReport");
        }
    }
}