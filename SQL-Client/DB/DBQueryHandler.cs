using System;
using System.Collections.Generic;
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

        public DBQueryHandler(string text, SqlConnection con) {
            Queri = text;
            connection = con;
        }

        
    }
}
