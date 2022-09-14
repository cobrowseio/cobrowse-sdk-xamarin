using System;
using ObjCRuntime;
using Xamarin.CobrowseIO;

namespace Xamarin.CobrowseIO
{
    [Native(NativeName = "CBIORemoteControlState")]
    public enum RemoteControlState : ulong
    {
        Off = 0,
        Requested,
        Rejected,
        On
    }

    [Native(NativeName = "CBIOFullDeviceState")]
    public enum FullDeviceState : ulong
    {
        Off = 0,
        Requested,
        Rejected,
        On
    }
}