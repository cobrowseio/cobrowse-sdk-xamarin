using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using CobrowseIOSdk;
using CobrowseIOSdk.UI;
using Debug = System.Diagnostics.Debug;

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

            CobrowseIO.Instance().License("trial");
            CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance().Start(this);

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
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                Toast.MakeText(
                    this,
                    "Full-device control is supported only in API 21 (5.0 Lollipop) and above.",
                    ToastLength.Short)
                    .Show();
                return;
            }
            bool isConfigured = Resources.GetBoolean(Resource.Boolean.cobrowse_enable_full_device_control);
            if (!isConfigured)
            {
                Toast.MakeText(
                    this,
                    "'cobrowse_enable_full_device_control' boolean resource value must be TRUE.",
                    ToastLength.Short)
                    .Show();
                return;
            }
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

        public class CustomCobrowseDelegate : Java.Lang.Object, CobrowseIO.ISessionRequestDelegate
        {
            public CustomCobrowseDelegate()
            {
            }

            public CustomCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
                : base(handle, transfer)
            {
            }

            public void HandleSessionRequest(Activity activity, Session session)
            {
                Debug.WriteLine("HandleSessionRequest");
                session.Activate(null);
            }

            public void SessionDidEnd(Session session)
            {
                Debug.WriteLine("SessionDidEnd");
            }

            public void SessionDidUpdate(Session session)
            {
                Debug.WriteLine("SessionDidUpdate");
            }
        }
    }
}