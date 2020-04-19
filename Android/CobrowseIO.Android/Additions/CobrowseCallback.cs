using System;
using Android.Runtime;
using Java.Lang;

namespace Xamarin.CobrowseIO
{
    public delegate void CobrowseCallbackDelegate(Error e, Session s);

    public class CobrowseCallback : Java.Lang.Object, Xamarin.CobrowseIO.ICallback
    {
        private readonly CobrowseCallbackDelegate _delegate;

        public CobrowseCallback(CobrowseCallbackDelegate @delegate)
        {
            _delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        public void Call(Java.Lang.Object err, Java.Lang.Object session)
        {
            _delegate.Invoke(err.JavaCast<Error>(), session.JavaCast<Session>());
        }

        public void Call(Java.Lang.Error err, Session session)
        {
            _delegate.Invoke(err, session);
        }
    }
}
