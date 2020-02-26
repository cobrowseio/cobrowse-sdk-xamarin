using System;
using Foundation;
using Xamarin.CobrowseIO.AppExtension;

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
