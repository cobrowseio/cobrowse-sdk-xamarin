using System;
using System.Runtime.CompilerServices;

namespace Cobrowse.IO.Android
{
    internal static class FullDeviceStateExtensions
    {
        internal static Cobrowse.IO.Android.Session.FullDeviceStateJava ToJavaEnum(
            this Cobrowse.IO.Android.FullDeviceState state)
        {
            switch (state)
            {
                case Cobrowse.IO.Android.FullDeviceState.Off:
                    return Cobrowse.IO.Android.Session.FullDeviceStateJava.Off;
                case Cobrowse.IO.Android.FullDeviceState.Requested:
                    return Cobrowse.IO.Android.Session.FullDeviceStateJava.Requested;
                case Cobrowse.IO.Android.FullDeviceState.Rejected:
                    return Cobrowse.IO.Android.Session.FullDeviceStateJava.Rejected;
                case Cobrowse.IO.Android.FullDeviceState.On:
                    return Cobrowse.IO.Android.Session.FullDeviceStateJava.On;
                default:
                    return default;
            }

        }

        internal static Cobrowse.IO.Android.FullDeviceState ToManagedEnum(
            this Cobrowse.IO.Android.Session.FullDeviceStateJava javaState)
        {
            if (javaState == Cobrowse.IO.Android.Session.FullDeviceStateJava.Off)
            {
                return Cobrowse.IO.Android.FullDeviceState.Off;
            }
            if (javaState == Cobrowse.IO.Android.Session.FullDeviceStateJava.Requested)
            {
                return Cobrowse.IO.Android.FullDeviceState.Requested;
            }
            if (javaState == Cobrowse.IO.Android.Session.FullDeviceStateJava.Rejected)
            {
                return Cobrowse.IO.Android.FullDeviceState.Rejected;
            }
            if (javaState == Cobrowse.IO.Android.Session.FullDeviceStateJava.On)
            {
                return Cobrowse.IO.Android.FullDeviceState.On;
            }
            return default;
        }
    }
}
