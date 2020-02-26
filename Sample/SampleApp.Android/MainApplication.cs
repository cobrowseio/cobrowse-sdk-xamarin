using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Runtime;
using Xamarin.CobrowseIO;

namespace SampleApp.Android
{
    [Application(
        #if DEBUG
        Debuggable = true,
        #else
        Debuggable = false,
        #endif
        Label = "@strings/app_name",
        Icon = "@mipmap/ic_launcher")]
    public class MainApplication : Application
    {
        public MainApplication()
        {
        }

        protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            CobrowseIO.Instance().License("trial");

            Debug.WriteLine("Cobrowse device id: " + CobrowseIO.Instance().DeviceId(this));

            var customData = new Dictionary<string, Java.Lang.Object>()
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
            };
            CobrowseIO.Instance().CustomData(customData);

            CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance().Start(this);
        }
    }

    public class CustomCobrowseDelegate : Java.Lang.Object, CobrowseIO.ISessionRequestDelegate
    {
        public CustomCobrowseDelegate()
        {
        }

        public CustomCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public void HandleSessionRequest(Activity activity, Session session)
        {
            Debug.WriteLine("HandleSessionRequest");
            session.Activate(null);
        }

        public void SessionDidEnd(Session session)
        {
            Debug.WriteLine("SessionDidEnd");
        }

        public void SessionDidUpdate(Session session)
        {
            Debug.WriteLine("SessionDidUpdate");
        }
    }
}
