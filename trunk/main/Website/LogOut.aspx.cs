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
using AgentStoryComponents.core;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        AgentStoryComponents.User u = null;
        string usrname = "";



        try
        {
            u = Session["user"] as AgentStoryComponents.User;

            usrname = u.UserName;

            if (u != null)
            {
                //$TODO: LOG EASIER
                utils ute = new utils();
                string msg = "user " + u.UserName + " logged out at " + ute.getDateStamp();
                StoryLog sl = new StoryLog(config.conn);
                sl.AddToLog(msg);
                sl = null;
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
