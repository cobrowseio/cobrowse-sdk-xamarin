using System.Collections.Generic;

namespace Xamarin.CobrowseIO
{
    public interface ICrossCobrowseIO
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
    }
}
