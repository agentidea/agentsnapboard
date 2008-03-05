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

public partial class CreateInvitations2 : SessionAwareWebForm
{

    public UserGroupHelper ugh = null;

    public User CurrentUser
    {
        get { return base.currentUser; }
    }


    

    public string FromUserNameList
    {
        get
        {
            string _usernameList = "";

            if (base.currentUser.isRoot())
            {
                _usernameList = ugh.getPlistUsersNames();
            }
            else
            {
                _usernameList = base.currentUser.UserName;
            }
            return _usernameList; 
        }
    }
    public string FromUserIDList
    {
        get
        {
            string _userIDlist = "";

            if (base.currentUser.isRoot())
            {
                _userIDlist = ugh.getPlistUsersID();
            }
            else
            {
                _userIDlist = base.currentUser.ID + "";
            }
            return _userIDlist;
        }
    }
	


    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

        if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
            Response.End();
        }

        try
        {
            ugh = new UserGroupHelper();
        }
        catch (Exception ex)
        {
            string msg = Server.UrlEncode(" there are no groups in the system yet, please add one.");
            Response.Redirect("AddNewGroup.aspx.aspx?msg=" + msg);
            Response.End();
        }


        if (base.currentUser.isObserver())
        {
            string msg = Server.UrlEncode(" observers may not create invitations at this time");
            Response.Redirect("msg.aspx?msg=" + msg);
            Response.End();
        }

    }

}
