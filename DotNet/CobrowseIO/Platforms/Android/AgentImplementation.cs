using System;
using Android.Runtime;
using Xamarin.CobrowseIO.Android;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io Agent class.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AgentImplementation : IAgent
    {
        private Agent _platformAgent;

        public AgentImplementation(Agent agent)
        {
            _platformAgent = agent ?? throw new ArgumentNullException(nameof(agent));
        }

        /// <summary>
        /// The email of the support agent, may be null if account privacy settings
        /// restrict access to the agent email
        /// </summary>
        public string Email => _platformAgent.Email;

        /// <summary>
        /// The id of the support agent.
        /// </summary>
        public string Id => _platformAgent.Id;

        /// <summary>
        /// The display name of the support agent.
        /// </summary>
        public string Name => _platformAgent.Name;
    }
}
