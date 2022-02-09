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


namespace AlytaloWPF
{

    public partial class DataGridWindow : Window
    {
        public DataGridWindow()
        {
            InitializeComponent();

            AsetaDataKentat();
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
            textColAjastin.Binding = new Binding("AjastinPaallaTieto");

            //sarakeotsikot luodaan
            textColAika.Header = "Aika"; //baindaus tehdään, viitataan olion ominaisuuteen mikä näkyy kolumnissa
            textColKeittio.Header = "Keittiön valon asetus";
            textColOlohuone.Header = "Olohuoneen valon asetus";
            textColTaloTermostaatti.Header = "Olohuoneen valon asetus";
            textColSaunaPaalla.Header = "Saunan päälläolotieto";
            textColSaunaLampotila.Header = "Saunan lämpötila";
            textColAjastin.Header = "Ajastin päällä";

            

            dtgLokitiedot.Columns.Add(textColAika);
            dtgLokitiedot.Columns.Add(textColKeittio);
            dtgLokitiedot.Columns.Add(textColOlohuone);
            dtgLokitiedot.Columns.Add(textColTaloTermostaatti);
            dtgLokitiedot.Columns.Add(textColSaunaPaalla);
            dtgLokitiedot.Columns.Add(textColSaunaLampotila);
            dtgLokitiedot.Columns.Add(textColAjastin);

            foreach (Lokitieto lokitieto in MainWindow.LokiLista)
            {
                dtgLokitiedot.Items.Add(lokitieto);
            }
        }


        public void WriteDataGrid()
        {
            foreach (Lokitieto lokitieto in MainWindow.LokiLista)
            {
                dtgLokitiedot.Items.Add(lokitieto);
            }
            MainWindow.LokiLista.Clear();
        }
    }
}
