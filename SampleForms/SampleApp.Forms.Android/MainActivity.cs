using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.CobrowseIO;

namespace SampleApp.Forms.Android
{
    [Activity(
        Label = "Cobrowse.io Xamarin.Forms",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity
        : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        CobrowseIO.IRedacted
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            CobrowseIO.Instance().SetDelegate(new CustomOverlayCobrowseDelegate()
            {
                IndicatorFunction = () => new CobrowseCustomView()
            });
        }

        public IList<View> RedactedViews()
        {
            return PlatformCobrowseRedactedViewEffect.RedactedViews;
        }
    }
}