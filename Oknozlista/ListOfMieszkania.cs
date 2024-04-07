using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Oknozlista
{
    internal class ListOfMieszkania
    {
        public ObservableCollection<Mieszkania> mieszkania;

        public ListOfMieszkania()
        {
            mieszkania = new ObservableCollection<Mieszkania>();
            LoadMieszkania("plik.txt");
        }

        public void AddMieszkanie(Mieszkania mieszkanie)
        {
            mieszkania.Add(mieszkanie);
        }
        public void RemoveMieszkanie(Mieszkania mieszkanie)
        {
            mieszkania.Remove(mieszkanie);
        }
        public void EditMieszkanie(int index, Mieszkania mieszkanie)
        {
            mieszkania[index] = mieszkanie;
        }
        public void RemoveMieszkanieAt(int index)
        {
            if (index >= 0 && index < mieszkania.Count)
            {
                mieszkania.RemoveAt(index);
            }
        }

        public void SaveMieszkania(string file)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(file, false, Encoding.UTF8))
                {
                    foreach (var mieszkanie in mieszkania)
                    {
                        writer.WriteLine($"{mieszkanie.id_mieszkania},{mieszkanie.Osiedle},{mieszkanie.Adres},{mieszkanie.Zgarazem},{mieszkanie.Rodzaj},{mieszkanie.Metraz},{mieszkanie.Dostepnosc},{mieszkanie.Opis}");
                    }
                }

                MessageBox.Show("Zapisano");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd");
            }
        }
        public void LoadMieszkania(string file)
        {
            mieszkania.Clear();

            if (File.Exists(file))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(',');
                            int id_mieszkania = int.Parse(parts[0]);
                            string osiedle = parts[1];
                            string adres = parts[2];
                            bool zGarazem = bool.Parse(parts[3]);
                            Rodzaj rodzaj = (Rodzaj)Enum.Parse(typeof(Rodzaj), parts[4]);
                            string metraz = parts[5];
                            bool dostepnosc = bool.Parse(parts[6]);
                            string opis = parts[7];

                            Mieszkania mieszkanie = new Mieszkania(id_mieszkania, osiedle, adres, zGarazem, rodzaj, metraz, dostepnosc, opis);
                            mieszkania.Add(mieszkanie);
                        }
                    }

                    MessageBox.Show("Wczytano Plik");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd");
                }
            }
        }
    }
}
