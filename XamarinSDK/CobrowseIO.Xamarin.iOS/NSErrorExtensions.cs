using Foundation;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// <see cref="NSError"/> <-> <see cref="NSException"/> converter.
    /// </summary>
    [Preserve(AllMembers = true)]
    public static class NSErrorExtensions
    {
        public static NSErrorException AsException(this NSError error)
            => new NSErrorException(error);
    }
}
