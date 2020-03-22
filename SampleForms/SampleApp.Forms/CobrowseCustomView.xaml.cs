using System;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public partial class CobrowseCustomView : ContentView
    {
        public CobrowseCustomView()
        {
            InitializeComponent();
        }

        void EndSessionButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ICobrowseAdapter>().EndCurrentSession();
        }
    }
}
