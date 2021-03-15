using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinkroccade.Models;

namespace Pinkroccade.Controllers
{
    public class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult Index()
        {
            return View("SendMail");
        }
        public ActionResult Sender(MailModel mailModel)
        {

            Pinkroccade.MailHelper.smtpClient().Send(mailModel.fromMail, "Mailbox@Pinkroccade.nl", mailModel.subject, mailModel.body);
            return View("SendMail");
        }
    }
}