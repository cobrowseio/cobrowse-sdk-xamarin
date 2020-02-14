using System;
using System.Diagnostics;
using CobrowseIOSdk;
using Foundation;
using UIKit;

namespace SampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("window")]
        public UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            CobrowseIO.Instance().License = "trial";
            Console.WriteLine($"Cobrowse device id: {CobrowseIO.Instance().DeviceId}");

            CobrowseIO.Instance().Delegate = new CustomCobrowseDelegate();
            CobrowseIO.Instance().CustomData = new NSDictionary<NSString, NSObject>(
                keys: new NSString[]
                {
                    CBIO.UserIdKey,
                    CBIO.UserNameKey,
                    CBIO.UserEmailKey,
                    CBIO.DeviceIdKey,
                    CBIO.DeviceNameKey,
                },
                values: new NSObject[]
                {
                    new NSString("<your_user_id>"),
                    new NSString("<your_user_name>"),
                    new NSString("<your_user_email>"),
                    new NSString("<your_device_id>"),
                    new NSString("<your_device_name>"),
                }
            );

            CobrowseIO.Instance().Start();

            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            return true;
        }

        public class CustomCobrowseDelegate : CobrowseIODelegate
        {
            public CustomCobrowseDelegate()
            {
            }

            public CustomCobrowseDelegate(System.IntPtr handle) : base(handle)
            {
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

