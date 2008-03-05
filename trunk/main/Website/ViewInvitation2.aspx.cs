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

public partial class ViewInvitation2 : System.Web.UI.Page
{
    private string guid = null;
    private utils ute = new utils();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.divActionButtons.Visible = false;

        guid = (string) Request["guid"];
        Invitation invite = new Invitation(config.conn, guid);

        User u = new User(config.conn);
        User userInviteSentTo = u.populateUserViaInviteCode(invite.InviteCode.Trim());


        if (userInviteSentTo.State == ((int)UserStates.accepted))
        {
            //invite picked up alread, redirect to login page
            //Response.Redirect("login.aspx?userID=" + userInviteSentTo.ID + "&msg=" + Server.UrlEncode("You are already a member, login ( default password is pwd "));
            invite.State = InviteStates.alreadyPickedUp;
            this.display(invite);
        }
        else
            if (userInviteSentTo.State == ((int)UserStates.added) || userInviteSentTo.State == ((int)UserStates.viewed_invite))
            {
                invite.State = InviteStates.open;
                this.display(invite);
                userInviteSentTo.State = (int)UserStates.viewed_invite;
                userInviteSentTo.Save();
                
                //$to do: invite.Sate = viewed?
            }
            else
            {
                log("Invalid operation for user of state " + userInviteSentTo.StateHR);
            }
    }

    private void display(Invitation invite)
    {
        System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();

        sbHtml.Append("<table>");
        sbHtml.Append("<tr>");
        sbHtml.Append("   <td><div class='clsInviteTitle'>");
        sbHtml.Append( ute.decode64( invite.Title ));
        sbHtml.Append("</div></td>");
        sbHtml.Append("</tr>");

        

        sbHtml.Append("<tr>");
        sbHtml.Append("   <td>");
        sbHtml.Append( ute.decode64( invite.InvitationText ));
        sbHtml.Append("   </td>");
        sbHtml.Append("</tr>");

        sbHtml.Append("</table>");



        this.divInviteAttachPoint.InnerHtml = sbHtml.ToString();

       

        if (invite.State == InviteStates.alreadyPickedUp)
        {
            this.divActionButtons.Visible = false;
            this.divInviteCodeAttachPoint.InnerHtml = "Invite with unique id of <i> " + invite.GUID + "</i>" + " state is accepted ";
        }
        else
        {
            if (invite.State == InviteStates.open)
            {
                this.divInviteCodeAttachPoint.InnerHtml = "<b>" + invite.InviteCode + "</b>";
                this.divActionButtons.Visible = true;
            }
        }
        



    }
    protected void cmdContinue_ServerClick(object sender, EventArgs e)
    {
        guid = (string)Request["guid"];

        Invitation invite = new Invitation(config.conn, guid);

        string inviteCode = invite.InviteCode;

        User u = new User(config.conn);
        User acceptingUser = u.populateUserViaInviteCode(inviteCode.Trim());

        if (acceptingUser.State == (int)UserStates.declined)
        {
            log("This invitation was previously declined.  If you have changed your mind, please email the webmaster at " + config.webMasterEmail);
            return;
        }


        if (invite.InviteCode.ToUpper() == this.txtInviteCode.Value.Trim().ToUpper())
        {



            if (invite.to.Email != null)
            {
                //good got email, can proceed
                invite.to.State = (int)UserStates.accepted;
                invite.to.Save();

                Response.Redirect("login.aspx?userID=" + invite.to.ID + "&msg=" + Server.UrlEncode("You are now a confirmed member, please login with default password of 'pwd'") );
            }
            else
            {
                //no email associated with this user, so ask them for it.
                Response.Redirect("AddEmailToUser.aspx?userID=" + invite.to.ID);
            }

        }
        else
        {
            log("Invalid INVITE code, please try again or email "+ config.webMasterEmail +" for help!");
        }

    }
    protected void cmdDecline_ServerClick(object sender, EventArgs e)
    {
        guid = (string)Request["guid"];

        Invitation invite = new Invitation(config.conn, guid);
        string inviteCode = invite.InviteCode;

        User u = new User(config.conn);
        User decliningUser = u.populateUserViaInviteCode(inviteCode.Trim());
        decliningUser.State = (int) UserStates.declined;
        decliningUser.Save();

        log("User account state set to 'declined' invitation.  If you'd like to reactivate your account, please contact the webmaster at " + config.webMasterEmail);

        

    }

    private void log(string msg)
    {
        this.divMsg.InnerHtml = "<b>" + msg + "</b>";
    }
}
