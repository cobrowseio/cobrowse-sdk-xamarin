using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace SampleApp.Forms.Android
{
    [Application(
        #if DEBUG
        Debuggable = true,
        #else
        Debuggable = false,
        #endif
        Label = "Cobrowse.io Xamarin.Forms",
        Icon = "@mipmap/icon")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }
    }
}
