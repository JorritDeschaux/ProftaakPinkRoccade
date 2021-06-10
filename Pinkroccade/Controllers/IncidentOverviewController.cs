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
        [HttpPost]
        public IActionResult EditAction(IncidentHistoryModel incidentHistory)
        {
            PinkRoccade.BS.Data.IncidentHistoryContext incidentHistoryContext = new BS.Data.IncidentHistoryContext();
            if (incidentHistory.currentStatus == IncidentHistoryModel.CurrentStatus.Gerepareerd)
            {
                MySqlCommand UpdateStatus = new MySqlCommand("UPDATE `alert` SET `description`= @val1,`status_id`= @val2,`solvedate`= @val3 WHERE `id` = @val4");
                UpdateStatus.Parameters.AddWithValue("@val1", incidentHistory.Description);
                UpdateStatus.Parameters.AddWithValue("@val2", (int)incidentHistory.currentStatus);
                UpdateStatus.Parameters.AddWithValue("@val3", DateTime.Now);
                UpdateStatus.Parameters.AddWithValue("@val4", incidentHistory.IncidentID);
                incidentHistoryContext.UpdateStatus(UpdateStatus, true);
            }
            else
            {
                MySqlCommand UpdateStatus = new MySqlCommand("UPDATE `alert` SET `description`= @val1,`status_id`= @val2 WHERE `id` = @val4");
                UpdateStatus.Parameters.AddWithValue("@val1", incidentHistory.Description);
                UpdateStatus.Parameters.AddWithValue("@val2", (int)incidentHistory.currentStatus);
                UpdateStatus.Parameters.AddWithValue("@val4", incidentHistory.IncidentID);
                incidentHistoryContext.UpdateStatus(UpdateStatus, true);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string id )
        {
            PinkRoccade.BS.Data.IncidentHistoryContext incidentHistoryContext = new BS.Data.IncidentHistoryContext();
            MySqlCommand deleteCommad = new MySqlCommand("DELETE FROM `alert` WHERE `alert`.`id` = @val1");
            deleteCommad.Parameters.AddWithValue("@val1", id);
            incidentHistoryContext.UpdateStatus(deleteCommad, true);
            return RedirectToAction("Index");
        }
    }
}
