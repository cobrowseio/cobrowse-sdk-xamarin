using System.Collections.Generic;
using UIKit;

namespace Xamarin.CobrowseIO
{
    public class CrossCobrowseIOImplementation : ICrossCobrowseIO
    {
        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance().DeviceId;

        /// <summary>
        /// Initializes the Cobrowse.io SDK.
        /// </summary>
        public void Initialize(string licenseKey)
        {
            CobrowseIO.Instance().SetLicense(licenseKey);
            CobrowseIO.Instance().Start();
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        public void SetCustomData(IDictionary<string, object> customData)
        {
            CobrowseIO.Instance().SetCustomData(customData);
        }

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        public void StartCobrowse()
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var nc = vc.GetUINavigationController();
            nc?.PushViewController(
                new CobrowseViewController(),
                animated: true);
        }

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        public void CheckCobrowseFullDevice()
        {
            return;
        }
    }
}
