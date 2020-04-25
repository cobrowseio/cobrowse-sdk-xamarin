using System;
using CoreFoundation;
using Foundation;
using UIKit;

namespace SampleApp.iOS
{
    public partial class CodeDisplayViewController : UIViewController
    {
        public CodeDisplayViewController() : base("CodeDisplayViewController", null)
        {
        }

        private Random _random;
        private NSTimer _animationTimer;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.working.Hidden = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (_animationTimer == null)
            {
                _animationTimer = NSTimer.CreateScheduledTimer(
                    0.08d,
                    repeats: true,
                    block: (arg) => { RenderRandomCode(); });
            }
            if (_random == null)
            {
                _random = new Random();
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _animationTimer?.Invalidate();
            _animationTimer = null;
        }

        private void RenderRandomCode()
        {
            string chars = "1234567890";
            string code = "";
            for (int i = 0; i < 6; i++)
            {
                code += chars[_random.Next() % chars.Length];
            }
            this.codeLabel.Alpha = 0.1f;
            RenderCode(code);
        }

        private void RenderCode(string code)
        {
            string first3 = code.Substring(0, 3);
            string last3 = code.Substring(3);
            this.codeLabel.Text = $"{first3}-{last3}";
        }

        public void SetCode(string code)
        {
            if (code == null)
            {
                return;
            }

            DispatchQueue.MainQueue.DispatchAfter(
                new DispatchTime(DispatchTime.Now, TimeSpan.FromMilliseconds(200)),
                () =>
                {
                    _animationTimer?.Invalidate();
                    _animationTimer = null;
                    this.working.Hidden = false;
                    this.codeLabel.Alpha = 1f;
                    RenderCode(code);
                });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

