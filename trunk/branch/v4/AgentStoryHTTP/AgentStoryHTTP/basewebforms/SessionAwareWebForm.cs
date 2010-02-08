using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryHTTP.screens
{
    public class SessionAwareWebForm : System.Web.UI.Page
    {

        protected User currentUser = null;

        protected void Page_Load(object sender, EventArgs e, HtmlGenericControl divToolbar, HtmlGenericControl divFooter)
        {
            currentUser = Session["user"] as User;

            if (currentUser == null)
            {
                Response.Redirect("Platform2.aspx?msg=" + Server.UrlEncode("You need to be logged in to do that."));
                return;
            }


            string msg = null;

            //check if there is a pending GUID action, or if the states are not correct.
            //if (currentUser.PendingGUID != null)
            //{
            //    msg += " your account is pending email verification ";
            //}

            if (currentUser.State == (int)UserStates.signed_in || currentUser.State == (int)UserStates.accepted || currentUser.State == (int)UserStates.pre_approved)
            {
                //user was either self accepted or preapproved or signed in
            }
            else
            {
                msg += currentUser.UserName + " your user account state is [" + currentUser.StateHR + "]";
            }

            if (msg != null)
            {
                Response.Redirect("msg.aspx?msg=" + Server.UrlEncode(msg));
                return;
            }


            if (divToolbar != null)
            {

                //render toolbar(s)
                Control ctlBasicHozToolbar = LoadControl("./basicHozToolbar.ascx");
                divToolbar.Controls.Add(ctlBasicHozToolbar);
            }

            if (divFooter != null)
            {
                //render footer
                Control ctlBasicFooter = LoadControl("./basicFooter.ascx");
                divFooter.Controls.Add(ctlBasicFooter);
            }



        }
    }
}
