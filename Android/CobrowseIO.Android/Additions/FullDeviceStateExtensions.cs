using System;
using System.Runtime.CompilerServices;

namespace Xamarin.CobrowseIO.Android
{
    internal static class FullDeviceStateExtensions
    {
        internal static Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava ToJavaEnum(
            this Xamarin.CobrowseIO.Android.FullDeviceState state)
        {
            switch (state)
            {
                case Xamarin.CobrowseIO.Android.FullDeviceState.Off:
                    return Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.Off;
                case Xamarin.CobrowseIO.Android.FullDeviceState.Requested:
                    return Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.Requested;
                case Xamarin.CobrowseIO.Android.FullDeviceState.Rejected:
                    return Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.Rejected;
                case Xamarin.CobrowseIO.Android.FullDeviceState.On:
                    return Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.On;
                default:
                    return default;
            }

        }

        internal static Xamarin.CobrowseIO.Android.FullDeviceState ToManagedEnum(
            this Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava javaState)
        {
            if (javaState == Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.Off)
            {
                return Xamarin.CobrowseIO.Android.FullDeviceState.Off;
            }
            if (javaState == Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.Requested)
            {
                return Xamarin.CobrowseIO.Android.FullDeviceState.Requested;
            }
            if (javaState == Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.Rejected)
            {
                return Xamarin.CobrowseIO.Android.FullDeviceState.Rejected;
            }
            if (javaState == Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava.On)
            {
                return Xamarin.CobrowseIO.Android.FullDeviceState.On;
            }
            return default;
        }
    }
}
