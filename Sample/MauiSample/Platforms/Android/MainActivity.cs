using Android.App;
using Android.Content.PM;
using Android.OS;
using Cobrowse.IO.Android;
using MauiSample.Platforms.Android;
using AView = Android.Views.View;

namespace MauiSample;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity,
    CobrowseIO.IRedacted
{
    public IList<AView> RedactedViews()
    {
        return PlatformCobrowseRedactedViewEffect.RedactedViews;
    }
}

