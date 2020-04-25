using Foundation;

namespace SampleApp.Forms.iOS
{
    public static class NSErrorExtensions
    {
        public static NSErrorException AsException(this NSError error)
            => new NSErrorException(error);
    }
}
