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

		public ActionResult Register(RegistrationModel registrationModel)
		{
			if (ModelState.IsValid)
			{
				return View();
			}
			else
			{
				return View("Login");
			}
		}
	}
}