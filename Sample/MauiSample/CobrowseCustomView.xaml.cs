using Cobrowse.IO;

namespace MauiSample;

public partial class CobrowseCustomView : ContentView
{
    public CobrowseCustomView()
    {
        InitializeComponent();
    }

    private void OnEndSessionClicked(object sender, EventArgs e)
    {
        CobrowseIO.Instance.CurrentSession?.End(null);
    }
}

