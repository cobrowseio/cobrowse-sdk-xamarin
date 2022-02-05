using Foundation;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cobrowse.io delegate provides callbacks to the cross-platform wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseDelegateImplementation : CobrowseIODelegate
    {
        private CobrowseIOImplementation CrossImplementation
            => (CobrowseIOImplementation)Xamarin.CobrowseIO.Abstractions.CobrowseIO.Instance;

        public CobrowseDelegateImplementation()
        {
        }

        public CobrowseDelegateImplementation(System.IntPtr handle) : base(handle)
        {
        }

        public override void HandleSessionRequest(Session session)
        {
            if (!CrossImplementation.RaiseSessionDidRequest(session))
            {
                session.Activate(callback: null);
            }
        }

        public override void HandleRemoteControlRequest(Session session)
        {
            if (!CrossImplementation.RaiseRemoteControlRequest(session))
            {
                session.SetRemoteControl(RemoteControlState.On, callback: null);
            }
        }

        public override void SessionDidLoad(Session session)
        {
            CrossImplementation.RaiseSessionDidLoad(session);
        }

        public override void SessionDidUpdate(Session session)
        {
            CrossImplementation.RaiseSessionDidUpdate(session);
        }

        public override void SessionDidEnd(Session session)
        {
            CrossImplementation.RaiseSessionDidEnd(session);
        }
    }
}
