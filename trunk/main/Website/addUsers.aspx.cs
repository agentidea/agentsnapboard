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

public partial class addUsers : SessionAwareWebForm
{

    private utils ute = new utils();
    protected void Page_Load(object sender, EventArgs e)
    {

        base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

        if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
            Response.End();
        }

        if(IsPostBack == false)
                 this.txtUsers.Value = "";
    }
    protected void cmdAddUsers_ServerClick(object sender, EventArgs e)
    {
        //take each line of users and process

        string commands = this.txtUsers.Value;
        string[] lines = commands.Split('~');
        int numUsers = 0;

        foreach (string line in lines)
        {
            if (line.Trim().Length == 0) break;

            string[] words = line.Split(',');
            User u = new User(config.conn);
            u.SponsorID = base.currentUser.ID;

            string fName = words[0].Trim();
            char[] fNameBits = fName.ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (char c in fNameBits)
            {
                if (c == 12 || c == 10)
                {

                }
                else
                {
                    sb.Append(c);
                }
            }

            u.FirstName = sb.ToString();

            u.LastName = words[1].Trim();
            u.UserName = words[2].Trim();
            u.Tags = words[3].Trim();
            u.Password = words[4].Trim();

            u.OrigInviteCode = words[7].Trim();
            string email = words[8].Trim();

            if (email.Length > 0)
            {
                u.Email = email;
            }

            u.Roles = words[9].Trim();
            u.State = Convert.ToInt32(words[14]);
            u.Save();

            string[] groups = words[15].Split('|');

            foreach (string group in groups)
            {
                if (group.Trim().Length > 0)
                {
                    Group grp = null;

                    try
                    {
                        grp = new Group(config.conn, ute.encode64( group.Trim()));
                        grp.AddUser(u);
                    }
                    catch (GroupDoesNotExistException gdnee)
                    {
                        User aish = base.currentUser;
                        grp = new Group(config.conn, aish);
                        grp.Name = ute.encode64( group.Trim() );
                        grp.Save();
                        grp.AddUser(u);
                        //grp.Save();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("error: " + exp.Message);
                    }
                }
            }
            
            
            numUsers++;
        }

        Response.Write(numUsers + " new users saved");


    }
}
