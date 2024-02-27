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
            //Передаем в конструктор нашего класса обработчика сам запрос, и конекшн
            DBQueryHandler dBQueryHandler = new DBQueryHandler(quireTextBox.Text,
                                                                con);
            try
            {
                using (SqlCommand command = new SqlCommand(quireTextBox.Text, con))
                {
                    switch (ParseQueri(quireTextBox.Text))
                    {
                        case "reader":
                            //тут и только тут мы будем обрабатывать селект запросы и возвращать таблицу
                            //для этого нам нужен Дата тейбл
                            DataTable dt = dBQueryHandler.Query();
                            //Делаем видимым Дата грид который создан специально для вывода таблиц
                            dataGridView.Visibility = Visibility.Visible;
                            //тут вставляем в дата грид то, что получилось вытащить из таблиц
                            dataGridView.ItemsSource = dt.DefaultView;
                            break;
                        case "nonquery":
                            //тут мы делаем видимым текст бокс что бы в нем отобразить результат того сколько
                            // строк у нас изменилось, т.к. он лучше подходит для этого, соответственно
                            // дата грид мы в этот момент просто скрываем
                            answerTextBox.Visibility = Visibility.Visible;
                            dataGridView.Visibility = Visibility.Hidden;
                            //тут просто впихиваем кол-во изменений
                            answerTextBox.Text = dBQueryHandler.Query(quireTextBox.Text);
                            break;
                        default:
                            //нераспознаный запрос записывается в текст бокс вот и всё грид скрываем
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
                MessageBox.Show("В тело вашего запроса закралась ошибка, повторите попытку",
                    "Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// метод который парсит
        /// </summary>
        /// <param name="text"></param>
        /// <returns> 'select' для метода который обрабатывает selectзапросы
        /// 'nonquery' для тех что обрабатывают delete isnert и update
        /// и unknow если ввели какую то фигню</returns>
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
