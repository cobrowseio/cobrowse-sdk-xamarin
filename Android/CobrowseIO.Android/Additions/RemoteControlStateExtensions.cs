using System;
using System.Runtime.CompilerServices;

namespace Cobrowse.IO.Android
{
    internal static class RemoteControlStateExtensions
    {
        internal static Cobrowse.IO.Android.Session.RemoteControlState ToJavaEnum(
            this Cobrowse.IO.Android.RemoteControlState state)
        {
            switch (state)
            {
                case Cobrowse.IO.Android.RemoteControlState.Off:
                    return Cobrowse.IO.Android.Session.RemoteControlState.Off;
                case Cobrowse.IO.Android.RemoteControlState.Requested:
                    return Cobrowse.IO.Android.Session.RemoteControlState.Requested;
                case Cobrowse.IO.Android.RemoteControlState.Rejected:
                    return Cobrowse.IO.Android.Session.RemoteControlState.Rejected;
                case Cobrowse.IO.Android.RemoteControlState.On:
                    return Cobrowse.IO.Android.Session.RemoteControlState.On;
                default:
                    return default;
            }

        }

        internal static Cobrowse.IO.Android.RemoteControlState ToManagedEnum(
            this Cobrowse.IO.Android.Session.RemoteControlState javaState)
        {
            if (javaState == Cobrowse.IO.Android.Session.RemoteControlState.Off)
            {
                return Cobrowse.IO.Android.RemoteControlState.Off;
            }
            if (javaState == Cobrowse.IO.Android.Session.RemoteControlState.Requested)
            {
                return Cobrowse.IO.Android.RemoteControlState.Requested;
            }
            if (javaState == Cobrowse.IO.Android.Session.RemoteControlState.Rejected)
            {
                return Cobrowse.IO.Android.RemoteControlState.Rejected;
            }
            if (javaState == Cobrowse.IO.Android.Session.RemoteControlState.On)
            {
                return Cobrowse.IO.Android.RemoteControlState.On;
            }
            return default;
        }
    }
}
