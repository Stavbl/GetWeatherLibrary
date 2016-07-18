using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class WeatherDataException: Exception
    {
        public WeatherDataException()
        {
        }

        public WeatherDataException(string message): base(message)
        {
        }
    }
}
