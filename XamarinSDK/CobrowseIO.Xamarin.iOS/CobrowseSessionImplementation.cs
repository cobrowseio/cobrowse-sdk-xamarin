﻿using System;
using Foundation;
using Xamarin.CobrowseIO.Abstractions;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io session.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseSessionImplementation : ISession
    {
        private Session _platformSession;

        public CobrowseSessionImplementation(Session session)
        {
            _platformSession = session ?? throw new ArgumentNullException(nameof(session));
        }

        public static CobrowseSessionImplementation TryCreate(Session session)
        {
            return session != null
                ? new CobrowseSessionImplementation(session)
                : null;
        }

        /// <summary>
        /// Gets the session's code.
        /// </summary>
        public string Code => _platformSession.Code;

        /// <summary>
        /// Gets the session's state.
        /// </summary>
        public string State => _platformSession.State;

        /// <summary>
        /// Gets a value indicating if the session running, frames are streaming to the agent.
        /// </summary>
        public bool IsActive => _platformSession.IsActive;

        /// <summary>
        /// Gets a value indicating if waiting for the user to confirm the session.
        /// </summary>
        public bool IsAuthorizing => _platformSession.IsAuthorizing;

        /// <summary>
        /// Gets a value indicating if the ession is over and can no longer be used or edited.
        /// </summary>
        public bool IsEnded => _platformSession.IsEnded;

        /// <summary>
        /// Gets a value indicating if the session has been created but is waiting for agent or user.
        /// </summary>
        public bool IsPending => _platformSession.IsPending;

        /// <summary>
        /// Gets a value indicating if an agent object is available.
        /// </summary>
        public bool HasAgent => _platformSession.HasAgent;

        /// <summary>
        /// Gets an agent instance.
        /// </summary>
        public IAgent Agent => _platformSession.Agent != null
            ? new AgentImplementation(_platformSession.Agent)
            : null;

        /// <inheritdoc/>
        public Abstractions.RemoteControlState RemoteControl
        {
            get
            {
                switch (_platformSession.RemoteControl)
                {
                    case RemoteControlState.Off:
                        return Abstractions.RemoteControlState.Off;
                    case RemoteControlState.Requested:
                        return Abstractions.RemoteControlState.Requested;
                    case RemoteControlState.Rejected:
                        return Abstractions.RemoteControlState.Rejected;
                    case RemoteControlState.On:
                        return Abstractions.RemoteControlState.On;
                    default:
                        return default;
                }
            }
        }

        /// <inheritdoc/>
        public void SetRemoteControl(Abstractions.RemoteControlState value, CobrowseCallback callback)
        {
            RemoteControlState toBeSet;
            switch (value)
            {
                case Abstractions.RemoteControlState.Off:
                    toBeSet = RemoteControlState.Off;
                    break;
                case Abstractions.RemoteControlState.Requested:
                    toBeSet = RemoteControlState.Requested;
                    break;
                case Abstractions.RemoteControlState.Rejected:
                    toBeSet = RemoteControlState.Rejected;
                    break;
                case Abstractions.RemoteControlState.On:
                    toBeSet = RemoteControlState.On;
                    break;
                default:
                    toBeSet = default;
                    break;
            }

            _platformSession.SetRemoteControl(toBeSet, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
        public bool FullDevice => _platformSession.FullDevice;

        /// <inheritdoc/>
        public void SetFullDevice(bool value, CobrowseCallback callback)
        {
            _platformSession.SetFullDevice(value, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Activates the session.
        /// </summary>
        public void Activate(CobrowseCallback callback)
        {
            _platformSession.Activate((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Ends the session.
        /// </summary>
        public void End(CobrowseCallback callback)
        {
            _platformSession.End((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }
    }
}
