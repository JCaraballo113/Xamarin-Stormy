using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Net;

namespace Stormy
{
	public class CurrentWeather
	{
		private string mIcon;
		private long mTime;
		private double mTemperature;
		private double mHumidity;
		private double mPrecipChance;
		private string mSummary;
		private string mTimeZone;
		private Activity activity;

		public CurrentWeather (Activity mainActivity)
		{
			activity = mainActivity;
		}

		public string Icon {
			get {
				return this.mIcon;
			}
			set {
				mIcon = value;
			}
		}

		public long Time {
			get {
				return this.mTime;
			}
			set {
				mTime = value;
			}
		}

		public double Temperature {
			get {
				return (int)Math.Round (mTemperature);
			}
			set {
				mTemperature = value;
			}
		}

		public double Humidity {
			get {
				return this.mHumidity;
			}
			set {
				mHumidity = value;
			}
		}

		public double PrecipChance {
			get {
				double precipPercentage = this.mPrecipChance * 100;
				return (int)Math.Round (precipPercentage);
			}
			set {
				mPrecipChance = value;
			}
		}

		public string Summary {
			get {
				return this.mSummary;
			}
			set {
				mSummary = value;
			}
		}

		public string TimeZone {
			get {
				return this.mTimeZone;
			}
			set {
				mTimeZone = value;
			}
		}


		public string getFormattedTime(){
			//Set UNIX start date
			DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			// Add the UNIX seconds
			start = start.AddSeconds (this.Time);
			//Convert timezone, e.g America/Los_Angeles to C# timeZone
			TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById (this.TimeZone);
			//Set the final date converted to the specific timezone
			DateTime finalDate = TimeZoneInfo.ConvertTimeFromUtc (start, timeInfo);
			//Format it to your needs( tt stands for AM/PM) and return it
			return finalDate.ToString ("h:mm tt");
		}

		public int getIconId () {
			int iconId = activity.Resources.GetIdentifier("clear_day","drawable", activity.ApplicationContext.PackageName);

			if (mIcon.Equals("clear-day")) {
				iconId = activity.Resources.GetIdentifier("clear_day","drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("clear-night")) {
				iconId = iconId = activity.Resources.GetIdentifier("clear_night", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("rain")) {
				iconId = activity.Resources.GetIdentifier("rain", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("snow")) {
				iconId = activity.Resources.GetIdentifier("snow", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("sleet")) {
						iconId = activity.Resources.GetIdentifier("sleet", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("wind")) {
						iconId = activity.Resources.GetIdentifier("wind", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("fog")) {
						iconId = activity.Resources.GetIdentifier("fog", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("cloudy")) {
						iconId = activity.Resources.GetIdentifier("cloudy", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("partly-cloudy-day")) {
						iconId = activity.Resources.GetIdentifier("partly_cloudy", "drawable", activity.ApplicationContext.PackageName);
			}
			else if (mIcon.Equals("partly-cloudy-night")) {
						iconId = activity.Resources.GetIdentifier("cloudy_night", "drawable", activity.ApplicationContext.PackageName);
			}

			return iconId;
		}
	}
}

