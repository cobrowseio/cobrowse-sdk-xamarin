using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Runtime;
using Cobrowse.IO.Android;
using Cobrowse.IO.Android.UI;
using JClass = Java.Lang.Class;
using JError = Java.Lang.Error;
using JMethod = Java.Lang.Reflect.Method;
using JObject = Java.Lang.Object;
using NativeCobrowseIO = Cobrowse.IO.Android.CobrowseIO;

namespace Cobrowse.IO
{
    /// <summary>
    /// Android-specific implementation of the cross-platform Cobrowse.io wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseIOImplementation : ICobrowseIO
    {
        protected Activity? Activity
        {
            get
            {
                Activity? rvalue = null;
                try
                {
                    JClass activityWatcher = JClass.ForName("io.cobrowse.ActivityWatcher");
                    JMethod foregroundActivity = activityWatcher.GetDeclaredMethod("foregroundActivity");
                    foregroundActivity.Accessible = true;
                    JObject activity = foregroundActivity.Invoke(activityWatcher);
                    rvalue = (Activity)activity;
                }
                catch (Exception)
                {
                    // Not expected to happen
                }
                return rvalue;
            }
        }

        /// <summary>
        /// Occurs when a session is requested.
        /// </summary>
        public event EventHandler<ISession>? SessionDidRequest;

        internal bool RaiseSessionDidRequest(Session session)
        {
            EventHandler<ISession>? sessionDidRequest = SessionDidRequest;
            if (sessionDidRequest != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidRequest(this, s);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when an agent requests remote control.
        /// </summary>
        public event EventHandler<ISession>? RemoteControlRequest;

        internal bool RaiseRemoteControlRequest(Session session)
        {
            EventHandler<ISession>? remoteControlRequest = RemoteControlRequest;
            if (remoteControlRequest != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                remoteControlRequest(this, s);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session is first made available to the device,
        /// whether by creating a 6 digit code, or via a connect request from an agent.
        /// </summary>
        public event EventHandler<ISession>? SessionDidLoad;

        internal bool RaiseSessionDidLoad(Session session)
        {
            EventHandler<ISession>? sessionDidLoad = SessionDidLoad;
            if (sessionDidLoad != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidLoad(this, s);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session is updated.
        /// </summary>
        public event EventHandler<ISession>? SessionDidUpdate;

        internal bool RaiseSessionDidUpdate(Session session)
        {
            EventHandler<ISession>? sessionDidUpdate = SessionDidUpdate;
            if (sessionDidUpdate != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidUpdate(this, s);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Occurs when a session ends.
        /// </summary>
        public event EventHandler<ISession>? SessionDidEnd;

        internal bool RaiseSessionDidEnd(Session session)
        {
            EventHandler<ISession>? sessionDidEnd = SessionDidEnd;
            if (sessionDidEnd != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidEnd(this, s);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the current session instance or null if it doesn't exist.
        /// </summary>
        public ISession? CurrentSession
            => CobrowseSessionImplementation.TryCreate(NativeCobrowseIO.Instance.CurrentSession);

        /// <summary>
        /// Creates a new Cobrowse.io session.
        /// </summary>
        public void CreateSession(CobrowseCallback? callback)
        {
            NativeCobrowseIO.Instance.CreateSession((JError e, Session session) =>
            {
                callback?.Invoke(e, CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Gets or sets the API string.
        /// </summary>
        public string Api
        {
            get => NativeCobrowseIO.Instance.Api;
            set => NativeCobrowseIO.Instance.Api = value;
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => NativeCobrowseIO.Instance.DeviceId;

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        public string License
        {
            get => NativeCobrowseIO.Instance.License;
            set => NativeCobrowseIO.Instance.License = value;
        }

        /// <summary>
        /// Sets the license.
        /// </summary>
        [Obsolete("Use License property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetLicense(string licenseKey)
        {
            NativeCobrowseIO.Instance.SetLicense(licenseKey);
        }

        /// <summary>
        /// Starts the Cobrowse.io.
        /// </summary>
        public void Start()
        {
            NativeCobrowseIO.Instance.SetDelegate(new CobrowseDelegateImplementation());
            NativeCobrowseIO.Instance.Start();
        }

        /// <summary>
        /// Stops the Cobrowse.io.
        /// </summary>
        public void Stop()
        {
            NativeCobrowseIO.Instance.Stop();
        }

        /// <summary>
        /// Gets or sets Cobrowse.io custom data. 
        /// </summary>
        public IReadOnlyDictionary<string, object> CustomData
        {
            get => NativeCobrowseIO.Instance.CustomData;
            set => NativeCobrowseIO.Instance.CustomData = value;
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        [Obsolete("Use CustomData property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetCustomData(IDictionary<string, object> customData)
        {
            NativeCobrowseIO.Instance.SetCustomData(customData);
        }

        /// <summary>
        /// Gets or sets the available capabilities for a session. Different
        /// annotation tools and events will be available during a session
        /// depending on the capabilities you set here. By default all
        /// capabilities supported by the device are enabled.
        /// </summary>
        public string[] Capabilities
        {
            get => NativeCobrowseIO.Instance.Capabilities;
            set => NativeCobrowseIO.Instance.Capabilities = value;
        }

        /// <summary>
        /// Gets or sets the CSS selectors which will be used to redact content within WebViews.
        /// Any HTML element matching one of the selectors configured here will be redacted and
        /// not visible to your agents.
        /// Defaults to an empty list which means the feature is disabled.
        /// </summary>
        public string[] WebViewRedactedViews
        {
            get => NativeCobrowseIO.Instance.WebViewRedactedViews;
            set => NativeCobrowseIO.Instance.WebViewRedactedViews = value;
        }

        /// <inheritdoc/>
        public bool Registration
        {
            get => NativeCobrowseIO.Instance.Registration;
            set => NativeCobrowseIO.Instance.Registration = value;
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
        [Obsolete("Use 'CobrowseAccessibilityService' directly in the Android project")]
        public bool CheckCobrowseFullDevice()
        {
            bool isRunning = CobrowseAccessibilityService.IsRunning(Activity);
            if (!isRunning)
            {
                CobrowseAccessibilityService.ShowSetup(Activity);
                return false;
            }
            return true;
        }
    }

}
