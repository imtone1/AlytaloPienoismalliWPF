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

namespace AlytaloWPF
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        List<Lokitieto> LokiLista = new List<Lokitieto>();
        public Page1()
        {
            InitializeComponent();
        }
        
        
        private void AsetaDataKentat()
        {
            DataGridTextColumn textColAika = new DataGridTextColumn();
            DataGridTextColumn textColKeittio = new DataGridTextColumn(); //luodaan olion, ja kolumnit
            DataGridTextColumn textColOlohuone = new DataGridTextColumn();
            DataGridTextColumn textColTaloTermostaatti = new DataGridTextColumn();
            DataGridTextColumn textColSaunaPaalla = new DataGridTextColumn();
            DataGridTextColumn textColSaunaLampotila = new DataGridTextColumn();
            DataGridTextColumn textColAjastin = new DataGridTextColumn();


            textColAika.Binding = new Binding("aika"); //baindaus tehdään, viitataan olion ominaisuuteen mikä näkyy kolumnissa
            textColKeittio.Binding = new Binding("KeittioValo");
            textColOlohuone.Binding = new Binding("OlohuoneValo");
            textColTaloTermostaatti.Binding = new Binding("TaloTermostaatti");
            textColSaunaPaalla.Binding = new Binding("SaunaPaallaData");
            textColSaunaLampotila.Binding = new Binding("SaunanLampotila");
            textColAjastin.Binding = new Binding("Ajastinpäällä");

            //sarakeotsikot luodaan
            textColAika.Header = "Aika"; //baindaus tehdään, viitataan olion ominaisuuteen mikä näkyy kolumnissa
            textColKeittio.Header = "Keittiön valon asetus";
            textColOlohuone.Header = "Olohuoneen valon asetus";
            textColTaloTermostaatti.Header = "Olohuoneen valon asetus";
            textColSaunaPaalla.Header = "Saunan päälläolotieto";
            textColSaunaLampotila.Header = "Saunan lämpötila";
            textColAjastin.Header = "Ajastin päällä";

            //LUO SARAKKEET >KEKSI MITEN VIET LISTALLE >LISÄÄ METODIN KUTSU PUBLIC PAGE1

            dtgLokitiedot.Columns.Add(textColAika);
            dtgLokitiedot.Columns.Add(textColKeittio);
            dtgLokitiedot.Columns.Add(textColOlohuone);
            dtgLokitiedot.Columns.Add(textColTaloTermostaatti);
            dtgLokitiedot.Columns.Add(textColSaunaPaalla);
            dtgLokitiedot.Columns.Add(textColSaunaLampotila);
            dtgLokitiedot.Columns.Add(textColAjastin);

        }
        Lokitieto Loki = new Lokitieto();
        private void btnLokiin_Click(object sender, RoutedEventArgs e)
        {
            //TARKISTA TOIMIIKO NÄIN MAIN IKKUNASTA TUONTI!!!!
            MainWindow paasivu = new MainWindow();
            string keittioHimmennys;
            keittioHimmennys = paasivu.txtHimmennyslkmKeittio.Text;
            Loki.aika = DateTime.Now;
            Loki.KeittioValo = keittioHimmennys;
            Loki.OlohuoneValo = paasivu.txtHimmennys.Text;
            Loki.SaunaPaallaData = paasivu.lblSaunaPaalla2.Content.ToString();
            Loki.SaunanLampotila = paasivu.gaugeSauna.CurrentValue.ToString();
            Loki.TaloTermostaatti = paasivu.txtNykLampotila.Text;
            LokiLista.Add(Loki);
            dtgLokitiedot.Items.Add(Loki);
        }

        private void btnToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            this.NavigationService.RemoveBackEntry();
        }

        //public void btnTapahtumalokiin_Click(object sender, RoutedEventArgs e)
        //{
        //    Lokitieto Loki = new Lokitieto();

        //    //TARKISTA TOIMIIKO NÄIN MAIN IKKUNASTA TUONTI!!!!
        //    MainWindow paasivu = new MainWindow();
        //    string keittioHimmennys;
        //    keittioHimmennys = paasivu.txtHimmennyslkmKeittio.Text;
        //    Loki.aika = DateTime.Now;
        //    Loki.KeittioValo = keittioHimmennys;
        //    Loki.OlohuoneValo = paasivu.txtHimmennys.Text;
        //    Loki.SaunaPaallaData = paasivu.lblSaunaPaalla2.Content.ToString();
        //    Loki.SaunanLampotila = paasivu.gaugeSauna.CurrentValue.ToString();
        //    Loki.TaloTermostaatti = paasivu.txtNykLampotila.Text;
        //    LokiLista.Add(Loki);
        //    dtgLokitiedot.Items.Add(Loki);
        //}
    }
}
