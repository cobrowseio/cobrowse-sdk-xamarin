using System;
using Foundation;
using ReplayKit;
using Xamarin.CobrowseIO.iOS.AppExtension;

namespace MauiSample.iOS.BroadcastUploadExtension
{
    public class SampleHandler : CobrowseIOReplayKitExtension
    {
        protected SampleHandler (IntPtr handle) : base (handle)
        {
        }
    }
}

