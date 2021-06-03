using Microsoft.AspNetCore.Mvc;
using PinkRoccade.BS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PinkRoccade.BS.Models;
using MySql.Data.MySqlClient;

namespace PinkRoccade.Controllers
{
    public class IncidentOverviewController : Controller
    {
        public IActionResult Index()
        {
            UserModel user = SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User");
            PinkRoccade.BS.Data.IncidentHistoryContext incidentHistoryContext = new BS.Data.IncidentHistoryContext();
            List<IncidentHistoryModel> list = new List<IncidentHistoryModel> { };
            if (user.IsAdmin == true)
            {
                MySqlCommand getUserData = new MySqlCommand("SELECT * FROM `alert`");
                list = incidentHistoryContext.GetIncidents(getUserData, false);
            }
            else
            {
                MySqlCommand getUserData = new MySqlCommand("SELECT * FROM `alert` WHERE `user_id`= @val1");
                getUserData.Parameters.AddWithValue("@val1", user.Unique_id);
                list = incidentHistoryContext.GetIncidents(getUserData);
            }
            return View("IncidentOverview", list);
        }
    }
}
