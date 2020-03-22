using SampleApp.Forms.iOS;
using UIKit;
using Xamarin.CobrowseIO;
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

        public void EndCurrentSession()
        {
            CobrowseIO.Instance().CurrentSession?.End(null);
        }
    }
}
