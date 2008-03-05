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

public partial class basicFooter : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        User currentUser = (User)Session["user"];

        this.logArea.Visible = false;

        if (currentUser != null)
        {
            if (currentUser.isRoot() || currentUser.isAdmin())
            {
                this.logArea.Visible = true;
                this.logArea.InnerHtml = "<b>connection string :</b> " + config.conn;
            }
        }

      
    }
}
