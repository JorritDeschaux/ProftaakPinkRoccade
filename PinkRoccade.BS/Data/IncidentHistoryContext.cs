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

        public List<IncidentHistoryModel> GetIncidents(MySqlCommand getUserData, bool prepare = true)
        {
            List<IncidentHistoryModel> modelList = new List<IncidentHistoryModel>();
            MySqlConnection conn = GetSqlConnection();
            {
                getUserData.Connection = conn;
                try
                {
                    conn.Open();
                    if (prepare == true)
                    {
                        getUserData.Prepare();
                    }
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
