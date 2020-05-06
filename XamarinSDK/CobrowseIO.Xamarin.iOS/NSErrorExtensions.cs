using Foundation;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// <see cref="NSError"/> <-> <see cref="NSException"/> converter.
    /// </summary>
    [Preserve(AllMembers = true)]
    internal static class NSErrorExtensions
    {
        public static NSErrorException AsException(this NSError error)
            => new NSErrorException(error);
    }
}
