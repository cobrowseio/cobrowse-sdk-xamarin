using System.Collections.Generic;
using Xamarin.CobrowseIO;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossCobrowseIO.Current.Initialize("trial");
            CrossCobrowseIO.Current.SetCustomData(new Dictionary<string, object>
            {
                { CobrowseDataKeys.UserId, "<your_user_id>" },
                { CobrowseDataKeys.UserName, "<your_user_name>" },
                { CobrowseDataKeys.UserEmail, "<your_user_email>" },
                { CobrowseDataKeys.DeviceId, "<your_device_id>" },
                { CobrowseDataKeys.DeviceName, "<your_device_name>" },
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
            CrossCobrowseIO.Current.SessionDidRequest += OnCobrowseSessionDidRequest;
        }

        private void Unsubscribe()
        {
            CrossCobrowseIO.Current.SessionDidRequest -= OnCobrowseSessionDidRequest;
        }

        private async void OnCobrowseSessionDidRequest(object sender, ICobrowseSession session)
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
