using System;

using ObjCRuntime;
using Foundation;
using UIKit;
using ReplayKit;

namespace Cobrowse.IO.iOS.AppExtension
{
    // @interface CobrowseIOReplayKitExtension : RPBroadcastSampleHandler
    [BaseType(typeof(RPBroadcastSampleHandler))]
    interface CobrowseIOReplayKitExtension
    {
    }
}

