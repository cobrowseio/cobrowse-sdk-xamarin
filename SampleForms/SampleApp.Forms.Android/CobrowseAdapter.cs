using Android.App;
using Android.Content;
using Android.Widget;
using Plugin.CurrentActivity;
using SampleApp.Forms.Android;
using Xamarin.CobrowseIO;
using Xamarin.CobrowseIO.UI;
using Xamarin.Forms;

[assembly: Dependency(typeof(CobrowseAdapter))]
namespace SampleApp.Forms.Android
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