using System.Linq;
using UIKit;

namespace SampleApp.iOS
{
    public static class UIViewControllerExtensions
    {
        public static UIViewController GetVisibleViewController(this UIViewController controller)
        {
            controller = controller ?? UIApplication.SharedApplication.KeyWindow.RootViewController;
            if (controller.PresentedViewController == null)
            {
                return controller;
            }
            if (controller.PresentedViewController is UINavigationController)
            {
                return ((UINavigationController)controller.PresentedViewController).VisibleViewController;
            }
            if (controller.PresentedViewController is UITabBarController)
            {
                return ((UITabBarController)controller.PresentedViewController).SelectedViewController;
            }
            return GetVisibleViewController(controller.PresentedViewController);
        }
    }
}