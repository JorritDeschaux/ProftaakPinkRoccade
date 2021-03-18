using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinkroccade.Models
{
    public class RegistrationModel
    {
        [DisplayName("First Name")]
        [Required]
        public string First_Name { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName("E-Mail")]
        [Required]
        public string EMail { get; set; }
    }
}