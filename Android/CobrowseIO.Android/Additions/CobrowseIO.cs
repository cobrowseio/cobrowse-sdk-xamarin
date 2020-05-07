using System;
using System.Collections.Generic;

namespace Xamarin.CobrowseIO
{
    public partial class CobrowseIO
    {
        public void SetCustomData(IDictionary<string, object> customData)
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
