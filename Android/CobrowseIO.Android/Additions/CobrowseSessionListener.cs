namespace Xamarin.CobrowseIO
{
    public delegate void CobrowseSessionListenerDelegate(Session session);

    public class CobrowseSessionListener : Java.Lang.Object, Session.IListener
    {
        private readonly CobrowseSessionListenerDelegate _delegateDidUpdate;
        private readonly CobrowseSessionListenerDelegate _delegateDidEnd;

        public CobrowseSessionListener(
            CobrowseSessionListenerDelegate onDidUpdate,
            CobrowseSessionListenerDelegate onDidEnd)
        {
            _delegateDidUpdate = onDidUpdate;
            _delegateDidEnd = onDidEnd;
        }

        public void SessionDidUpdate(Session session)
        {
            _delegateDidUpdate(session);
        }

        public void SessionDidEnd(Session session)
        {
            _delegateDidEnd(session);
        }
    }
}
