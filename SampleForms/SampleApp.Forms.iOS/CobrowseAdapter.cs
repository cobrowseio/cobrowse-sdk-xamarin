using System;
using System.Collections.Generic;
using System.Diagnostics;
using SampleApp.Forms.iOS;
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XView = Xamarin.Forms.View;

[assembly: Dependency(typeof(CobrowseAdapter))]
namespace SampleApp.Forms.iOS
{
    public class CobrowseAdapter : ICobrowseAdapter
    {
        private CustomOverlayCobrowseDelegate _overlayDelegate;

        public CobrowseAdapter()
        {
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance().DeviceId;

        /// <summary>
        /// Initializes the Cobrowse.io SDK.
        /// </summary>
        public void Initialize(string licenseKey)
        {
            CobrowseIO.Instance().SetLicense(licenseKey);
            CobrowseIO.Instance().SetDelegate(
                _overlayDelegate = new CustomOverlayCobrowseDelegate());
            CobrowseIO.Instance().Start();
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        public void SetCustomData(IDictionary<string, object> customData)
        {
            CobrowseIO.Instance().SetCustomData(customData);
        }

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        public void StartCobrowse()
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var nc = vc.GetUINavigationController();
            nc.PushViewController(
                new CBIOViewController(),
                animated: true);
        }

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        public void CheckCobrowseFullDevice()
        {
            return;
        }

        /// <summary>
        /// Ends the current session (if active).
        /// </summary>
        public void EndCurrentSession()
        {
            CobrowseIO.Instance().CurrentSession?.End(null);
        }

        /// <summary>
        /// Sets the custom overlay view.
        /// </summary>
        public void SetCustomOverlayView(Func<View> viewFunc)
        {
            _overlayDelegate.IndicatorFunction = viewFunc;
        }
    }

    public class CustomOverlayCobrowseDelegate : CobrowseIODelegate
    {
        public CustomOverlayCobrowseDelegate()
        {
        }

        public CustomOverlayCobrowseDelegate(System.IntPtr handle) : base(handle)
        {
        }

        public Func<XView> IndicatorFunction { get; set; }

        private UIView _indicatorInstance;

        public override void CobrowseShowSessionControls(CBIOSession session)
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

        public override void CobrowseHideSessionControls(CBIOSession session)
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

        public override void CobrowseSessionDidUpdate(CBIOSession session)
        {
            Debug.WriteLine("CobrowseSessionDidUpdate");
        }

        public override void CobrowseSessionDidEnd(CBIOSession session)
        {
            Debug.WriteLine("CobrowseSessionDidEnd");
        }
    }
}
