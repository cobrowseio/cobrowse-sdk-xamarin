using System;
using System.ComponentModel;
using Xamarin.CobrowseIO;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CobrowseButton_Clicked(object sender, EventArgs e)
        {
            CrossCobrowseIO.Current.StartCobrowse();
        }

        private void CobrowseCustomUiButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CobrowseCustomPage());
        }

        private void CheckCobrowseFullDevice_Clicked(object sender, EventArgs e)
        {
            CrossCobrowseIO.Current.CheckCobrowseFullDevice();
        }

        private void PageRedactedViews_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new LoginPage());
        }

        private void CobrowseUserId_Clicked(object sender, EventArgs e)
        {
            string userId = CrossCobrowseIO.Current.DeviceId;
            this.DisplayAlert(
                title: "Cobrowse.io",
                message: $"Cobrowse.io DeviceId: {userId}",
                cancel: "OK");
        }
    }
}
