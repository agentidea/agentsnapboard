using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using AgentStoryComponents;
using AgentStoryComponents.core;

public partial class _Main : SessionAwareWebForm
{
    protected void Page_Load(object sender, EventArgs e)
    {

        base.Page_Load(sender, e, null, null);

        if (base.currentUser.isAdmin() || base.currentUser.isRoot() )
        {

            //allow only admins and roots to see log.
        }
        else
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login as Root / Admin to perform this operation."));
            Response.End();
        }
        
    }
}
