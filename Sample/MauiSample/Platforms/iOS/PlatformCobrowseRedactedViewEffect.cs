using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace MauiSample.Platforms.iOS
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
