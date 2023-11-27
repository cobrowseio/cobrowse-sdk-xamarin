using System.Diagnostics;
using Xamarin.CobrowseIO;

namespace MauiSample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        CobrowseIO.Instance.License = "e-73R7b1nIeoFQ";
        CobrowseIO.Instance.Api = "https://staging.cobrowse.io";
        CobrowseIO.Instance.Start();
        CobrowseIO.Instance.CustomData = new Dictionary<string, object>
        {
            { CobrowseIO.UserIdKey, "<your_user_id>" },
            { CobrowseIO.UserNameKey, "<your_user_name>" },
            { CobrowseIO.UserEmailKey, "<your_user_email>" },
            { CobrowseIO.DeviceIdKey, "<your_device_id>" },
            { CobrowseIO.DeviceNameKey, "<your_device_name>" },
            { "custom_field", 5.75f }
        };
    }

    protected override void OnStart()
    {
        CobrowseIO.Instance.SessionDidLoad += OnCobrowseSessionDidLoad;
        CobrowseIO.Instance.SessionDidUpdate += OnCobrowseSessionDidUpdate;
        CobrowseIO.Instance.SessionDidEnd += OnCobrowseSessionDidEnd;

        CobrowseIO.Instance.SessionDidRequest += OnCobrowseSessionDidRequest;

        CobrowseIO.Instance.RemoteControlRequest += OnRemoteControlRequest;
    }

    private void OnCobrowseSessionDidLoad(object sender, ISession session)
    {
        Debug.WriteLine("Session loaded");
    }

    private void OnCobrowseSessionDidUpdate(object sender, ISession session)
    {
        Debug.WriteLine("Session updated");
    }

    private void OnCobrowseSessionDidEnd(object sender, ISession session)
    {
        Debug.WriteLine("Session ended");
    }

    private async void OnCobrowseSessionDidRequest(object sender, ISession session)
    {
        Debug.WriteLine("RemoteControl: " + session.RemoteControl);

        bool allowed = await this.MainPage.DisplayAlert(
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

    private async void OnRemoteControlRequest(object sender, ISession session)
    {
        Debug.WriteLine("RemoteControl: " + session.RemoteControl);

        bool allowed = await this.MainPage.DisplayAlert(
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