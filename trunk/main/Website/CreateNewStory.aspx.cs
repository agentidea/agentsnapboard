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

using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;

public partial class CreateNewStory : SessionAwareWebForm
{

   
    public User CurrentUser
    {
        get { return base.currentUser; }
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e,this.divToolBarAttachPoint,this.divFooter);


        if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
            Response.End();
        }

        if (base.currentUser.isObserver())
        {
            string msg = Server.UrlEncode(" observers may not add stories at this time") ;
            Response.Redirect("msg.aspx?msg=" + msg );
            Response.End();
        }

    }
}
