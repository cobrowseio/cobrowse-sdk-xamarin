using System;
using System.Collections.Generic;
using System.Diagnostics;
using Foundation;
using SampleApp.Forms.iOS;
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XView = Xamarin.Forms.View;

[assembly: Dependency(typeof(CobrowseAdapter))]
namespace SampleApp.Forms.iOS
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io session.
    /// </summary>
    public class CobrowseSession : ICobrowseSession
    {
        private Session _platformSession;

        public CobrowseSession(Session session)
        {
            _platformSession = session ?? throw new ArgumentNullException(nameof(session));
        }

        public static CobrowseSession TryCreate(Session session)
        {
            return session != null
                ? new CobrowseSession(session)
                : null;
        }

        /// <summary>
        /// Gets the session's code.
        /// </summary>
        public string Code => _platformSession.Code;

        /// <summary>
        /// Gets a value indicating if the session running, frames are streaming to the agent.
        /// </summary>
        public bool IsActive => _platformSession.IsActive;

        /// <summary>
        /// Gets a value indicating if waiting for the user to confirm the session.
        /// </summary>
        public bool IsAuthorizing => _platformSession.IsAuthorizing;

        /// <summary>
        /// Gets a value indicating if the ession is over and can no longer be used or edited.
        /// </summary>
        public bool IsEnded => _platformSession.IsEnded;

        /// <summary>
        /// Gets a value indicating if the session has been created but is waiting for agent or user.
        /// </summary>
        public bool IsPending => _platformSession.IsPending;

        /// <summary>
        /// Ends the session.
        /// </summary>
        public void End(CobrowseCallback callback)
        {
            _platformSession.End((NSError e, Session session) =>
            {
                callback.Invoke(e?.AsException(), CobrowseSession.TryCreate(session));
            });
        }
    }

    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io SDK.
    /// </summary>
    public class CobrowseAdapter : ICobrowseAdapter
    {
        private readonly CustomOverlayCobrowseDelegate _overlayDelegate;

        public CobrowseAdapter()
        {
            _overlayDelegate = new CustomOverlayCobrowseDelegate();
        }

        /// <summary>
        /// Occurs when a session is updated.
        /// </summary>
        public event EventHandler<ICobrowseSession> SessionDidUpdate
        {
            add => _overlayDelegate.SessionDidUpdate += value;
            remove => _overlayDelegate.SessionDidUpdate -= value;
        }

        /// <summary>
        /// Occurs when a session ends.
        /// </summary>
        public event EventHandler<ICobrowseSession> SessionDidEnd
        {
            add => _overlayDelegate.SessionDidEnd += value;
            remove => _overlayDelegate.SessionDidEnd -= value;
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
                callback.Invoke(e?.AsException(), CobrowseSession.TryCreate(session));
            });
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance().DeviceId;

        /// <summary>
        /// Initializes the Cobrowse.io SDK.
        /// </summary>
        public void Initialize(string licenseKey)
        {
            CobrowseIO.Instance().SetLicense(licenseKey);
            CobrowseIO.Instance().SetDelegate(_overlayDelegate);
            CobrowseIO.Instance().Start();
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
        public void StartCobrowse()
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var nc = vc.GetUINavigationController();
            nc.PushViewController(
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

        /// <summary>
        /// Ends the current session (if active).
        /// </summary>
        public void EndCurrentSession()
        {
            CobrowseIO.Instance().CurrentSession?.End(null);
        }

        /// <summary>
        /// Sets the custom overlay view.
        /// </summary>
        public void SetCustomOverlayView(Func<View> viewFunc)
        {
            _overlayDelegate.IndicatorFunction = viewFunc;
        }
    }

    public class CustomOverlayCobrowseDelegate : CobrowseIODelegate
    {
        public event EventHandler<ICobrowseSession> SessionDidUpdate;

        public event EventHandler<ICobrowseSession> SessionDidEnd;

        public CustomOverlayCobrowseDelegate()
        {
        }

        public CustomOverlayCobrowseDelegate(System.IntPtr handle) : base(handle)
        {
        }

        public Func<XView> IndicatorFunction { get; set; }

        private UIView _indicatorInstance;

        public override void CobrowseShowSessionControls(Session session)
        {
            if (_indicatorInstance == null)
            {
                _indicatorInstance = GetDefaultSessionIndicator(container: UIApplication.SharedApplication.KeyWindow);
            }
            if (_indicatorInstance != null)
            {
                _indicatorInstance.Hidden = false;
            }
        }

        public override void CobrowseHideSessionControls(Session session)
        {
            if (_indicatorInstance != null)
            {
                _indicatorInstance.Hidden = true;
            }
        }

        private UIView GetDefaultSessionIndicator(UIView container)
        {
            var indicator = IndicatorFunction();
            if (indicator == null)
            {
                return null;
            }
            var renderer = Platform.CreateRenderer(indicator);
            renderer.Element.Layout(new Xamarin.Forms.Rectangle(0, 0, indicator.WidthRequest, indicator.HeightRequest));
            var nativeIndicator = renderer.NativeView;
            nativeIndicator.TranslatesAutoresizingMaskIntoConstraints = false;

            container.AddSubview(nativeIndicator);

            nativeIndicator.WidthAnchor.ConstraintEqualTo((float)indicator.WidthRequest).Active = true;
            nativeIndicator.HeightAnchor.ConstraintEqualTo((float)indicator.HeightRequest).Active = true;
            nativeIndicator.CenterYAnchor.ConstraintEqualTo(container.CenterYAnchor).Active = true;
            nativeIndicator.RightAnchor.ConstraintEqualTo(container.RightAnchor, constant: -20f).Active = true;

            return nativeIndicator;
        }

        public override void CobrowseSessionDidUpdate(Session session)
        {
            Debug.WriteLine("CobrowseSessionDidUpdate");
            SessionDidUpdate?.Invoke(this, CobrowseSession.TryCreate(session));
        }

        public override void CobrowseSessionDidEnd(Session session)
        {
            Debug.WriteLine("CobrowseSessionDidEnd");
            SessionDidEnd?.Invoke(this, CobrowseSession.TryCreate(session));
        }
    }
}
