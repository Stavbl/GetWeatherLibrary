using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public interface Location
    {
    }

    public class CityLocation : Location
    {
        public string CityName { get; set; }
        public string CountryCode { get; set; }

        public CityLocation(string name, string code = null)
        {
            CityName = name;
            CountryCode = code;
        }
    }
}
