using System;
using CobrowseIOSdk;
using Foundation;
using SampleApp.Forms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CobrowseAdapter))]
namespace SampleApp.Forms.iOS
{
    public class CobrowseAdapter : ICobrowseAdapter
    {
        public CobrowseAdapter()
        {
        }

        public void StartCobrowse()
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var nc = vc.GetUINavigationController();
            nc.PushViewController(
                new CBIOViewController(),
                animated: true);
        }

        public void CheckCobrowseFullDevice()
        {
            return;
        }
    }
}
