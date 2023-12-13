using Xamarin.CobrowseIO;

namespace MauiSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        CobrowseIO.Instance.OpenCobrowseUI();
    }
}


