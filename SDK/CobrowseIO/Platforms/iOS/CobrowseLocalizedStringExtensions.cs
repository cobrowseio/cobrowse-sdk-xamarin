using System;
using Foundation;

namespace Cobrowse.IO
{
    [Preserve(AllMembers = true)]
    internal static class CobrowseLocalizedStringExtensions
    {
        public static string GetLocalizedString(
            this string key,
            string fallback)
        {
            return NSBundle.MainBundle.GetLocalizedString(key, fallback, table: "CobrowseIO");
        }
    }
}

