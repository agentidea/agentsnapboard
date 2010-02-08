using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AgentStoryComponents.User u = null;
            string usrname = "";



            try
            {
                u = Session["user"] as AgentStoryComponents.User;



                if (u != null)
                {
                    usrname = u.UserName;
                    //$TODO: LOG EASIER
                    utils ute = new utils();
                    string msg = "user " + u.UserName + " logged out at " + ute.getDateStamp();
                    StoryLog sl = new StoryLog(config.conn);
                    sl.AddToLog(msg);
                    sl = null;
                }
                else
                {
                    usrname = "timed out client";
                }

            }
            catch (Exception ex)
            {
                // throw new Exception("undhandled exception " + ex.Message);
            }
            finally
            {
                //log off 
                Session["user"] = null;
                Session["loggedIn"] = false;
                Session.Clear();
                Session.Abandon();


                Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode(" Goodbye " + usrname));

            }


        }
    }
}
