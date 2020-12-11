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
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Drawing;

namespace Daniel_Kasprów_lista6
{
    /// <summary>
    /// Interaction logic for AddPacjent.xaml
    /// </summary>
    public partial class AddPacjent : Window
    {
        public String sqladd = "";


        Pacjent kln;

        MainWindow mainwindow;

        private string picture;
        private byte[] byteImage;
        // Uri uri;
        public AddPacjent()
        {
            InitializeComponent();
            //TextPesel.MaxLength = 11;
        }

        public AddPacjent(MainWindow mainwindow) : this()
        {
            this.mainwindow = mainwindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                {
                    openFileDialog.Title = "Select picture of patient";
                    openFileDialog.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                    openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                    openFileDialog.Multiselect = false;
                }
                string hhh = "";

                if (openFileDialog.ShowDialog() == true)
                {
                    picture = openFileDialog.FileName;
                    byteImage = File.ReadAllBytes(picture);
                    foreach(byte b in byteImage)
                    {
                        
                        hhh = hhh + (char)b;
                    }
                    
                }
                if (TextPesel.Text.Length == 11)
                {
                    kln = new Pacjent(TextImie.Text, TextNazwisko.Text, TextUlica.Text, TextMiasto.Text, TextKraj.Text, Convert.ToInt32(TextNr.Text), Convert.ToInt32(TextWiek.Text), Convert.ToInt64(TextPesel.Text), byteImage);
                    MainWindow.klient.Add(kln);
                    sqladd = "Insert into Base(Imie,Nazwisko,Ulica,Miasto,Kraj,Nr,Wiek,Pesel,Image) values('" + 
                      TextImie.Text + "','"+ TextNazwisko.Text+"','"+ TextUlica.Text+ "','"+ TextMiasto.Text+ "','" + TextKraj.Text+"',"+ 
                      Convert.ToInt32(TextNr.Text)+","+ Convert.ToInt32(TextWiek.Text)+","+ Convert.ToInt64(TextPesel.Text)+",@data)";
                    mainwindow.savebaze(sqladd,byteImage);
                    this.Hide();
                }
                else MessageBox.Show("zla dlugosc pesela");
            }
           // catch
            {
                MessageBox.Show("nr ulicy,wiek i pesel musi byc liczba");
            }
        }

       
        public void refresh()
        {
            TextImie.Text = "";
            TextNazwisko.Text = "";
            TextUlica.Text = "";
            TextNr.Text = "";
            TextMiasto.Text = "";
            TextNazwisko.Text = "";
            TextKraj.Text = "";
            TextWiek.Text = "";
            TextPesel.Text = "";
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


