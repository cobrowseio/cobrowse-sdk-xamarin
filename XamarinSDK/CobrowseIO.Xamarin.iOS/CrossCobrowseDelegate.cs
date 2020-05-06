using Foundation;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cobrowse.io delegate provides callbacks to the cross-platform wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CrossCobrowseDelegate : CobrowseIODelegate
    {
        private CrossCobrowseIOImplementation CrossImplementation
            => (CrossCobrowseIOImplementation)CrossCobrowseIO.Instance();

        public CrossCobrowseDelegate()
        {
        }

        public CrossCobrowseDelegate(System.IntPtr handle) : base(handle)
        {
        }

        public override void HandleSessionRequest(Session session)
        {
            if (!CrossImplementation.RaiseSessionDidRequest(session))
            {
                session.Activate(callback: null);
            }
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
