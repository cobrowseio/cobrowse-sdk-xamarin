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

        public void SetCustomData(IDictionary<string, string> customData)
        {
            if (customData == null)
            {
                this.CustomData = null;
                return;
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

        public void SetCustomData(NSDictionary<NSString, NSObject> customData)
        {
            this.CustomData = customData;
        }

        public void SetCustomData(IDictionary<string, object> customData)
        {
            if (customData == null)
            {
                this.CustomData = null;
                return;
            }
            NSObject[] objects = new NSObject[customData.Count];
            NSString[] keys = new NSString[customData.Count];
            int counter = 0;
            foreach (KeyValuePair<string, object> next in customData)
            {
                keys[counter] = new NSString(next.Key);
                switch (next.Value)
                {
                    case string stringValue:
                        objects[counter] = new NSString(stringValue);
                        break;
                    case int intVallue:
                        objects[counter] = new NSNumber(intVallue);
                        break;
                    case float floatValue:
                        objects[counter] = new NSNumber(floatValue);
                        break;
                    case nfloat nfloatValue:
                        objects[counter] = new NSNumber(nfloatValue);
                        break;
                    case double doubleValue:
                        objects[counter] = new NSNumber(doubleValue);
                        break;
                    case bool boolValue:
                        objects[counter] = new NSNumber(boolValue);
                        break;
                    default:
                        objects[counter] = new NSString(next.Value.ToString());
                        break;
                }

                counter++;
            }
            this.CustomData = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(objects, keys, customData.Count);
        }


        public void SetLicense(string license)
        {
            this.License = license;
        }

        public void SetDelegate(CobrowseIODelegate @delegate)
        {
            this.Delegate = @delegate;
        }
    }
}
