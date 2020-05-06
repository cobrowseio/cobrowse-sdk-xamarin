using System;
using Xamarin.CobrowseIO;
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
            CrossCobrowseIO.Instance.CurrentSession?.End(null);
        }
    }
}
