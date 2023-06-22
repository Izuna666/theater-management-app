using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace okno_logowania.View
{
    public partial class MainWindow : Window
    {
        private RepertuarRepository repertuarRepository;
        private SaleRepository saleRepository;

        public MainWindow()
        {
            InitializeComponent();
            //txtGodzina.Focus();
            repertuarRepository = new RepertuarRepository();
            saleRepository = new SaleRepository();
            LoadDataIntoGrid();
            GodzinaCombobox();
            LoadDataToComobox();
            combobox1.SelectionChanged += combobox1_SelectionChanged;
            
        }
        private void GodzinaCombobox()
        {
            List<string> godziny = new List<string>()
            {
                "8:00", "9:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00"
            };
            foreach (string item in godziny)
            {
                combobox2.Items.Add(item);
            }
        }


        private void LoadDataToComobox()
        {
            try
            {
                List<SalaClass> salaList = saleRepository.GetSaleList();
                combobox1.ItemsSource = salaList;
                combobox1.DisplayMemberPath = "Nazwa";
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }


        private void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combobox1.SelectedItem is SalaClass selectedSala)
            {
                txtMiejsca.Text = selectedSala.LiczbaMiejsc;
            }
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

        private bool checkMaxSeats()
        {
            if (Convert.ToInt32(txtLiczba.Text) > Convert.ToInt32(txtMiejsca.Text))
            {
                //warning.Text = "wartosc przekracza ilosc dostepnych miejsc";
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkSale() //sprawdza najpierw date jesli taka sama to sprawdza sale jesli taka sama to godzine 
        {
            List<string> columnValuesSala = new List<string>();
            List<string> columnValuesData = new List<string>();
            List<string> columnValuesGodzina = new List<string>();

            foreach (var item in dataGrid1.Items)
            {
                var CustomItemSala = (Repertuar)item;
                string value = CustomItemSala.Sala;
                columnValuesSala.Add(value.ToString());
            }
            foreach (var item in dataGrid1.Items)
            {
                var CustomItemData = (Repertuar)item;
                string value = CustomItemData.Data;
                columnValuesData.Add(value.ToString());
            }
            foreach (var item in dataGrid1.Items)
            {
                var CustomItemGodzina = (Repertuar)item;
                string value = CustomItemGodzina.Godzina;
                columnValuesGodzina.Add(value.ToString());
            }
            string word1 = combobox1.Text;
            string word2 = datePicker.SelectedDate.Value.Date.ToShortDateString();
            string word3 = combobox2.Text;
            if (columnValuesData.Contains(word2))
            {
                if (columnValuesSala.Contains(word1))
                {
                    if (columnValuesGodzina.Contains(word3))
                    {
                        //warning.Text = "Sala jest juz zarezerwowana";
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Repertuar repertuar = new Repertuar()
                {
                    Godzina = combobox2.Text,
                    Data = datePicker.SelectedDate.Value.Date.ToShortDateString(),
                    Sala = combobox1.Text,
                    Nazwa = txtNazwa.Text,
                    LiczbaMiejsc = txtLiczba.Text,
                    Cena = txtCena.Text
                };
                //checkMaxSeats();
                bool sprawdz = checkSale();
                bool sprawdz2 = checkMaxSeats();
                if (sprawdz == true)
                {
                    warning.Text = "Sala jest już zarezerwowana";
                }
                else if (sprawdz2 ==true)
                {
                    warning.Text = "wartość przekracza ilość dostępnych miejsc";
                }
                else
                {
                    repertuarRepository.AddRepertuar(repertuar);
                    warning.Text = "repertuar dodany";
                    ClearText();
                }

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
            try
            {
                if (dataGrid1.SelectedItem != null)
                {
                    Repertuar selectedRepertuar = (Repertuar)dataGrid1.SelectedItem;
                    Repertuar updatedRepertuar = new Repertuar()
                    {
                        Godzina = combobox2.Text,
                        Data = datePicker.SelectedDate.Value.Date.ToShortDateString(),
                        Sala = combobox1.Text,
                        Nazwa = txtNazwa.Text,
                        LiczbaMiejsc = txtLiczba.Text,
                        Cena = txtCena.Text
                    };
                    repertuarRepository.UpdateRepertuar(updatedRepertuar, selectedRepertuar.ID);
                    ClearText();
                };
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

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid1.SelectedItem != null)
                {
                    Repertuar selectedRepertuar = (Repertuar)dataGrid1.SelectedItem;
                    repertuarRepository.RemoveRepertuar(selectedRepertuar.ID);
                    warning.Text = "repertuar usuniety";
                    ClearText();
                }
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
            txtNazwa.Clear();
            txtLiczba.Clear();
            txtCena.Clear();
        }
    }
}
