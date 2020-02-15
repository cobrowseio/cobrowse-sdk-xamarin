using Xamarin.Forms;

namespace SampleApp.Forms
{
    /// <summary>
    /// A Xamarin.Forms effect that helps to redact X.F view in Cobrowse.io.
    /// </summary>
    public class CobrowseRedactedViewEffect : RoutingEffect
    {
        public bool IsRedacted { get; set; } = true;

        public CobrowseRedactedViewEffect()
            : base("SampleApp" + "." + nameof(CobrowseRedactedViewEffect))
        {
        }
    }
}
