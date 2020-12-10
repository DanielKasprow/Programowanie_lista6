using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Daniel_Kasprów_lista6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static ObservableCollection<Pacjent> klient = new ObservableCollection<Pacjent>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeSql();
            initializeBinding();
        }

        private void initializeBinding()
        {
             //klient = new ObservableCollection<Pacjent>();
             Persons.ItemsSource = klient;
        }
        private void InitializeSql()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-3SJ6CNC\ASDF2019;Initial Catalog=Lista6;User ID=sa;Password=asdf";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select Imie,Nazwisko,Ulica,Nr,Miasto,Kraj,Wiek,Pesel from Baza";

            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            //MessageBox.Show(Output);

            klient = new ObservableCollection<Pacjent>();

            while (dataReader.Read())
            {
                Pacjent pacjent = new Pacjent();
                pacjent.imie = (String)dataReader["Imie"];
                pacjent.nazwisko = (String)dataReader["Nazwisko"];
                pacjent.ulica = (String)dataReader["Ulica"];
                pacjent.nr = (int)dataReader["Nr"];
                pacjent.miasto = (String)dataReader["Miasto"];
                pacjent.kraj = (String)dataReader["Kraj"];
                pacjent.wiek = (int)dataReader["Wiek"];
                pacjent.pesel = (long)dataReader["Pesel"];
               // pacjent.image = (byte[])dataReader["Image"];
                klient.Add(pacjent);
            }



            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        private void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
         //   addPacjent.refresh();
         //   addPacjent.Show();
        }

        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
        //    savefile();
        }

        private void ButtonWczytaj_Click(object sender, RoutedEventArgs e)
        {
            //   openfile();
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           //     i = Persons.SelectedIndex;
          //      changePacjent.refresh();
         //       changePacjent.Show();
            }
            catch
            {
                MessageBox.Show("Nie wybrano osoby");
            }
        }
    }
   
}
