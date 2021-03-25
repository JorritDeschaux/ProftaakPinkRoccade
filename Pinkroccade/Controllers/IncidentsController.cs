using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinkroccade.Controllers
{
    public class IncidentsController : Controller
    {
        // GET: Incidents
        public ActionResult Index()
        {
            return View();
        }

        //
        [HttpPost]
        public ActionResult CreateIncident()
		{
            return View();
		}

    }
}