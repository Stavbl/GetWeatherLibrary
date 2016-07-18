using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactoryType.OPEN_WEATHER_MAP);
            CityLocation location = new CityLocation("Tel Aviv");
            WeatherData weatherData = service.GetWeatherData(location);
          
        }
    }
}
