using System;
using System.Collections.Generic;
using System.Diagnostics;
using Foundation;
using UIKit;
using Xamarin.CobrowseIO;

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
            CobrowseIO.Instance.SetLicense("trial");
            Console.WriteLine("Cobrowse device id:" + CobrowseIO.Instance.DeviceId);
            
            CobrowseIO.Instance.SetCustomData(new Dictionary<string, object>
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
                { "custom_field", 5.75f }
            });

            CobrowseIO.Instance.SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance.Start();

            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            return true;
        }
    }

    public class CustomCobrowseDelegate : CobrowseIODelegate
    {
        // Sample end session UIView, constructor, and tap gesture recognizer implementation
        private UIView _indicatorInstance;

        public override void ShowSessionControls(Session session)
        {
            // You can render controls however you like here.
            // One option is to add our sample end session UI defined below.
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
            var indicator = new UILabel();
            indicator.BackgroundColor = new UIColor(red: 1.0f, green: 0.0f, blue: 0.0f, alpha: 0.7f);
            indicator.Text = "End Session";
            indicator.UserInteractionEnabled = true;
            indicator.TextAlignment = UITextAlignment.Center;
            indicator.Font.WithSize(UIFont.SmallSystemFontSize);
            indicator.TextColor = UIColor.White;
            indicator.Layer.CornerRadius = 10;
            indicator.ClipsToBounds = true;
            indicator.TranslatesAutoresizingMaskIntoConstraints = false;
            container.AddSubview(indicator);

            indicator.WidthAnchor.ConstraintEqualTo(200f).Active = true;
            indicator.HeightAnchor.ConstraintEqualTo(40f).Active = true;
            indicator.CenterXAnchor.ConstraintEqualTo(container.CenterXAnchor).Active = true;
            indicator.BottomAnchor.ConstraintEqualTo(container.BottomAnchor, constant: -20f).Active = true;

            var tapRecognizer = new UITapGestureRecognizer(() =>
            {
                CobrowseIO.Instance.CurrentSession?.End(null);
            });
            tapRecognizer.NumberOfTapsRequired = 1;
            indicator.AddGestureRecognizer(tapRecognizer);
            return indicator;
        }

        public override void SessionDidUpdate(Session session)
        {
            Debug.WriteLine("CobrowseSessionDidUpdate");
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var vvc = vc.GetVisibleViewController();
            if (vvc is UINavigationController nc && nc.TopViewController is CustomCobrowseViewController cobrowseVc)
            {
                cobrowseVc.SessionDidUpdate(session);
            }
        }

        public override void SessionDidEnd(Session session)
        {
            Debug.WriteLine("CobrowseSessionDidEnd");
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var vvc = vc.GetVisibleViewController();
            if (vvc is UINavigationController nc && nc.TopViewController is CustomCobrowseViewController cobrowseVc)
            {
                cobrowseVc.SessionDidEnd(session);
            }
        }
    }
}

