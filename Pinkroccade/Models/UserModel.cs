using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinkroccade.Models
{
	public class UserModel
	{
		public string Unique_id { get; set; }

		[DisplayName("First Name")]
		[Required]
		public string First_Name { get; set; }

		[DisplayName("Last Name")]
		[Required]
		public string Last_Name { get; set; }

		[DisplayName("Username")]
		[Required]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Password")]
		[Required]
		public string Password { get; set; }

		[DataType(DataType.EmailAddress)]
		[DisplayName("E-Mail")]
		[Required]
		public string EMail { get; set; }

	}
}