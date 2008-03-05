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

public partial class ViewInvitation : System.Web.UI.Page
{

   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cmdEnterGUID_ServerClick(object sender, EventArgs e)
    {
        string guid = this.txtGUID.Value;

        Invitation invite = new Invitation(config.conn, guid);

        if (invite != null)
        {
            Response.Redirect("ViewInvitation2.aspx?guid=" + guid);
        }
        else
        {
            Response.Write("Invalid GUID");
        }

    }
}
