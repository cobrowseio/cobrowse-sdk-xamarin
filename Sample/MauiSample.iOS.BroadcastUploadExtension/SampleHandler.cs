using System;
using Foundation;
using ReplayKit;
using Cobrowse.IO.iOS.AppExtension;

namespace MauiSample.iOS.BroadcastUploadExtension
{
    [Register("SampleHandler")]
    public class SampleHandler : CobrowseIOReplayKitExtension
    {
        protected SampleHandler (IntPtr handle) : base (handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}

