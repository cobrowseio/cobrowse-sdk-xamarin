using System.Collections.Generic;
using SampleApp.Forms.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("SampleApp")]
[assembly: ExportEffect(typeof(PlatformCobrowseRedactedViewEffect), "CobrowseRedactedViewEffect")]
namespace SampleApp.Forms.iOS
{
    public class PlatformCobrowseRedactedViewEffect : PlatformEffect
    {
        private static readonly object sRedactedLock = new object();
        private static readonly List<UIView> sRedacted = new List<UIView>();

        public static UIView[] RedactedViews
        {
            get
            {
                lock (sRedactedLock)
                {
                    return sRedacted.ToArray();
                }
            }
        }

        public PlatformCobrowseRedactedViewEffect()
        {
        }

        protected override void OnAttached()
        {
            // We have to always use 'Container' and never 'Control'
            // because 'Control' is null in 'OnDetached'
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

            // Effect should be run in UI thread only, se locking is redundant
            lock (sRedactedLock)
            {
                sRedacted.Add(view);
            }
        }

        private static void RemoveFromRedacted(UIView view)
        {
            if (view == null)
            {
                return;
            }
            lock (sRedactedLock)
            {
                if (sRedacted.Contains(view))
                {
                    sRedacted.Remove(view);
                }
            }
        }
    }
}
