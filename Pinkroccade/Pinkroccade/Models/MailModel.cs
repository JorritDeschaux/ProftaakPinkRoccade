using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Pinkroccade.Models
{
    public class MailModel
    {
        public string subject { get; set; }

        public string fromMail { get; set; }

        public string body { get; set; }

        public string recipient = "sanderaarts19992@gmail.com";
    }
}