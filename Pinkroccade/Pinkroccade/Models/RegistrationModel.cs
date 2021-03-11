using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pinkroccade.Models
{
    public class RegistrationModel
    {
        [DisplayName("First Name")]
        public string First_Name { get; set; }
        [DisplayName("Last Name")]
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [DisplayName("E-Mail")]
        public string EMail { get; set; }
    }
}