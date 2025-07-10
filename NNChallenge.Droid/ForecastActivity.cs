
using Android.App;
using Android.OS;
using Android.Widget;
using NNChallenge.Services;
using System.Threading.Tasks;
using System.Linq;

namespace NNChallenge.Droid
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity
    {
        private WeatherService _weatherService;
        private ListView _listView;
        private ArrayAdapter<string> _adapter;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forecast);

            _listView = FindViewById<ListView>(Resource.Id.list_forecast);
            _weatherService = new WeatherService();

            string city = Intent.GetStringExtra("city") ?? "Berlin";

            await LoadForecast(city);
        }

        private async Task LoadForecast(string city)
        {
            var forecast = await _weatherService.GetForecastAsync(city);

            var items = forecast.HourForecast.Select(h =>
                $"{h.Date}: {h.TeperatureCelcius}°C / {h.TeperatureFahrenheit}°F"
            ).ToList();

            _adapter = new ArrayAdapter<string>(
                this,
                Android.Resource.Layout.SimpleListItem1,
                items
            );

            _listView.Adapter = _adapter;
        }
    }
}