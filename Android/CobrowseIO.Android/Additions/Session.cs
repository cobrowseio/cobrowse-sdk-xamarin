using System;
using Android.Runtime;

namespace Xamarin.CobrowseIO
{
    public partial class Session
    {
        public void Activate(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.Activate(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        public void End(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.End(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        public bool FullDevice => this._FullDevice().BooleanValue();

        public void SetFullDevice(bool value, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.SetFullDevice(value, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        [GeneratedEnum]
        public Xamarin.CobrowseIO.RemoteControlState RemoteControl
        {
            get
            {
                Xamarin.CobrowseIO.Session.RemoteControlState javaState = this._RemoteControl();
                if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.Off)
                {
                    return Xamarin.CobrowseIO.RemoteControlState.Off;
                }
                if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.Requested)
                {
                    return Xamarin.CobrowseIO.RemoteControlState.Requested;
                }
                if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.Rejected)
                {
                    return Xamarin.CobrowseIO.RemoteControlState.Rejected;
                }
                if (javaState == Xamarin.CobrowseIO.Session.RemoteControlState.On)
                {
                    return Xamarin.CobrowseIO.RemoteControlState.On;
                }
                return default;
            }
        }

        public void SetRemoteControl([GeneratedEnum] Xamarin.CobrowseIO.RemoteControlState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            Xamarin.CobrowseIO.Session.RemoteControlState javaState;
            switch (state)
            {
                case Xamarin.CobrowseIO.RemoteControlState.Off:
                    javaState = Xamarin.CobrowseIO.Session.RemoteControlState.Off;
                    break;
                case Xamarin.CobrowseIO.RemoteControlState.Requested:
                    javaState = Xamarin.CobrowseIO.Session.RemoteControlState.Requested;
                    break;
                case Xamarin.CobrowseIO.RemoteControlState.Rejected:
                    javaState = Xamarin.CobrowseIO.Session.RemoteControlState.Rejected;
                    break;
                case Xamarin.CobrowseIO.RemoteControlState.On:
                    javaState = Xamarin.CobrowseIO.Session.RemoteControlState.On;
                    break;
                default:
                    javaState = default;
                    break;
            }
            this._SetRemoteControl(javaState, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }
    }
}
