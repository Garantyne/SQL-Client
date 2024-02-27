using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQL_Client.DB
{
    /// <summary>
    /// Логика взаимодействия для DBWork.xaml
    /// </summary>
    public partial class DBWork : Window
    {
        private readonly SqlConnection con;
        public DBWork(SqlConnection con)
        {
            InitializeComponent();
            this.con = con;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            using(SqlCommand command = new SqlCommand(quireTextBox.Text, con))
            {
                switch(ParseQueri(quireTextBox.Text))
                {
                    case "reader":
                        answerTextBox.Text = quireTextBox.Text;
                        break;
                    case "nonquery":
                        answerTextBox.Text = quireTextBox.Text;
                        break;
                    default:
                        answerTextBox.Text = "Неизвестный тип запроса, повторите попытку";
                        break;
                }
                
            }
        }

        private string ParseQueri(string text)
        {
            string parse = quireTextBox.Text.Split(' ')[0];

            if( parse.ToLower() == "select")
            {
                return "reader";
            }else if(   parse.ToLower() == "insert" ||
                        parse.ToLower() == "delete" ||
                        parse.ToLower() == "update")
            {
                return "nonquery";
            }
            return "unknow";
        }
    }
}
