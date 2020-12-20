using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LocalWeather
{
    
    public class FetchWeather
    {
        private const string APIKey = PrivateKeys.APIKey;
        private const string ForecastURL = "http://api.openweathermap.org/data/2.5/weather?@QUERY@&units=imperial&mode=xml&APPID=" + APIKey;
        private string CurrentURL;
        public XmlDocument WeatherData;
        public struct WeatherValues
        {
            public WeatherValues(double _current, double _high, double _low, double _speed, string _direction, int _cover, double _rainfall, string _description)
            {
                TempCurrent = _current;
                TempHigh = _high;
                TempLow = _low;
                WindSpeed = _speed;
                WindDirection = _direction;
                CloudCover = _cover;
                Rainfall = _rainfall;
                Description = _description;
            }

            public double TempCurrent;
            public double TempHigh;
            public double TempLow;
            public double WindSpeed;
            public double Rainfall;
            public string WindDirection;
            public string Description;
            public int CloudCover;


        }

        private readonly WeatherValues BlankValues = new WeatherValues(0, 0, 0, 0, "Unknown", 0, 0, "Unknown");
        public WeatherValues ReadValues;
        
        public FetchWeather ()
        {
            CurrentURL = ForecastURL;
            WeatherData = new XmlDocument();
            ReadValues = BlankValues;
        }

        public WeatherValues GetValues()
        {
            return ReadValues;
        }

        public void SetCurrentURL (string City) //sets URL to gather data based on city
        {
            CurrentURL = ForecastURL.Replace("@QUERY@", $"q={City}");
        }

        public void SetCurrentURL(double latitude, double longitude) //set URL to gather data based on geographic coords
        {
            CurrentURL = ForecastURL.Replace("@QUERY@", $"lat={latitude}&lon={longitude}");
            Console.WriteLine(CurrentURL);
        }

        public void SetCurrentURL(int zipcode) // gathers data based on zip code
        {
            CurrentURL = ForecastURL.Replace("@QUERY@", $"zip={zipcode}");
        }

        public void GetWeatherData()
        {
        try
            {
                WeatherData.Load(CurrentURL);
                SetWeatherValues();
            }
        catch (WebException ex)
            {
                Console.WriteLine(ex);
                ReadValues = BlankValues;
            }
        catch (Exception)
            {
                Console.WriteLine("Unknown error when getting weather data.");
                ReadValues = BlankValues;
            }
        }

        private void SetWeatherValues()
        {
            try
            {
                //reads temperature values (current, high, low)
                XmlNode temp_node = WeatherData.SelectSingleNode("current/temperature");
                double TempCurrent = double.Parse(temp_node.Attributes["value"].Value);
                double TempHigh = double.Parse(temp_node.Attributes["max"].Value);
                double TempLow = double.Parse(temp_node.Attributes["min"].Value);

                //reads wind speed
                XmlNode windspeed_node = WeatherData.SelectSingleNode("current/wind/speed");
                double WindSpeed = double.Parse(windspeed_node.Attributes["value"].Value);

                //reads wind direction
                XmlNode winddir_node = WeatherData.SelectSingleNode("current/wind/direction");
                string WindDirection = winddir_node.Attributes["code"].Value;

                //reads cloud cover value
                XmlNode clouds_node = WeatherData.SelectSingleNode("current/clouds");
                int CloudCover = int.Parse(clouds_node.Attributes["value"].Value);

                //reads current precipitation
                XmlNode rain_node = WeatherData.SelectSingleNode("current/precipitation");
                double Rainfall = 0;
                if (!rain_node.Attributes["mode"].Value.Equals("no"))
                {
                    Rainfall = double.Parse(rain_node.Attributes["value"].Value);
                    //Console.WriteLine(Rainfall);
                }

                // reads the weather description
                XmlNode weather_node = WeatherData.SelectSingleNode("current/weather");
                string WeatherDescription = weather_node.Attributes["value"].Value;
                Console.WriteLine(WeatherDescription);

                ReadValues = new WeatherValues(TempCurrent,
                    TempHigh,
                    TempLow,
                    WindSpeed,
                    WindDirection,
                    CloudCover,
                    Rainfall,
                    WeatherDescription);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to read XML data.");
                ReadValues = BlankValues;
            }
            
        }
    }

}
