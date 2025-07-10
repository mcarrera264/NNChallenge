using System;
using NNChallenge.Interfaces;

namespace NNChallenge.Models
{
    public class WeatherForecastVO : IWeatherForcastVO
    {
        public string City { get; set; }
        public IHourWeatherForecastVO[] HourForecast { get; set; }
    }

    public class HourWeatherForecastVO : IHourWeatherForecastVO
    {
        public DateTime Date { get; set; }
        public float TeperatureCelcius { get; set; }
        public float TeperatureFahrenheit { get; set; }
        public string ForecastPitureURL { get; set; }
    }
}
