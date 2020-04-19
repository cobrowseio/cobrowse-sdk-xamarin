using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    public static class CobrowseDataKeys
    {
        public static string UserId = "user_id";

        public static string UserEmail = "user_email";

        public static string UserName = "user_name";

        public static string DeviceId = "device_id";

        public static string DeviceName = "device_name";
    }

    public interface ICobrowseAdapter
    {
        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        string DeviceId { get; }

        /// <summary>
        /// Initializes the Cobrowse.io SDK.
        /// </summary>
        void Initialize(string licenseKey);

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        void SetCustomData(IDictionary<string, object> customData);

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        void StartCobrowse();

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        void CheckCobrowseFullDevice();

        /// <summary>
        /// Ends the current session (if active).
        /// </summary>
        void EndCurrentSession();

        /// <summary>
        /// Sets the custom overlay view.
        /// </summary>
        void SetCustomOverlayView(Func<View> viewFunc);
    }
}
