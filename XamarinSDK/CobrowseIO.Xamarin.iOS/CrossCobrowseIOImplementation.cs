using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// iOS-specific implementation of the cross-platform Cobrowse.io wrapper.
    /// </summary>
    [global::Foundation.Preserve(AllMembers = true)]
    public class CrossCobrowseIOImplementation : ICrossCobrowseIO
    {
        /// <summary>
        /// Occurs when a session is requested.
        /// </summary>
        public event EventHandler<ICobrowseSession> SessionDidRequest;

        internal bool RaiseSessionDidRequest(Session session)
        {
            var sessionDidRequest = SessionDidRequest;
            if (sessionDidRequest != null)
            {
                sessionDidRequest(this, CobrowseSession.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session is updated.
        /// </summary>
        public event EventHandler<ICobrowseSession> SessionDidUpdate;

        internal bool RaiseSessionDidUpdate(Session session)
        {
            var sessionDidUpdate = SessionDidUpdate;
            if (sessionDidUpdate != null)
            {
                sessionDidUpdate(this, CobrowseSession.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session ends.
        /// </summary>
        public event EventHandler<ICobrowseSession> SessionDidEnd;

        internal bool RaiseSessionDidEnd(Session session)
        {
            var sessionDidEnd = SessionDidEnd;
            if (sessionDidEnd != null)
            {
                sessionDidEnd(this, CobrowseSession.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the current session instance or null if it doesn't exist.
        /// </summary>
        public ICobrowseSession CurrentSession
            => CobrowseSession.TryCreate(CobrowseIO.Instance().CurrentSession);

        /// <summary>
        /// Creates a new Cobrowse.io session.
        /// </summary>
        public void CreateSession(CobrowseCallback callback)
        {
            CobrowseIO.Instance().CreateSession((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSession.TryCreate(session));
            });
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance().DeviceId;

        /// <summary>
        /// Sets the license.
        /// </summary>
        public void SetLicense(string licenseKey)
        {
            CobrowseIO.Instance().SetLicense(licenseKey);
        }

        /// <summary>
        /// Starts the Cobrowse.io.
        /// </summary>
        public void Start()
        {
            CobrowseIO.Instance().SetDelegate(new CrossCobrowseDelegate());
            CobrowseIO.Instance().Start();
        }

        /// <summary>
        /// Stops the Cobrowse.io.
        /// </summary>
        public void Stop()
        {
            CobrowseIO.Instance().Stop();
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        public void SetCustomData(IDictionary<string, object> customData)
        {
            CobrowseIO.Instance().SetCustomData(customData);
        }

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        public void OpenCobrowseUI()
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var nc = vc.GetUINavigationController();
            nc?.PushViewController(
                new CobrowseViewController(),
                animated: true);
        }

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        public void CheckCobrowseFullDevice()
        {
            return;
        }
    }
}
