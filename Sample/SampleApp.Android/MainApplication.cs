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
        Label = "Cobrowse.io Xamarin.Android",
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

            CobrowseIO.Instance.License = "trial";
            Debug.WriteLine("Cobrowse device id: " + CobrowseIO.Instance.GetDeviceId(this));

            CobrowseIO.Instance.CustomData = new Dictionary<string, object>()
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
                { "custom_field", 5.75f }
            };

            CobrowseIO.Instance.SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance.Start(this);
        }
    }

    public class CustomCobrowseDelegate : Java.Lang.Object,
        // CobrowseIO.ISessionRequestDelegate,
        CobrowseIO.ISessionLoadDelegate
        // CobrowseIO.IRemoteControlRequestDelegate,
        // CobrowseIO.IFullDeviceRequestDelegate
    {
        public CustomCobrowseDelegate()
        {
        }

        public CustomCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        /*
         * If you're overriding HandleSessionRequest you become responsible
         * to activate the session.
        public void HandleSessionRequest(Activity activity, Session session)
        {
            Debug.WriteLine("HandleSessionRequest");
            session.Activate(callback: null);
        }
         */

        public void SessionDidLoad(Session session)
        {
            Debug.WriteLine("SessionDidLoad");
        }

        public void SessionDidEnd(Session session)
        {
            Debug.WriteLine("SessionDidEnd");
        }

        public void SessionDidUpdate(Session session)
        {
            Debug.WriteLine("SessionDidUpdate");
        }

        /*
         * If you're overriding HandleRemoteControlRequest you become responsible
         * to update the session remote-control state.
        public void HandleRemoteControlRequest(Activity activity, Session session)
        {
            session.SetRemoteControl(RemoteControlState.On, callback: null);
        }
         */

        /*
         * If you're overriding HandleFullDeviceRequest you become responsible
         * to update the session full-device state.
        public void HandleFullDeviceRequest(Activity activity, Session session)
        {
            session.SetFullDeviceState(FullDeviceState.On, callback: null);
        }
         */
    }
}
