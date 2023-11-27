using System;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Common Cobrowse.io Session callback.
    /// </summary>
    /// <param name="e">Error, if exists.</param>
    /// <param name="session">Cobrowse.io Session, if exists.</param>
    public delegate void CobrowseCallback(Exception e, ISession session);
}
