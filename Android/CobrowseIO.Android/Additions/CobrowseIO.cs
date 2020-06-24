using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Java.Lang.Annotation;

namespace Xamarin.CobrowseIO
{
    public partial class CobrowseIO
    {
        [Obsolete("Use License property instead")]
        public void SetLicense(string license)
            => this.License = license;

        public IReadOnlyDictionary<string, object> _CustomData
        {
            get => null; // TODO return the actual custom data
            set => SetCustomData(value);
        }

        [Obsolete("Use CustomData property instead")]
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
            this.SetCustomJavaData(javaCustomData);
        }
        
        public void CreateSession(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.CreateSession(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        public void GetSession(string idOrCode, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.GetSession(idOrCode, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }
    }
}
