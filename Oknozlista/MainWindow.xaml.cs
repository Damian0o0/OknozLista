using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Oknozlista
{
    public partial class MainWindow : Window
    {
        private ListOfMieszkania listOfMieszkania;

        public MainWindow()
        {
            InitializeComponent();
            listOfMieszkania = new ListOfMieszkania();
            datagridMieszkania.ItemsSource = listOfMieszkania.mieszkania;

            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Zapisz_Click(sender, e);
            }

            if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Pobierz_Click(sender, e);
            }

            if (e.Key == Key.Z && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Zamknij_Click(sender, e);
            }

            if (e.Key == Key.Q && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Dodaj_Click(sender, e);
            }

            if (e.Key == Key.E && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Zmien_Click(sender, e);
            }

            if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Usun_Click(sender, e);
            }

            if (e.Key == Key.J && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Numer_Click(sender, e);
            }
            if (e.Key == Key.K && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Dane_Click(sender, e);
            }
            if (e.Key == Key.L && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Kontakt_Click(sender, e);
            }
        }
        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                listOfMieszkania.SaveMieszkania(dialog.FileName); 
            }
        }

        private void Pobierz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var path = openFileDialog.FileName;
                listOfMieszkania.LoadMieszkania(path);
                RefreshDataGrid();
            }
        }

        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void RefreshDataGrid()
        {
            datagridMieszkania.Items.Refresh();
        }

        public void DodajNoweMieszkanie(int idMieszkania, string osiedle, string adres, bool zGarazem, Rodzaj rodzaj, string metraz, bool dostepnosc, string opis)
        {
            listOfMieszkania.AddMieszkanie(new Mieszkania(idMieszkania, osiedle, adres, zGarazem, rodzaj, metraz, dostepnosc, opis));
            RefreshDataGrid();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodajOsobe dodajOsobe = new DodajOsobe(this);
            dodajOsobe.ShowDialog();
        }

        private void DatagridMieszkania_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridMieszkania.SelectedItem != null)
            {
                ZmienMenuItem.IsEnabled = true; 
            }
            else
            {
                ZmienMenuItem.IsEnabled = false; 
            }
        }

        private void Zmien_Click(object sender, RoutedEventArgs e)
        {
            if (datagridMieszkania.SelectedItem != null)
            {
                Mieszkania selectedMieszkanie = (Mieszkania)datagridMieszkania.SelectedItem;
                DodajOsobe dodajOsobe = new DodajOsobe(this, selectedMieszkanie);
                dodajOsobe.ShowDialog();
            }
            else
            {
                MessageBox.Show("Proszę wybrać mieszkanie do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            if (datagridMieszkania.SelectedItem != null)
            {
                Mieszkania selectedMieszkanie = (Mieszkania)datagridMieszkania.SelectedItem;
                listOfMieszkania.RemoveMieszkanie(selectedMieszkanie);
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Proszę wybrać mieszkanie do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Numer_Click(object sender, RoutedEventArgs e)
        {
            Numer Kontakt = new Numer();
            Kontakt.ShowDialog();
        }

        private void Dane_Click(object sender, RoutedEventArgs e)
        {
            Dane Danee = new Dane();
            Danee.ShowDialog();
        }

        private void Kontakt_Click(object sender, RoutedEventArgs e)
        {
            Kontakt Formularz = new Kontakt();
            Formularz.ShowDialog();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listOfMieszkania != null)
            {
                string searchText = SearchTextBox.Text.ToLower();

                if (!string.IsNullOrEmpty(searchText))
                {
                    if (listOfMieszkania.mieszkania != null)
                    {
                        var filteredList = listOfMieszkania.mieszkania.Where(mieszkanie =>
                            mieszkanie.id_mieszkania.ToString().ToLower().Contains(searchText) ||
                            mieszkanie.Osiedle.ToLower().Contains(searchText) ||
                            mieszkanie.Adres.ToLower().Contains(searchText) ||
                            mieszkanie.Rodzaj.ToString().ToLower().Contains(searchText) ||
                            mieszkanie.Metraz.ToLower().Contains(searchText) ||
                            mieszkanie.Opis.ToLower().Contains(searchText)
                        ).ToList();

                        datagridMieszkania.ItemsSource = filteredList;
                    }
                }
                else
                {
                    datagridMieszkania.ItemsSource = listOfMieszkania.mieszkania;
                }
            }
        }
    }
}
