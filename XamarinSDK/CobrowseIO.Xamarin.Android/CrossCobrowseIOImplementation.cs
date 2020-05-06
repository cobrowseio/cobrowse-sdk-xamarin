using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Plugin.CurrentActivity;
using Xamarin.CobrowseIO.UI;
using JError = Java.Lang.Error;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Android-specific implementation of the cross-platform Cobrowse.io wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CrossCobrowseIOImplementation : ICrossCobrowseIO
    {
        protected Activity Activity => CrossCurrentActivity.Current.Activity;

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
            => CobrowseSession.TryCreate(CobrowseIO.Instance.CurrentSession);

        /// <summary>
        /// Creates a new Cobrowse.io session.
        /// </summary>
        public void CreateSession(CobrowseCallback callback)
        {
            CobrowseIO.Instance.CreateSession((JError e, Session session) =>
            {
                callback?.Invoke(e, CobrowseSession.TryCreate(session));
            });
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance.GetDeviceId(Activity.Application);

        /// <summary>
        /// Sets the license.
        /// </summary>
        public void SetLicense(string licenseKey)
        {
            CobrowseIO.Instance.SetLicense(licenseKey);
        }

        /// <summary>
        /// Starts the Cobrowse.io.
        /// </summary>
        public void Start()
        {
            if (Application.Context is Application application)
            {
                CobrowseIO.Instance.SetDelegate(new CrossCobrowseDelegate());
                CobrowseIO.Instance.Start(application);
            }
        }

        /// <summary>
        /// Stops the Cobrowse.io.
        /// </summary>
        public void Stop()
        {
            CobrowseIO.Instance.Stop();
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        public void SetCustomData(IDictionary<string, object> customData)
        {
            CobrowseIO.Instance.SetCustomData(customData);
        }

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        public void OpenCobrowseUI()
        {
            var intent = new Intent(Activity, typeof(CobrowseActivity));
            Activity.StartActivity(intent);
        }

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        public void CheckCobrowseFullDevice()
        {
            bool isRunning = CobrowseAccessibilityService.IsRunning(Activity);
            if (!isRunning)
            {
                CobrowseAccessibilityService.ShowSetup(Activity);
                return;
            }
        }
    }

}
