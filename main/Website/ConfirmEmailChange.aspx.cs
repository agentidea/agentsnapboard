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

public partial class ConfirmEmailChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string guid = Request["guid"];
        /*
         * 
         http://localhost:1033/AgentStory/ConfirmEmailChange.aspx?guid=5bebf984-5671-4af3-a51a-dc10a72a526d
         * 
         */

        System.Guid pg = new Guid(guid);
        //so now with this url the user is loaded off a PendingGUID

        User userConfirmingEmail = new User(config.conn, pg,"pendingGUID");

        userConfirmingEmail.State = (int)UserStates.accepted;
        userConfirmingEmail.PendingGUID = null;

        userConfirmingEmail.Save();

        Response.Redirect("login.aspx?userID=" + userConfirmingEmail.ID + "&msg=" + Server.UrlEncode("Your email and membership has been confirmed.") );

    }
}
