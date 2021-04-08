using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinkroccade.Models;
using Pinkroccade.Classes;

namespace Pinkroccade.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult LoginAction(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                loginModel = Login.SelectUserData(loginModel);
                if (loginModel.Unique_id != "" && loginModel.Unique_id != null)
                {
                    TempData["unique_id"] = loginModel.Unique_id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult NoAccount()
        {
            return View("~/Views/register/register.cshtml");
        }
    }
}
    