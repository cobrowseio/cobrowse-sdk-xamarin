using System;
using Foundation;
using ObjCRuntime;
using ReplayKit;
using Cobrowse.IO.iOS.AppExtension;

namespace MauiSample.iOS.BroadcastUploadExtension
{
    [Register("SampleHandler")]
    public class SampleHandler : CobrowseIOReplayKitExtension
    {
        protected SampleHandler(NativeHandle handle) : base (handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}

