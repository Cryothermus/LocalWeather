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
using System.Device.Location;

namespace LocalWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        LocationHandler Tracker;
        public string LocationString { get; set; }
        FetchWeather Fetcher;
        public MainWindow()
        {
           
            Tracker = new LocationHandler();
            Tracker.Watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(Refresh);
            InitializeComponent();
            Fetcher = new FetchWeather();
            //Fetcher.SetCurrentURL("Tempe");
            //Fetcher.GetWeatherData();
        }


        private void Refresh(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (Tracker.Watcher.Status == GeoPositionStatus.Ready)
            {
                Tracker.RefreshLocation();
                Fetcher.SetCurrentURL(Tracker.CurrentLocation.Latitude, Tracker.CurrentLocation.Longitude);
                Fetcher.GetWeatherData();
            }
            
        }

    }
}
