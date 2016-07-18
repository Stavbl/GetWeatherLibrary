using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public enum WeatherDataServiceFactoryType { OPEN_WEATHER_MAP, OTHER };
    public class WeatherDataServiceFactory
    {

        public static IWeatherDataService GetWeatherDataService(WeatherDataServiceFactoryType service)
        {
            switch (service)
            {
                case WeatherDataServiceFactoryType.OPEN_WEATHER_MAP:
                    return OpenWeatherMapDataService.Instace;

                default:
                    throw new WeatherDataException("Unexpected factory type");

            }
        }

    }
}
