using System;
using System.Collections.Generic;
using Foundation;

namespace Xamarin.CobrowseIO
{
    public partial class CobrowseIO
    {
        // These values copied directly from CobrowseIO.h
        // https://forums.xamarin.com/discussion/8572/how-do-you-bind-extern-nsstring-const
        public static NSString UserIdKey { get; } = new NSString("user_id");

        public static NSString UserEmailKey { get; } = new NSString("user_email");

        public static NSString UserNameKey { get; } = new NSString("user_name");

        public static NSString DeviceIdKey { get; } = new NSString("device_id");

        public static NSString DeviceNameKey { get; } = new NSString("device_name");

        public void SetCustomData(Dictionary<string, string> customData)
        {
            if (customData == null)
            {
                throw new ArgumentNullException(nameof(customData));
            }
            NSString[] objects = new NSString[customData.Count];
            NSString[] keys = new NSString[customData.Count];
            int counter = 0;
            foreach (KeyValuePair<string, string> next in customData)
            {
                keys[counter] = new NSString(next.Key);
                objects[counter] = new NSString(next.Value);
                counter++;
            }
            this.CustomData = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(objects, keys, customData.Count);
        }
    }
}
