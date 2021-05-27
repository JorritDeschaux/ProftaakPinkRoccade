using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using PinkRoccade.BS.Models;

namespace PinkRoccade.BS.Classes
{
    public class SaveIncident
    {
        public static void Store_Incident(IncidentModel incidentModel)
        {
            MySqlConnection databaseConnection = new MySqlConnection("Datasource=127.0.0.1;port=3306;username=root;password=;database= pinkroccade;");
            MySqlCommand SendIncidentCommand = new MySqlCommand("INSERT INTO `alert` (`location`, `latitude`, `longitude`, `description`, `img_data`, `status_id`, `user_id`, `email`) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8);", databaseConnection);
            SendIncidentCommand.Parameters.AddWithValue("@val1", incidentModel.Location);
            SendIncidentCommand.Parameters.AddWithValue("@val2", incidentModel.Latitude);
            SendIncidentCommand.Parameters.AddWithValue("@val3", incidentModel.Longitude);
            SendIncidentCommand.Parameters.AddWithValue("@val4", incidentModel.Description);
            SendIncidentCommand.Parameters.AddWithValue("@val5", incidentModel.Img_Data);
            SendIncidentCommand.Parameters.AddWithValue("@val6", 1);
            SendIncidentCommand.Parameters.AddWithValue("@val7", incidentModel.User_Id);
            SendIncidentCommand.Parameters.AddWithValue("@val8", incidentModel.Email);
            StoreData(SendIncidentCommand, databaseConnection, true);
        }

        public static void Check_databaseConnectionState(MySqlConnection databaseConnection)
        {
            if (databaseConnection.State == System.Data.ConnectionState.Open)
            {
                databaseConnection.Close();
            }
        }
        public static bool StoreData(MySqlCommand storeData, MySqlConnection databaseConnection,bool prepare)
        {
            Check_databaseConnectionState(databaseConnection);
            try
            {
                databaseConnection.Open();
                if (prepare == true)
                {
                    storeData.Prepare();
                }
                MySqlDataReader executeString = storeData.ExecuteReader();
                databaseConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
                return false;
            }
        }
    }
}
