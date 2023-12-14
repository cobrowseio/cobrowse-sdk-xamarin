using Foundation;
using UIKit;
using Cobrowse.IO.iOS;
using NativeRemoteControlState = Cobrowse.IO.iOS.RemoteControlState;
using NativeFullDeviceState = Cobrowse.IO.iOS.FullDeviceState;

namespace Cobrowse.IO
{
    /// <summary>
    /// Cobrowse.io delegate provides callbacks to the cross-platform wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseDelegateImplementation : CobrowseIODelegate
    {
        private CobrowseIOImplementation CrossImplementation
            => (CobrowseIOImplementation) CobrowseIO.Instance;

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
                var alert = UIAlertController.Create(
                    title: "SessionRequestConsentPromptTitle".GetLocalizedString("Support Request"),
                    message: "SessionRequestConsentPromptMessage".GetLocalizedString("A support agent would like to use this app with you. Do you wish to allow this?"),
                    preferredStyle: UIAlertControllerStyle.Alert);
                UIAlertAction allow = UIAlertAction.Create(
                    title: "SessionRequestConsentPromptAllow".GetLocalizedString("Allow"),
                    style: UIAlertActionStyle.Default,
                    handler: e =>
                    {
                        session.Activate(callback: null);
                    });
                UIAlertAction deny = UIAlertAction.Create(
                    title: "SessionRequestConsentPromptDeny".GetLocalizedString("Deny"),
                    style: UIAlertActionStyle.Cancel,
                    handler: e =>
                    {
                        session.End(callback: null);
                    });
                alert.AddAction(allow);
                alert.AddAction(deny);

                UIViewControllerExtensions
                    .GetVisibleViewController(null)
                    .PresentViewController(alert, animated: true, completionHandler: null);
            }
        }

        public override void HandleRemoteControlRequest(Session session)
        {
            if (!CrossImplementation.RaiseRemoteControlRequest(session))
            {
                var alert = UIAlertController.Create(
                    title: "RemoteControlConsentPromptTitle".GetLocalizedString("Remote Control Request"),
                    message: "RemoteControlConsentPromptMessage".GetLocalizedString("A support agent would like to remotely control this app. Do you wish to allow this?"),
                    preferredStyle: UIAlertControllerStyle.Alert);
                UIAlertAction allow = UIAlertAction.Create(
                    title: "RemoteControlConsentPromptAllow".GetLocalizedString("Allow"),
                    style: UIAlertActionStyle.Default,
                    handler: e =>
                    {
                        session.SetRemoteControl(NativeRemoteControlState.On, callback: null);
                    });
                UIAlertAction deny = UIAlertAction.Create(
                    title: "RemoteControlConsentPromptDeny".GetLocalizedString("Deny"),
                    style: UIAlertActionStyle.Cancel,
                    handler: e =>
                    {
                        session.SetRemoteControl(NativeRemoteControlState.Rejected, callback: null);
                    });
                alert.AddAction(allow);
                alert.AddAction(deny);

                UIViewControllerExtensions
                    .GetVisibleViewController(null)
                    .PresentViewController(alert, animated: true, completionHandler: null);
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
