using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        // Database connection String
        private static string connectionString =
            @"Data Source=localhost;Initial Catalog=Ljs_DB;Integrated Security=True";

        public static SqlConnection GetDBConnection()
        {
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
