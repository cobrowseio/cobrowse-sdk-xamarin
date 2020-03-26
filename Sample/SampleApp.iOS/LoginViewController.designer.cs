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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textFieldLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textFieldPassword { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (textFieldLogin != null) {
                textFieldLogin.Dispose ();
                textFieldLogin = null;
            }

            if (textFieldPassword != null) {
                textFieldPassword.Dispose ();
                textFieldPassword = null;
            }
        }
    }
}