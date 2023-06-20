using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            txtGodzina.Focus();
            repertuarRepository = new RepertuarRepository();
            saleRepository = new SaleRepository();
            LoadDataIntoGrid();
            LoadDataToComobox();
            combobox1.SelectionChanged += combobox1_SelectionChanged;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Repertuar repertuar = new Repertuar()
                {
                    Godzina = txtGodzina.Text,
                    Sala = combobox1.Text,
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
            try
            {
                if (dataGrid1.SelectedItem != null)
                {
                    Repertuar selectedRepertuar = (Repertuar)dataGrid1.SelectedItem;
                    Repertuar updatedRepertuar = new Repertuar()
                    {
                        Godzina = txtGodzina.Text,
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
        private void LoadDataintoTextBox()
        {
            if(dataGrid1.SelectedItem != null)
            {
                Repertuar selectedRepertuar = (Repertuar)dataGrid1.SelectedItem;
                txtGodzina.Text = selectedRepertuar.Godzina;
                combobox1.Text = selectedRepertuar.Sala;
                txtNazwa.Text = selectedRepertuar.Nazwa;
                txtLiczba.Text = selectedRepertuar.LiczbaMiejsc;
                txtCena.Text = selectedRepertuar.Cena;
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
            //txtSala.Clear();
            txtNazwa.Clear();
            txtLiczba.Clear();
            txtCena.Clear();
        }

    }
}
