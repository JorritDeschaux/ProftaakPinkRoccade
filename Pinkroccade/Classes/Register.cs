using MySql.Data.MySqlClient;
using Pinkroccade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinkroccade.Classes
{
	public class Register
	{
		public static void CreateUserData(UserModel userModel)
		{
			MySqlConnection databaseConnection = new MySqlConnection(DB_Credentials.DbConnectionString);

			string StoreDataString = "INSERT INTO `user`(`first_name`, `last_name`, `username`, `email`, `password`) VALUES (@val1,@val2,@val3,@val4,@val5);";
			MySqlCommand storeData = new MySqlCommand(StoreDataString, databaseConnection);
			storeData.Parameters.AddWithValue("@val1", userModel.First_Name);
			storeData.Parameters.AddWithValue("@val2", userModel.Last_Name);
			storeData.Parameters.AddWithValue("@val3", userModel.Username);
			storeData.Parameters.AddWithValue("@val4", userModel.EMail);
			storeData.Parameters.AddWithValue("@val5", userModel.Password);

			try
			{
				databaseConnection.Open();
				storeData.Prepare();
				var executeString = storeData.ExecuteReader();
				databaseConnection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("error: " + e.Message);
			}
		}
	}
}