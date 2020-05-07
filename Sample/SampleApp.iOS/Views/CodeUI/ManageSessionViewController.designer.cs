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
    [Register ("ManageSessionViewController")]
    partial class ManageSessionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UIButton end { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel text { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (end != null) {
                end.Dispose ();
                end = null;
            }

            if (text != null) {
                text.Dispose ();
                text = null;
            }
        }
    }
}