using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkRoccade.BS.Models
{
    public class MailModel
    {
        [DisplayName("Onderwerp")]
        [Required(ErrorMessage = "Vul hier het onderwerp over het probleem in")]
        public string subject { get; set; }

        [DisplayName("E-mailadres")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Vul je e-mailadres hier in")]
        public string fromMail { get; set; }

        [DisplayName("Informatie")]
        [StringLength(500, ErrorMessage = "Je kan maximaal 500 karakters invoeren in dit veld")]
        [MaxLength(500)]
        [Required(ErrorMessage = "Vul informatie over het probleem in")]
        public string body { get; set; }

        public string recipient = "sanderaarts19992@gmail.com";
    }
}