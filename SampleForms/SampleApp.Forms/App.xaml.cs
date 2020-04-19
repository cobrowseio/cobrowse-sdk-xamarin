using System.Collections.Generic;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var cobrowse = DependencyService.Get<ICobrowseAdapter>();
            cobrowse.Initialize("trial");
            cobrowse.SetCustomData(new Dictionary<string, object>
            {
                { CobrowseDataKeys.UserId, "<your_user_id>" },
                { CobrowseDataKeys.UserName, "<your_user_name>" },
                { CobrowseDataKeys.UserEmail, "<your_user_email>" },
                { CobrowseDataKeys.DeviceId, "<your_device_id>" },
                { CobrowseDataKeys.DeviceName, "<your_device_name>" },
                { "custom_field", 5.75f }
            });
            cobrowse.SetCustomOverlayView(
                () => new CobrowseCustomView());

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
