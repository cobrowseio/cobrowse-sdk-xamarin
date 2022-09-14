using System;
using System.Collections.Generic;
using System.ComponentModel;
using Foundation;
using UIKit;
using Xamarin.CobrowseIO.Abstractions;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// iOS-specific implementation of the cross-platform Cobrowse.io wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseIOImplementation : ICobrowseIO
    {
        /// <summary>
        /// Occurs when a session is requested.
        /// </summary>
        public event EventHandler<ISession> SessionDidRequest;

        internal bool RaiseSessionDidRequest(Session session)
        {
            var sessionDidRequest = SessionDidRequest;
            if (sessionDidRequest != null)
            {
                sessionDidRequest(this, CobrowseSessionImplementation.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when an agent requests remote control.
        /// </summary>
        public event EventHandler<ISession> RemoteControlRequest;

        internal bool RaiseRemoteControlRequest(Session session)
        {
            var remoteControlRequest = RemoteControlRequest;
            if (remoteControlRequest != null)
            {
                remoteControlRequest(this, CobrowseSessionImplementation.TryCreate(session));
                return true;
            }
            return false;
        }

        /* TODO implement in the next version
        /// <inheritdoc/>
        public event EventHandler<ISession> FullDeviceRequest;

        internal bool RaiseFullDeviceRequest(Session session)
        {
            var fullDeviceRequest = FullDeviceRequest;
            if (fullDeviceRequest != null)
            {
                fullDeviceRequest(this, CobrowseSessionImplementation.TryCreate(session));
                return true;
            }
            return false;
        }
        */

        /// <summary>
        /// Occurs when a session is first made available to the device,
        /// whether by creating a 6 digit code, or via a connect request from an agent.
        /// </summary>
        public event EventHandler<ISession> SessionDidLoad;

        internal bool RaiseSessionDidLoad(Session session)
        {
            var sessionDidLoad = SessionDidLoad;
            if (sessionDidLoad != null)
            {
                sessionDidLoad(this, CobrowseSessionImplementation.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session is updated.
        /// </summary>
        public event EventHandler<ISession> SessionDidUpdate;

        internal bool RaiseSessionDidUpdate(Session session)
        {
            var sessionDidUpdate = SessionDidUpdate;
            if (sessionDidUpdate != null)
            {
                sessionDidUpdate(this, CobrowseSessionImplementation.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session ends.
        /// </summary>
        public event EventHandler<ISession> SessionDidEnd;

        internal bool RaiseSessionDidEnd(Session session)
        {
            var sessionDidEnd = SessionDidEnd;
            if (sessionDidEnd != null)
            {
                sessionDidEnd(this, CobrowseSessionImplementation.TryCreate(session));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the current session instance or null if it doesn't exist.
        /// </summary>
        public ISession CurrentSession
            => CobrowseSessionImplementation.TryCreate(CobrowseIO.Instance.CurrentSession);

        /// <summary>
        /// Creates a new Cobrowse.io session.
        /// </summary>
        public void CreateSession(CobrowseCallback callback)
        {
            CobrowseIO.Instance.CreateSession((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Gets or sets the API string.
        /// </summary>
        public string Api
        {
            get => CobrowseIO.Instance.Api;
            set => CobrowseIO.Instance.Api = value;
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance.DeviceId;

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        public string License
        {
            get => CobrowseIO.Instance.License;
            set => CobrowseIO.Instance.License = value;
        }

        /// <summary>
        /// Sets the license.
        /// </summary>
        [Obsolete("Use License property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetLicense(string licenseKey)
        {
            CobrowseIO.Instance.SetLicense(licenseKey);
        }

        /// <summary>
        /// Starts the Cobrowse.io.
        /// </summary>
        public void Start()
        {
            CobrowseIO.Instance.SetDelegate(new CobrowseDelegateImplementation());
            CobrowseIO.Instance.Start();
        }

        /// <summary>
        /// Stops the Cobrowse.io.
        /// </summary>
        public void Stop()
        {
            CobrowseIO.Instance.Stop();
        }

        /// <summary>
        /// Gets or sets Cobrowse.io custom data. 
        /// </summary>
        public IReadOnlyDictionary<string, object> CustomData
        {
            get => CobrowseIO.Instance.CustomData;
            set => CobrowseIO.Instance.CustomData = value;
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        [Obsolete("Use CustomData property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetCustomData(IDictionary<string, object> customData)
        {
            CobrowseIO.Instance.SetCustomData(customData);
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
        [Obsolete("Use 'CobrowseAccessibilityService' directly in the Android project")]
        public bool CheckCobrowseFullDevice()
        {
            // On iOS it is only requered to add a broadcast extension to the app.
            // Assuming the extension is configured, we always return 'true'.
            return true;
        }
    }
}
