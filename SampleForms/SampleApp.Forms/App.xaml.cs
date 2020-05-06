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

            CrossCobrowseIO.Instance().SetLicense("trial");
            CrossCobrowseIO.Instance().Start();
            CrossCobrowseIO.Instance().SetCustomData(new Dictionary<string, object>
            {
                { CrossCobrowseIO.UserId, "<your_user_id>" },
                { CrossCobrowseIO.UserName, "<your_user_name>" },
                { CrossCobrowseIO.UserEmail, "<your_user_email>" },
                { CrossCobrowseIO.DeviceId, "<your_device_id>" },
                { CrossCobrowseIO.DeviceName, "<your_device_name>" },
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
            CrossCobrowseIO.Instance().SessionDidRequest += OnCobrowseSessionDidRequest;
        }

        private void Unsubscribe()
        {
            CrossCobrowseIO.Instance().SessionDidRequest -= OnCobrowseSessionDidRequest;
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
