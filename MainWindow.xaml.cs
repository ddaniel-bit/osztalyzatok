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
using System.IO;
using WpfApp1;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fajlNev = "naplo.csv";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OsztalyokBetoltese(string fajlNev)
        {
            List<Osztalyzat> jegyek = new List<Osztalyzat>();
            jegyek.Clear();
            StreamReader sr = new StreamReader(fajlNev);
            while (!sr.EndOfStream)
            {
                string[] mezok = sr.ReadLine().Split(";");
                Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                jegyek.Add(ujJegy);
            }
            sr.Close();
            dgNaplo.ItemsSource = jegyek;
            MessageBox.Show("Az állomány beolvasása");
        }

        private void sliJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblJegy.Content = sliJegy.Value;
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            OsztalyokBetoltese(fajlNev);
        }
        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sr = new StreamWriter("naplo.csv", append: true, encoding: Encoding.UTF8);

            if (txtNev.Text == "" || dpDatum.Text == "")
                MessageBox.Show("nem adtál meg minden adatot");
            else
                sr.WriteLine($"{txtNev.Text};{cboTantargy.Text};{dpDatum.Text};{sliJegy.Value}");
                MessageBox.Show("sikeres írás");

            sr.Close();
        }
    }
}
