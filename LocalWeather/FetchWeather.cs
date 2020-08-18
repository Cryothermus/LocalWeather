using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

// OpenWeatherMap API key: 3c4bb73550d625dacbf72c8a8f80a570

namespace LocalWeather
{
    
    class FetchWeather
    {
        private const string APIKey = "3c4bb73550d625dacbf72c8a8f80a570";
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

        public WeatherValues ReadValues;
        
        public FetchWeather ()
        {
            CurrentURL = ForecastURL;
            WeatherData = new XmlDocument();
            ReadValues = new WeatherValues();
        }

        public void SetCurrentURL (string City) //sets URL to gather data based on city
        {
            CurrentURL = ForecastURL.Replace("@QUERY@", $"q={City}");
        }

        public void SetCurrentURL(double latitude, double longitude) //set URL to gather data based on geographic coords
        {
            CurrentURL = ForecastURL.Replace("@QUERY@", $"lat={latitude}&lon={longitude}");
        }

        public void SetCurrentURL(int zipcode) // gathers data based on zip code
        {
            CurrentURL = ForecastURL.Replace("@QUERY@", $"zip={zipcode}");
        }

        // reference weather doc: http://api.openweathermap.org/data/2.5/weather?lat=33.41&lon=-111.91&units=imperial&mode=xml&APPID=3c4bb73550d625dacbf72c8a8f80a570
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
            }
        catch (Exception)
            {
                Console.WriteLine("Unknown error when getting weather data.");
            }
        }

        private void SetWeatherValues()
        {
            XmlNode temp_node = WeatherData.SelectSingleNode("current/temperature");
            double TempCurrent = double.Parse(temp_node.Attributes["value"].Value);
            double TempHigh = double.Parse(temp_node.Attributes["max"].Value);
            double TempLow = double.Parse(temp_node.Attributes["min"].Value);

            XmlNode windspeed_node = WeatherData.SelectSingleNode("current/wind/speed");
            double WindSpeed = double.Parse(windspeed_node.Attributes["value"].Value);

            XmlNode winddir_node = WeatherData.SelectSingleNode("current/wind/direction");
            string WindDirection = winddir_node.Attributes["code"].Value;

            XmlNode clouds_node = WeatherData.SelectSingleNode("current/clouds");
            int CloudCover = int.Parse(clouds_node.Attributes["value"].Value);

            XmlNode rain_node = WeatherData.SelectSingleNode("current/precipitation");
            double Rainfall = 0;
            if (!rain_node.Attributes["mode"].Value.Equals("no")) 
            {
                Rainfall = double.Parse(rain_node.Attributes["value"].Value);
                Console.WriteLine(Rainfall);
            }

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
    }

}
