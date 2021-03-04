using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinkroccade.Models
{
    public class RegistrationModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
    }
}