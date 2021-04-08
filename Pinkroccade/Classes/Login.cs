using MySql.Data.MySqlClient;
using Pinkroccade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinkroccade.Classes
{
    public class Login
    {
        public static LoginModel SelectUserData(LoginModel loginModel)
        {
            MySqlConnection databaseConnection = new MySqlConnection(DB_Credentials.DbConnectionString);
            MySqlCommand getUserData = new MySqlCommand("SELECT * FROM user WHERE email=@val1 AND password=@val2", databaseConnection);
            getUserData.Parameters.AddWithValue("@val1", loginModel.Email);
            getUserData.Parameters.AddWithValue("@val2", loginModel.Password);
            try
            {
                databaseConnection.Open();
                getUserData.Prepare();
                var executeString = getUserData.ExecuteReader();
                while (executeString.Read())
                {
                    loginModel.Unique_id = executeString.GetString(0);
                    loginModel.First_Name = executeString.GetString(1);
                    loginModel.Last_Name = executeString.GetString(2);
                }
				databaseConnection.Close();
			}
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
            }
            return loginModel;
        }
    }
}