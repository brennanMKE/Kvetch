using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace Kvetch.Website
{

    /// <summary>
    /// Summary description for SiteUtility
    /// </summary>
    public static class SiteUtility
    {

        public static string GetMasterPageFile()
        {
            return ConfigurationManager.AppSettings["Kvetch.MasterPageFile"];
        }

        public static void RegisterJavaScript(Page page)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(page);
            if (IsDebugMode)
            {
                scriptManager.Scripts.Add(new ScriptReference("~/Scripts/jquery-1.1.3.1.js"));
                scriptManager.Scripts.Add(new ScriptReference("~/Scripts/Kvetch.js"));
            }
            else
            {
                scriptManager.Scripts.Add(new ScriptReference("~/Scripts/jquery-1.1.3.1.pack.js"));
                scriptManager.Scripts.Add(new ScriptReference("~/Scripts/Kvetch.pack.js"));
            }
        }

        public static bool IsDebugMode
        {
            get
            {
                return HttpContext.Current.IsDebuggingEnabled;
            }
        }

    }

}
