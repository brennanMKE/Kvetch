using System;
using System.Web;
using System.Web.UI;

namespace Kvetch.Website
{

    /// <summary>
    /// Summary description for AjaxUserControl
    /// </summary>
    public class AjaxUserControl : UserControl
    {

        public AjaxUserControl()
        {
            Load += new EventHandler(AjaxUserControl_Load);
        }

        protected void AjaxUserControl_Load(object sender, EventArgs e)
        {
            RegisterJavaScript();
        }

        public virtual void RegisterJavaScript()
        {
            SiteUtility.RegisterJavaScript(Page);

            string controlName = GetControlName();
            string controlScriptUrl = "~/Controls/" + controlName + ".js";
            string className = "Kvetch." + controlName;

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            scriptManager.Scripts.Add(new ScriptReference(controlScriptUrl));

            string initializeUrl = "~/Scripts/UserControl.js?className=" + 
                className + "&clientId=" + ClientID;
            scriptManager.Scripts.Add(new ScriptReference(initializeUrl));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute("id", ClientID);
            writer.AddAttribute("class", GetControlName());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }

        public virtual string GetControlName()
        {
            string controlName = GetType().BaseType.FullName;
            int startIndex = controlName.IndexOf("Controls_");
            if (startIndex == -1)
            {
                startIndex = 0;
            }
            else
            {
                startIndex += "Controls_".Length;
            }
            int lastIndex = controlName.LastIndexOf("Control");
            if (lastIndex == -1)
            {
                lastIndex = controlName.Length - startIndex;
            }
            return controlName.Substring(startIndex, lastIndex - startIndex);
        }

    }

}
