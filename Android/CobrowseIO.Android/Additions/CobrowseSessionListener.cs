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

        public void CobrowseSessionDidUpdate(Session session)
        {
            _delegateDidUpdate(session);
        }

        public void CobrowseSessionDidEnd(Session session)
        {
            _delegateDidEnd(session);
        }
    }
}
