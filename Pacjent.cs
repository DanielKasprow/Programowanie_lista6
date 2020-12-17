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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace Daniel_Kasprów_lista6
{
    public class Pacjent
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string ulica { get; set; }
        public string miasto { get; set; }
        public string kraj { get; set; }
        public long pesel { get; set; }
        public int nr { get; set; }
        public int wiek { get; set; }
        public byte[] image { get; set; }

        [XmlIgnore]
        public BitmapImage obraz = new BitmapImage();
        [XmlIgnore]
        private int imageByteArray;

        //public string picture { get; set; }

         //public Uri uri { get; set; }

         [XmlElement("LargeIcon")]
         //[XmlIgnore]
         public byte[] IconUrl
         {
             get
             { // serialize
               //if (obraz == null) return null;
               //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
               //encoder.Frames.Add(BitmapFrame.Create(obraz));
               //using (MemoryStream ms = new MemoryStream())
               //{
               //    encoder.Save(ms);
               //    return ms.ToArray();
               //}
                return null;
             }
             set
             { // deserialize
                 if (value == null)
                 {
                     obraz = null;
                 }
                 else
                 {
                     using (MemoryStream ms = new MemoryStream(value))
                     {
                         obraz = new BitmapImage();
                         obraz.BeginInit();
                        obraz.CreateOptions = BitmapCreateOptions.PreservePixelFormat;

                        obraz.CacheOption = BitmapCacheOption.OnLoad;
                        obraz.UriSource = null;
                        obraz.StreamSource = ms;
                         obraz.EndInit();
                     }
                    obraz.Freeze();
                 }
             }
         }

         //protected Uri(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext);

        public Pacjent(string nimie, string nnazwisko, string nulica, string nmiasto, string nkraj, int nnr, int nwiek, long npesel, byte[] nimage)
        {
            imie = nimie;
            nazwisko = nnazwisko;
            ulica = nulica;
            miasto = nmiasto;
            kraj = nkraj;
            nr = nnr;
            wiek = nwiek;
            pesel = npesel;
            image = nimage;
            //picture = nobraz;
            //uri = new Uri (nobraz,UriKind.Absolute);

            //obraz = new BitmapImage();
            //obraz.BeginInit();
           // obraz.UriSource = null;
            //obraz.EndInit();
        }
        public Pacjent()
        {
        }
    }
}
