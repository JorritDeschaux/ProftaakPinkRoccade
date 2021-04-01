using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinkroccade.Data;

namespace Pinkroccade.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly DbContext _context;

        //public IncidentsController(DbContext context)
        //{
        //    _context = context;

        //}

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