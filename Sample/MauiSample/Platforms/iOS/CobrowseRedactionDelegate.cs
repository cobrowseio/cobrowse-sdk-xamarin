using UIKit;
using Cobrowse.IO.iOS;

namespace MauiSample.Platforms.iOS
{
    public class CobrowseRedactionDelegate
        : Cobrowse.IO.CobrowseDelegateImplementation
    {
        public override UIView[] RedactedViewsForViewController(UIViewController vc)
            => PlatformCobrowseRedactedViewEffect.RedactedViews;

        private UIView _indicatorInstance;

        public override void ShowSessionControls(Session session)
        {
            if (_indicatorInstance == null)
            {
                _indicatorInstance = GetDefaultSessionIndicator(container: UIApplication.SharedApplication.KeyWindow);
            }
            _indicatorInstance.Hidden = false;
        }

        public override void HideSessionControls(Session session)
        {
            if (_indicatorInstance != null)
                _indicatorInstance.Hidden = true;
        }

        private UIView GetDefaultSessionIndicator(UIView container)
        {
            var indicator = new CobrowseCustomView();
            var renderer = Microsoft.Maui.Controls.Compatibility.Platform.iOS.Platform.CreateRenderer(indicator);
            renderer.Element.Layout(new Rect(0, 0, indicator.WidthRequest, indicator.HeightRequest));
            var nativeIndicator = renderer.NativeView;
            nativeIndicator.TranslatesAutoresizingMaskIntoConstraints = false;

            container.AddSubview(nativeIndicator);

            nativeIndicator.WidthAnchor.ConstraintEqualTo((float)indicator.WidthRequest).Active = true;
            nativeIndicator.HeightAnchor.ConstraintEqualTo((float)indicator.HeightRequest).Active = true;
            nativeIndicator.CenterYAnchor.ConstraintEqualTo(container.CenterYAnchor).Active = true;
            nativeIndicator.RightAnchor.ConstraintEqualTo(container.RightAnchor, constant: -20f).Active = true;

            return nativeIndicator;
        }
    }
}