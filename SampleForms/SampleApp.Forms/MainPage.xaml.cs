using System;
using System.ComponentModel;
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
            DependencyService.Get<ICobrowseAdapter>().StartCobrowse();
        }

        private void CheckCobrowseFullDevice_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ICobrowseAdapter>().CheckCobrowseFullDevice();
        }
    }
}
