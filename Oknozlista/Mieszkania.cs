using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Oknozlista
{
    public class Mieszkania
    {
        public int id_mieszkania { get; set; }
        public string Osiedle { get; set; }
        public string Adres { get; set; }
        public bool Zgarazem { get; set; }
        public Rodzaj Rodzaj { get; set; }
        public string Metraz { get; set; }
        public bool Dostepnosc { get; set; }
        public string Opis { get; set; }

        public Mieszkania(int id_mieszkania, string osiedle, string adres, bool zgarazem, Rodzaj rodzaj, string metraz, bool dostepnosc, string opis)
        {
            this.id_mieszkania = id_mieszkania;
            Osiedle = osiedle;
            Adres = adres;
            Zgarazem = zgarazem;
            Rodzaj = rodzaj;
            Metraz = metraz;
            Dostepnosc = dostepnosc;
            Opis = opis;
        }

        public Mieszkania()
        {
            id_mieszkania = 0;
        }
    }

    public enum Rodzaj
    {
        rodzinny,
        blizniak,
        blok
    }
}