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

public partial class AccountActivation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string requestActivationCode = Request["ac"];
        if (requestActivationCode != null && requestActivationCode.Trim().Length > 0)
        {
            if (!IsPostBack)
                this.txtActivationCode.Text = requestActivationCode.Trim();
        }


        this.lblTOS.Text = config.TOS;


        string msg = Request["msg"];
        if (msg != null && msg.Trim().Length > 0)
        {
            this.lblMsg.Text = msg;
        }
        else
        {
            this.lblMsg.Text = "";
        }


    }
    protected void cmdActivate_Click(object sender, EventArgs e)
    {
        
        
        string activationCode64 = this.txtActivationCode.Text.Trim();

       // User newUser = new User(
        string activationCode = TheUtils.ute.decode64(activationCode64);

        

        try
        {
            Guid guid = new Guid(activationCode);
            AgentStoryComponents.User u = new User(config.conn, guid, "pendingGUID");

            if (u != null)
            {

                if (u.State == (int)UserStates.accepted)
                {
                    Response.Redirect("./msg.aspx?msg=Account%20Already%20Activated");
                    return;

                }

                u.State = (int)UserStates.pre_approved;

                Logger.log("preapproved " + u.UserName);

                u.DateActivated = DateTime.Now;

                u.Save();


                //as part of account activation create a default story for this user?
                Story userStory = new Story(config.conn, u);
                userStory.Title = TheUtils.ute.encode64(u.UserName + "'s story ");
                userStory.Description = TheUtils.ute.encode64(u.FirstName + " describe something about yourself here");

                userStory.TypeStory = 1;
                userStory.Save();

                userStory.AddUserEditor(u);
                userStory.AddUserViewer(u);

                //to a defualt page it has been decided that there will be a default TEXT page element representing the page name.
                AgentStoryComponents.Page defaultPage = new AgentStoryComponents.Page(config.conn, u);
                defaultPage.GridX = 800;
                defaultPage.GridY = 800;
                defaultPage.GridZ = 0;
                defaultPage.Name = TheUtils.ute.encode64("( default )");
                defaultPage.Save();

                //to a defualt page it has been decided that there will be a default TEXT page element representing the page name.
               
                PageElement pe = new PageElement(config.conn, u);
                pe.TypeID = 1; //text

                string defPageTxt = config.defaultPEtext;


                pe.Value = TheUtils.ute.encode64(defPageTxt);
                pe.Save();

                userStory.AddPageElement(pe);

                PageElementMap pem = new PageElementMap(config.conn);
                pem.PageElementID = pe.ID;
                pem.GridX = 280; //ouch!!!
                pem.GridY = 230;
                pem.GridZ = 1;
                pem.Save();


                defaultPage.AddPageElementMap(pem, true);

                userStory.AddPage(defaultPage);

                Logger.log(" activated new user " + u.UserName + " [" + u.ID + " {" + u.UserGUID + "}]");


                Response.Redirect("./PostAccountActivation.aspx");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Unsuccesfull activation attempt " + ex.Message);
        }

       




    }
    protected void chkAcceptTOS_CheckedChanged(object sender, EventArgs e)
    {
        this.cmdActivate.Enabled = chkAcceptTOS.Checked;
    }
}
