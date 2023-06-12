using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;



namespace okno_logowania.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        private void Login()
        {
            string user = txtUserName.Text;
            string password = txtUserPassword.Password;
            string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            try
            {
                String querry = "SELECT * FROM login WHERE Username = '" + txtUserName.Text + "' AND Password = '" + txtUserPassword.Password + "'";
                MySqlDataAdapter sda = new MySqlDataAdapter(querry, connection);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    user = txtUserName.Text;
                    password = txtUserPassword.Password;

                    MainWindow objMainWindow = new MainWindow();
                    //this.Visibility = Visibility.Hidden;
                    objMainWindow.Show();
                    this.Close();
                }

                else
                {
                    invalidLogin.Text = "invalid login or password, try again";
                    txtUserName.Clear();
                    txtUserPassword.Clear();

                    txtUserName.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
