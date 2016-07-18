## GetWeatherLibrary 

GetWeatherLibrary is an generic C# Library for developers to get weather data from around the world.

## Code Example

```c#
// get the weather data in London using the GetWeatherData function

OpenWeatherMapDataService.SetAppId("App ID that u got by signing in to openweathermap.org");
IWeatherDataService weatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactoryType.OPEN_WEATHER_MAP);

//(weatherService as OpenWeatherMapDataService).SetAppId("cb3f6e62c433d931e54b743eb177c694");
            
Location locationTest = new CityLocation("London", "UK");
WeatherData weatherDataToTest = weatherService.GetWeatherData(locationTest);
```
###### WeatherData:
Now WeatherData holds up weather information like: 

- int clouds;
- double lon, lat;
- string city, country;
- double temp, windSpeed;
- int humidity;
- DateTime dateTime;

## Motivation

This implimintaion of GetWeather Library was given to me as a school assignment in my 3rd year BSc in Software Engineering by Shenkar College.

## Installation

If using the OPEN_WEATHER_MAP Data Source U must sign in on [openweathermap.org](openweathermap.org) and Set the given App ID before 
getting the WeatherData From the weather data service.
```c#
  // get the weather data in London using the GetWeatherData function
  
  OpenWeatherMapDataService.SetAppId("Your App ID...");
  IWeatherDataService weatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactoryType.OPEN_WEATHER_MAP);
```

## API Reference
##### `WeatherData GetWeatherData(Location location);`
This function gets City based location `Location location = new CityLocation("city", "country code")` (country code is not obligatory) & Returns [WeatherData](#weatherdata) type object.

##### `void SetUnitsFormat(Units unit);`
This function sets the unit of measure for the API
inputs could be:
- metric `Units.Celsius`
- imperial `Units.Fahrenheit`
- Temperture in Kelvin Units by Default

``` c#
weatherService.SetUnitsFormat(Units.Option);
```
Only After getting the weather data service!!!

## Tests

There is a project called `WeatherUnitTest`
just run tests as Unit test Project.
it will run:

`GetWeatherDataValueTest` comparing live web requset from openweathermap.org to one gotten from the library

`SetTempUnits` comparing live web requset from openweathermap.org to one gotten from the library using metric unit
