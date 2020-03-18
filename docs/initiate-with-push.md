# Initiate with push (optional)

Please see full documentation at [https://cobrowse.io/docs](https://cobrowse.io/docs).

Try our **online demo** at the bottom of our homepage at <https://cobrowse.io/#tryit>.

## Implementation

These are the native-side requirements on iOS and Android to initiate sessions with push. More info at <https://cobrowse.io/docs#initiate-with-push>

## Xamarin.iOS implementation

If you are already using push notifications in your app, there is nothing further required on the native side.

If you are not already using push notifications in your app, please enable them under Capabilities in the Xamarin.iOS app project and request push permission from the user whenever is appropriate:

```cs
[Export("applicationDidBecomeActive:")]
public void OnActivated(UIApplication application)
{
    application.RegisterUserNotificationSettings(
        UIUserNotificationSettings.GetSettingsForTypes(
            UIUserNotificationType.Badge | UIUserNotificationType.Sound | UIUserNotificationType.Alert,
            categories: null));
    application.RegisterForRemoteNotifications();
}
```

## Xamarin.Android implementation

You must first add Firebase Cloud Messaging (FCM) to your app. Please see FCM documentation at <https://docs.microsoft.com/en-us/xamarin/android/data-cloud/google-messaging/firebase-cloud-messaging>.

Next, whenever your device receives a registration token or push notification from FCM, pass that to the Cobrowse.io SDK, for example:

```cs
using System;
using Android.App;
using Android.Runtime;
using Firebase.Messaging;
using Xamarin.CobrowseIO;

namespace YourAppNamespace
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessaging : FirebaseMessagingService
    {
        public FirebaseMessaging()
        {
        }

        protected FirebaseMessaging(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            CobrowseIO.Instance().OnPushNotification(remoteMessage.Data);
        }

        public override void OnNewToken(string token)
        {
            CobrowseIO.Instance().SetDeviceToken(Application, token);
        }
    }
}

```

## Questions?
Any questions at all? Please email us directly at [hello@cobrowse.io](mailto:hello@cobrowse.io).