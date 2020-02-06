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

            FindViewById<Button>(Resource.Id.button_launch_cobrowse).Click += OnCobreowseButtonClick;

            CobrowseIO.Instance().License("trial");
            CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance().Start(this);
        }

        private void OnCobreowseButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CobrowseActivity));
            StartActivity(intent);
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