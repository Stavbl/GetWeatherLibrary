using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class WeatherData
    {
        public int clouds { get; set; }
        public double lon, lat;
        public string city, country;
        public double temp, windSpeed;
        public int humidity;
        public DateTime dateTime;
    }
}
