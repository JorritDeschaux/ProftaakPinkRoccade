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
        [Required]
        public string Id { get; set; }

        [StringLength(200)]
        public string Adres { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        public decimal Longtitude { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(5000)]
        public string Img_Data { get; set; }

        [Required]
        public int Status_Id { get; set; }

        [Required]
        public int User_Id { get; set; }
    }
}