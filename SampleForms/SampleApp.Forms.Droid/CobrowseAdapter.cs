using Android.App;
using Android.Content;
using Android.Widget;
using CobrowseIOSdk;
using CobrowseIOSdk.UI;
using Plugin.CurrentActivity;
using SampleApp.Forms.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(CobrowseAdapter))]
namespace SampleApp.Forms.Droid
{
    public class CobrowseAdapter : ICobrowseAdapter
    {
        private Activity Activity => CrossCurrentActivity.Current.Activity;

        public CobrowseAdapter()
        {
        }

        public void StartCobrowse()
        {
            var intent = new Intent(Activity, typeof(CobrowseActivity));
            Activity.StartActivity(intent);
        }

        public void CheckCobrowseFullDevice()
        {
            bool isRunning = CobrowseAccessibilityService.IsRunning(Activity);
            if (!isRunning)
            {
                CobrowseAccessibilityService.ShowSetup(Activity);
                return;
            }

            Toast.MakeText(
                Activity,
                "Full-device control is enabled and ready.",
                ToastLength.Short)
                .Show();
        }
    }
}