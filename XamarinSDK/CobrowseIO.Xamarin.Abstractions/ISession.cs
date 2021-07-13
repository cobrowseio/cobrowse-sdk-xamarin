﻿namespace Xamarin.CobrowseIO.Abstractions
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io session.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets the session's code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets the session's state.
        /// </summary>
        string State { get; }

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
        /// Gets a value indicating if an agent object is available.
        /// </summary>
        bool HasAgent { get; }

        /// <summary>
        /// Gets an agent instance.
        /// </summary>
        IAgent Agent { get; }

        /// <summary>
        /// Gets the current remote control status.
        /// </summary>
        RemoteControlState RemoteControl { get; }

        /// <summary>
        /// Enables or disables remote control.
        /// </summary>
        void SetRemoteControl(RemoteControlState value, CobrowseCallback callback);

        /// <summary>
        /// Gets a value indicating if the session is in full-device mode.
        /// </summary>
        bool FullDevice { get; }

        /// <summary>
        /// Enables or disables full-device mode.
        /// </summary>
        void SetFullDevice(bool value, CobrowseCallback callback);

        /// <summary>
        /// Activates the session.
        /// </summary>
        void Activate(CobrowseCallback callback);

        /// <summary>
        /// Ends the session.
        /// </summary>
        void End(CobrowseCallback callback);
    }
}
