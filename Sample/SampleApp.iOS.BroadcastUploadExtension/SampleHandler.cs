using System;
using CobrowseIOSdk.AppExtension;
using Foundation;

namespace SampleApp.iOS.BroadcastUploadExtension
{
    [Register("SampleHandler")]
    public class SampleHandler : CobrowseIOReplayKitExtension
    {
        protected SampleHandler(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
