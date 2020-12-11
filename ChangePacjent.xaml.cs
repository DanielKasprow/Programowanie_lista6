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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Daniel_Kasprów_lista6
{
    /// <summary>
    /// Interaction logic for ChangePacjent.xaml
    /// </summary>
    public partial class ChangePacjent : Window
    {
        Pacjent kln;

        MainWindow mainwindow;
        private byte[] byteImage;

        public ChangePacjent()
        {
            InitializeComponent();
            TextPesel.MaxLength = 11;
        }
        public ChangePacjent(MainWindow mainwindow) : this()
        {
            this.mainwindow = mainwindow;
        }
        private string picture = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            {
                openFileDialog.Title = "Select picture of patient";
                openFileDialog.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Multiselect = false;
            }
            if (openFileDialog.ShowDialog() == true)
            {
                picture = openFileDialog.FileName;
                Zdjecie.Source = new BitmapImage(new Uri(picture));
            }
        }

        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextPesel.Text.Length == 11)
                {
                    kln = new Pacjent(TextImie.Text, TextNazwisko.Text, TextUlica.Text, TextMiasto.Text, TextKraj.Text, Convert.ToInt32(TextNr.Text), Convert.ToInt32(TextWiek.Text), Convert.ToInt64(TextPesel.Text), byteImage);
                    string sqladd = "Update Baza set Imie='"+ TextImie.Text +"',Nazwisko='"+ TextNazwisko.Text +"',Ulica='"+ TextMiasto.Text+"',Kraj='"
                        + TextKraj.Text+"',Nr="+ Convert.ToInt32(TextNr.Text)+",Wiek="+ Convert.ToInt32(TextWiek.Text)+",Pesel="+ Convert.ToInt64(TextPesel.Text)
                        + " where Pesel="+ MainWindow.klient[mainwindow.i].pesel;
                    mainwindow.changebaze(sqladd);
                    MainWindow.klient[mainwindow.i] = (kln);
                    this.Hide();
                }
                else MessageBox.Show("zla dlugosc pesela");
            }
            catch
            {
                MessageBox.Show("nr ulicy,wiek i pesel musi byc liczba");
            }
        }

        public void refresh()
        {
            TextImie.Text = MainWindow.klient[mainwindow.i].imie;
            TextNazwisko.Text = MainWindow.klient[mainwindow.i].nazwisko;
            TextUlica.Text = MainWindow.klient[mainwindow.i].ulica;
            TextNr.Text = MainWindow.klient[mainwindow.i].nr + "";
            TextMiasto.Text = MainWindow.klient[mainwindow.i].miasto;
            TextNazwisko.Text = MainWindow.klient[mainwindow.i].nazwisko;
            TextKraj.Text = MainWindow.klient[mainwindow.i].kraj;
            TextWiek.Text = MainWindow.klient[mainwindow.i].wiek + "";
            TextPesel.Text = MainWindow.klient[mainwindow.i].pesel + "";
            //picture = MainWindow.klient[mainwindow.i].picture;
            //Zdjecie.Source = new BitmapImage(new Uri(picture));
        }
        private void TextIntinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void TextStringinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-z]+").IsMatch(e.Text);
        }
    }
}
