using SQL_Client.Connection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQL_Client.DB;

namespace SQL_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void serverNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(serverNameTextBox.Text == "Введите данные сервера")
            {
                serverNameTextBox.Text = string.Empty;
            }
        }

        private void serverNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if( serverNameTextBox.Text == "")
            {
                serverNameTextBox.Text = "Введите данные сервера";
            }
        }

        private void DataBaseTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(DataBaseTextBox.Text == "Введите название базы данных")
            {
                DataBaseTextBox.Text = string.Empty;
            }
        }

        private void DataBaseTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DataBaseTextBox.Text == "")
            {
                DataBaseTextBox.Text = "Введите название базы данных";
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = SQLConnectionProvider.OpenConnection(
                serverNameTextBox.Text,
                DataBaseTextBox.Text)
                )
            {
                
                Hide();
                //new form тут
                DBWork dBWork = new DBWork(con);
                dBWork.ShowDialog();
                Show();
            }
        }
    }
}
