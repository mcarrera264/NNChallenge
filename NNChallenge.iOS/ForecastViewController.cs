using System;
using System.Linq;
using UIKit;
using Foundation;
using NNChallenge.Services;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {
        private WeatherService _weatherService;
        private UITableView _tableView;
        private string[] _items;

        public string SelectedCity { get; set; } = "Berlin"; // Hardcodeada de forma segura si no recibimos valor externo.

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Weather Forecast";

            View.BackgroundColor = UIColor.SystemBackground;

            _weatherService = new WeatherService();

            _tableView = new UITableView(View.Bounds)
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth
            };
            View.AddSubview(_tableView);

            await LoadForecast();
        }

        private async System.Threading.Tasks.Task LoadForecast()
        {
            try
            {
                var forecast = await _weatherService.GetForecastAsync(SelectedCity);

                _items = forecast.HourForecast
                    .Select(h => $"{h.Date:dd/MM HH:mm}: {h.TeperatureCelcius}°C / {h.TeperatureFahrenheit}°F")
                    .ToArray();

                _tableView.Source = new ForecastTableViewSource(_items);
                _tableView.ReloadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching forecast: {ex.Message}");
                ShowErrorAlert();
            }
        }

        private void ShowErrorAlert()
        {
            var alert = UIAlertController.Create("Error", "Unable to load forecast data.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, animated: true, completionHandler: null);
        }

        private class ForecastTableViewSource : UITableViewSource
        {
            private readonly string[] _data;

            public ForecastTableViewSource(string[] data)
            {
                _data = data;
            }

            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return _data.Length;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell("Cell") ?? new UITableViewCell(UITableViewCellStyle.Default, "Cell");
                cell.TextLabel.Text = _data[indexPath.Row];
                return cell;
            }
        }
    }
}
