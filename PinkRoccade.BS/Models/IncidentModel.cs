using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkRoccade.BS.Models
{
	public class IncidentModel
	{
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(5000)]
        public string Img_Data { get; set; }

        [Required]
        public int Status_Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Vul hier je e-mailadres in")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}