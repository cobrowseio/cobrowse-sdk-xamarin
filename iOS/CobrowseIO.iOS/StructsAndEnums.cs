using System;
using ObjCRuntime;

namespace Cobrowse.IO.iOS
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
