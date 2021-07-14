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
                return this._RemoteControl().ToManagedEnum();
            }
        }

        public void SetRemoteControl([GeneratedEnum] Xamarin.CobrowseIO.RemoteControlState state, ICallback callback)
        {
            Xamarin.CobrowseIO.Session.RemoteControlState javaState = state.ToJavaEnum();
            this._SetRemoteControl(javaState, callback);
        }

        public void SetRemoteControl([GeneratedEnum] Xamarin.CobrowseIO.RemoteControlState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            Xamarin.CobrowseIO.Session.RemoteControlState javaState = state.ToJavaEnum();
            this._SetRemoteControl(javaState, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }
    }
}
