using System;
using UIKit;
using Xamarin.CobrowseIO;

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
            this.buttonCobrowseCustomUI.TouchUpInside += ButtonCobrowseCustomUi_TouchUpInside;
            this.buttonRedactedViews.TouchUpInside += ButtonRedactedViews_TouchUpInside;
            this.buttonDeviceId.TouchUpInside += ButtonDeviceId_TouchUpInside;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void ButtonCobrowse_TouchUpInside(object sender, EventArgs e)
        {
            this.NavigationController.PushViewController(
                new CobrowseViewController(),
                animated: true);
        }

        private void ButtonCobrowseCustomUi_TouchUpInside(object sender, EventArgs e)
        {
            this.NavigationController.PushViewController(
                new CustomCobrowseViewController(),
                animated: true);
        }

        private void ButtonRedactedViews_TouchUpInside(object sender, EventArgs e)
        {
            this.NavigationController.PushViewController(
                new LoginViewController(),
                animated: true);
        }

        private void ButtonDeviceId_TouchUpInside(object sender, EventArgs e)
        {
            var alert = new UIAlertView
            {
                Title = "Cobrowse.io",
                Message = $"Cobrowse.io DeviceId: {CobrowseIO.Instance.DeviceId}"
            };
            alert.AddButton("OK");
            alert.Show();
        }
    }
}