using System;
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms.Platform.iOS;
using XView = Xamarin.Forms.View;

namespace SampleApp.Forms.iOS
{
    public class CustomOverlayCobrowseDelegate : CobrowseDelegateImplementation
    {
        public CustomOverlayCobrowseDelegate()
        {
        }

        public CustomOverlayCobrowseDelegate(System.IntPtr handle) : base(handle)
        {
        }

        public Func<XView> IndicatorFunction { get; set; }

        private UIView _indicatorInstance;

        public override void ShowSessionControls(Session session)
        {
            if (_indicatorInstance == null)
            {
                _indicatorInstance = GetDefaultSessionIndicator(container: UIApplication.SharedApplication.KeyWindow);
            }
            if (_indicatorInstance != null)
            {
                _indicatorInstance.Hidden = false;
            }
        }

        public override void HideSessionControls(Session session)
        {
            if (_indicatorInstance != null)
            {
                _indicatorInstance.Hidden = true;
            }
        }

        private UIView GetDefaultSessionIndicator(UIView container)
        {
            var indicator = IndicatorFunction();
            if (indicator == null)
            {
                return null;
            }
            var renderer = Platform.CreateRenderer(indicator);
            renderer.Element.Layout(new Xamarin.Forms.Rectangle(0, 0, indicator.WidthRequest, indicator.HeightRequest));
            var nativeIndicator = renderer.NativeView;
            nativeIndicator.TranslatesAutoresizingMaskIntoConstraints = false;

            container.AddSubview(nativeIndicator);

            nativeIndicator.WidthAnchor.ConstraintEqualTo((float)indicator.WidthRequest).Active = true;
            nativeIndicator.HeightAnchor.ConstraintEqualTo((float)indicator.HeightRequest).Active = true;
            nativeIndicator.CenterYAnchor.ConstraintEqualTo(container.CenterYAnchor).Active = true;
            nativeIndicator.RightAnchor.ConstraintEqualTo(container.RightAnchor, constant: -20f).Active = true;

            return nativeIndicator;
        }

        /*
         * If you're overriding HandleFullDeviceRequest you become responsible
         * to show RPSystemBroadcastPickerView and ask user to start full-device broadcasting.
        public override void HandleFullDeviceRequest(Session session)
        {
            session.SetFullDeviceState(FullDeviceState.On, callback: null);
        }
        */
    }
}
