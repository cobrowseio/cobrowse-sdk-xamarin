using System;
using Android.App;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.CobrowseIO;
using Xamarin.Forms.Platform.Android;
using XView = Xamarin.Forms.View;

namespace SampleApp.Forms.Android
{
    public class CustomOverlayCobrowseDelegate
        : CobrowseDelegateImplementation,
        CobrowseIO.ISessionControlsDelegate
    {
        public CustomOverlayCobrowseDelegate()
        {
        }

        public CustomOverlayCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public Func<XView> IndicatorFunction { get; set; }

        private View _overlayIndicator;

        public void ShowSessionControls(Activity activity, Session session)
        {
            if (_overlayIndicator != null)
            {
                return;
            }
            if (!(activity is FormsAppCompatActivity))
            {
                return;
            }
            var indicator = IndicatorFunction();
            if (indicator == null)
            {
                return;
            }
            var renderer = Platform.CreateRendererWithContext(indicator, activity);
            renderer.Element.Layout(new Xamarin.Forms.Rectangle(0, 0, indicator.WidthRequest, indicator.HeightRequest));
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
            if (!(activity is FormsAppCompatActivity))
            {
                return;
            }
            var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
            rootFrameLayout.RemoveView(_overlayIndicator);
            _overlayIndicator = null;
        }

        /*
         * If you're overriding HandleFullDeviceRequest you become responsible
         * to update the session full-device state.
        public void HandleFullDeviceRequest(Activity activity, Session session)
        {
            session.SetFullDeviceState(FullDeviceState.On, callback: null);
        }
         */
    }
}