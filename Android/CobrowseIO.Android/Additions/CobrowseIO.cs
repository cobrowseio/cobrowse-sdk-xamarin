using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Java.Lang.Annotation;

namespace Xamarin.CobrowseIO
{
    public partial class CobrowseIO
    {
        [Obsolete("Use Api property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetApi(string api)
            => this.Api = api;

        [Obsolete("Use License property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetLicense(string license)
            => this.License = license;

        public IReadOnlyDictionary<string, object> CustomData
        {
            get
            {
                if (this.CustomJavaData is Dictionary<string, Java.Lang.Object> dictionary)
                {
                    var rvalue = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, Java.Lang.Object> next in dictionary)
                    {
                        rvalue.Add(next.Key, next.Value);
                    }
                    return rvalue;
                }
                return null;
            }
            set => SetCustomData(value);
        }

        [Obsolete("Use CustomData property instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetCustomData(IDictionary<string, object> customData)
        {
            this.SetCustomData(
                (IReadOnlyDictionary<string, object>)
                new ReadOnlyDictionary<string, object>(customData));
        }

        internal void SetCustomData(IReadOnlyDictionary<string, object> customData)
        {
            if (customData == null)
            {
                throw new ArgumentNullException(nameof(customData));
            }
            var javaCustomData = new Dictionary<string, Java.Lang.Object>();
            foreach (var next in customData)
            {
                if (next.Value is Java.Lang.Object jObject)
                {
                    javaCustomData.Add(next.Key, jObject);
                }
                else
                {
                    javaCustomData.Add(next.Key, next.Value.ToString());
                }
            }
            this.CustomJavaData = javaCustomData;
        }
        
        public void CreateSession(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.CreateSession(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        public void GetSession(string idOrCode, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.GetSession(idOrCode, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        /// <summary>
        /// Gets or sets the available capabilities for a session. Different
        /// annotation tools and events will be available during a session
        /// depending on the capabilities you set here. By default all
        /// capabilities supported by the device are enabled.
        /// </summary>
        public string[] Capabilities
        {
            get => GetCapabilities();
            set => SetCapabilities(value);
        }

        /// <summary>
        /// Gets or sets the CSS selectors which will be used to redact content within WebViews.
        /// Any HTML element matching one of the selectors configured here will be redacted and
        /// not visible to your agents.
        /// Defaults to an empty list which means the feature is disabled.
        /// </summary>
        public string[] WebViewRedactedViews
        {
            get => GetCapabilities();
            set => SetCapabilities(value);
        }
    }
}
