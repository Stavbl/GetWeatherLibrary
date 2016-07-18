using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Demo
{
    class OpenWeatherMapDataService: IWeatherDataService
    {
        #region Fields
        enum Units {  }
        private static OpenWeatherMapDataService instance;
        string url = "http://api.openweathermap.org/data/2.5/weather?q=";
        string appId = "&APPID=" + "cb3f6e62c433d931e54b743eb177c694";
        string mode = "&mode=" + "json";
        string units = "&units=" + "metric";
        //string place = "London";
        #endregion

        #region Ctors
        private OpenWeatherMapDataService(){}
        public static OpenWeatherMapDataService Instace
        {
            get
            {
                if(instance == null)
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
            if (location is CityLocation)
            {
                url += (location as CityLocation).CityName;
                if((location as CityLocation).CountryCode !=null)
                    url+= ","+(location as CityLocation).CountryCode;
            }
            url += appId;
            url += mode;
            url += units;
            WeatherJson json = new WeatherJson();
            json.rootObject = MakeRequest(url);
            //WeatherJson weatherData = new WeatherJson(xDoc);
            //return weatherData;
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
            weatherData.dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0,System.DateTimeKind.Utc);
            weatherData.dateTime = weatherData.dateTime.AddSeconds(json.rootObject.dt).ToLocalTime();
            return weatherData;
        }
        protected WeatherJson.RootObject MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if(response.StatusCode != HttpStatusCode.OK)
                    throw new WeatherDataException("Problem getting response from web.");
                DataContractJsonSerializer  serializer = new DataContractJsonSerializer(typeof(WeatherJson.RootObject));
                
                return (WeatherJson.RootObject)serializer.ReadObject(response.GetResponseStream());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }
        }
        protected WeatherJson ProcessRequest(XmlDocument doc)
        {
            return null;
        }
        #endregion
    }
}
//try
//{
//    var xDoc = XDocument.Load("http://api.openweathermap.org/data/2.5/weather?q=" + place + "&APPID=" + appId + "&mode=" + mode);
//    var wind = xDoc.Root.Element("wind");
//    var speed = (float)wind.Element("speed").Attribute("value");
//}
//catch (Exception e)
//{
//    Console.WriteLine(e.Message);
//    Console.Read();
//}
