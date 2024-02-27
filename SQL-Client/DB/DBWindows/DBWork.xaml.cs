using System;
using System.Collections.Generic;
using System.Data;
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
            DBQueryHandler dBQueryHandler = new DBQueryHandler(quireTextBox.Text,
                                                                con);
            try
            {
                using (SqlCommand command = new SqlCommand(quireTextBox.Text, con))
                {
                    switch (ParseQueri(quireTextBox.Text))
                    {
                        case "reader":
                            DataTable dt = dBQueryHandler.Query();
                            dataGridView.Visibility = Visibility.Visible;
                            dataGridView.ItemsSource = dt.DefaultView;
                            break;
                        case "nonquery":
                            answerTextBox.Visibility = Visibility.Visible;
                            dataGridView.Visibility = Visibility.Hidden;
                            answerTextBox.Text = dBQueryHandler.Query(quireTextBox.Text);
                            break;
                        default:
                            answerTextBox.Visibility = Visibility.Visible;
                            dataGridView.Visibility = Visibility.Hidden;
                            answerTextBox.Text = "Ваш запрос некорректен, пожалуйста проверьте правильность" +
                                "написания вашего запроса";
                            break;
                    }

                }
            }
            catch
            {
                MessageBox.Show("в тело вашего запроса закралась ошибка, повторите попытку");
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
