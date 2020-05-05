using System;
using System.Diagnostics;

namespace Xamarin.CobrowseIO
{
    /// <summary>
    /// Cross-platform Cobrowse.io API.
    /// </summary>
    public class CrossCobrowseIO
    {
        static readonly Lazy<ICrossCobrowseIO> Implementation = new Lazy<ICrossCobrowseIO>(CreateCrossCobrowseIO);

        public static ICrossCobrowseIO Current
        {
            get
            {
                if (Implementation.Value == null)
                    throw NotImplementedInReferenceAssembly();

                return Implementation.Value;
            }
        }

        static ICrossCobrowseIO CreateCrossCobrowseIO()
        {
            #if PORTABLE || NETSTANDARD
            Debug.WriteLine("PORTABLE || NETSTANDARD reached");
            return null;
            #else
            Debug.WriteLine("Other reached");
            return new CrossCobrowseIOImplementation();
            #endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
