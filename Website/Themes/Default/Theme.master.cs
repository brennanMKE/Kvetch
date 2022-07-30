using System;
using System.Web;
using Kvetch.Website;

public partial class Themes_Default_Theme : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SiteUtility.RegisterJavaScript(Page);
    }
}
