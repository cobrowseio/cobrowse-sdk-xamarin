using System;
using Foundation;

namespace Xamarin.CobrowseIO
{
    public static class CBIO
    {
        // These values copied directly from CobrowseIO.h
        // https://forums.xamarin.com/discussion/8572/how-do-you-bind-extern-nsstring-const
        public static NSString UserIdKey { get; } = new NSString("user_id");

        public static NSString UserEmailKey { get; } = new NSString("user_email");

        public static NSString UserNameKey { get; } = new NSString("user_name");

        public static NSString DeviceIdKey { get; } = new NSString("device_id");

        public static NSString DeviceNameKey { get; } = new NSString("device_name");
    }
}
