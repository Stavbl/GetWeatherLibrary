﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public interface IWeatherDataService
    {
        WeatherData GetWeatherData(Location location);
        void SetUnitsFormat(Units unit);
    }
}
