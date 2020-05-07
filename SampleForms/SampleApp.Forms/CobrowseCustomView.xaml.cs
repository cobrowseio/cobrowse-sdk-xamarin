using System;
using Xamarin.CobrowseIO.Abstractions;
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
            CobrowseIO.Instance.CurrentSession?.End(null);
        }
    }
}
