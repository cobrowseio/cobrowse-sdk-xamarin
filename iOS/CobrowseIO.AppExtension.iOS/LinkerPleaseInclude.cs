using System;

namespace Cobrowse.IO.iOS.AppExtension
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

        public void Include(CobrowseIOReplayKitExtension _)
        {
            _ = new CobrowseIOReplayKitExtension();
        }
    }
}
