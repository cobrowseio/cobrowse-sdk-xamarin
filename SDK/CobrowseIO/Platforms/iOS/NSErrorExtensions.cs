using Foundation;

namespace Cobrowse.IO
{
    /// <summary>
    /// <see cref="NSError"/> to/from <see cref="NSException"/> converter.
    /// </summary>
    [Preserve(AllMembers = true)]
    internal static class NSErrorExtensions
    {
        public static NSErrorException AsException(this NSError error)
            => new NSErrorException(error);
    }
}
