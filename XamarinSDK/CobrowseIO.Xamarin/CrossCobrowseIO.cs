using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Xamarin.CobrowseIO.Abstractions
{
    /// <summary>
    /// Cross-platform Cobrowse.io API.
    /// </summary>
    public class CobrowseIO
    {
        #region Common Cobrowse.io custom data keys

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string UserId => UserIdKey;

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string UserEmail => UserEmailKey;

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string UserName => UserNameKey;

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string DeviceId => DeviceIdKey;

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string DeviceName => DeviceNameKey;

        public static string UserIdKey = "user_id";

        public static string UserEmailKey = "user_email";

        public static string UserNameKey = "user_name";

        public static string DeviceIdKey = "device_id";

        public static string DeviceNameKey = "device_name";

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
