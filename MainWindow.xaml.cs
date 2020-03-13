using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbresidenza.Items.Add("Perugia");
            cmbresidenza.Items.Add("Napoli");
            cmbresidenza.Items.Add("Firenze");
            cmbresidenza.Items.Add("Milano");
            cmbresidenza.Items.Add("Torino");

        }

        private const string file = "File.csv";



        List<string> lst = new List<string>();



        string frase;



        string sesso;



        DateTime date;

        private void Calcola_Click(object sender, RoutedEventArgs e)
        {


            if ((string.IsNullOrWhiteSpace(txtcognome.Text)) || string.IsNullOrWhiteSpace(txtnome.Text) || (cmbresidenza.SelectedItem == null) || (string.IsNullOrWhiteSpace(txtimportorichiesto.Text)) || (string.IsNullOrWhiteSpace(txtrate.Text)) || (dtpdata.SelectedDate == null))
            {
                MessageBox.Show("Inserire tutti i campi richiesti!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            else if ((rdbF.IsChecked == false) && (rdbM.IsChecked == false))
            {
                MessageBox.Show("Inserire il sesso", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            date = (DateTime)dtpdata.SelectedDate;



            if (date >= DateTime.Today)
            {
                MessageBox.Show("Inserire una data valida", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            double richiesta = double.Parse(txtimportorichiesto.Text);
            int rate = int.Parse(txtrate.Text);
            int percentuale = int.Parse(txtpercentuale.Text);



            double restituire = richiesta + (richiesta * percentuale / 100);
            double importorata = restituire / rate;



            txtimportodarestituire.Text = restituire.ToString();
            txtimportoperrata.Text = importorata.ToString();



            string nato = "";
            if (rdbF.IsChecked == true)
            {
                sesso = "F";
                nato = "nata";

            }
            else if (rdbM.IsChecked == true)
            {
                sesso = "M";
                nato = "nato";
            }

            frase = $"{txtcognome.Text} {txtnome.Text}, residente in {cmbresidenza.SelectedItem} {nato} il {date.ToShortDateString()}. Prestito di € {richiesta} ad un tasso del {percentuale}% da restituire in {rate} rate da {importorata}€ ciascuna, per un totale di {restituire}€.";



            lst.Add($"{txtcognome.Text}; {txtnome.Text} ; {cmbresidenza.SelectedItem}; {sesso} ;{date.ToShortDateString()}; {richiesta} ; {percentuale};{rate} ;{importorata}; {restituire}");



            lbltotale.Content += $"{frase} \n";

        }

        private void Stampa_Click_1(object sender, RoutedEventArgs e)
        {


            lst.Sort();



            StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8);
            {


                sw.WriteLine($"Cognome; Nome; Città; Sesso; Data di Nascita; Importo richiesto €; % di interesse; Numero di rate; Importo rata €; Totale € da restituire");
                foreach (string frase in lst)
                {
                    sw.WriteLine(frase);
                }
            }
            sw.Close();
        }

        private void Nuovo_Click_2(object sender, RoutedEventArgs e)
        {


            txtcognome.Clear();
            txtnome.Clear();
            txtimportorichiesto.Clear();
            txtpercentuale.Clear();
            txtrate.Clear();
            txtimportorichiesto.Clear();
            txtimportodarestituire.Clear();

            txtimportoperrata.Clear();

            dtpdata.SelectedDate = null;
            rdbF.IsChecked = false;
            rdbM.IsChecked = false;
            cmbresidenza.SelectedItem = null;
        }
    }



}
