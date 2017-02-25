using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cache_lab5.Models
{
    public class DbHelper
    {
       
        private static CacheConnection _connection;

        public static CacheConnection GetConnection()
        {
            if (_connection == null)
            {
                try
                {
                    _connection = new CacheConnection();
                    _connection.ConnectionString = "Server=localhost; Port=1972; Namespace=USER;" +
                        "Password=qazwsx001; User ID=_SYSTEM;";
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message);
                }
            }
            return _connection;
        }

        public static void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
