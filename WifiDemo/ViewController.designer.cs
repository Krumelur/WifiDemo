// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace WifiDemo
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TxtPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TxtSsid { get; set; }

        [Action ("OnConnectClicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnConnectClicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (TxtPassword != null) {
                TxtPassword.Dispose ();
                TxtPassword = null;
            }

            if (TxtSsid != null) {
                TxtSsid.Dispose ();
                TxtSsid = null;
            }
        }
    }
}