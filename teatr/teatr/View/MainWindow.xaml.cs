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
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtGodzina.Focus();
            LoadDataIntoGrid();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView objloginView = new LoginView();
            objloginView.Show();
            this.Close();

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                String querry = "insert into repertuar (godzina,sala,nazwa,liczbaMiejsc,cena) values('"+this.txtGodzina.Text+"','"+this.txtSala.Text+"','"+this.txtNazwa.Text+"','"+this.txtLiczba.Text+"','"+this.txtCena.Text+"')";
                MySqlCommand createCommand = new MySqlCommand(querry, connection);
                createCommand.ExecuteNonQuery();
                ClearText();
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
                LoadDataIntoGrid();
            }
        }   

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                String querry = "delete from repertuar where godzina='"+this.txtGodzina.Text+"'";
                MySqlCommand createCommand = new MySqlCommand(querry, connection);
                createCommand.ExecuteNonQuery();
                
                ClearText();
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
                LoadDataIntoGrid();
            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDataIntoGrid();
            /*string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            try
            {
                String querry = "SELECT godzina,sala,nazwa,liczba miejsc,cena FROM repertuar  ";
                MySqlCommand createCommand = new MySqlCommand(querry, connection);
                createCommand.ExecuteNonQuery();


                MySqlDataAdapter sda = new MySqlDataAdapter(createCommand);
                DataTable dt = new DataTable("repertuar");
                sda.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                sda.Update(dt);
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
            }*/
        }
        private void LoadDataIntoGrid()
        {
            string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            String querry = "SELECT godzina,sala,nazwa,liczbaMiejsc,cena FROM repertuar  ";
            MySqlCommand createCommand = new MySqlCommand(querry, connection);
            createCommand.ExecuteNonQuery();


            MySqlDataAdapter sda = new MySqlDataAdapter(createCommand);
            DataTable dt = new DataTable("repertuar");
            sda.Fill(dt);
            dataGrid1.ItemsSource = dt.DefaultView;
            sda.Update(dt);
            connection.Close();

        }
        private void ClearText()
        {
            txtGodzina.Clear();
            txtSala.Clear();
            txtNazwa.Clear();
            txtLiczba.Clear();
            txtCena.Clear();
        }
    }
}
