// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SampleApp.iOS
{
    [Register ("CustomCobrowseViewController")]
    partial class CustomCobrowseViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton closeBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView subview { get; set; }

        [Action ("close:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Close (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (closeBtn != null) {
                closeBtn.Dispose ();
                closeBtn = null;
            }

            if (subview != null) {
                subview.Dispose ();
                subview = null;
            }
        }
    }
}