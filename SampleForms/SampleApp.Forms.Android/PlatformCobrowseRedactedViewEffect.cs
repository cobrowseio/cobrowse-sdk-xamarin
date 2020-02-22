using System.Collections.Generic;
using SampleApp.Forms.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;

[assembly: ResolutionGroupName("SampleApp")]
[assembly: ExportEffect(typeof(PlatformCobrowseRedactedViewEffect), "CobrowseRedactedViewEffect")]
namespace SampleApp.Forms.Android
{
    public class PlatformCobrowseRedactedViewEffect : PlatformEffect
    {
        private static readonly object sRedactedLock = new object();
        private static readonly List<AView> sRedacted = new List<AView>();

        public static IList<AView> RedactedViews
        {
            get
            {
                lock (sRedactedLock)
                {
                    return sRedacted;
                }
            }
        }

        public PlatformCobrowseRedactedViewEffect()
        {
        }

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

            // Effect should be run in UI thread only, se locking is redundant
            lock (sRedactedLock)
            {
                sRedacted.Add(view);
            }
        }

        private static void RemoveFromRedacted(AView view)
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
