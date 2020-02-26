using SampleApp.Forms.iOS;
using UIKit;
using Xamarin.CobrowseIO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CobrowseRedactedPageRenderer))]
namespace SampleApp.Forms.iOS
{
    public class CobrowseRedactedPageRenderer : PageRenderer, ICobrowseIORedacted
    {
        public CobrowseRedactedPageRenderer()
        {
        }

        public UIView[] RedactedViews => PlatformCobrowseRedactedViewEffect.RedactedViews;
    }
}
