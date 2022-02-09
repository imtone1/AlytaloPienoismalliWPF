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
using System.Windows.Threading;

namespace AlytaloWPF
{
    /// <summary>
    /// Interaction logic for WindowSauna.xaml
    /// </summary>
    public partial class WindowSauna : Window
    {
        //Sauna sauna = new Sauna();
        
        //public DispatcherTimer SaunaAjastin = new DispatcherTimer();
        public WindowSauna()
        {
            InitializeComponent();
            txtInfo.Visibility = Visibility.Hidden;
            //SaunaAjastin.Tick += SaunaAjastimen_Tick;
            //SaunaAjastin.Interval = new TimeSpan(0, 0, 0, 500);
        }
        //private void SaunaAjastimen_Tick (object sender, EventArgs e)
        //{
            
        //}
        //private void btnAsetaSaunaLampotila_Click(object sender, RoutedEventArgs e)
        //{
        //    sauna.Lampotila = int.Parse(txtAsetaSaunaLampotila.Text);
        //    sauna.SaunaPaalle();
        //    SaunaAjastin.Start();
        //    gaugeSauna.MaxValue = int.Parse(txtAsetaSaunaLampotila.Text);
            
        //}
       
           
private void btnAstuPoisSaunasta_Click(object sender, RoutedEventArgs e)
        {
 MainWindow saunasta = new MainWindow();
            saunasta.Show();
        }


        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtInfo.Visibility == Visibility.Hidden)
            {
                txtInfo.Visibility = Visibility.Visible;
            }else txtInfo.Visibility = Visibility.Hidden;
        }
    }
}
