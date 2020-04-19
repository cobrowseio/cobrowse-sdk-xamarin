using System;
using System.Collections.Generic;
using System.Diagnostics;
using Foundation;
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms.Platform.iOS;

namespace SampleApp.Forms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            CobrowseIO.Instance().SetLicense("trial");
            Console.WriteLine($"Cobrowse device id: {CobrowseIO.Instance().DeviceId}");

            CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance().SetCustomData(new Dictionary<string, object>
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
                { "custom_field", 5.75f }
            });

            CobrowseIO.Instance().Start();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public class CustomCobrowseDelegate : CobrowseIODelegate
        {
            public CustomCobrowseDelegate()
            {
            }

            public CustomCobrowseDelegate(System.IntPtr handle) : base(handle)
            {
            }

            private UIView _indicatorInstance;

            public override void CobrowseShowSessionControls(CBIOSession session)
            {
                if (_indicatorInstance == null)
                {
                    _indicatorInstance = GetDefaultSessionIndicator(container: UIApplication.SharedApplication.KeyWindow);
                }
                _indicatorInstance.Hidden = false;
            }

            public override void CobrowseHideSessionControls(CBIOSession session)
            {
                if (_indicatorInstance != null)
                    _indicatorInstance.Hidden = true;
            }

            private UIView GetDefaultSessionIndicator(UIView container)
            {
                var indicator = new CobrowseCustomView();
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
}
