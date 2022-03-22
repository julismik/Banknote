using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Banknote
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool Eingabeprufung(string nummer)
        {
            Regex regex = new Regex("^[0-9]+$");
            bool ruckgabe = false;
            if (nummer.Length != 12)
            {
                Ausgabe.Text = "Die Serinnummer muss folgendes Format haben: XX 000 000 000 0";
                ruckgabe = false;
                return ruckgabe;
            }
            else if ((!char.IsLetter(nummer[0])) || (!char.IsLetter(nummer[0])))
            {
                Ausgabe.Text = "Die Serinnummer muss folgendes Format haben: XX 000 000 000 0";
                ruckgabe = false;
                return ruckgabe;
            }
            else if (!Regex.IsMatch(nummer.Substring(2, nummer.Length - 2), "^[0-9]+$"))
            {
                Ausgabe.Text = "Die letzte 10 Stellen muss Ziffern sein!";
                ruckgabe = false;
                return ruckgabe;
            }
            else
            {
                Ausgabe.Text = "";
                return true;
            }

            // ohne bool just return false
        }
       
        private void Button(object sender, RoutedEventArgs e)
        {
            if (Eingabeprufung(Eingabe.Text))
            {
                Check(Eingabe.Text);
            }
           
        }

        private void Eingabe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Eingabeprufung(Eingabe.Text))
                {
                    Check(Eingabe.Text);
                }
            }
        }

        private void Check(string nummer)
        {
            int countrycode1, countrycode2;
            countrycode1 = nummer.ToUpper()[0]-64;
            countrycode2 = nummer.ToUpper()[1]-64;
            string zahl = countrycode1.ToString() + countrycode2.ToString() + nummer.Substring(2, nummer.Length - 3);
            
            int quersumme = 0;
            foreach(char c in zahl)
            {
                quersumme += Convert.ToInt32(c.ToString());
            }
            Ausgabe.Text = quersumme.ToString();

            int rest = quersumme % 9;
            int kontrollnummer = Convert.ToInt32(nummer.Last().ToString());
            if((7 - rest == kontrollnummer ) || (7 - rest == 0  && kontrollnummer == 9 ) || (7 - rest == -1 && kontrollnummer == 8))
            {
                Ausgabe.Text = "Die Seriennummer ist gültig!";
            }
            else Ausgabe.Text = "Die Seriennummer ist  NICHT gültig!";



        }

    }
}

