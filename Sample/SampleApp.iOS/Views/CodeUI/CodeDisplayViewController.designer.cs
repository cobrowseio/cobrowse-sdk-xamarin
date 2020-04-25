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
    [Register ("CodeDisplayViewController")]
    partial class CodeDisplayViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel codeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel instructionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView working { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (codeLabel != null) {
                codeLabel.Dispose ();
                codeLabel = null;
            }

            if (instructionLabel != null) {
                instructionLabel.Dispose ();
                instructionLabel = null;
            }

            if (working != null) {
                working.Dispose ();
                working = null;
            }
        }
    }
}