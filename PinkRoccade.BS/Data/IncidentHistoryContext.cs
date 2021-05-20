using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PinkRoccade.BS.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace PinkRoccade.BS.Data
{
    public class IncidentHistoryContext
    {
        private MySqlConnection GetSqlConnection()
        {
            MySqlConnection connectionString = new MySqlConnection("server = localhost; port = 3306; database = pinkroccade; user = root");
            return connectionString;
        }

        public List<IncidentHistoryModel> GetIncidents(int userID)
        {
            List<IncidentHistoryModel> modelList = new List<IncidentHistoryModel>();
            using (MySqlConnection conn = GetSqlConnection())
            {
                MySqlCommand getUserData = new MySqlCommand("SELECT * FROM `alert` WHERE `user_id`= @val1", conn);
                getUserData.Parameters.AddWithValue("@val1", userID);
                try
                {
                    conn.Open();
                    getUserData.Prepare();
                    var executeString = getUserData.ExecuteReader();
                    while (executeString.Read())
                    {
                        IncidentHistoryModel incidentHistoryModel = new IncidentHistoryModel();
                        incidentHistoryModel.IncidentID = executeString.GetInt32(0);
                        incidentHistoryModel.Location = executeString.GetString(1);
                        incidentHistoryModel.Description = executeString.GetString(4);
                        incidentHistoryModel.currentStatus = (IncidentHistoryModel.CurrentStatus)executeString.GetInt32(6);
                        incidentHistoryModel.User_ID = executeString.GetInt32(7);
                        modelList.Add(incidentHistoryModel);
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.Message);
                }
            }
            return modelList;
        }
    }
}
