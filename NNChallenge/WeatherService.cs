using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using NNChallenge.Interfaces;
using NNChallenge.Models;

namespace NNChallenge.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "898147f83a734b7dbaa95705211612";
        private const string ApiUrl = "https://api.weatherapi.com/v1/forecast.json?key={0}&q={1}&days=3&aqi=no&alerts=no";

        public async Task<IWeatherForcastVO> GetForecastAsync(string city)
        {
            var url = string.Format(ApiUrl, ApiKey, city);
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var forecastList = doc.RootElement
                .GetProperty("forecast")
                .GetProperty("forecastday")
                .EnumerateArray()
                .SelectMany(day => day.GetProperty("hour").EnumerateArray())
                .Select(hour => new HourWeatherForecastVO
                {
                    Date = DateTime.Parse(hour.GetProperty("time").GetString()),
                    TeperatureCelcius = hour.GetProperty("temp_c").GetSingle(),
                    TeperatureFahrenheit = hour.GetProperty("temp_f").GetSingle(),
                    ForecastPitureURL = "https:" + hour.GetProperty("condition").GetProperty("icon").GetString()
                })
                .ToArray();

            return new WeatherForecastVO
            {
                City = city,
                HourForecast = forecastList
            };
        }
    }
}
