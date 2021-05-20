using Microsoft.AspNetCore.Mvc;
using PinkRoccade.BS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public class IncidentOverviewController : Controller
    {
        public IActionResult Index()
        {
            int userId = SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User").Unique_id;
            PinkRoccade.BS.Data.IncidentHistoryContext incidentHistoryContext = new BS.Data.IncidentHistoryContext();
            List<IncidentHistoryModel> list = incidentHistoryContext.GetIncidents(userId);
            return View("IncidentOverview",list);
        }
    }
}
