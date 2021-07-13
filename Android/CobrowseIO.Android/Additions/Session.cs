using System;

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

        public void SetRemoteControl(Xamarin.CobrowseIO.RemoteControlState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.SetRemoteControl(state, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }
    }
}
