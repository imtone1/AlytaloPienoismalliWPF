using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
using System.Windows.Threading;


namespace AlytaloWPF
{
    public partial class MainWindow : Window
    {
        #region valot
        Lights valotKeittio = new Lights();
        Lights valotOlohuone = new Lights();
        Color valo = Colors.Yellow; //globaali muuttuja, koska väri ei muutu
        #endregion

        Thermostat lampotila = new Thermostat();
        Sauna sauna = new Sauna();
        public DispatcherTimer SaunaAjastin = new DispatcherTimer();
        public DispatcherTimer SaunaJaahdytys = new DispatcherTimer();

        //puhesyntetisaattoria varten
        Boolean saunaPaalla = false;
      
        //List<Lokitieto> LokiLista = new List<Lokitieto>();

        public static List<Lokitieto> LokiLista = new List<Lokitieto>();  //Lokitieto.cs tulee olla public

        public MainWindow()
        {
            InitializeComponent();
            AsetaDataKentat();
            #region Valot
            valotKeittio.Switched = false;
            valotOlohuone.Switched = false;
            txtValoInfo.Visibility = Visibility.Hidden;
            txtValoInfo2.Visibility = Visibility.Hidden;
            if (valotKeittio.Switched)
            {
                byte variVoim;
                valo.A = 0; // (alpha, 0 meaning completely transparent)
                variVoim = valo.A;
                Valot(variVoim);
            }
            #endregion

            SaunaAjastin.Tick +=new EventHandler(SaunaAjastimen_Tick);
            SaunaAjastin.Interval = new TimeSpan (0,0,2);
         
            SaunaJaahdytys.Tick += new EventHandler(SaunaJaahdytyksen_Tick);
            SaunaJaahdytys.Interval = new TimeSpan(0, 0, 1);
            
            txtInfo.Visibility = Visibility.Hidden;
        }
        #region Valot

        private void btnKeittioValot_Click(object sender, RoutedEventArgs e)
        {
            byte variVoim;
            if (valotKeittio.Switched)
            {
                valotKeittio.Switched = false;
                valo.A = 0;
                variVoim = valo.A;
                Valot(variVoim);
            }
            else
            {
                valotKeittio.Switched = true;
                valo.A = ((byte)(((byte)Convert.ToInt32(sliderHimmenninKeittio.Value)) * 2.5));
                variVoim = valo.A;
                Valot(variVoim);
            }

        }
        private void Valot(byte variV)
        {
            valo.A = variV;
            lblKeittioValo.Background = new SolidColorBrush(valo);
            lblKeittioValo2.Background = new SolidColorBrush(valo);

        }
        private void Valot2(byte variV)
        {
            valo.A = variV;
            lblLastenValo.Background = new SolidColorBrush(valo);
            lblOlohuoneValo.Background = new SolidColorBrush(valo);
            lblOlohuoneValo2.Background = new SolidColorBrush(valo);
        }

        private void sliderHimmenninKeittio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte variVoim;
            valo.A = (byte)Convert.ToInt32(sliderHimmenninKeittio.Value);
            txtHimmennyslkmKeittio.Text = e.NewValue.ToString();
            valo.A = ((byte)(((byte)Convert.ToInt32(sliderHimmenninKeittio.Value)) * 2.5));
            variVoim = valo.A;
            Valot(variVoim);
        }

        private void btnValotOlohuone_Click(object sender, RoutedEventArgs e)
        {
            byte variVoim;
            if (valotOlohuone.Switched)
            {
                valotOlohuone.Switched = false;
                valo.A = 0;
                variVoim = valo.A;
                Valot2(variVoim);
            }
            else
            {
                valotOlohuone.Switched = true;
                valo.A = ((byte)(((byte)Convert.ToInt32(sliderOlohuone.Value)) * 2.5));
                variVoim = valo.A;
                Valot2(variVoim);
            }
        }

        private void sliderOlohuone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte variVoim;
            txtHimmennys.Text = e.NewValue.ToString();
            valo.A = ((byte)(((byte)Convert.ToInt32(sliderOlohuone.Value)) * 2.5));
            variVoim = valo.A;
            Valot2(variVoim);
        }
        private void btnValoInfo2_Click(object sender, RoutedEventArgs e)
        {
            if (txtValoInfo2.Visibility == Visibility.Hidden)
            {
                txtValoInfo2.Visibility = Visibility.Visible;
            }
            else
            {
                txtValoInfo2.Visibility = Visibility.Hidden;
            }
        }

        private void btnValoInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtValoInfo.Visibility == Visibility.Hidden)
            {
                txtValoInfo.Visibility = Visibility.Visible;
            }
            else
            {
                txtValoInfo.Visibility = Visibility.Hidden;
            }
            //himmentimen säätäminen syöttämällä luku tekstikenttään
            //private void txtHimmennyslkmKeittio_TextChanged(object sender, TextChangedEventArgs e)
            //{
            //    string val = (txtHimmennyslkmKeittio.Text != "") ? txtHimmennyslkmKeittio.Text : "0";
            //    sliderHimmenninKeittio.Value = int.Parse(val);
            //}
            #endregion

        }
        #region Lämpötila
        private void btnAsetaLampotila_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lampotila.Temperature = int.Parse(txtAsetaLampotila.Text);
                txtNykLampotila.Text = lampotila.Temperature.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Annathan syötteen numeromuodossa.");
                txtAsetaLampotila.Focus();
            }
            txtAsetaLampotila.Text = "";


        }
        #endregion

        #region Sauna
        private void btnSauna_Click(object sender, RoutedEventArgs e) //sammuttaa saunan
        {
                sauna.Switched = false;
                saunaPaalla = false;
                Color valopois = Colors.Red;
                valopois.A = 0;
                lblSaunaPaalla2.Content = "";
                lblSaunaPaalla2.Background = new SolidColorBrush(valopois);
                lblSaunaPaalla.Background = new SolidColorBrush(valopois);

            gaugeSauna.MinValue = int.Parse(txtNykLampotila.Text);
            gaugeSauna.CurrentValue = sauna.Lampotila;
            
            sauna.SaunaPois();
            SaunaAjastin.Stop();
            SaunaJaahdytys.Start();
           
        }


        private void btnAsetaSaunanLamp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sauna.TavoiteLampotila = int.Parse(txtAsetaSaunaLamp.Text);
                gaugeSauna.MinValue = int.Parse(txtNykLampotila.Text);
              
                
                if (sauna.Lampotila < sauna.TavoiteLampotila)
            {
                sauna.SaunaPaalle();
                sauna.Switched = true;
                    sauna.Switched = true;
                   saunaPaalla = true;
                  lblSaunaPaalla2.Content = "SAUNA PÄÄLLÄ";
                   lblSaunaPaalla2.Background = new SolidColorBrush(Colors.Red);
                   lblSaunaPaalla.Background = new SolidColorBrush(Colors.Red);
                    gaugeSauna.CurrentValue = sauna.Lampotila; //mittari näyttää nykyisen lukeman
            }
                
                SaunaJaahdytys.Stop();
            SaunaAjastin.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Annathan syötteen numeromuodossa.");
                txtAsetaSaunaLamp.Focus();
            }
            gaugeSauna.MaxValue = 100;
            
            //gaugeSauna.CurrentValue = sauna.Lampotila;

  
        } 
           
        private void btnAstuSaunaan_Click(object sender, RoutedEventArgs e)
        {
            WindowSauna saunaan = new WindowSauna();
            saunaan.Show();
        }


        private void SaunaAjastimen_Tick(object sender, EventArgs e)
        {

            if (sauna.Lampotila < sauna.TavoiteLampotila) //jos nykyinen nopeus kuin maksiminopeus
            {
                btnAsetaSaunanLamp_Click(btnAsetaSaunanLamp, new RoutedEventArgs(Button.ClickEvent)); 
            }
            else
            {
                SaunaAjastin.Stop(); //jos tavoitelämpötila saavutettu, sammutetaan taimeri
            }

        }

        private void SaunaJaahdytyksen_Tick (object sender, EventArgs e)
        {
            if (sauna.Lampotila >= gaugeSauna.MinValue)
            {
                btnSauna_Click(btnSauna, new RoutedEventArgs(Button.ClickEvent)); 
            }
            else
            {
                SaunaJaahdytys.Stop(); //jos tavoitelämpötila saavutettu, sammutetaan taimeri
            }
        }
        //private void btnTapahtumalokiin_Click(object sender, RoutedEventArgs e)
        //{
        //    frmPaige.NavigationService.Navigate(new Page1());
           
        //}


        #endregion

        #region Taulukko
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
        }

        private void btnTapahtumalokiin_Click_1(object sender, RoutedEventArgs e)
        {
            Lokitieto Loki = new Lokitieto();
            
            Loki.aika = DateTime.Now;
            Loki.KeittioValo = txtHimmennyslkmKeittio.Text;
            Loki.OlohuoneValo = txtHimmennys.Text;
            Loki.TaloTermostaatti = txtNykLampotila.Text;
            Loki.SaunaPaallaData = lblSaunaPaalla2.Content.ToString();
            Loki.SaunanLampotila = gaugeSauna.CurrentValue.ToString();
            Loki.AjastinPaallaTieto = sauna.Switched;
          
            LokiLista.Add(Loki);
            dtgLokitiedot.Items.Add(Loki);
        }

        
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            DataGridWindow dgWindow = new DataGridWindow();
            dgWindow.Show();
        }

        #endregion

        #region info ja puhe

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtInfo.Visibility == Visibility.Hidden)
            {
                txtInfo.Visibility = Visibility.Visible;
            }
            else txtInfo.Visibility = Visibility.Hidden;
        }

        private void btnSano_Click(object sender, RoutedEventArgs e)
        { 
            SpeechSynthesizer sano = new SpeechSynthesizer();
            if (saunaPaalla)
            {
                sano.Speak("Sauna is on.");
            }
            else sano.Speak("Sauna is off.");
            sano.Speak("Temperature in the sauna is " + sauna.Lampotila.ToString());

            if (valotKeittio.Switched) {
                sano.Speak("Lights is on in kitchen");
            }
            else
            {
                sano.Speak("Lights in off in kitchen");
            }
            
            if (valotOlohuone.Switched) 
            { 
                sano.Speak("Lights is on in living room.");
             }
            else
            {
                sano.Speak("Lights in off in living room.");
            }
            sano.Speak("Temperature in the house is" + lampotila.Temperature.ToString());
            
        }

        private void btnInfoPuhe_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer sano = new SpeechSynthesizer();
            sano.Speak("Welcome to the Smarthouse application! You are able to switch lights on or off and just dimm the lights in kitchen, living room and kids room. You can regulate a temperature of the house. By pressing Sauna button you can set up it's temperature.");
        }
      
        #endregion


        //private void btnToPageOne_Click(object sender, RoutedEventArgs e)
        //{
        //    frmPaige.NavigationService.Navigate(new Page1());
        //}
    }
}
