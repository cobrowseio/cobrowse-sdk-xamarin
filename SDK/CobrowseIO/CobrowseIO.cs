using System;
using System.Diagnostics;

namespace Cobrowse.IO
{
    /// <summary>
    /// Cross-platform Cobrowse.io API.
    /// </summary>
    public class CobrowseIO
    {
        #region Common Cobrowse.io custom data keys

        public static string UserIdKey = "user_id";

        public static string UserEmailKey = "user_email";

        public static string UserNameKey = "user_name";

        public static string DeviceIdKey = "device_id";

        public static string DeviceNameKey = "device_name";

        #endregion

        private static readonly Lazy<ICobrowseIO> Implementation = new Lazy<ICobrowseIO>(CreateCobrowseIO);

        public static ICobrowseIO Instance
        {
            get
            {
                if (Implementation.Value == null)
                    throw NotImplementedInReferenceAssembly();

                return Implementation.Value;
            }
        }

        private static ICobrowseIO CreateCobrowseIO()
        {
            #if __ANDROID__ || __IOS__
            return new CobrowseIOImplementation();
            #else
            return null;
            #endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
