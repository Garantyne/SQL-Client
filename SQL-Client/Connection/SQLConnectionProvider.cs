using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Client.Connection
{
    internal static class SQLConnectionProvider
    {
        public static SqlConnection OpenConnection(string serverName, string dbName)
        {
            string conString = $"Data Source={serverName}; Initial Catalog={dbName};" +
                $"Integrated Security=SSPI; Timeout=10";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            return con;
        }
    }
}
