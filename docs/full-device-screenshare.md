# Full device screenshare (optional)

Please see full documentation at [https://cobrowse.io/docs](https://cobrowse.io/docs).

Try our **online demo** at the bottom of our homepage at <https://cobrowse.io/#tryit>.


## Overview

By default, the Cobrowse.io SDKs for iOS and Android will capture the user activity inside your app only. You can enable the capture of the full device, i.e. screens outside your app, including home screen, device settings, and everything else. Follow this guide to add the required App Extension for iOS and enable an accessibility service on Android required for capturing full device frames.   

## Xamarin.iOS implementation

**Add a Broadcast Extension project**

In Visual Studio for Mac:

1. Open your Xamarin solution
2. Right click on the solution, select Add > New Project...
3. Navigate iOS > Extension
3. Pick "Broadcast Upload Extension"
4. Enter a name for the target, e.g. `YourApp.iOS.BroadcastUploadExtension`
5. Select your iOS app to add the extension to
6. Create the location for the extension and press "Create"
7. Visual Studio for Mac will create two extension projects for you: `YourApp.iOS.BroadcastUploadExtension` and `YourApp.iOS.BroadcastUploadExtensionUI`. The second project is not required and you can safely delete it.
8. Change the target SDK of your Broadcast Extension target to at least iOS 10.0


**Set up Keychain Sharing**

Your app and the app extension you created above need to share some secrets via the iOS Keychain. They do this using their own Keychain group so they are isolated from the rest of your apps Keychain.

In **both** your **iOS app** and your **extension project** add a Keychain Sharing entitlement for the "io.cobrowse" keychain group.


**Add the bundle ID to your plist**

Take the bundle ID of the **extension** you created above, and add the following entry in your apps `Info.plist` (*Note:* **not** in the extensions `Info.plist`), replacing the bundle ID below with your own:

```xml
<key>CBIOBroadcastExtension</key>
<string>your.app.extension.bundle.ID.here</string>
```

**Add the new target to your Podfile**

The app extension needs a dependency on the CobrowseIO app extension framework. Add the following NuGet to the **extension project**:

- [![CobrowseIO.AppExtension.iOS NuGet](https://img.shields.io/nuget/v/CobrowseIO.AppExtension.iOS.svg?label=CobrowseIO.AppExtension.iOS)](https://www.nuget.org/packages/CobrowseIO.AppExtension.iOS/)

**Implement the extension**

Xcode will have added `SampleHandler.cs` file as part of the extension project you created earlier. Replace the content of the file with the following:

```cs
using Xamarin.CobrowseIO.AppExtension;

[Register("SampleHandler")]
public class SampleHandler : CobrowseIOReplayKitExtension
{
    protected SampleHandler(IntPtr handle) : base(handle)
    {
    }
}
```

**Make sure Info.plist points to the correct class**

Open Info.plist of the extension project and make sure that `NSExtension` section looks like this:

```xml
<plist version="1.0">
<dict>
    ...
	<key>NSExtension</key>
	<dict>
		<key>NSExtensionPointIdentifier</key>
		<string>com.apple.broadcast-services-upload</string>
		<key>NSExtensionPrincipalClass</key>
		<string>SampleHandler</string>
		<key>RPBroadcastProcessMode</key>
		<string>RPBroadcastProcessModeSampleBuffer</string>
	</dict>
</dict>
</plist>

```

**Build and run your app**

You're now ready to build and run your app. The full device capability is only available on phsyical devices, it will not work in the iOS simulator.

## Xamarin.Android implementation

Full device remote control for Android uses an accessibility service that must be enabled on the device to grant access. This feature is supported in API 21 (5.0 Lollipop) and above.

**Configure Full Device Control flag**

Add the following line to one of your resources xml files, eg. in `res/values/bools.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <bool name="cobrowse_enable_full_device_control">true</bool>
</resources>
```

*Note: Please add this value to a file with Build Action set to `AndroidResource`, and not to an XML resource file.*

Enable the accessibility service the Cobrowse SDK will have added in the main device settings, eg. Settings -> Accessibility -> Your App Name. Note: this only has to be done the very first time. 

We also have built some logic to detect if accessibility service is running, and if not, to deep link the user to the settings to enable it. 

Show the sample UI with:

```cs
CobrowseAccessibilityService.ShowSetup(...)
```

Check if accessibility service is already running with:

```cs
CobrowseAccessibilityService.IsRunning(...)
```

Deep link user to accessibility settings with:

```cs
Intent intent = new Intent(global::Android.Provider.Settings.ActionAccessibilitySettings);
intent.AddFlags(ActivityFlags.NewTask);
StartActivity(intent);
```

**Notes for unattended access**

For unattended full device access, we strongly recommend: 
- Please initiate sessions with push notifications, rather than our default sockets. This will enable unattended access, even when your app has been backgrounded a long time, or force closed. More info at <https://github.com/cobrowseio/cobrowse-sdk-xamarin/blob/master/docs/initiate-with-push.md>.
- Please turn off "Require User Consent" prompts at <https://cobrowse.io/dashboard/settings>. Otherwise, a user must always accept the consent prompt on the device before a session can start.

## Questions?

Any questions at all? Please email us directly at [hello@cobrowse.io](mailto:hello@cobrowse.io).
