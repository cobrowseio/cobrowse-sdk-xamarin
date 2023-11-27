using System;
using Android.Runtime;

namespace Xamarin.CobrowseIO.Android
{
    public delegate void CobrowseCallbackDelegate<T1, T2>(T1 e, T2 s)
        where T1 : class, IJavaObject
        where T2 : class, IJavaObject;

    public class CobrowseCallback<T1, T2> : Java.Lang.Object, Xamarin.CobrowseIO.Android.ICallback
        where T1 : class, IJavaObject
        where T2 : class, IJavaObject
    {
        private readonly CobrowseCallbackDelegate<T1, T2> _delegate;

        public CobrowseCallback(CobrowseCallbackDelegate<T1, T2> @delegate)
        {
            _delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        public void Call(Java.Lang.Object err, Java.Lang.Object session)
        {
            _delegate.Invoke(err.JavaCast<T1>(), session.JavaCast<T2>());
        }

        public void Call(T1 err, T2 session)
        {
            _delegate.Invoke(err, session);
        }
    }
}
