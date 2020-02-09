using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using CobrowseIOSdk;
using CobrowseIOSdk.UI;

namespace SampleApp.Droid
{
    [Activity(
        Label = "@string/app_name",
        Theme = "@style/AppTheme.NoActionBar",
        MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainLayout);

            FindViewById<Button>(Resource.Id.button_launch_cobrowse).Click += OnCobreowseButtonClick;
            FindViewById<Button>(Resource.Id.button_check_cobrowse_full_device).Click += OnCheckCobrowseFullDeviceClick;
        }

        private void OnCobreowseButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CobrowseActivity));
            StartActivity(intent);
        }

        private void OnCheckCobrowseFullDeviceClick(object sender, EventArgs e)
        {
            bool isRunning = CobrowseAccessibilityService.IsRunning(this);
            if (!isRunning)
            {
                CobrowseAccessibilityService.ShowSetup(this);
                return;
            }

            Toast.MakeText(
                this,
                "Full-device control is enabled and ready.",
                ToastLength.Short)
                .Show();
        }
    }
}