using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AgentStoryComponents;

public partial class contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Text.StringBuilder html = new System.Text.StringBuilder();
        html.Append(" Grant: ");
        html.Append("g@agentidea.com");
        html.Append("<br>");
        html.Append("<br>");
        html.Append(" Webmaster: ");
        html.Append(config.webMasterEmail);
        this.contactDiv.InnerHtml = html.ToString();
    }
}
