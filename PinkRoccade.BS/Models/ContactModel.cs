using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PinkRoccade.BS.Models
{
    public class ContactModel
    {
        [DisplayName("E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Vul je e-mailadres hier in")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [DisplayName("Onderwerp")]
        [Required(ErrorMessage = "Vul hier het onderwerp in")]
        public string Subject { get; set; }

        [DisplayName("Informatie")]
        [Required(ErrorMessage = "Vul hier informatie in.")]
        [StringLength(500, ErrorMessage = "Je kan maximaal 500 karakters invoeren in dit veld")]
        [MaxLength(500)]
        public string Body { get; set; }

    }
}
