using System;
using System.Diagnostics;
using CobrowseIOSdk;
using Foundation;
using UIKit;

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
