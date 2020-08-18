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
        public MainWindow()
        {
           
            Tracker = new LocationHandler();
            Tracker.Watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(Refresh);
            InitializeComponent();
            //CoordsBlock.Text = LocationString;
            FetchWeather Fetcher = new FetchWeather();
            Fetcher.SetCurrentURL("Tempe");
            Fetcher.GetWeatherData();
        }

        
        private void Refresh(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Tracker.RefreshLocation();
            //CoordsBlock.Text = Tracker.GetCoordsString(Tracker.CurrentLocation);
        }
    }
}
