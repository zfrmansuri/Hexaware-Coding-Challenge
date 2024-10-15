using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.DatabaseLayer
{
    public static class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            string connectionString = "Data Source = ZFR-DESKTOP; Initial Catalog=CareerHub; Integrated Security=true";  // Replace with your actual connection string
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
