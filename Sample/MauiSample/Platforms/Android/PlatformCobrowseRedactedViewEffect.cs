using Microsoft.Maui.Controls.Platform;
using AView = Android.Views.View;

namespace MauiSample.Platforms.Android
{
    public class PlatformCobrowseRedactedViewEffect : PlatformEffect
    {
        public PlatformCobrowseRedactedViewEffect()
        {
        }
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
