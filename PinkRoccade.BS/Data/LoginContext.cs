﻿using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PinkRoccade.BS.Models;
using System;

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

        public LoginModel GetLogin(LoginModel loginModel)
        {
            using(MySqlConnection conn = GetConnection())
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
                        loginModel.Unique_id = executeString.GetString(0);
                        loginModel.First_Name = executeString.GetString(1);
                        loginModel.Last_Name = executeString.GetString(2);
                    }

                    conn.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.Message);
                }
                
            }

            return loginModel;
        }

        public void CreateUser(UserModel userModel)
        {
            using (MySqlConnection conn = GetConnection())
            {

                string StoreDataString = "INSERT INTO `user`(`first_name`, `last_name`, `username`, `email`, `password`) VALUES (@val1,@val2,@val3,@val4,@val5);";
                MySqlCommand storeData = new MySqlCommand(StoreDataString, conn);

                storeData.Parameters.AddWithValue("@val1", userModel.First_Name);
                storeData.Parameters.AddWithValue("@val2", userModel.Last_Name);
                storeData.Parameters.AddWithValue("@val3", userModel.Username);
                storeData.Parameters.AddWithValue("@val4", userModel.EMail);
                storeData.Parameters.AddWithValue("@val5", userModel.Password);

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