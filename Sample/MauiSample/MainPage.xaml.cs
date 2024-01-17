using Cobrowse.IO;

namespace MauiSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Title = "Home";
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        CobrowseIO.Instance.OpenCobrowseUI();
    }
}


