using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public enum Units { Fahrenheit, Celsius, Kelvin };
    public class OpenWeatherMapDataService : IWeatherDataService
    {
        #region Fields
        
        private static OpenWeatherMapDataService instance;
        private static string appId = null; //"&APPID=" + "cb3f6e62c433d931e54b743eb177c694";
        private string url = "http://api.openweathermap.org/data/2.5/weather?q=";
        private string mode = "&mode=" + "json";
        private string units = "";   // = "&units=" + "metric";
        #endregion

        #region Ctors
        protected OpenWeatherMapDataService()
        {
            if (appId == null)
            {
                throw new WeatherDataException("App ID value is null. App ID set is needed.");
            }
        }
        public static OpenWeatherMapDataService Instace
        {
            get
            {
                if (instance == null)
                {
                    instance = new OpenWeatherMapDataService();
                }
                return instance;
            }
        }
        #endregion

        #region Methods
        public WeatherData GetWeatherData(Location location)
        {
            if(location == null)
            {
                throw new WeatherDataException("Location value is null");
            }
            if (location is CityLocation)
            {
                Console.WriteLine("The service got a request to return the current weather in this location: \n" +
                            "City : " + (location as CityLocation).CityName);
                url += (location as CityLocation).CityName;
                if ((location as CityLocation).CountryCode != null)
                {
                    url += "," + (location as CityLocation).CountryCode;
                    Console.WriteLine("Country :" + (location as CityLocation).CountryCode);
                }
            }
            url += appId;
            url += mode;
            url += units;
            WeatherJson json = new WeatherJson();
            json.rootObject = MakeRequest(url);
            return JsonToObject(json);
        }
        private WeatherData JsonToObject(WeatherJson json)
        {
            WeatherData weatherData = new WeatherData();
            weatherData.clouds = json.rootObject.clouds.all;
            weatherData.lon = json.rootObject.coord.lon;
            weatherData.lat = json.rootObject.coord.lat;
            weatherData.temp = json.rootObject.main.temp;
            weatherData.humidity = json.rootObject.main.humidity;
            weatherData.windSpeed = json.rootObject.wind.speed;
            weatherData.city = json.rootObject.name;
            weatherData.country = json.rootObject.sys.country;
            weatherData.dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            weatherData.dateTime = weatherData.dateTime.AddSeconds(json.rootObject.dt).ToLocalTime();
            return weatherData;
        }
        protected WeatherJson.RootObject MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new WeatherDataException("Problem getting response from web. Check App ID or location value.");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WeatherJson.RootObject));

                return (WeatherJson.RootObject)serializer.ReadObject(response.GetResponseStream());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }
        }
        public static void SetAppId(string userAppId)
        {
            appId = "&APPID=" + userAppId;
        }

        public void SetUnitsFormat(Units unit)
        {
            switch (unit)
            {
                case Units.Fahrenheit:
                    this.units = "&units=" + "imperial";
                    break;
                case Units.Celsius:
                    this.units = "&units=" + "metric";
                    break;
                default:
                    break;
            }
            return;
        }
        #endregion
    }
}
