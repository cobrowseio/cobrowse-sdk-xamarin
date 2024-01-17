using Android.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cobrowse.IO.Android;
using AView = Android.Views.View;

namespace MauiSample.Platforms.Android
{
    public class CobrowseRedactionDelegate
        : Cobrowse.IO.CobrowseDelegateImplementation,
        Cobrowse.IO.Android.CobrowseIO.IRedactionDelegate,
        Cobrowse.IO.Android.CobrowseIO.ISessionControlsDelegate
    {
        public IList<AView>? RedactedViews(Activity activity)
            => PlatformCobrowseRedactedViewEffect.RedactedViews;

        private AView _overlayIndicator;

        public void ShowSessionControls(Activity activity, Session session)
        {
            if (_overlayIndicator != null)
            {
                return;
            }
            if (!(activity is MauiAppCompatActivity))
            {
                return;
            }
            var indicator = new CobrowseCustomView();
            var renderer = Microsoft.Maui.Controls.Compatibility.Platform.Android.Platform.CreateRendererWithContext(indicator, activity);
            renderer.Element.Layout(new Rect(0, 0, indicator.WidthRequest, indicator.HeightRequest));
            var nativeIndicator = renderer.View;

            var modal = new RelativeLayout(activity);
            var layoutParams = new RelativeLayout.LayoutParams(
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)indicator.WidthRequest, activity.Resources.DisplayMetrics),
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)indicator.HeightRequest, activity.Resources.DisplayMetrics))
            {
                MarginEnd = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 4f, activity.Resources.DisplayMetrics)
            };
            layoutParams.AddRule(LayoutRules.CenterVertical);
            layoutParams.AddRule(LayoutRules.AlignParentEnd);
            modal.AddView(nativeIndicator, layoutParams);

            var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
            rootFrameLayout.AddView(modal, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            rootFrameLayout.Invalidate();

            _overlayIndicator = modal;
        }

        public void HideSessionControls(Activity activity, Session session)
        {
            if (_overlayIndicator == null)
            {
                return;
            }
            if (!(activity is MauiAppCompatActivity))
            {
                return;
            }
            var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
            rootFrameLayout.RemoveView(_overlayIndicator);
            _overlayIndicator = null;
        }
    }
}

