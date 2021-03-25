using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinkroccade.Models
{
	public class IncidentModel
	{
        [Required]
        public string Id { get; set; }
    }
}