using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PinkRoccade.BS.Models;
using System;
using System.Web;

namespace PinkRoccade.BS.Data
{
    public class LoginContext : DbContext
    {
        public string ConnectionString { get; set; }

        public LoginContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public UserModel GetLogin(LoginModel loginModel)
        {
            UserModel user = new UserModel();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand getUserData = new MySqlCommand("SELECT * FROM user WHERE email=@val1 AND password=@val2", conn);

                getUserData.Parameters.AddWithValue("@val1", loginModel.Email);
                getUserData.Parameters.AddWithValue("@val2", loginModel.Password);

                try
                {
                    conn.Open();
                    getUserData.Prepare();
                    var executeString = getUserData.ExecuteReader();
                    while (executeString.Read())
                    {
                        user.Unique_id = executeString.GetInt32(0);
                        user.First_Name = executeString.GetString(1);
                        user.Last_Name = executeString.GetString(2);
                        user.Username = executeString.GetString(3);
                        user.Email = executeString.GetString(5);
                        if (executeString.GetInt32(7) == 1)
                        {
                            user.IsAdmin = true;
                        }
                        else
                        {
                            user.IsAdmin = false;
                        }
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.Message);
                }

            }

            return user;
        }


        public void CreateUser(UserModel userModel)
        {
            using (MySqlConnection conn = GetConnection())
            {

                string StoreDataString = "INSERT INTO `user`(`first_name`, `last_name`, `username`, `email`, `password`) VALUES (@val1,@val2,@val3,@val4,@val5);";
                MySqlCommand storeData = new MySqlCommand(StoreDataString, conn);

                HttpUtility.HtmlEncode(storeData.Parameters.AddWithValue("@val1", userModel.First_Name));
                HttpUtility.HtmlEncode(storeData.Parameters.AddWithValue("@val2", userModel.Last_Name));
                HttpUtility.HtmlEncode(storeData.Parameters.AddWithValue("@val3", userModel.Username));
                HttpUtility.HtmlEncode(storeData.Parameters.AddWithValue("@val4", userModel.Email));
                HttpUtility.HtmlEncode(storeData.Parameters.AddWithValue("@val5", userModel.Password));


                try
                {
                    conn.Open();

                    storeData.Prepare();
                    var executeString = storeData.ExecuteReader();

                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.Message);
                }
            }
        }

    }

}
