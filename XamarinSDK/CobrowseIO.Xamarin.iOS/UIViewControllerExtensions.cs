using System.Linq;
using UIKit;

namespace Xamarin.CobrowseIO
{
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
    }
}