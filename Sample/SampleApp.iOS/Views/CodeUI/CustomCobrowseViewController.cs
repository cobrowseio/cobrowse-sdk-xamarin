using System;
using System.Diagnostics;
using CoreFoundation;
using Foundation;
using UIKit;
using Xamarin.CobrowseIO;

namespace SampleApp.iOS
{
    public partial class CustomCobrowseViewController : UIViewController
    {
        private UIView _activeView;
        private Session _session;
        private bool _loadingSession;
        private CodeDisplayViewController _codeDisplay;
        private ManageSessionViewController _manageSession;
        private ErrorDisplayViewController _errorDisplay;

        public CustomCobrowseViewController() : base("CustomCobrowseViewController", null)
        {
        }

        private void SetupSubviews()
        {
            // this viewcontroller doesn't really do any rendering of views
            // it delegates that to child view controllers to do
            _codeDisplay = new CodeDisplayViewController();
            _manageSession = new ManageSessionViewController();
            _errorDisplay = new ErrorDisplayViewController();

            // hack: force view heirarchys to load
            _ = _codeDisplay.View;
            _ = _manageSession.View;
            _ = _errorDisplay.View;

            // hook up events
            _manageSession.end.TouchUpInside += (object sender, EventArgs e) => { EndSession(); };

            Render();
        }

        private void InitSession()
        {
            // if we have specific session that we're loading
            // then dont try to bootstrap a session object
            // from anywhere else, just wait for it to finish
            // loading.
            if (!_loadingSession)
            {
                // if the current session looks like it's still active
                // then we'll use that one
                if (CobrowseIO.Instance().CurrentSession?.IsActive == true)
                {
                    _session = CobrowseIO.Instance().CurrentSession;
                    //[session registerSessionObserver:self];
                }
                else
                {
                    // otherwise create a new session
                    CreateSession();
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.closeBtn.Hidden = !this.IsBeingPresented;
            InitSession();
            SetupSubviews();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            //[session removeSessionObserver:self];
        }

        private void CreateSession()
        {
            CobrowseIO.Instance().CreateSession((NSError err, Session session) =>
            {
                if (err != null)
                {
                    RenderError(err);
                }
                else
                {
                    _session = session;
                    //[self->session registerSessionObserver:self];
                    Render();
                }
            });
        }

        private void LoadSession(string codeOrId)
        {
            _loadingSession = true;
            CobrowseIO.Instance().GetSession(codeOrId, (NSError err, Session session) =>
            { 
                if (err != null)
                {
                    RenderError(err);
                }
                else
                {
                    _session = session;
                    //[self->session registerSessionObserver:self];
                    Render();
                }
            });
        }

        private void ShowSubview(UIView view)
        {
            if (_activeView == view)
            {
                return;
            }

            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                foreach (UIView next in this.subview.Subviews)
                {
                    next.RemoveFromSuperview();
                }
                view.Frame = this.subview.Bounds;
                view.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                this.subview.AddSubview(view);
                _activeView = view;
            });
        }

        private void Render()
        {
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                if (_session?.IsActive == true)
                {
                    ShowSubview(_manageSession.View);
                }
                else
                {
                    ShowSubview(_codeDisplay.View);
                    _codeDisplay.SetCode(_session?.Code);
                }
            });
        }

        private void RenderError(NSError err)
        {
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                this.View.AddSubview(_errorDisplay.View);
                this.View.BringSubviewToFront(this.closeBtn);
                this._errorDisplay.View.Frame = this.View.Bounds;
                this._errorDisplay.View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                Debug.WriteLine("CobrowseIO Error: {0}", err);
            });
        }

        partial void Close(UIKit.UIButton sender)
        {
            this.DismissViewController(animated: true, completionHandler: null);
        }

        private void EndSession()
        {
            if (!closeBtn.Hidden)
            {
                Close(null);
            }
            else
            {
                this.NavigationController.PopViewController(animated: true);
            }

            _session.End((NSError err, Session session) =>
            { 
                if (err != null)
                {
                    RenderError(err);
                }
            });
        }

        public void SessionDidUpdate(Session session)
        {
            Render();
        }

        public void SessionDidEnd(Session session)
        {
            InitSession();
            Render();
        }
    }
}

