using System;

using Android.App;
using Android.Content;
using Android.OS;

namespace Stormy
{
	public class AlertDialogFragment : DialogFragment
	{
		public AlertDialogFragment ()
		{
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState) {
			Context context = (Context)Activity;

			AlertDialog.Builder builder = new AlertDialog.Builder (context)
				.SetTitle ("Oops sorry!")
				.SetMessage ("Something has gone wrong!")
				.SetPositiveButton("OK",delegate {
					
				});

			AlertDialog dialog = builder.Create ();

			return dialog;
		}
	}
}

