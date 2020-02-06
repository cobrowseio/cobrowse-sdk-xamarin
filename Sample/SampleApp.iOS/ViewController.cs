using System;
using CobrowseIOSdk;
using UIKit;

namespace SampleApp.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            this.buttonCobrowse.TouchUpInside += ButtonCobrowse_TouchUpInside;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void ButtonCobrowse_TouchUpInside(object sender, EventArgs e)
        {
            this.NavigationController.PushViewController(
                new CBIOViewController(),
                animated: true);
        }
    }
}