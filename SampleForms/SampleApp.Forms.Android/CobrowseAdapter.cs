using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using SampleApp.Forms.Android;
using Xamarin.CobrowseIO;
using Xamarin.CobrowseIO.UI;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;
using XView = Xamarin.Forms.View;

[assembly: Xamarin.Forms.Dependency(typeof(CobrowseAdapter))]
namespace SampleApp.Forms.Android
{
    public class CobrowseAdapter : ICobrowseAdapter
    {
        private Activity Activity => CrossCurrentActivity.Current.Activity;

        private CustomOverlayCobrowseDelegate _overlayDelegate;

        public CobrowseAdapter()
        {
        }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        public string DeviceId => CobrowseIO.Instance().GetDeviceId(Activity.Application);

        /// <summary>
        /// Initializes the Cobrowse.io SDK.
        /// </summary>
        public void Initialize(string licenseKey)
        {
            if (Application.Context is Application application)
            {
                CobrowseIO.Instance().SetLicense(licenseKey);
                CobrowseIO.Instance().SetDelegate(
                    _overlayDelegate = new CustomOverlayCobrowseDelegate());
                CobrowseIO.Instance().Start(application);
            }
        }

        /// <summary>
        /// Sets Cobrowse.io custom data.
        /// </summary>
        public void SetCustomData(IDictionary<string, object> customData)
        {
            CobrowseIO.Instance().SetCustomData(customData);
        }

        /// <summary>
        /// Launches 6-digits code UI.
        /// </summary>
        public void StartCobrowse()
        {
            var intent = new Intent(Activity, typeof(CobrowseActivity));
            Activity.StartActivity(intent);
        }

        /// <summary>
        /// Checks if full-device screen sharing is allowed.
        /// </summary>
        public void CheckCobrowseFullDevice()
        {
            bool isRunning = CobrowseAccessibilityService.IsRunning(Activity);
            if (!isRunning)
            {
                CobrowseAccessibilityService.ShowSetup(Activity);
                return;
            }

            Toast.MakeText(
                Activity,
                "Full-device control is enabled and ready.",
                ToastLength.Short)
                .Show();
        }

        /// <summary>
        /// Ends the current session (if active).
        /// </summary>
        public void EndCurrentSession()
        {
            CobrowseIO.Instance().CurrentSession?.End(callback: null);
        }

        /// <summary>
        /// Sets the custom overlay view.
        /// </summary>
        public void SetCustomOverlayView(Func<XView> viewFunc)
        {
            _overlayDelegate.IndicatorFunction = viewFunc;
        }
    }

    public class CustomOverlayCobrowseDelegate : Java.Lang.Object,
        CobrowseIO.ISessionRequestDelegate,
        CobrowseIO.ISessionControlsDelegate
    {
        public CustomOverlayCobrowseDelegate()
        {
        }

        public CustomOverlayCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public Func<XView> IndicatorFunction { get; set; }

        private View _overlayIndicator;

        public void ShowSessionControls(Activity activity, Session session)
        {
            if (_overlayIndicator != null)
            {
                return;
            }
            if (!(activity is FormsAppCompatActivity))
            {
                return;
            }
            var indicator = IndicatorFunction();
            if (indicator == null)
            {
                return;
            }
            var renderer = Platform.CreateRendererWithContext(indicator, activity);
            renderer.Element.Layout(new Xamarin.Forms.Rectangle(0, 0, indicator.WidthRequest, indicator.HeightRequest));
            var nativeIndicator = renderer.View;

            var modal = new RelativeLayout(activity);
            var layoutParams = new RelativeLayout.LayoutParams(
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)indicator.WidthRequest, activity.Resources.DisplayMetrics),
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)indicator.HeightRequest, activity.Resources.DisplayMetrics))
            {
                MarginEnd = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 4f, activity.Resources.DisplayMetrics)
            };
            layoutParams.AddRule(LayoutRules.CenterVertical);
            layoutParams.AddRule(LayoutRules.AlignParentEnd);
            modal.AddView(nativeIndicator, layoutParams);

            var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
            rootFrameLayout.AddView(modal, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            rootFrameLayout.Invalidate();

            _overlayIndicator = modal;
        }

        public void HideSessionControls(Activity activity, Session session)
        {
            if (_overlayIndicator == null)
            {
                return;
            }
            if (!(activity is FormsAppCompatActivity))
            {
                return;
            }
            var rootFrameLayout = (ViewGroup)activity.Window.PeekDecorView();
            rootFrameLayout.RemoveView(_overlayIndicator);
            _overlayIndicator = null;
        }

        public void HandleSessionRequest(Activity activity, Session session)
        {
            Debug.WriteLine("HandleSessionRequest");
            session.Activate(callback: null);
        }

        public void SessionDidEnd(Session session)
        {
            Debug.WriteLine("SessionDidEnd");
        }

        public void SessionDidUpdate(Session session)
        {
            Debug.WriteLine("SessionDidUpdate");
        }
    }
}