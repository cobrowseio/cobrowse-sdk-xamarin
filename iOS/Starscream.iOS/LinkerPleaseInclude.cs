using System;
namespace Starscream.iOS
{
    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public LinkerPleaseInclude()
        {
        }

        public void Include(FoundationStream _)
        {
            _ = new FoundationStream();
        }

    }
}
