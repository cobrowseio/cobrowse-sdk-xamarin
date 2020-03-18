# Redact sensitive data (optional)

Please see full documentation at [https://cobrowse.io/docs](https://cobrowse.io/docs).

Try our **online demo** at the bottom of our homepage at <https://cobrowse.io/#tryit>.

## Implementation

Redaction allows you to remove specific elements from the agents view. This allows you to keep private user data private. There are two ways to redact data in Cobrowse:

### 1. Define the redacted views in your app source code (recommended)

#### Xamarin.iOS implementation

This is the recommended method as it will make sure your redactions are tied to app version. Implement the `ICobrowseIORedacted` interface on any `UIViewController` that contains sensitive views. This interface contains one `RedactedViews` property:

```cs
public partial class ViewController : UIViewController, ICobrowseIORedacted
{
    // From this property you should return a list of the views you want Cobrowse to redact, for example:
    public UIView[] RedactedViews
        => new[] { redactedTextView };
}
```

If making changes to your `UIViewController` subclasses isn't an option, we also support a delegate style method to allow you to supply this information in one place. Find out more about this by emailing us at [hello@cobrowse.io](hello@cobrowse.io).

#### Xamarin.Android implementation

Implement the `CobrowseIO.IRedacted` interface on any `Activity` that contains sensitive views. This interface contains one method:

```cs
[Activity]
public class MainActivity : AppCompatActivity, CobrowseIO.IRedacted
{
    // From this method you should return a list of the views you want Cobrowse to redact, for example:
    public IList<View> RedactedViews()
    {
        return new[]
        {
            FindViewById(Resource.Id.redact_me)
        }
    }
}
```

If making changes to your Activity classes isn't an option, we also support a delegate style method to allow you to supply this information in one place. Find out more about this by emailing us at [hello@cobrowse.io](hello@cobrowse.io).

#### Xamarin.Forms implementation

While it is not possible to access platform-specific UI code directly from a cross-platform project, you can easily achive it using [Effects](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/effects/introduction) and [Custom Renderers](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/).

In your **cross-platform** project declare a new `Effect` for marking Xamarin.Forms UI elements as *redacted*:

```cs
using Xamarin.Forms;

namespace YourAppNamespace.Forms
{
    /// <summary>
    /// A Xamarin.Forms effect that helps to redact Xamarin.Forms view in Cobrowse.io.
    /// </summary>
    public class CobrowseRedactedViewEffect : RoutingEffect
    {
        public bool IsRedacted { get; set; } = true;

        public CobrowseRedactedViewEffect()
            : base("YourAppName" + "." + nameof(CobrowseRedactedViewEffect))
        {
        }
    }
}
```

**iOS-specific** platform Effect would look like this:

```cs
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("YourAppName")]
[assembly: ExportEffect(typeof(YourAppNamespace.iOS.PlatformCobrowseRedactedViewEffect), "CobrowseRedactedViewEffect")]
namespace YourAppNamespace.iOS
{
    public class PlatformCobrowseRedactedViewEffect : PlatformEffect
    {
        private static readonly List<UIView> sRedacted = new List<UIView>();

        public static UIView[] RedactedViews => sRedacted.ToArray();

        public PlatformCobrowseRedactedViewEffect()
        {
        }

        protected override void OnAttached()
        {
            // We have to always use 'Container' and never 'Control'
            // because 'Control' is null in 'OnDetached', at least in Xamarin.Forms 4.5.0.356
            AddToRedacted(Container);
        }

        protected override void OnDetached()
        {
            RemoveFromRedacted(Container);
        }

        private static void AddToRedacted(UIView view)
        {
            if (view == null)
            {
                return;
            }
            sRedacted.Add(view);
        }

        private static void RemoveFromRedacted(UIView view)
        {
            if (view == null)
            {
                return;
            }
            if (sRedacted.Contains(view))
            {
                sRedacted.Remove(view);
            }
        }
    }
}
```

Then create a new default `Xamarin.Forms.Page` renderer which would implement `ICobrowseIORedacted` interface:

```cs
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(YourAppNamespace.iOS.CobrowseRedactedPageRenderer))]
namespace YourAppNamespace.iOS
{
    public class CobrowseRedactedPageRenderer : PageRenderer, ICobrowseIORedacted
    {
        public UIView[] RedactedViews => PlatformCobrowseRedactedViewEffect.RedactedViews;
    }
}
```

**iOS-specific** platform Effect would look like this:

```cs
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;

[assembly: ResolutionGroupName("YourAppName")]
[assembly: ExportEffect(typeof(YourAppNamespace.Android.PlatformCobrowseRedactedViewEffect), "CobrowseRedactedViewEffect")]
namespace YourAppNamespace.Android
{
    public class PlatformCobrowseRedactedViewEffect : PlatformEffect
    {
        private static readonly List<AView> sRedacted = new List<AView>();

        public static IList<AView> RedactedViews => sRedacted;

        protected override void OnAttached()
        {
            AddToRedacted(Control ?? Container);
        }

        protected override void OnDetached()
        {
            RemoveFromRedacted(Control ?? Container);
        }

        private static void AddToRedacted(AView view)
        {
            if (view == null)
            {
                return;
            }
            sRedacted.Add(view);
        }

        private static void RemoveFromRedacted(AView view)
        {
            if (view == null)
            {
                return;
            }
            if (sRedacted.Contains(view))
            {
                sRedacted.Remove(view);
            }
        }
    }
}
```

Then implement `CobrowseIO.IRedacted` interface in your Forms activity:

```cs
using System.Collections.Generic;
using Android.App;
using Xamarin.CobrowseIO;
using AView = Android.Views.View;

namespace YourAppNamespace.Android
{
    [Activity]
    public class MainActivity
        : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        CobrowseIO.IRedacted
    {
        public IList<AView> RedactedViews()
        {
            return PlatformCobrowseRedactedViewEffect.RedactedViews;
        }
    }
}
```

### 2. Use the Cobrowse web dashboard to define redacted views

You can also define redactions using a selector entered into the web dashboard. This can be useful if your app is already in production and you need to redact a field retrospectively, either due to a missed redaction entry in the app build or changing requirements. Visit the [dashboard settings](https://cobrowse.io/dashboard/settings/redaction) to enter redaction selectors.

## Questions?
Any questions at all? Please email us directly at [hello@cobrowse.io](mailto:hello@cobrowse.io).
