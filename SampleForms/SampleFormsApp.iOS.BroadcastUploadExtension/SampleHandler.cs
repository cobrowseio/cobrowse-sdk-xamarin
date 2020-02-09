using System;
using CobrowseIOSdk.AppExtension;
using Foundation;

namespace SampleFormsApp.iOS.BroadcastUploadExtension
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
