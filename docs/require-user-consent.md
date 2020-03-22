# Requiring user consent (optional)

Please see full documentation at [https://cobrowse.io/docs](https://cobrowse.io/docs).

Try our **online demo** at the bottom of our homepage at <https://cobrowse.io/#tryit>.

## Implementation

You may want to ask the user for permission to view their screen before starting a session. You can use the following SDK hook to handle a remote session request.

### Xamarin.iOS implementation

```cs
using Xamarin.CobrowseIO;

[Register("AppDelegate")]
public class AppDelegate : UIResponder, IUIApplicationDelegate
{
    [Export("application:didFinishLaunchingWithOptions:")]
    public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // ... the rest of your app setup
        CobrowseIO.Instance().Delegate = new CustomCobrowseDelegate();
        return true;
    }
}

public class CustomCobrowseDelegate : CobrowseIODelegate
{
    public override void CobrowseHandleSessionRequest(CBIOSession session)
    {
        // show your own UI here
        // call Activate(<callback>) to accept and start the session
        // provide a callback to handle any errors during session initiation
        session.Activate(callback: null);
    }
    
    // ...
}
```

## Xamarin.Android implementation

```cs
using Xamarin.CobrowseIO;

[Application]
public class MainApplication : Application
{
    public MainApplication()
    {
    }

    protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
        // and the rest of cobrowse setup ...
    }
}

public class CustomCobrowseDelegate : Java.Lang.Object, CobrowseIO.ISessionRequestDelegate
{
    public CustomCobrowseDelegate()
    {
    }

    public CustomCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
        : base(handle, transfer)
    {
    }

    public void HandleSessionRequest(Activity activity, Session session)
    {
        // Do something here, e.g. showing a permission request dialog
        // Make sure to call Activate(<callback>) on the session object if
        // you want to start the session.
        // Provide a callback if you wish to handle errors during session
        // initiation.
        session.Activate(null);
    }

    // ...
}
```

## Questions?
Any questions at all? Please email us directly at [hello@cobrowse.io](mailto:hello@cobrowse.io).