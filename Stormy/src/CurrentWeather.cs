using System;

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

		public CurrentWeather ()
		{
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
				return this.mTemperature;
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
				return this.mPrecipChance;
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
	}
}

