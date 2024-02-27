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
        public SqlConnection connection { get; set; }
        public SqlDataAdapter adapter { get; set; }

        public DBQueryHandler(string text, SqlConnection con) {
           
            connection = con;
            //вытягиваем данные из БД переданым запросом
            adapter = new SqlDataAdapter(text, connection);
        }

        public DataTable Query()
        {
            //создаем дататейбл который у нас и будет выводить данные в виде таблицы
            DataTable dt = new DataTable();
            //передаем в адаптер дата тейбл, для того, что бы в него записались все строки
            // которые мы получили от запроса
            adapter.Fill(dt);
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
