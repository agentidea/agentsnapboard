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

    public class PossibleSessionAwareWebForm : System.Web.UI.Page
    {

        protected User currentUser = null;

        public PossibleSessionAwareWebForm()
        {
        }



        protected void Page_Load(object sender, EventArgs e, HtmlGenericControl divToolbar, HtmlGenericControl divFooter)
        {

            currentUser = Session["user"] as User;

            string sessionID = Session.SessionID;
            int x = 1;
            string u64 = Request.Form["hdnUserName"];
            string p64 = Request.Form["hdnPassword"];

            if (u64 != null)
            {
                //attempt a SU
                string emailAddress = TheUtils.ute.decode64(u64);
                string password = TheUtils.ute.decode64(p64);

                try
                {

                    User u = new User(config.conn, emailAddress);
                    if (u.Password == password)
                    {
                        u.TxSessionID = u.UserName + "_" + sessionID;
                        Session["user"] = u;
                        this.currentUser = u;
                    }


                }
                catch (UserDoesNotExistException unex)
                {
                    //try login via username instead
                    User u = new User(config.conn, emailAddress, true);


                    if (u != null)
                    {
                        if (u.Password == password)
                        {
                            u.TxSessionID = u.UserName + "_" + sessionID;
                            Session["user"] = u;
                            this.currentUser = u;
                        }


                    }
                    else
                    {
                        Logger.log("bad credentials for :: " + emailAddress);
                        throw new UserDoesNotExistException("No user found matching these credentials");
                    }
                }

            }
            else
            {
                if (currentUser == null)
                {
                    // non logged in user looking at page.
                    // do no user checking...
                    // impersonate public
                    currentUser = new User(config.conn, config.publicUserEmail);
                    currentUser.TxSessionID = currentUser.UserName + "_" + sessionID;
                    Session["user"] = currentUser;
                }
                else
                {
                    //logged in user looking at page.
                    string msg = null;


                    if (currentUser.State == (int)UserStates.pending_email_confirm)
                    {
                        Response.Redirect("./AccountActivation.aspx?msg=Please Activate your account from the key we sent to your email address: " + currentUser.Email);
                        Response.End();
                        return;
                    }


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



                }
            }

            if (currentUser.State == (int)UserStates.added)
            {
                string msg = " Please check your email inbox and junk mail in case for an activation email";
                Logger.log(" User " + currentUser.UserName + "[" + currentUser.ID + "] attempting to login prior to activation");
                Response.Redirect("AccountActivation.aspx?msg=" + Server.UrlEncode(msg)); 
                //http://localhost:2014/AgentStory/AccountActivation.aspx
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
