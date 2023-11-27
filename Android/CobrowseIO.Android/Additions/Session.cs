using System;
using Android.Runtime;

namespace Xamarin.CobrowseIO.Android
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

        #region Full device

        [Obsolete("Use FullDeviceState instead")]
        public bool FullDevice => this._FullDevice().BooleanValue();

        [Obsolete("Use SetFullDeviceState instead")]
        public void SetFullDevice(bool value, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.SetFullDevice(value, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        [GeneratedEnum]
        public Xamarin.CobrowseIO.Android.FullDeviceState FullDeviceState
        {
            get
            {
                return this._FullDeviceState().ToManagedEnum();
            }
        }

        public void SetFullDeviceState([GeneratedEnum] Xamarin.CobrowseIO.Android.FullDeviceState state, ICallback callback)
        {
            Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava javaState = state.ToJavaEnum();
            this._SetFullDeviceState(javaState, callback);
        }

        public void SetFullDeviceState([GeneratedEnum] Xamarin.CobrowseIO.Android.FullDeviceState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            Xamarin.CobrowseIO.Android.Session.FullDeviceStateJava javaState = state.ToJavaEnum();
            this._SetFullDeviceState(javaState, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #endregion

        #region Capabilities

        public void SetCapabilities(string[] capabilities, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this._SetCapabilities(capabilities, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #endregion

        #region Remote control

        [GeneratedEnum]
        public Xamarin.CobrowseIO.Android.RemoteControlState RemoteControl
        {
            get
            {
                return this._RemoteControl().ToManagedEnum();
            }
        }

        public void SetRemoteControl([GeneratedEnum] Xamarin.CobrowseIO.Android.RemoteControlState state, ICallback callback)
        {
            Xamarin.CobrowseIO.Android.Session.RemoteControlState javaState = state.ToJavaEnum();
            this._SetRemoteControl(javaState, callback);
        }

        public void SetRemoteControl([GeneratedEnum] Xamarin.CobrowseIO.Android.RemoteControlState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            Xamarin.CobrowseIO.Android.Session.RemoteControlState javaState = state.ToJavaEnum();
            this._SetRemoteControl(javaState, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #endregion
    }
}
