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


public partial class login : LoginBase
{



    public string PageTitle
    {
        get
        {

            return config.Orginization + " - login";
        }
    }

    public string ClubName
    {
        get { return config.logoText; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);



        string msg = Request.QueryString["msg"];
        if (msg != null && msg.Trim().Length > 0)
        {
            this.lblPageMessage.Text = msg;
        }
        else
        {
           // this.lblPageMessage.Text = config.defaultLoginMessage;
            Response.Write("<div class='clsLoginMsg'>" + config.defaultLoginMessage + "</div>");
        }


    }


  
}
