using System;
using System.Runtime.CompilerServices;

namespace Xamarin.CobrowseIO
{
    internal static class FullDeviceStateExtensions
    {
        internal static Xamarin.CobrowseIO.Session.FullDeviceStateJava ToJavaEnum(
            this Xamarin.CobrowseIO.FullDeviceState state)
        {
            switch (state)
            {
                case Xamarin.CobrowseIO.FullDeviceState.Off:
                    return Xamarin.CobrowseIO.Session.FullDeviceStateJava.Off;
                case Xamarin.CobrowseIO.FullDeviceState.Requested:
                    return Xamarin.CobrowseIO.Session.FullDeviceStateJava.Requested;
                case Xamarin.CobrowseIO.FullDeviceState.Rejected:
                    return Xamarin.CobrowseIO.Session.FullDeviceStateJava.Rejected;
                case Xamarin.CobrowseIO.FullDeviceState.On:
                    return Xamarin.CobrowseIO.Session.FullDeviceStateJava.On;
                default:
                    return default;
            }

        }

        internal static Xamarin.CobrowseIO.FullDeviceState ToManagedEnum(
            this Xamarin.CobrowseIO.Session.FullDeviceStateJava javaState)
        {
            if (javaState == Xamarin.CobrowseIO.Session.FullDeviceStateJava.Off)
            {
                return Xamarin.CobrowseIO.FullDeviceState.Off;
            }
            if (javaState == Xamarin.CobrowseIO.Session.FullDeviceStateJava.Requested)
            {
                return Xamarin.CobrowseIO.FullDeviceState.Requested;
            }
            if (javaState == Xamarin.CobrowseIO.Session.FullDeviceStateJava.Rejected)
            {
                return Xamarin.CobrowseIO.FullDeviceState.Rejected;
            }
            if (javaState == Xamarin.CobrowseIO.Session.FullDeviceStateJava.On)
            {
                return Xamarin.CobrowseIO.FullDeviceState.On;
            }
            return default;
        }
    }
}

