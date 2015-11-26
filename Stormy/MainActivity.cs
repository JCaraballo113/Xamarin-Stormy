using System;
using Newtonsoft.Json.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Net;
using Android.Graphics.Drawables;

using Square.OkHttp;
using Com.Lilarcor.Cheeseknife;

namespace Stormy
{
	[Activity (Label = "Stormy", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private CurrentWeather mCurrentWeather;
		
		[InjectView (Resource.Id.humidityValue)] TextView humidityValue;
		[InjectView (Resource.Id.precipValue)] TextView precipValue;
		[InjectView (Resource.Id.temperatureLabel)] TextView temperatureLabel;
		[InjectView (Resource.Id.progressBar)] ProgressBar progressBar;
		[InjectView (Resource.Id.refreshIcon)] ImageView refreshIcon;
		[InjectView (Resource.Id.weatherIcon)] ImageView weatherIcon;
		[InjectView (Resource.Id.timeLabel)] TextView timeLabel;
		[InjectView (Resource.Id.summaryLabel)] TextView summaryLabel;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			Cheeseknife.Inject (this);
			progressBar.Visibility = ViewStates.Invisible;
		
			double latitude = 37.8267;
			double longitude = -122.423;

			refreshIcon.Click += delegate {
				getWeather(latitude,longitude);
			};

			getWeather (latitude, longitude);


		}

		private void getWeather (double latitude, double longitude)
		{
			string apiKey = "32b0b275eea19d641b0069e91c91b50a";
			string apiUrl = "https://api.forecast.io/forecast/" + apiKey + "/" + latitude + "," + longitude;
			if (isNetworkAvailable ()) {
				RunOnUiThread(() => toggleRefresh());
				OkHttpClient client = new OkHttpClient ();
				Request request = new Request.Builder ().Url (apiUrl).Build ();
				Call call = client.NewCall (request);
				call.Enqueue (response =>  {
					RunOnUiThread(() => toggleRefresh());
					var jsonData = JObject.Parse (response.Body ().String ());
					mCurrentWeather = getCurrentDetails (jsonData);
					RunOnUiThread(() => updateDisplay());
				}, (req, exception) =>  {
					ShowError ();
				});
			}
			else {
				Toast.MakeText (this, "Network unavailable", ToastLength.Long).Show ();
			}
		}

		private void updateDisplay() {
			humidityValue.Text = mCurrentWeather.Humidity.ToString();
			precipValue.Text = mCurrentWeather.PrecipChance.ToString();
			temperatureLabel.Text = mCurrentWeather.Temperature.ToString();
			Drawable drawable = Resources.GetDrawable (mCurrentWeather.getIconId());
			weatherIcon.SetImageDrawable (drawable);
			timeLabel.Text = "At " + mCurrentWeather.getFormattedTime() + " it will be...";
			summaryLabel.Text = mCurrentWeather.Summary;
		}

		private void toggleRefresh() {
			if(refreshIcon.Visibility == ViewStates.Visible) {
				summaryLabel.Text = "Getting Current Weather...";
				refreshIcon.Visibility = ViewStates.Invisible;
				progressBar.Visibility = ViewStates.Visible;
			}
			else {
				refreshIcon.Visibility = ViewStates.Visible;
				progressBar.Visibility = ViewStates.Invisible;
			}
		}

		private void ShowError() {
			AlertDialogFragment dialog = new AlertDialogFragment ();
			dialog.Show (this.FragmentManager, "error_dialog");
		}

		private bool isNetworkAvailable() {
			bool isAvailable = false;

			ConnectivityManager manager = (ConnectivityManager) GetSystemService (Context.ConnectivityService);
			NetworkInfo netWorkInfo = manager.ActiveNetworkInfo;

			if(netWorkInfo != null && netWorkInfo.IsConnected){
				isAvailable = true;
			}

			return isAvailable;
		}

		private CurrentWeather getCurrentDetails(JObject jsonData) {
			string timezone = (string)jsonData ["timezone"];
			JObject currentDetails = JObject.FromObject (jsonData ["currently"]);
			CurrentWeather currentWeather= new CurrentWeather(this);
			currentWeather.Humidity = (double)currentDetails ["humidity"];
			currentWeather.Time = (long)currentDetails ["time"];
			currentWeather.Icon = (string)currentDetails ["icon"];
			currentWeather.PrecipChance = (double)currentDetails ["precipProbability"];
			currentWeather.Summary = (string)currentDetails ["summary"];
			currentWeather.Temperature = (double)currentDetails ["temperature"];
			currentWeather.TimeZone = timezone;

			return currentWeather;
		}
	}
}


