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


public partial class SendNewEmail : SessionAwareWebForm
{

    public UserGroupHelper ugh = null;

    public User CurrentUser
    {
        get { return base.currentUser; }

    }


    private string _emailJSON;

    public string emailJSON
    {
        get 
        {
            
            if (_emailJSON == null)
                _emailJSON = "{}";
            
            return _emailJSON; 
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        //call base first
        base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

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

        if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
            Response.End();
        }

        if (base.currentUser.isObserver())
        {
            string msg = Server.UrlEncode(" observers may not create new messages at this time");
            Response.Redirect("msg.aspx?msg=" + msg);
            Response.End();
        }


        //sending to
        string sToID = Request["idTo"];
        if (sToID != null)
        {
            if (sToID.IndexOf("|") != -1)
            {
                //possibly multiple destinations
                throw new NotImplementedException("multiple dest not supported yet");
            }
            else
            {
                //possible single user?
                int nUserID = Convert.ToInt32(sToID);
                // _to = new User(config.conn, nUserID);
            }
          
        }

    }
}
