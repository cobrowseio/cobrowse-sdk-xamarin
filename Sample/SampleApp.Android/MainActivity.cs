using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Xamarin.CobrowseIO;
using Xamarin.CobrowseIO.UI;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SampleApp.Android
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

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FindViewById<Button>(Resource.Id.button_launch_cobrowse).Click += OnCobrowseButtonClick;
            FindViewById<Button>(Resource.Id.button_launch_cobrowse_custom_ui).Click += OnCobrowseCustomUIButtonClick;
            FindViewById<Button>(Resource.Id.button_check_cobrowse_full_device).Click += OnCheckCobrowseFullDeviceClick;
            FindViewById<Button>(Resource.Id.button_open_login_view).Click += OnShowLoginViewClick;
            FindViewById<Button>(Resource.Id.button_show_device_id).Click += OnShowDeviceIdClick;
        }

        private void OnCobrowseButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CobrowseActivity));
            StartActivity(intent);
        }

        private void OnCobrowseCustomUIButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CustomCobrowseActivity));
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

        private void OnShowLoginViewClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void OnShowDeviceIdClick(object sender, EventArgs e)
        {
            new AlertDialog.Builder(this)
                .SetTitle("Cobrowse.io ")
                .SetMessage($"Cobrowse.io DeviceId: {CobrowseIO.Instance().GetDeviceId(this.Application)}")
                .SetPositiveButton(global::Android.Resource.String.Yes, (sender, args) => { })
                .Show();
        }
    }
}