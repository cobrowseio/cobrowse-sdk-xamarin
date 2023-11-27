namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io Agent class.
    /// </summary>
    public interface IAgent
    {
        /// <summary>
        /// The email of the support agent, may be null if account privacy settings
        /// restrict access to the agent email
        /// </summary>
        string Email { get; }

        /// <summary>
        /// The id of the support agent.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The display name of the support agent.
        /// </summary>
        string Name { get; }
    }
}