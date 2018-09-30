using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.SessionSetting
{
    public class SessionSet<T> where T:class
    {

        public static T CurrentUser(string key)
        {
            return GetSession(key);
        }

        public static void  SetSession(T obj,string key)
        {
            HttpContext.Current.Session[key] = obj;
        }

        public static T GetSession(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }


        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }


       


    }
}