using System;
using UIKit;
using Cobrowse.IO.iOS;

namespace MauiSample.Platforms.iOS
{
    public class CobrowseRedactionDelegate
        : Cobrowse.IO.CobrowseDelegateImplementation
    {
        //public UIView[] RedactedViews
        public override UIView[] RedactedViewsForViewController(UIViewController vc)
            => PlatformCobrowseRedactedViewEffect.RedactedViews;
    }
}