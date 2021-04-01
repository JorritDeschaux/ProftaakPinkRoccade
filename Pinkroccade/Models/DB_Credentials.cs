using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinkroccade.Models
{
    public class DB_Credentials
    {
        private static string dbIp = "127.0.0.1";
        private static string dbPort = "3306";
        private static string dbUname = "root";
        private static string dbPassword = "";
        private static string dbName = "pinkroccade";
        private static string dbConnectionString = "Datasource=" + dbIp + ";port=" + dbPort + ";username=" + dbUname + ";password=;database=" + dbName + ";";

        public static string DbIp { get { return dbIp; } }
        public static string DbPort { get { return dbPort; } }
        public static string DbUname { get { return dbUname; } }
        public static string DbPassword { get { return dbPassword; } }
        public static string DbName { get { return dbName; } }
        public static string DbConnectionString
        {
            get { return dbConnectionString; }
            set { dbConnectionString = value; }
        }
    }
}