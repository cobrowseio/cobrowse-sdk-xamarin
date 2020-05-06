using System;
using System.Collections.Generic;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io SDK.
    /// </summary>
    public interface ICrossCobrowseIO
    {
        /// <summary>
        /// Occurs when a session is requested.
        /// </summary>
        event EventHandler<ICobrowseSession> SessionDidRequest;

        /// <summary>
        /// Occurs when a session is updated.
        /// </summary>
        event EventHandler<ICobrowseSession> SessionDidUpdate;

        /// <summary>
        /// Occurs when a session ends.
        /// </summary>
        event EventHandler<ICobrowseSession> SessionDidEnd;

        /// <summary>
        /// Returns the current session instance or null if it doesn't exist.
        /// </summary>
        ICobrowseSession CurrentSession { get; }

        /// <summary>
        /// Creates a new Cobrowse.io session.
        /// </summary>
        void CreateSession(CobrowseCallback callback);

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        string DeviceId { get; }

        /// <summary>
        /// Sets the license.
        /// </summary>
        void SetLicense(string licenseKey);

        /// <summary>
        /// Starts the Cobrowse.io.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the Cobrowse.io.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        void SetCustomData(IDictionary<string, object> customData);

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        void OpenCobrowseUI();

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        void CheckCobrowseFullDevice();
    }
}
