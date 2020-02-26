using System;
namespace Xamarin.CobrowseIO
{
    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public LinkerPleaseInclude()
        {
        }

        public static void Preserve()
        {

        }

        public void Include(CobrowseIO _)
        {
            _ = new CobrowseIO();
        }

        public void Include(Starscream.iOS.LinkerPleaseInclude _)
        {
            _ = new Starscream.iOS.LinkerPleaseInclude();
        }

        public void Include(SwiftCBOR.iOS.LinkerPleaseInclude _)
        {
            _ = new SwiftCBOR.iOS.LinkerPleaseInclude();
        }
    }
}
