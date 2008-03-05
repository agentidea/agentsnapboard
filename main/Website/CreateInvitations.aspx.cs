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

using System.Collections.Generic;

using AgentStoryComponents;

public partial class CreateInvitations : SessionAwareWebForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //call base first
        base.Page_Load(sender, e,this.divToolBarAttachPoint,this.divFooter);

        if (base.currentUser.isAdmin() || base.currentUser.isRoot())
        {
            //proceed, do nothing
        }
        else
        {
            Response.Redirect("msg.aspx?msg=" + Server.UrlEncode("You do not have permission to use tools"));
            return;
        }

        if (IsPostBack == false)
        {
            Users us = new Users(config.conn);
            List<User> users = us.UserList;

            foreach (User usr in users)
            {
                this.listBoxUsers.Items.Add(new ListItem(usr.UserName, "" + usr.ID));
                this.selUsrFrom.Items.Add(new ListItem(usr.UserName, "" + usr.ID));
            }



        }
    }
    protected void cmdCreateInvites_Click(object sender, EventArgs e)
    {
        //get selected items and create invitations
        ListItem fromUser = this.selUsrFrom.SelectedItem;
        int fromUserId = Convert.ToInt32(fromUser.Value);
        User from = new User(config.conn, fromUserId);

        List<int> selUserList = new List<int>();

        foreach (ListItem listItem in this.listBoxUsers.Items)
        {
            if (listItem.Selected == true)
            {
                selUserList.Add(Convert.ToInt32(listItem.Value));
            }
        }

        foreach (int uid in selUserList)
        {
            User to = new User(config.conn, uid);

            Invitation i = new Invitation(config.conn, from, to);
            i.Title = this.txtTitle.Value.Trim();
            i.InvitationText = this.txtGreeting.Value.Trim() + " " + to.UserName;
            i.InvitationText += "\r\n";
            i.InvitationText += this.txtBody.Value.Trim();
            i.InviteCode = to.OrigInviteCode;
            i.Save();

            Response.Write("create invite [" + i.InviteCode + "] for user " + uid + "<a href='http://localhost:1033/AgentStory/ViewInvitation2.aspx?guid=" + i.GUID + "' >GUID " + i.GUID + "</a>");
            Response.Write("<br>");


        }


    }
}
