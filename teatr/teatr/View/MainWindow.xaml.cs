/*using System;
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
}*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace okno_logowania.View
{
    public partial class MainWindow : Window
    {
        private RepertuarRepository repertuarRepository;

        public MainWindow()
        {
            InitializeComponent();
            txtGodzina.Focus();
            repertuarRepository = new RepertuarRepository();
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
            Close();
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
            try
            {
                Repertuar repertuar = new Repertuar()
                {
                    Godzina = txtGodzina.Text,
                    Sala = txtSala.Text,
                    Nazwa = txtNazwa.Text,
                    LiczbaMiejsc = txtLiczba.Text,
                    Cena = txtCena.Text
                };

                repertuarRepository.AddRepertuar(repertuar);
                ClearText();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                LoadDataIntoGrid();
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dataGrid1.SelectedItem != null)
                {
                    Repertuar selectedRepertuar = (Repertuar)dataGrid1.SelectedItem;
                    repertuarRepository.RemoveRepertuar(selectedRepertuar.ID);
                    ClearText();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                LoadDataIntoGrid();
            }
            
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDataIntoGrid();
        }

        private void LoadDataIntoGrid()
        {
            try
            {
                List<Repertuar> repertuarList = repertuarRepository.GetRepertuarList();
                dataGrid1.ItemsSource = repertuarList;
            }
            catch
            {
                MessageBox.Show("Error");
            }
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

    public class Repertuar
    {
        public bool IsSelected { get; set; }
        public int ID { get; set; }
        public string? Godzina { get; set; }
        public string? Sala { get; set; }
        public string? Nazwa { get; set; }
        public string? LiczbaMiejsc { get; set; }
        public string? Cena { get; set; }
    }

    public class RepertuarRepository
    {
        private string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";

        public void AddRepertuar(Repertuar repertuar)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO repertuar ( godzina, sala, nazwa, liczbaMiejsc, cena) " +
                               $"VALUES ('{repertuar.Godzina}', '{repertuar.Sala}', '{repertuar.Nazwa}', '{repertuar.LiczbaMiejsc}', '{repertuar.Cena}')";
                MySqlCommand createCommand = new MySqlCommand(query, connection);
                createCommand.ExecuteNonQuery();
            }
        }

        public void RemoveRepertuar(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM repertuar WHERE ID = {ID}";
                MySqlCommand createCommand = new MySqlCommand(query, connection);
                createCommand.ExecuteNonQuery();
            }
        }

        public List<Repertuar> GetRepertuarList()
        {
            List<Repertuar> repertuarList = new List<Repertuar>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, godzina, sala, nazwa, liczbaMiejsc, cena FROM repertuar";
                MySqlCommand createCommand = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = createCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Repertuar repertuar = new Repertuar()
                        {
                            ID = reader.GetInt16("ID"),
                            Godzina = reader.GetString("godzina"),
                            Sala = reader.GetString("sala"),
                            Nazwa = reader.GetString("nazwa"),
                            LiczbaMiejsc = reader.GetString("liczbaMiejsc"),
                            Cena = reader.GetString("cena")
                        };
                        repertuarList.Add(repertuar);
                    }
                }
            }

            return repertuarList;
        }
    }
}

