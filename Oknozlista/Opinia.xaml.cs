using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Oknozlista
{
    public partial class Opinia : Window
    {
        private Kontakt kontakt;
        public Opinia(Kontakt kontakt)
        {
            InitializeComponent();
            this.kontakt = kontakt;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.kontakt.Close();
            this.Close();
        }
    }
}
