﻿
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
}

