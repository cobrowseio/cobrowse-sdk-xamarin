# Cobrowse.io - Xamarin SDK

Cobrowse.io for Xamarin supports iOS 9.0+ and Android API 19+ (4.4 KitKat and above).

Cobrowse.io is 100% free and easy to try out in your own apps. Please see full documentation at [https://cobrowse.io/docs](https://cobrowse.io/docs).

Try our **online demo** at the bottom of our homepage at <https://cobrowse.io/#tryit>.

## Installation

We recommend installing the Cobrowse.io SDK using NuGet. Add the following packages to your Xamarin projects:

- iOS: [![CobrowseIO.iOS NuGet](https://img.shields.io/nuget/v/CobrowseIO.iOS.svg?label=CobrowseIO.iOS)](https://www.nuget.org/packages/CobrowseIO.iOS/)
- Android: [![CobrowseIO.Android NuGet](https://img.shields.io/nuget/v/CobrowseIO.Android.svg?label=CobrowseIO.Android)](https://www.nuget.org/packages/CobrowseIO.Android/)

#### Xamarin.iOS

To use Cobrowse.io in your Xamarin.iOS project, please add the following lines to your `AppDelegate.cs`:

```cs
using Xamarin.CobrowseIO;

[Register("AppDelegate")]
public class AppDelegate : UIResponder, IUIApplicationDelegate
{
    [Export("application:didFinishLaunchingWithOptions:")]
    public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        CobrowseIO.Instance().License = "<your license key here>";
        CobrowseIO.Instance().Start();
        
        return true;
    }
}
```

*Important: Do this in your `UIApplicationDelegate.FinishedLaunching` implementation to make sure your device shows up in your dashboard right away.*

#### Xamarin.iOS Swift support

Cobrowse.io SDK uses Swift language, and if you are targeting iOS 12.1 or earlier, it is required to add [Xamarin.iOS.SwiftRuntimeSupport]() NuGet dependency to ship specific Swift dylibs used by Cobrowse with your application. 

* [![NuGet](https://img.shields.io/nuget/v/Xamarin.iOS.SwiftRuntimeSupport.svg?label=Xamarin.iOS.SwiftRuntimeSupport)](https://www.nuget.org/packages/Xamarin.iOS.SwiftRuntimeSupport/)

This NuGet copies the necessary Swift libraries in the right place when building your app or when generating the archive of the app (not the IPA).

This NuGet, however, does not copy the Swift libraries in the right place when generating the IPA using Visual Studio, causing the rejection of the App Store when uploading the IPA. Please follow [Xamarin documentation](https://github.com/xamarin/XamarinComponents/blob/master/iOS/SwiftRuntimeSupport/readme.txt) describing how to build and publish your Xamarin.iOS application using the Xcode IPA wizard. The steps are:

1. In Visual Studio, select a valid iOS device before archiving.
2. Go to *Build* menu → *Archive for Publishing*
3. Once done, open Xcode and go to *Window* → *Organizer*
4. Select the *Archives* tab
5. On the left side of the window, select your app
6. Click on *Distribute App* button and follow the wizard

#### Xamarin.Android

To use Cobrowse.io in your Xamarin.Android project, please add the following lines to your Application subclass.

```cs
using Xamarin.CobrowseIO;

[Application]
public class MainApplication : Application
{
    public override void OnCreate()
    {
        base.OnCreate();

        CobrowseIO.Instance().License("<your license key here>");
        CobrowseIO.Instance().Start(this);
    }
}
```

**Important:** 

- Make sure you do this in your custom Application subclass `OnCreate()` to ensure devices register in your dashboard right away.
- Also make sure you are targeting Android 10 (API 29). In Visual Studio:
    - Open the project settings
    - Navigate to *Build* → *General*
    - In *"Compile using Android version: (Target Framework)*" drop-down list choose **Android 10.0 (Q)**

You may also start CobrowseIO in your `MainActivity` or other Activity if necessary. In that case, the SDK will continue to function even as new Activities are being created and destroyed.

### Add your License Key

Please register an account and generate your free License Key at <https://cobrowse.io/dashboard/settings>.

This will associate sessions from your mobile app with your Cobrowse account.

### Add device metadata

To help you identify, search, and filter devices in your Cobrowse dashboard, it's helpful to specify any meaningful metadata. We recommend specifying the end-user's email if available.

You may add any custom key/value pairs you'd like, and they will all be searchable and filterable in your online dashboard. We've added a few placeholders for convenience only - all fields are optional.

#### Xamarin.iOS

```cs
using Xamarin.CobrowseIO;

namespace YourAppNamespace
{
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            CobrowseIO.Instance().License = "<your license key here>";
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
            
            return true;
        }
    }
}
```

#### Xamarin.Android

```cs
using Xamarin.CobrowseIO;

namespace YourAppNamespace
{
    [Application]
    public class MainApplication : Application
    {
        public override void OnCreate()
        {
            base.OnCreate();

            CobrowseIO.Instance().License("<your license key here>");

            var customData = new Dictionary<string, Java.Lang.Object>()
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
            };
            CobrowseIO.Instance().CustomData(customData);

            CobrowseIO.Instance().Start(this);
        }
    }
}
```

## Try it out

Once you have your app running in the iOS Simulator or on a physical device, navigate to <https://cobrowse.io/dashboard> to see your device listed. You can click the "Connect" button to initiate a Cobrowse session!

## Optional features

[Initiate sessions with push](./docs/initiate-with-push.md)

[Use 6-digit codes](./docs/user-generated-codes.md)

[Redact sensitive data](./docs/redact-sensitive-data.md)

[Requiring acceptance from the user](./docs/require-user-consent.md)

[Customizing the interface](./docs/customizing-the-interface.md)

[Full device screenshare](./docs/full-device-screenshare.md)

[Alternate render method](./docs/alternate-render-method.md)

## Questions?
Any questions at all? Please email us directly at [hello@cobrowse.io](mailto:hello@cobrowse.io).

## Requirements

* iOS 9.0 or later
* Android API version 19 or later
