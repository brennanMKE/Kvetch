using System;
using System.Web;
using System.Web.UI;

namespace Kvetch.Website
{

    /// <summary>
    /// Summary description for BasePage
    /// </summary>
    public class BasePage : Page
    {
        public BasePage()
        {
            PreInit += new EventHandler(BasePage_PreInit);
        }

        void BasePage_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = SiteUtility.GetMasterPageFile();
        }
    }

}
