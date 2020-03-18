using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.CobrowseIO;
using Xamarin.Forms.Platform.Android;

namespace SampleApp.Forms.Android
{
    [Application(
        #if DEBUG
        Debuggable = true,
        #else
        Debuggable = false,
        #endif
        Label = "Cobrowse.io Xamarin.Forms",
        Icon = "@mipmap/icon")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);

            CobrowseIO.Instance().License("trial");

            Debug.WriteLine("Cobrowse device id: " + CobrowseIO.Instance().DeviceId(this));

            var customData = new Dictionary<string, Java.Lang.Object>()
            {
                { CobrowseIO.UserIdKey, "<your_user_id>" },
                { CobrowseIO.UserNameKey, "<your_user_name>" },
                { CobrowseIO.UserEmailKey, "<your_user_email>" },
                { CobrowseIO.DeviceIdKey, "<your_device_id>" },
                { CobrowseIO.DeviceNameKey, "<your_device_name>" },
            };
            CobrowseIO.Instance().CustomData(customData);

            CobrowseIO.Instance().SetDelegate(new CustomCobrowseDelegate());
            CobrowseIO.Instance().Start(this);
        }

        public class CustomCobrowseDelegate : Java.Lang.Object,
            CobrowseIO.ISessionRequestDelegate,
            CobrowseIO.ISessionControlsDelegate
        {
            public CustomCobrowseDelegate()
            {
            }

            public CustomCobrowseDelegate(IntPtr handle, JniHandleOwnership transfer)
                : base(handle, transfer)
            {
            }

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
                var indicator = new CobrowseCustomView();
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
                session.Activate(null);
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
}
