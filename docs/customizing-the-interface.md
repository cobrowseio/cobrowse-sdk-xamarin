# Customizing the interface (optional)

Please see full documentation at [https://cobrowse.io/docs](https://cobrowse.io/docs).

Try our **online demo** at the bottom of our homepage at <https://cobrowse.io/#tryit>.

## Implementation

You can fully customize the interface for a Cobrowse session. The SDK provides hooks via `CobrowseIODelegate` for you to render your own interface:

### Xamarin.iOS implementation

```cs
using Xamarin.CobrowseIO;

[Register("AppDelegate")]
public class AppDelegate : UIResponder, IUIApplicationDelegate
{
    [Export("application:didFinishLaunchingWithOptions:")]
    public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
        // ... the rest of your app setup
        return true;
    }
}

public class CustomCobrowseDelegate : CobrowseIODelegate
{
    // Sample end session UIView, constructor, and tap gesture recognizer implementation
    private UIView _indicatorInstance;

    public override void CobrowseShowSessionControls(Session session)
    {
        // You can render controls however you like here.
        // One option is to add our sample end session UI defined below.
        if (_indicatorInstance == null)
        {
            _indicatorInstance = GetDefaultSessionIndicator(container: UIApplication.SharedApplication.KeyWindow);
        }
        _indicatorInstance.Hidden = false;
    }

    public override void CobrowseHideSessionControls(Session session)
    {
        if (_indicatorInstance != null)
            _indicatorInstance.Hidden = true;
    }

    private UIView GetDefaultSessionIndicator(UIView container)
    {
        var indicator = new UILabel();
        indicator.BackgroundColor = new UIColor(red: 1.0f, green: 0.0f, blue: 0.0f, alpha: 0.7f);
        indicator.Text = "End Session";
        indicator.UserInteractionEnabled = true;
        indicator.TextAlignment = UITextAlignment.Center;
        indicator.Font.WithSize(UIFont.SmallSystemFontSize);
        indicator.TextColor = UIColor.White;
        indicator.Layer.CornerRadius = 10;
        indicator.ClipsToBounds = true;
        indicator.TranslatesAutoresizingMaskIntoConstraints = false;
        container.AddSubview(indicator);

        indicator.WidthAnchor.ConstraintEqualTo(200f).Active = true;
        indicator.HeightAnchor.ConstraintEqualTo(40f).Active = true;
        indicator.CenterXAnchor.ConstraintEqualTo(container.CenterXAnchor).Active = true;
        indicator.BottomAnchor.ConstraintEqualTo(container.BottomAnchor, constant: -20f).Active = true;

        var tapRecognizer = new UITapGestureRecognizer(() =>
        {
            CobrowseIO.Instance().CurrentSession?.End(null);
        });
        tapRecognizer.NumberOfTapsRequired = 1;
        indicator.AddGestureRecognizer(tapRecognizer);
        return indicator;
    }

    public override void SessionDidUpdate(Session session)
    {
    }

    public override void SessionDidEnd(Session session)
    {
    }
}
```

### Xamarin.Android implementation

You can fully customize the interface for a Cobrowse session. The SDK provides hooks via `CobrowseIO.ISessionControlsDelegate` for you to render your own interface:

```cs
using Xamarin.CobrowseIO;

[Application]
public class MainApplication : Application, CobrowseIO.ISessionControlsDelegate
{
    public override void OnCreate()
    {
        base.OnCreate();
        CobrowseIO.Instance().SetDelegate(this);
        // and the rest of cobrowse setup ...
    }

    public void ShowSessionControls(Activity activity, Session session)
    {
        // optionally show your own controls here
    }

    public void HideSessionControls(Activity activity, Session session)
    {
        // hide controls created by the method above here
    }

    //...
}
```

### Xamarin.Forms implementation

Even though Cobrowse.io works with native views, there is nothing that would prevent you from using `Xamarin.Forms.VisualElement` as a session indicator.

First, create an indicator view using Xamarin.Forms (`CobrowseCustomView.xaml`):

```xml
<?xml version="1.0" encoding="UTF-8"?>
<ContentView
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    x:Class="SampleApp.Forms.CobrowseCustomView"
    HeightRequest="42"
    WidthRequest="130">
    <Button
        BackgroundColor="Red"
        TextColor="White"
        Text="End Session"
        CornerRadius="4"
        Clicked="EndSessionButton_Clicked" />
</ContentView>
```

Then, in the **iOS project**:

```cs
using System;
using Foundation;
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms.Platform.iOS;

[Register("AppDelegate")]
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
        // and the rest of cobrowse setup ...
    }
}

public class CustomCobrowseDelegate : CobrowseIODelegate
{
    private UIView _indicatorInstance;

    public override void CobrowseShowSessionControls(Session session)
    {
        if (_indicatorInstance == null)
        {
            _indicatorInstance = GetDefaultSessionIndicator(container: UIApplication.SharedApplication.KeyWindow);
        }
        _indicatorInstance.Hidden = false;
    }

    public override void CobrowseHideSessionControls(Session session)
    {
        if (_indicatorInstance != null)
            _indicatorInstance.Hidden = true;
    }

    private UIView GetDefaultSessionIndicator(UIView container)
    {
        var indicator = new CobrowseCustomView();
        var renderer = Platform.CreateRenderer(indicator);
        renderer.Element.Layout(new Xamarin.Forms.Rectangle(0, 0, indicator.WidthRequest, indicator.HeightRequest));
        var nativeIndicator = renderer.NativeView;
        nativeIndicator.TranslatesAutoresizingMaskIntoConstraints = false;

        container.AddSubview(nativeIndicator);

        nativeIndicator.WidthAnchor.ConstraintEqualTo((float)indicator.WidthRequest).Active = true;
        nativeIndicator.HeightAnchor.ConstraintEqualTo((float)indicator.HeightRequest).Active = true;
        nativeIndicator.CenterYAnchor.ConstraintEqualTo(container.CenterYAnchor).Active = true;
        nativeIndicator.RightAnchor.ConstraintEqualTo(container.RightAnchor, constant: -20f).Active = true;

        return nativeIndicator;
    }
    
    // ...
}
```

And in the **Android project**:

```cs
using Xamarin.CobrowseIO;
using Xamarin.Forms.Platform.Android;

[Application]
public class MainApplication : Application
{
    public override void OnCreate()
    {
        CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
        // and the rest of cobrowse setup ...
    }
}

public class CustomCobrowseDelegate : Java.Lang.Object, CobrowseIO.ISessionControlsDelegate
{
    private View _overlayIndicator;

    public void ShowSessionControls(Activity activity, Session session)
    {
        if (_overlayIndicator != null)
        {
            return;
        }
        if (!(activity is FormsAppCompatActivity))
        {
            return;
        }
        var indicator = new CobrowseCustomView();
        var renderer = Platform.CreateRendererWithContext(indicator, activity);
        renderer.Element.Layout(new Xamarin.Forms.Rectangle(0, 0, indicator.WidthRequest, indicator.HeightRequest));
        var nativeIndicator = renderer.View;

        var modal = new RelativeLayout(activity);
        var layoutParams = new RelativeLayout.LayoutParams(
            (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)indicator.WidthRequest, activity.Resources.DisplayMetrics),
            (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)indicator.HeightRequest, activity.Resources.DisplayMetrics))
        {
            MarginEnd = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 4f, activity.Resources.DisplayMetrics)
        };
        layoutParams.AddRule(LayoutRules.CenterVertical);
        layoutParams.AddRule(LayoutRules.AlignParentEnd);
        modal.AddView(nativeIndicator, layoutParams);

        var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
        rootFrameLayout.AddView(modal, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        rootFrameLayout.Invalidate();

        _overlayIndicator = modal;
    }

    public void HideSessionControls(Activity activity, Session session)
    {
        if (_overlayIndicator == null)
        {
            return;
        }
        if (!(activity is FormsAppCompatActivity))
        {
            return;
        }
        var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
        rootFrameLayout.RemoveView(_overlayIndicator);
        _overlayIndicator = null;
    }
    
    // ...
}
```

## Customizing the 6 Digit Code screen

You can build your own UI to completely replace the default UI we provide for generating 6 digit codes. You can generate a code for your UI by using the `CreateSession` API:

#### Xamarin.iOS:

```cs
CobrowseIO.Instance().CreateSession((NSError error, Session session) =>
{
    if (error != null)
    {
        Debug.WriteLine("Error creating code: {0}", error);
    }
    else
    {
        Debug.WriteLine("Created session code: {0}", session.Code);
    }
});
```

#### Xamarin.Android:

```cs
CobrowseIO.Instance().CreateSession((Java.Lang.Error error, Session session) =>
{
    if (error != null)
    {
        Debug.WriteLine("Error creating code: {0}", error);
    }
    else
    {
        Debug.WriteLine("Created session code: {0}", session.Code);
    }
});
```

**Note:** the codes expire shortly after creation (codes last less than 10 minutes), so wait until your user is ready to share the code with an agent before generating it.

You can monitor changes in the state of the session you create using the CobrowseIO delegate methods:

```cs
public void SessionDidUpdate (Session session);
public void SessionDidEnd (Session session);
```

You can get information about the state of the session using the following properties, which may adjust the UI you are showing:

| Property | Description |
| --- | --- |
| Session.IsPending | Session has been created but is waiting for agent or user |
| Session.IsAuthorizing | Waiting for the user to confirm the session |
| Session.IsActive | Session running, frames are streaming to the agent |
| Session.IsEnded | Session is over and can no longer be used or edited|

## Questions?
Any questions at all? Please email us directly at [hello@cobrowse.io](mailto:hello@cobrowse.io).
