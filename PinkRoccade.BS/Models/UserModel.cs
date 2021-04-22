using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkRoccade.BS.Models
{
	public class UserModel
	{
		[Key]
		public int Unique_id { get; set; }

        [DisplayName("Voornaam")]
        [Required(ErrorMessage = "Vul hier je voornaam in")]
		public string First_Name { get; set; }

        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "Vul hier je achternaam in")]
		public string Last_Name { get; set; }

        [DisplayName("Gebruikersnaam")]
        [Required(ErrorMessage = "Vul hier je gebruikersnaam in")]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "Vul hier je wachtwoord in")]
		public string Password { get; set; }

		[DataType(DataType.EmailAddress)]
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Vul hier je e-mailadres in")]
		public string EMail { get; set; }

	}
}