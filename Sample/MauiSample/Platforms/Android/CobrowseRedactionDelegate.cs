using Android.App;
using AView = Android.Views.View;

namespace MauiSample.Platforms.Android
{
    public class CobrowseRedactionDelegate
        : Cobrowse.IO.CobrowseDelegateImplementation,
        Cobrowse.IO.Android.CobrowseIO.IRedactionDelegate
    {
        public IList<AView>? RedactedViews(Activity activity)
            => PlatformCobrowseRedactedViewEffect.RedactedViews;
    }
}

