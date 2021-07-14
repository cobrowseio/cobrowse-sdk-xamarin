using System;
using System.Runtime.CompilerServices;

namespace Xamarin.CobrowseIO
{
    internal static class RemoteControlStateExtensions
    {
        internal static Xamarin.CobrowseIO.Session.RemoteControlState ToJavaEnum(
            this Xamarin.CobrowseIO.RemoteControlState state)
        {
            switch (state)
            {
                case Xamarin.CobrowseIO.RemoteControlState.Off:
                    return Xamarin.CobrowseIO.Session.RemoteControlState.Off;
                case Xamarin.CobrowseIO.RemoteControlState.Requested:
                    return Xamarin.CobrowseIO.Session.RemoteControlState.Requested;
                case Xamarin.CobrowseIO.RemoteControlState.Rejected:
                    return Xamarin.CobrowseIO.Session.RemoteControlState.Rejected;
                case Xamarin.CobrowseIO.RemoteControlState.On:
                    return Xamarin.CobrowseIO.Session.RemoteControlState.On;
                default:
                    return default;
            }

        }

        internal static Xamarin.CobrowseIO.RemoteControlState ToManagedEnum(
            this Xamarin.CobrowseIO.Session.RemoteControlState javaState)
        {
            if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.Off)
            {
                return Xamarin.CobrowseIO.RemoteControlState.Off;
            }
            if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.Requested)
            {
                return Xamarin.CobrowseIO.RemoteControlState.Requested;
            }
            if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.Rejected)
            {
                return Xamarin.CobrowseIO.RemoteControlState.Rejected;
            }
            if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.On)
            {
                return Xamarin.CobrowseIO.RemoteControlState.On;
            }
            return default;
        }
    }
}
