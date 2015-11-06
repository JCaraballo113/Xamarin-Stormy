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

using Square.OkHttp;

namespace Stormy
{
	[Activity (Label = "Stormy", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			string apiKey = "32b0b275eea19d641b0069e91c91b50a";
			double latitude = 37.8267;
			double longitude = -122.423;
			string apiUrl = "https://api.forecast.io/forecast/" + apiKey + "/" + latitude + "," + longitude;

			if (isNetworkAvailable ()) {
				OkHttpClient client = new OkHttpClient ();
				Request request = new Request.Builder ().Url (apiUrl).Build ();

				Call call = client.NewCall (request);

				call.Enqueue (
					response => {
						var jsonData = JObject.Parse(response.Body().String());
						getCurrentDetails(jsonData);
					}, (req, exception) => {
						ShowError();
				}
				);
			}
			else {
				Toast.MakeText (this, "Network unavailable", ToastLength.Long).Show ();
			}

		}

		private void ShowError(){
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

		private void getCurrentDetails(JObject jsonData) {
			string timezone = (string)jsonData ["timezone"];
			JObject currentDetails = JObject.FromObject (jsonData ["currently"]);
			CurrentWeather currentWeather= new CurrentWeather();
			currentWeather.Humidity = (double)currentDetails ["humidity"];
			currentWeather.Time = (long)currentDetails ["time"];
			currentWeather.Icon = (string)currentDetails ["icon"];
			currentWeather.PrecipChance = (double)currentDetails ["precipProbability"];
			currentWeather.Summary = (string)currentDetails ["summary"];
			currentWeather.Temperature = (double)currentDetails ["temperature"];
			currentWeather.TimeZone = timezone;
		}
	}
}


