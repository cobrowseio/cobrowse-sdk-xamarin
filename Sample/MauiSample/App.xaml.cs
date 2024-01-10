using System.Diagnostics;
using Cobrowse.IO;

namespace MauiSample;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());

        CobrowseIO.Instance.License = "trial";
        CobrowseIO.Instance.Start();
        CobrowseIO.Instance.CustomData = new Dictionary<string, object>
        {
            { CobrowseIO.UserIdKey, "<set your optional user id>" },
            { CobrowseIO.UserNameKey, "<set your optional user name>" },
            { CobrowseIO.UserEmailKey, "<set your optional user email>" },
            { CobrowseIO.DeviceIdKey, "<set your optional device id>" },
            { CobrowseIO.DeviceNameKey, "<set your optional device name>" },
            { "<set your optional custom field>", 3.14f }
        };

        // Optional. Needed for the redaction feature.
#if ANDROID
        Cobrowse.IO.Android.CobrowseIO.Instance.SetDelegate(new MauiSample.Platforms.Android.CobrowseRedactionDelegate());
#elif IOS
        Cobrowse.IO.iOS.CobrowseIO.Instance.SetDelegate(new MauiSample.Platforms.iOS.CobrowseRedactionDelegate());
#endif
    }

    protected override void OnStart()
    {
        CobrowseIO.Instance.SessionDidLoad += OnCobrowseSessionDidLoad;
        CobrowseIO.Instance.SessionDidUpdate += OnCobrowseSessionDidUpdate;
        CobrowseIO.Instance.SessionDidEnd += OnCobrowseSessionDidEnd;

        CobrowseIO.Instance.SessionDidRequest += OnCobrowseSessionDidRequestAsync;

        CobrowseIO.Instance.RemoteControlRequest += OnRemoteControlRequestAsync;
    }

    private Page RequireMainPage()
        => MainPage ?? throw new InvalidOperationException("MainPage is not set!");

    private void OnCobrowseSessionDidLoad(object? sender, ISession session)
    {
        Debug.WriteLine("Session loaded");
    }

    private void OnCobrowseSessionDidUpdate(object? sender, ISession session)
    {
        Debug.WriteLine("Session updated");
    }

    private void OnCobrowseSessionDidEnd(object? sender, ISession session)
    {
        Debug.WriteLine("Session ended");
    }

    private async void OnCobrowseSessionDidRequestAsync(object? sender, ISession session)
    {
        Debug.WriteLine("RemoteControl: " + session.RemoteControl);

        bool allowed = await RequireMainPage().DisplayAlert(
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

    private async void OnRemoteControlRequestAsync(object? sender, ISession session)
    {
        Debug.WriteLine("RemoteControl: " + session.RemoteControl);

        bool allowed = await RequireMainPage().DisplayAlert(
            title: "Cobrowse.io",
            message: "Allow remote control?",
            accept: "Allow",
            cancel: "Reject");
        if (allowed)
        {
            session.SetRemoteControl(RemoteControlState.On, (e, s) =>
            {
                Debug.WriteLine("RemoteControl: " + session.RemoteControl);
            });
        }
        else
        {
            session.SetRemoteControl(RemoteControlState.Rejected, (e, s) =>
            {
                Debug.WriteLine("RemoteControl: " + session.RemoteControl);
            });
        }
    }
}