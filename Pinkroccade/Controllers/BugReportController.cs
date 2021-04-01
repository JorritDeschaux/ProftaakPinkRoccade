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
    public partial class BugReportController : Controller
    {
        // GET: BugReport
        public ActionResult Index()
        {
            return View("BugReport");
        }

        public ActionResult Sender(MailModel mailModel)
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