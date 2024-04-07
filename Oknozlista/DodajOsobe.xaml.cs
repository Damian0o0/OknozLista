using System;
using System.Windows;

namespace Oknozlista
{
    public partial class DodajOsobe : Window
    {
        private readonly MainWindow mainWindow;
        private readonly Mieszkania selectedMieszkanie;

        public DodajOsobe(MainWindow mainWindow, Mieszkania existingMieszkanie = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.selectedMieszkanie = existingMieszkanie;

            if (existingMieszkanie != null)
            {
                UstawPolaTekstowe(existingMieszkanie);
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IdMieszkaniaTextBox.Text, out int idMieszkania))
            {
                string osiedle = OsiedleTextBox.Text;
                string adres = AdresTextBox.Text;
                bool zGarazem = ZGarazemCheckBox.IsChecked ?? false;
                Rodzaj rodzaj = (Rodzaj)RodzajComboBox.SelectedIndex;
                string metraz = MetrazTextBox.Text;
                bool dostepnosc = DostepnoscCheckBox.IsChecked ?? false;
                string opis = OpisTextBox.Text;

                if (selectedMieszkanie != null)
                {
                    UpdateMieszkanie(selectedMieszkanie, idMieszkania, osiedle, adres, zGarazem, rodzaj, metraz, dostepnosc, opis);
                    mainWindow.RefreshDataGrid();
                }
                else
                {
                    mainWindow.DodajNoweMieszkanie(idMieszkania, osiedle, adres, zGarazem, rodzaj, metraz, dostepnosc, opis);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Wprowadzona wartość Id_mieszkania jest nieprawidłowa.", "Błąd", MessageBoxButton.OK);
            }
        }

        private void UstawPolaTekstowe(Mieszkania mieszkanie)
        {
            IdMieszkaniaTextBox.Text = mieszkanie.id_mieszkania.ToString();
            OsiedleTextBox.Text = mieszkanie.Osiedle;
            AdresTextBox.Text = mieszkanie.Adres;
            ZGarazemCheckBox.IsChecked = mieszkanie.Zgarazem;
            RodzajComboBox.SelectedIndex = (int)mieszkanie.Rodzaj;
            MetrazTextBox.Text = mieszkanie.Metraz;
            DostepnoscCheckBox.IsChecked = mieszkanie.Dostepnosc;
            OpisTextBox.Text = mieszkanie.Opis;
        }

        private void UpdateMieszkanie(Mieszkania mieszkanie, int idMieszkania, string osiedle, string adres, bool zGarazem, Rodzaj rodzaj, string metraz, bool dostepnosc, string opis)
        {
            mieszkanie.id_mieszkania = idMieszkania;
            mieszkanie.Osiedle = osiedle;
            mieszkanie.Adres = adres;
            mieszkanie.Zgarazem = zGarazem;
            mieszkanie.Rodzaj = rodzaj;
            mieszkanie.Metraz = metraz;
            mieszkanie.Dostepnosc = dostepnosc;
            mieszkanie.Opis = opis;
        }
    }
}
