using System;
using System.Diagnostics;

namespace Xamarin.CobrowseIO.Abstractions
{
    /// <summary>
    /// Cross-platform Cobrowse.io API.
    /// </summary>
    public class CobrowseIO
    {
        #region Common Cobrowse.io custom data keys

        public static string UserId = "user_id";

        public static string UserEmail = "user_email";

        public static string UserName = "user_name";

        public static string DeviceId = "device_id";

        public static string DeviceName = "device_name";

        #endregion

        static readonly Lazy<ICobrowseIO> Implementation = new Lazy<ICobrowseIO>(CreateCobrowseIO);

        public static ICobrowseIO Instance
        {
            get
            {
                if (Implementation.Value == null)
                    throw NotImplementedInReferenceAssembly();

                return Implementation.Value;
            }
        }

        static ICobrowseIO CreateCobrowseIO()
        {
            #if PORTABLE || NETSTANDARD
            Debug.WriteLine("PORTABLE || NETSTANDARD reached");
            return null;
            #else
            Debug.WriteLine("Other reached");
            return new CobrowseIOImplementation();
            #endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
