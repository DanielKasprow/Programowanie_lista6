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
using System.Data;
using System.Text.RegularExpressions;

namespace Daniel_Kasprów_lista6
{
    /// <summary>
    /// Interaction logic for FunkcjaPacjent.xaml
    /// </summary>
    public partial class FunkcjaPacjent : Window
    {
        public static ObservableCollection<Pacjent> funkcja = new ObservableCollection<Pacjent>();
        MainWindow mainwindow;
        public FunkcjaPacjent(MainWindow mainWindow) : this()
        {
            this.mainwindow = mainWindow;
        }
        public FunkcjaPacjent()
        {
            InitializeComponent();
            Initializesr();
        }

        private void initializeBinding()
        {
            //klient = new ObservableCollection<Pacjent>();
            Persons.ItemsSource = funkcja;
        }
        private void Initializesr()
        {
            string connetionString;
            SqlConnection cnn;
            SqlCommand command;

            connetionString = @"Data Source=DESKTOP-3SJ6CNC\ASDF2019;Initial Catalog=Lista6;User ID=sa;Password=asdf";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlDataReader dataReader;
            string sql = "Select avg(Wiek) as srednia from Base";

            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                sql = (int)dataReader["srednia"] + "";
            }

           // sqlquery(sql);
            Textsrwiek.Content = "Sr Wiek: " + sql;
            dataReader.Close();
            command.Dispose();
            cnn.Close();

        }
        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
           string sql = "Select * from funkcja(" + Convert.ToInt32(Textsprwiek.Text) + ")";
            sqlquery(sql);
        }
      
        private void ButtonWyjscie_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void sqlquery(string sql)
        {
            string connetionString;
            SqlConnection cnn;
            SqlCommand command;

            connetionString = @"Data Source=DESKTOP-3SJ6CNC\ASDF2019;Initial Catalog=Lista6;User ID=sa;Password=asdf";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlDataReader dataReader;

            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            //MessageBox.Show(Output);

            funkcja = new ObservableCollection<Pacjent>();

            while (dataReader.Read())
            {
                Pacjent pacjent = new Pacjent();
                pacjent.wiek = (int)dataReader["Wiek"];
                pacjent.pesel = (long)dataReader["Pesel"];
                funkcja.Add(pacjent);
            }
            dataReader.Close();
            command.Dispose();
            cnn.Close();
            initializeBinding();
        }
        private void TextIntinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
