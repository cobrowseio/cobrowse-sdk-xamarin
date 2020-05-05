using System;
using System.Diagnostics;
using Xamarin.CobrowseIO;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public partial class CobrowseCustomPage : ContentPage
    {
        private ICobrowseSession _session;
        private bool _loadingSession;

        private Random _random;
        private bool _animationTimerActive;

        public CobrowseCustomPage()
        {
            InitializeComponent();

            _random = new Random();

            viewCodeDiplayWorking.IsVisible = false;

            if (!_animationTimerActive)
            {
                _animationTimerActive = true;
                Device.StartTimer(TimeSpan.FromSeconds(0.08d), () =>
                {
                    if (_animationTimerActive)
                    {
                        RenderRandomCode();
                    }
                    return _animationTimerActive;
                });
            }

            InitSession();
            Render();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CrossCobrowseIO.Current.SessionDidUpdate += CobrowseAdapter_SessionDidUpdate;
            CrossCobrowseIO.Current.SessionDidEnd += CobrowseAdapter_SessionDidEnd;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossCobrowseIO.Current.SessionDidUpdate -= CobrowseAdapter_SessionDidUpdate;
            CrossCobrowseIO.Current.SessionDidEnd -= CobrowseAdapter_SessionDidEnd;

            _animationTimerActive = false;
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
                if (CrossCobrowseIO.Current.CurrentSession?.IsActive == true)
                {
                    _session = CrossCobrowseIO.Current.CurrentSession;
                    //[session registerSessionObserver:self];
                }
                else
                {
                    // otherwise create a new session
                    CreateSession();
                }
            }
        }

        private void CreateSession()
        {
            CrossCobrowseIO.Current.CreateSession((Exception err, ICobrowseSession session) =>
            {
                if (err != null)
                {
                    RenderError(err);
                }
                else
                {
                    _session = session;
                    Render();
                }
            });
        }

        private void ShowSubview(View view)
        {
            foreach (var next in viewContainer.Children)
            {
                next.IsVisible = next == view;
            }
        }

        private void Render()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (_session?.IsActive == true)
                {
                    ShowSubview(viewManageSession);
                }
                else
                {
                    ShowSubview(viewCodeDisplay);
                    SetCodeDisplay(_session?.Code);
                }
            });
        }

        private void RenderError(Exception e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ShowSubview(viewError);
                Debug.WriteLine("CobrowseIO Error: {0}", e);
            });
        }

        private void EndSession(object sender, EventArgs args)
        {
            this.Navigation.PopAsync();
            _session.End((Exception e, ICobrowseSession session) =>
            {
                if (e != null)
                {
                    RenderError(e);
                }
            });
        }

        private void CobrowseAdapter_SessionDidUpdate(object sender, ICobrowseSession e)
        {
            Render();
        }

        private void CobrowseAdapter_SessionDidEnd(object sender, ICobrowseSession e)
        {
            InitSession();
            Render();
        }

        public void SetCodeDisplay(string code)
        {
            if (code == null)
            {
                return;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                _animationTimerActive = false;
                this.viewCodeDiplayWorking.IsVisible = true;
                this.viewCodeLabel.Opacity = 1f;
                RenderCode(code);
            });
        }

        private void RenderRandomCode()
        {
            string chars = "1234567890";
            string code = "";
            for (int i = 0; i < 6; i++)
            {
                code += chars[_random.Next() % chars.Length];
            }
            RenderCode(code, 0.1f);
        }

        private void RenderCode(string code, double desiredOpacity = 1d)
        {
            string first3 = code.Substring(0, 3);
            string last3 = code.Substring(3);
            this.viewCodeLabel.Opacity = desiredOpacity;
            this.viewCodeLabel.Text = $"{first3}-{last3}";
        }
    }
}
