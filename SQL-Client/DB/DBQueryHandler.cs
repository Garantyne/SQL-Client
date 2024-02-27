using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Client.DB
{
    internal class DBQueryHandler
    {
        public string Queri {  get; set; }
        public SqlConnection connection { get; set; }
        public SqlDataAdapter adapter { get; set; }

        public DBQueryHandler(string text, SqlConnection con) {
            Queri = text;
            connection = con;
            adapter = new SqlDataAdapter(Queri,connection);
        }

        public DataTable Query()
        {
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public string Query(string query)
        {
            SqlCommand command = new SqlCommand(query,connection);
            int i = command.ExecuteNonQuery();
            return $"Ваш запрос выполнил {i} команд";
        }
    }
}
