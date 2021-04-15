
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PinkRoccade.BS.Data;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginContext context;

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
                loginModel = context.GetLogin(loginModel);

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

        public IActionResult NoAccount()
        {
            return View("~/Views/Registration/Registration.cshtml");
        }

        public IActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(IFormCollection formCollection)
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

                context.CreateUser(newUserModel);
                return View("Registration");
            }
            else
            {
                return View("Registration");
            }
        }
    }
}
    