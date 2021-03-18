using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinkroccade.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Registrate()
        {
            return View("Registration");
        }
    }
}