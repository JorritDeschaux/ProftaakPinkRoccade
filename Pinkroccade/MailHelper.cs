using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Pinkroccade
{
    public class MailHelper
    {
        public static SmtpClient smtpClient()
        {
            return new SmtpClient("smtp.mailtrap.io")
            {
                Port = 587,
                Credentials = new NetworkCredential("836db70506df9b", "fff8be6a5c82be"),
                EnableSsl = true,
            };
            //ryan "414dd7d23ac100", "322a95df45ae26"
        }
    }
}