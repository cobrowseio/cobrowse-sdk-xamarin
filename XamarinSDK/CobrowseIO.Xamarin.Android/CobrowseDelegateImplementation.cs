using System;
using Android.App;
using Android.Runtime;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cobrowse.io delegate provides callbacks to the cross-platform wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseDelegateImplementation : Java.Lang.Object,
        CobrowseIO.ISessionRequestDelegate
    {
        private CobrowseIOImplementation CrossImplementation
            => (CobrowseIOImplementation)Xamarin.CobrowseIO.Abstractions.CobrowseIO.Instance;

        public CobrowseDelegateImplementation()
        {
        }

        public CobrowseDelegateImplementation(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public void HandleSessionRequest(Activity activity, Session session)
        {
            if (!CrossImplementation.RaiseSessionDidRequest(session))
            {
                session.Activate(callback: null);
            }
        }

        public void SessionDidUpdate(Session session)
        {
            CrossImplementation.RaiseSessionDidUpdate(session);
        }

        public void SessionDidEnd(Session session)
        {
            CrossImplementation.RaiseSessionDidEnd(session);
        }
    }
}
