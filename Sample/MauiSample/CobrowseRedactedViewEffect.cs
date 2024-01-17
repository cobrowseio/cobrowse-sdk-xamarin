namespace MauiSample
{
    /// <summary>
    /// A MAUI effect that helps to redact MAUI views in Cobrowse.io.
    /// </summary>
    public class CobrowseRedactedViewEffect : RoutingEffect
    {
        public bool IsRedacted { get; set; } = true;
    }
}
