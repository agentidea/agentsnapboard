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

using System.Text;
using AgentStoryComponents;
using AgentStoryComponents.core;

public partial class EditUserInfo : SessionAwareWebForm
{

    public string logoText = config.logoText;

    private User UserBeenEdited = null;


    public User CurrentUser
    {
        get { return base.currentUser; }
    }

    public string userJSON
    {
        get
        {
            return UserBeenEdited.ToJSONString();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        //call base first
        base.Page_Load(sender, e,this.divToolBarAttachPoint,this.divFooter);

        if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
        {
            Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Public user may not perform this operation."));
            Response.End();
        }

        string userGUID = Request.QueryString["UserGUID"];

        UserBeenEdited = base.currentUser;

        if (userGUID != null)
        {
            User u = null;
            try
            {
                u = new User(config.conn, new Guid(userGUID), "userGUID");
                if (u != null) UserBeenEdited = u;
            }
            catch (UserDoesNotExistException udneex)
            {
                throw new UserDoesNotExistException(" no user found for GUID " + userGUID);
            }
        }


        string msg = Request["msg"];
        if (msg != null && msg.Trim().Length > 0)
            this.displayMsg(msg);


               
        

    }

    private void displayMsg(string msg)
    {
        this.divMsgAttachPoint.InnerHtml = msg;
    }

    //private void displayUserDemographics(User user)
    //{
    //    StringBuilder html = new StringBuilder();

    //    //display a user form here
    //    html.Append(" <table> ");
    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("   Username: ");
    //    html.Append("    </td> ");
    //    html.Append("    <td> ");
    //    html.Append(" <input type='text' id='txtUserName' name='txtUserName' value='");
    //    html.Append(       user.UserName.Trim() );
    //    html.Append("' />");
    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("   Role(s): ");
    //    html.Append("    </td> ");
    //    html.Append("    <td> ");
        
    //    html.Append(user.Roles.Trim());
        
    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("<div title='When to alert me via email about Story(ies) changes.'>Story Notification:</div> ");
    //    html.Append("    </td> ");
    //    html.Append("    <td> ");
    //    html.Append(" <select id='selNotificationSchedule' name='selNotificationSchedule' onchange='form1.hdnNotificationSchedule.value=this.options[this.selectedIndex].id;'>");

    //    html.Append("<option id='0' title='No email notifications will be sent.'");
    //    if (user.NotificationFrequency == 0) html.Append(" SELECTED ");
    //    html.Append(">NEVER</option>");

    //    html.Append("<option id='1' title='As Story changes are made, email me immediately.'");
    //    if (user.NotificationFrequency == 1) html.Append(" SELECTED ");
    //    html.Append(">IMMEDIATELY</option>");

    //    html.Append("<option id='2' title='Send out a daily email report of Story changes.'");
    //    if (user.NotificationFrequency == 2) html.Append(" SELECTED ");
    //    html.Append(">DAILY ( 5PM PST ) digest</option>");

    //    html.Append("<option id='3' title='Send out a weekly email report of Story changes.'");
    //    if (user.NotificationFrequency == 3) html.Append(" SELECTED ");
    //    html.Append(">WEEKLY ( Sunday 10AM PST ) digest</option>");
        
        
    //    html.Append("</select>");
    //    html.Append("<input type='hidden' name='hdnNotificationSchedule' value='");
    //    html.Append(user.NotificationFrequency);
    //    html.Append("' /> ");
    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("   First Name: ");
    //    html.Append("    </td> ");
    //    html.Append("    <td> ");
    //    html.Append(" <input type='text' id='txtFirstName' name='txtFirstName' value='");
    //    html.Append(user.FirstName.Trim() );
    //    html.Append("' />");
    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("   Last Name: ");
    //    html.Append("    </td> ");
    //    html.Append("    <td> ");
    //    html.Append(" <input type='text' id='txtLastName' name='txtLastName' value='");
    //    html.Append(user.LastName.Trim() );
    //    html.Append("' />");
    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("   Email: ");
    //    html.Append("    </td> ");
    //    html.Append("    <td> ");
    //    html.Append(" <input type='text' id='txtEmail' name='txtEmail' value='");
    //    html.Append(user.Email.Trim() );
    //    html.Append("' />");
    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td> ");
    //    html.Append("   Password: ");
    //    html.Append("    </td> ");
    //    html.Append("    <td>");
    //    html.Append(" <input type='text' id='txtPassword' name='txtPassword' value='");
    //    html.Append(user.Password.Trim() );
    //    html.Append("' ");

    //    if (user.Password.Trim().ToLower() == "pwd")
    //    {
    //        html.Append(" style='background-color:red;' ");
    //        html.Append(" title='please change default password!' ");
    //    }
    //    if (user.Password.Trim().Length < 6)
    //    {
    //        html.Append(" style='background-color:yellow;' ");
    //        html.Append(" title='password appears weak, try 6-8 characters.'");
    //    }


    //    html.Append(" />");

    //    html.Append("    </td> ");
    //    html.Append(" </tr> ");

    //    html.Append(" <tr> ");
    //    html.Append("    <td colspan='1'> ");
    //    html.Append(" <input type='button' name='cmdSave' id='cmdSave' onclick='SaveUserInfo();' value='Save Profile Information' class='clsButtonAction'/>");
    //    html.Append("    </td> ");
    //    html.Append("    <td colspan='1'> ");
    //    html.Append(" <input type='button' id='cmdRemove' name='cmdRemove'  onclick='RemoveUser();' value='Terminate my membership' class='clsButtonAction'/>");
    //    html.Append(" <input type='hidden' name='hdnAction' value='' />");
    //    html.Append("    </td> ");
       
    //    html.Append(" </tr> ");



    //    html.Append(" </table> ");










    //    this.divBodyAttachPoint.InnerHtml = html.ToString();


    //}
}
