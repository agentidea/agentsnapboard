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
    public class LoginBase : System.Web.UI.Page
    {

        protected User currentUser = null;
        protected void Page_Load(object sender, EventArgs e, HtmlGenericControl divToolbar, HtmlGenericControl divFooter)
        {

            //currentUser = Session["user"] as User;

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

                //if got here ready to go to the platform page?  or last logged in story and (page)?
                Response.Redirect("Platform2.aspx");
                Response.End();
                return;
            }
        }

    }
}
