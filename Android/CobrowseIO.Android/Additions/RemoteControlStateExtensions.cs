using System;
using System.Runtime.CompilerServices;

namespace Xamarin.CobrowseIO.Android
{
    internal static class RemoteControlStateExtensions
    {
        internal static Xamarin.CobrowseIO.Android.Session.RemoteControlState ToJavaEnum(
            this Xamarin.CobrowseIO.Android.RemoteControlState state)
        {
            switch (state)
            {
                case Xamarin.CobrowseIO.Android.RemoteControlState.Off:
                    return Xamarin.CobrowseIO.Android.Session.RemoteControlState.Off;
                case Xamarin.CobrowseIO.Android.RemoteControlState.Requested:
                    return Xamarin.CobrowseIO.Android.Session.RemoteControlState.Requested;
                case Xamarin.CobrowseIO.Android.RemoteControlState.Rejected:
                    return Xamarin.CobrowseIO.Android.Session.RemoteControlState.Rejected;
                case Xamarin.CobrowseIO.Android.RemoteControlState.On:
                    return Xamarin.CobrowseIO.Android.Session.RemoteControlState.On;
                default:
                    return default;
            }

        }

        internal static Xamarin.CobrowseIO.Android.RemoteControlState ToManagedEnum(
            this Xamarin.CobrowseIO.Android.Session.RemoteControlState javaState)
        {
            if (javaState == Xamarin.CobrowseIO.Android.Session.RemoteControlState.Off)
            {
                return Xamarin.CobrowseIO.Android.RemoteControlState.Off;
            }
            if (javaState == Xamarin.CobrowseIO.Android.Session.RemoteControlState.Requested)
            {
                return Xamarin.CobrowseIO.Android.RemoteControlState.Requested;
            }
            if (javaState == Xamarin.CobrowseIO.Android.Session.RemoteControlState.Rejected)
            {
                return Xamarin.CobrowseIO.Android.RemoteControlState.Rejected;
            }
            if (javaState == Xamarin.CobrowseIO.Android.Session.RemoteControlState.On)
            {
                return Xamarin.CobrowseIO.Android.RemoteControlState.On;
            }
            return default;
        }
    }
}
