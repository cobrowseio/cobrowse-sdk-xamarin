using System.Collections.Generic;
using Xamarin.CobrowseIO.Abstractions;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CobrowseIO.Instance.SetLicense("trial");
            CobrowseIO.Instance.Start();
            CobrowseIO.Instance.SetCustomData(new Dictionary<string, object>
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
                { "custom_field", 5.75f }
            });

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            Subscribe();
        }

        protected override void OnSleep()
        {
            Unsubscribe();
        }

        protected override void OnResume()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            CobrowseIO.Instance.SessionDidRequest += OnCobrowseSessionDidRequest;
        }

        private void Unsubscribe()
        {
            CobrowseIO.Instance.SessionDidRequest -= OnCobrowseSessionDidRequest;
        }

        private async void OnCobrowseSessionDidRequest(object sender, ISession session)
        {
            bool allowed = await this.MainPage.DisplayAlert(
                title: "Cobrowse.io",
                message: "Allow Cobrowse.io session?",
                accept: "Allow",
                cancel: "Reject");
            if (allowed)
            {
                session.Activate(null);
            }
            else
            {
                session.End(null);
            }
        }
    }
}
