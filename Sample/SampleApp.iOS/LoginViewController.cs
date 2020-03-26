using UIKit;
using Xamarin.CobrowseIO;

namespace SampleApp.iOS
{
    public partial class LoginViewController : UIViewController, ICobrowseIORedacted
    {
        private UIView[] _redactedViews;

        public LoginViewController() : base("LoginViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            _redactedViews = new UIView[]
            {
                textFieldLogin,
                textFieldPassword
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public UIView[] RedactedViews => _redactedViews;
    }
}

