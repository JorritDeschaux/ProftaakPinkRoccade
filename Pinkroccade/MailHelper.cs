using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Pinkroccade {
    public class MailHelper {
        static SmtpClient client = new SmtpClient("smtp.mailtrap.io") {
            Port = 587,
            Credentials = new NetworkCredential("836db70506df9b", "fff8be6a5c82be"),
            EnableSsl = true,
        };

        public static void SendMail(string from, string[] to, string subject, string content) {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            to.ToList().ForEach(email => mail.To.Add(email));
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = content;
            client.Send(mail);
        }

        public static void SendMail(string from, string to, string subject, string content) {
            string[] mails = { to };
            SendMail(from, mails, subject, content);
        }
    }
}