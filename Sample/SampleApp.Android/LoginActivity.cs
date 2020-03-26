using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Xamarin.CobrowseIO;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SampleApp.Android
{
    [Activity(
        Label = "Login Activity",
        Theme = "@style/AppTheme.NoActionBar")]
    public class LoginActivity : AppCompatActivity, CobrowseIO.IRedacted
    {
        private IList<View> _redactedViews;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            _redactedViews = new List<View>
            {
                FindViewById<EditText>(Resource.Id.edittext_login),
                FindViewById<EditText>(Resource.Id.edittext_password)
            };
        }

        public IList<View> RedactedViews() => _redactedViews;

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return true;
        }
    }
}
