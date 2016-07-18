using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherLibrary;
using System.Net;
using System.Runtime.Serialization.Json;

namespace WeatherUnitTests
{
    [TestClass]
    public class WeatherTests
    {
        /// <summary>
        /// Testing GetWeatherData(Location) method in London,UK
        /// Expected same data so the assert will be true
        /// </summary>
        [TestMethod()]
        public void GetWeatherDataValueTest()
        {
            // get the weather data in London using the GetWeatherData function
            OpenWeatherMapDataService.SetAppId("cb3f6e62c433d931e54b743eb177c694");
            IWeatherDataService weatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactoryType.OPEN_WEATHER_MAP);
            //(weatherService as OpenWeatherMapDataService).SetAppId("cb3f6e62c433d931e54b743eb177c694");
            Location locationTest = new CityLocation("London", "UK");
            WeatherData weatherDataToTest = weatherService.GetWeatherData(locationTest);
            // get the weather data in London from the website
            string city = (locationTest as CityLocation).CityName;
            string country = (locationTest as CityLocation).CountryCode;
            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "," + country + "&APPID=" + "cb3f6e62c433d931e54b743eb177c694" + "&mode=json";
            WeatherJson.RootObject weatherSrc = GetWeatherByUrlForTesting(url);
            WeatherJson weatherJsonTest = new WeatherJson();
            weatherJsonTest.rootObject = weatherSrc;

            Assert.AreEqual(weatherDataToTest.clouds, weatherJsonTest.rootObject.clouds.all);
            Assert.AreEqual(weatherDataToTest.lon, weatherJsonTest.rootObject.coord.lon);
            Assert.AreEqual(weatherDataToTest.lat, weatherJsonTest.rootObject.coord.lat);
            Assert.AreEqual(weatherDataToTest.city, weatherJsonTest.rootObject.name);
            //Assert.AreEqual(weatherDataToTest.country, weatherJsonTest.rootObject.cod);
            Assert.AreEqual(weatherDataToTest.temp, weatherJsonTest.rootObject.main.temp);
        }

        [TestMethod()]
        public void SetTempUnits()
        {
            // get the weather data in London using the GetWeatherData function
            OpenWeatherMapDataService.SetAppId("cb3f6e62c433d931e54b743eb177c694");
            IWeatherDataService weatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactoryType.OPEN_WEATHER_MAP);
            weatherService.SetUnitsFormat(Units.Celsius);
            //(weatherService as OpenWeatherMapDataService).SetAppId("cb3f6e62c433d931e54b743eb177c694");
            Location locationTest = new CityLocation("London", "UK");
            WeatherData weatherDataToTest = weatherService.GetWeatherData(locationTest);
            // get the weather data in London from the website
            string city = (locationTest as CityLocation).CityName;
            string country = (locationTest as CityLocation).CountryCode;
            string units = "&units=" + "metric";
            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "," + country + "&APPID=" + "cb3f6e62c433d931e54b743eb177c694" + "&mode=json" + units;
            WeatherJson.RootObject weatherSrc = GetWeatherByUrlForTesting(url);
            WeatherJson weatherJsonTest = new WeatherJson();
            weatherJsonTest.rootObject = weatherSrc;

            Assert.AreEqual(weatherDataToTest.temp, weatherJsonTest.rootObject.main.temp);
        }

        private WeatherJson.RootObject GetWeatherByUrlForTesting(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new WeatherDataException("Problem getting response from web. Check App ID or location value.");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WeatherJson.RootObject));
                WeatherJson.RootObject testWeatherJson = new WeatherJson.RootObject();
                testWeatherJson = (WeatherJson.RootObject)serializer.ReadObject(response.GetResponseStream());

                return testWeatherJson;
            
            }
            catch(WeatherDataException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }
    }
}
