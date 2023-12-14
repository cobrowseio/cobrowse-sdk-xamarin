using System.Linq;
using Foundation;
using UIKit;

namespace Cobrowse.IO
{
    /// <summary>
    /// <see cref="UIViewController"/> extension methods.
    /// </summary>
    [Preserve(AllMembers = true)]
    internal static class UIViewControllerExtensions
    {
        public static UINavigationController GetUINavigationController(this UIViewController controller)
        {
            if (controller != null)
            {
                if (controller is UINavigationController nv)
                {
                    return nv;
                }
                else if (controller.ChildViewControllers.Any())
                {
                    int count = controller.ChildViewControllers.Count();
                    for (int i = 0; i < count; i++)
                    {
                        var child = GetUINavigationController(controller.ChildViewControllers[i]);
                        if (child is UINavigationController nc)
                        {
                            return nc;
                        }
                    }
                }
            }

            return null;
        }

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