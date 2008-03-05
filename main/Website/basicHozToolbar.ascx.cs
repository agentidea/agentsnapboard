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

public partial class basicHozToolbar : System.Web.UI.UserControl
{

    public string IncludeMaster
    {
        get { return config.includeMasterURL; }
    }

    public string ClubLogo
    {
        get { return config.logoText; }
       
    }

    public int HelpStoryID
    {
        get { return config.helpStoryID; }
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        User currentUser = Session["user"] as User;

        if (currentUser != null) ;
            this.divUserNameAttachPoint.InnerHtml = currentUser.UserName;

            //this.mnuActivity.Visible = false;
            //this.mnuCreateNewStory.Visible = true;
            //this.mnuMemberOperations.Visible = true;
            //this.mnuMessageManager.Visible = true;

            //if (currentUser.isRoot() || currentUser.isAdmin() )
            //{
            //    this.mnuActivity.Visible = true;
            //}

            if (currentUser.ID == config.publicUserID)
            {
                this.dvLogOut.Visible = false;
            }
    }
}
