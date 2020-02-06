namespace SwiftCBOR.iOS
{
    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public LinkerPleaseInclude()
        {
        }

        public void Include(XamarinSwiftHelper _)
        {
            _ = new SwiftCBOR.iOS.XamarinSwiftHelper();
        }
    }
}
