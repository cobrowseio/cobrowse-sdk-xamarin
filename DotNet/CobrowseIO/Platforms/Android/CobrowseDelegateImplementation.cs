using System;
using Android.App;
using Android.Runtime;
using Xamarin.CobrowseIO.Android;
using Xamarin.CobrowseIO.Android.UI;
using NativeCobrowseIO = Xamarin.CobrowseIO.Android.CobrowseIO;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cobrowse.io delegate provides callbacks to the cross-platform wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseDelegateImplementation : Java.Lang.Object,
        NativeCobrowseIO.ISessionRequestDelegate,
        NativeCobrowseIO.IRemoteControlRequestDelegate,
        NativeCobrowseIO.ISessionLoadDelegate
    {
        private CobrowseIOImplementation CrossImplementation
            => (CobrowseIOImplementation)CobrowseIO.Instance;

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
                new SessionConsentDialogFragment().Show(activity);
            }
        }

        public void HandleRemoteControlRequest(Activity activity, Session session)
        {
            if (!CrossImplementation.RaiseRemoteControlRequest(session))
            {
                new RemoteControlConsentDialogFragment().Show(activity);
            }
        }

        public void SessionDidLoad(Session session)
        {
            CrossImplementation.RaiseSessionDidLoad(session);
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
