using Pinkroccade.Classes;
using Pinkroccade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinkroccade.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Registration()
        {
            return View("Registration");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Registration(FormCollection formCollection)
		{
			if (ModelState.IsValid)
			{
				UserModel newUserModel = new UserModel()
				{
					First_Name = formCollection["First_Name"],
					Last_Name = formCollection["Last_Name"],
					Username = formCollection["Username"],
					Password = formCollection["Password"],
					EMail = formCollection["EMail"],
				};

				Register.CreateUserData(newUserModel);
				return View("Registration");
			}
			else
			{
				return View("Registration");
			}
		}
	}
}