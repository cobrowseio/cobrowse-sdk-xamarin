using Android.App;
using Android.OS;
using Android.Views;
using Java.Interop;
using Xamarin.CobrowseIO;
using Xamarin.CobrowseIO.UI;
using JError = Java.Lang.Error;

namespace SampleApp.Android
{
    [Activity(
        Label = "Login Activity",
        Theme = "@style/AppTheme.NoActionBar")]
    public class CustomCobrowseActivity : Activity
    {
        private readonly CodeDisplay _codeDisplay = new CodeDisplay();
        private readonly ManageSession _manageView = new ManageSession();
        private readonly ErrorView _errorView = new ErrorView();

        public CustomCobrowseActivity()
        {
        }

        protected void ShowFragment(Fragment fragment)
        {
            if (IsFinishing || IsDestroyed)
            {
                return;
            }
            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.cobrowse_fragment_container, fragment);
            transaction.Commit();
        }

        protected void CreateSession(CobrowseCallback callback)
        {
            CobrowseIO.Instance().CreateSession(new CobrowseCallback((err, session) =>
            {
                if (err != null)
                {
                    ShowError(err);
                }
                else
                {
                    Render(session);
                }
                if (callback != null) callback.Call(err, session);
            }));
        }

        protected void ShowError(JError e)
        {
            System.Diagnostics.Debug.WriteLine("Cobrowse error: " + e.Message);
            ShowFragment(_errorView);
        }

        protected void Render(Session session)
        {
            if (session == null || session.IsPending)
            {
                ShowFragment(_codeDisplay);
                if (session != null)
                {
                    _codeDisplay.SetCode(session.Code());
                }
            }
            else if (session.IsActive)
            {
                ShowFragment(_manageView);
            }
        }

        protected void ListenTo(Session session)
        {
            session.RegisterSessionListener(new CobrowseSessionListener(
                onDidUpdate: (session) =>
                {
                    Render(session);
                },
                onDidEnd: (session) =>
                {
                    Render(session);
                    Finish();
                }));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_cobrowse);
        }

        protected override void OnResume()
        {
            base.OnResume();

            Session current = CobrowseIO.Instance().CurrentSession();
            if (current != null)
            {
                ListenTo(current);
            }

            if (CobrowseIO.Instance().CurrentSession() == null || CobrowseIO.Instance().CurrentSession().IsEnded)
            {
                CreateSession(new CobrowseCallback((err, session) =>
                {
                    if (err != null)
                    {
                        ShowError(err);
                    }
                    if (session != null)
                    {
                        ListenTo(session);
                    }
                }));
            }

            Render(CobrowseIO.Instance().CurrentSession());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Session s = CobrowseIO.Instance().CurrentSession();
            if (s != null && s.IsPending)
            {
                s.End(null);
            }
        }

        [Export("endSessionClicked")]
        public void EndSessionClicked(View view)
        {
            Session session = CobrowseIO.Instance().CurrentSession();
            if (session != null)
            {
                session.End(new CobrowseCallback((JError e, Session s) =>
                {
                    if (e != null)
                    {
                        ShowError(e);
                    }
                    else
                    {
                        Finish();
                    }
                }));
            }
        }
    }
}