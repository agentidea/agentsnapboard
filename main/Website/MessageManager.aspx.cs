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


public partial class MessageManager : SessionAwareWebForm
{
    public User CurrentUser
    {
        get { return base.currentUser; }

    }

    private string _myMessageMetaJSON;
    public string myMessageMetaJSON
    {
        get
        {
            Emails emails = new Emails(config.conn);
            _myMessageMetaJSON = emails.getEmailMsgMetaJSON(this.CurrentUser);
            return _myMessageMetaJSON;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        //call base first
        base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

        if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
            Response.End();
        }
    }
}
