namespace Xamarin.CobrowseIO
{
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
        /// Activates the session.
        /// </summary>
        void Activate(CobrowseCallback callback);

        /// <summary>
        /// Ends the session.
        /// </summary>
        void End(CobrowseCallback callback);
    }
}
