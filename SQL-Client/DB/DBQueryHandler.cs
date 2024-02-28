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
        public string com {  get; set; }
        public SqlConnection connection { get; set; }
        

        public DBQueryHandler(string text, SqlConnection con) {
           
            com = text;
            connection = con;
            
        }

        public DataTable Query()
        {
            //создаем дататейбл который у нас и будет выводить данные в виде таблицы
            DataTable dt = new DataTable();
            //передаем в адаптер дата тейбл, для того, что бы в него записались все строки
            // которые мы получили от запроса
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                SqlDataReader red = command.ExecuteReader();
                
                dt.Load(red);
            }
            return dt;
        }

        /// <summary>
        /// тут возвращаемм количество измененных или добавленных в результате запроса записей
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string Query(string query)
        {
            SqlCommand command = new SqlCommand(query,connection);
            int i = command.ExecuteNonQuery();
            return $"Ваш запрос выполнил {i} команд";
        }
    }
}
