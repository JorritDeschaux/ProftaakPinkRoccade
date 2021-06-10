
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PinkRoccade.BS.Data;
using PinkRoccade.BS.Models;
using PinkRoccade.BS.Classes;

namespace PinkRoccade.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginContext context;

        public const string SessionKeyLoggedIn = "_LoggedIn";

        public const string SessionKeyUser = "_User";

        public LoginController(LoginContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult LoginAction(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserModel user = context.GetLogin(loginModel);

                if (user.Unique_id!= 0)
                {
                    HttpContext.Session.SetInt32("LoggedIn", 1);
                    HttpContext.Session.SetInt32("UserId", user.Unique_id);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, SessionKeyUser, user);
                    return RedirectToAction("Index", "Incidents");
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

        public IActionResult Logout()
		{
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("LoggedIn", 0);
            return View("~/Views/Incidents/Index.cshtml");
		}

        public IActionResult NoAccount()
        {
            return View("~/Views/Login/Registration.cshtml");
        }

        public IActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(UserModel newUser)
        {
            if (ModelState.IsValid)
            {
                context.CreateUser(newUser);
                UserModel loginCheck = context.GetLogin(new LoginModel { Email = newUser.Email, Password = newUser.Password });
                SaveIncident.Update_User_Id(loginCheck);
                TempData["Success"] = "Nieuw account succesvol aangemaakt.";
                return View("Login");
            }
            else
            {
                TempData["Danger"] = "Registratie is niet gelukt.";
                return View("Registration", newUser);
            }
        }
    }
}
    