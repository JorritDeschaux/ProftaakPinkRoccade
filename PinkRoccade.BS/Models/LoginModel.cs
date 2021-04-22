using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkRoccade.BS.Models
{
    public class LoginModel
    {
        public string Unique_id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Vul hier je e-mailadres in")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vul een wachtwoord in")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
    }
}