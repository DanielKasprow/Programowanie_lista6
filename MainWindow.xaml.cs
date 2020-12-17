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

namespace Daniel_Kasprów_lista6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connetionString;
        SqlConnection cnn;
        SqlCommand command;
        String sql,sql2;
        public int i;

        public static ObservableCollection<Pacjent> klient = new ObservableCollection<Pacjent>();

        AddPacjent addPacjent = new AddPacjent();
        ChangePacjent changePacjent = new ChangePacjent();
        FunkcjaPacjent funkcjaPacjent = new FunkcjaPacjent();
        public MainWindow()
        {
            InitializeComponent();
            addPacjent = new AddPacjent(this);
            changePacjent = new ChangePacjent(this);
            funkcjaPacjent = new FunkcjaPacjent(this);
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
            connetionString = @"Data Source=DESKTOP-3SJ6CNC\ASDF2019;Initial Catalog=Lista6;User ID=sa;Password=asdf";
            //connetionString = @"Server = localhost; Database = Music; User Id = SA; Password = yourStrong(!)Password;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlDataReader dataReader;

            sql = "Select Imie,Nazwisko,Ulica,Nr,Miasto,Kraj,Wiek,Pesel,Image from Base";

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
                //pacjent.image = Encoding.ASCII.GetBytes((string)dataReader["Image"]);
                pacjent.image = (byte[])dataReader["Image"];
                klient.Add(pacjent);
            }
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        public  void savebaze(string ssql,byte[] someByteArray)
        {
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();


            command = new SqlCommand(ssql, cnn);
            command.Parameters.Add("@data", SqlDbType.VarBinary, int.MaxValue).Value = someByteArray;

            //adapter.InsertCommand = new SqlCommand(ssql, cnn);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
        }

        public void changebaze(string ssql)
        {
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();

            command = new SqlCommand(ssql, cnn);

            adapter.UpdateCommand = new SqlCommand(ssql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
        }
        private void refresh()
        {
            Persons.ItemsSource = "";
            Persons.ItemsSource = klient;
        }
        private void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
            addPacjent.refresh();
            addPacjent.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                i = Persons.SelectedIndex;
                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                sql2 = "Delete Baza where Pesel="+klient[i].pesel;

                command = new SqlCommand(sql2, cnn);

                adapter.DeleteCommand = new SqlCommand(sql2, cnn);
                adapter.DeleteCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();
                InitializeSql();
                refresh();
            }
            catch
            {
                MessageBox.Show("Nie wybrano osoby");
            }

        }

        private void ButtonFunkcja_Click(object sender, RoutedEventArgs e)
        {
            funkcjaPacjent.Show();
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                  i = Persons.SelectedIndex;
                  changePacjent.refresh();
                  changePacjent.Show();
            }
            catch
            {
                MessageBox.Show("Nie wybrano osoby");
            }
        }
       

    }
   
}
