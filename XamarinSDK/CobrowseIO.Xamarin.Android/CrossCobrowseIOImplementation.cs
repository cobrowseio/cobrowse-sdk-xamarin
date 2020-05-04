using System.Collections.Generic;
using Android.App;
using Android.Content;
using Plugin.CurrentActivity;
using Xamarin.CobrowseIO.UI;

namespace Xamarin.CobrowseIO
{
    public class CrossCobrowseIOImplementation : ICrossCobrowseIO
    {
        protected Activity Activity => CrossCurrentActivity.Current.Activity;

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance().GetDeviceId(Activity.Application);

        /// <summary>
        /// Initializes the Cobrowse.io SDK.
        /// </summary>
        public void Initialize(string licenseKey)
        {
            if (Application.Context is Application application)
            {
                CobrowseIO.Instance().SetLicense(licenseKey);
                CobrowseIO.Instance().Start(application);
            }
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
            var intent = new Intent(Activity, typeof(CobrowseActivity));
            Activity.StartActivity(intent);
        }

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        public void CheckCobrowseFullDevice()
        {
            bool isRunning = CobrowseAccessibilityService.IsRunning(Activity);
            if (!isRunning)
            {
                CobrowseAccessibilityService.ShowSetup(Activity);
                return;
            }
        }
    }
}
