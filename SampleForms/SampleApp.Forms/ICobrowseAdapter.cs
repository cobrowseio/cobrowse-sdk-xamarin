using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SampleApp.Forms
{
    /// <summary>
    /// Common Cobrowse.io custom data keys.
    /// </summary>
    public static class CobrowseDataKeys
    {
        public static string UserId = "user_id";

        public static string UserEmail = "user_email";

        public static string UserName = "user_name";

        public static string DeviceId = "device_id";

        public static string DeviceName = "device_name";
    }

    /// <summary>
    /// Common Cobrowse.io Session callback.
    /// </summary>
    /// <param name="e">Error, if exists.</param>
    /// <param name="session">Cobrowse.io Session, if exists.</param>
    public delegate void CobrowseCallback(Exception e, ICobrowseSession session);

    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io session.
    /// </summary>
    public interface ICobrowseSession
    {
        /// <summary>
        /// Gets the session's code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets a value indicating if the session running, frames are streaming to the agent.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets a value indicating if waiting for the user to confirm the session.
        /// </summary>
        bool IsAuthorizing { get; }

        /// <summary>
        /// Gets a value indicating if the ession is over and can no longer be used or edited.
        /// </summary>
        bool IsEnded { get; }

        /// <summary>
        /// Gets a value indicating if the session has been created but is waiting for agent or user.
        /// </summary>
        bool IsPending { get; }

        /// <summary>
        /// Ends the session.
        /// </summary>
        void End(CobrowseCallback callback);
    }

    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io SDK.
    /// </summary>
    public interface ICobrowseAdapter
    {
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
