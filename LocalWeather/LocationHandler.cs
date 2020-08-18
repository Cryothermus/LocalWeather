using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalWeather
{
    class LocationHandler
    {
        public GeoCoordinateWatcher Watcher;
        public GeoCoordinate CurrentLocation;
       
        public LocationHandler()
        {
            Watcher = new GeoCoordinateWatcher();
            CurrentLocation = GetCurrentLocation();
           

        }

       
        public GeoCoordinate GetCurrentLocation()
        {
            Watcher.TryStart(true, TimeSpan.FromSeconds(.05));
            if (Watcher.Status == GeoPositionStatus.Ready)
            {
                return Watcher.Position.Location;
            }
            else
            {
                Console.WriteLine("Unable to retrieve coordinates.");
                return new GeoCoordinate();
            }
        }

        /*public CivicAddress GetAddress(GeoCoordinate _coordinate)
        {
            if (!_coordinate.IsUnknown)
            {
                Console.WriteLine("Coordinates can be resolved. Testing...");
                CivicAddress result = AddressResolver.ResolveAddress(_coordinate);

                if (result.IsUnknown) Console.WriteLine("Address is unknown.");
                else Console.WriteLine($"Address successfully resolved. Postal code: {result.PostalCode}");

                return result;
            }
            else
            {
                Console.WriteLine("Could not retrieve address.");
                return new CivicAddress();
            }
        }*/

        public void RefreshLocation()
        {
            CurrentLocation = GetCurrentLocation();
            //Address = GetAddress(CurrentLocation);
        }

        public string GetCoordsString(GeoCoordinate _coordinate)
        {
            if (!_coordinate.IsUnknown)
            {
                return $"{_coordinate.Latitude}, {_coordinate.Longitude}";
            }

            else
            {
                return "Location Not Found";
            }
        }


    }
}
