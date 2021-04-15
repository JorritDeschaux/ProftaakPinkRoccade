using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace PinkRoccade.Controllers
{
    public class IncidentsController : Controller
    {

        // GET: Incidents
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateIncident()
        {
            return View();
        }

    }
}