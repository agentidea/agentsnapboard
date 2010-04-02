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

            string msg = Request.QueryString["msg"];
            //pass login to base
            try
            {
                base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);
           
            }
            catch (Exception exp)
            {

                msg += exp.Message;
            }

            var m = string.Empty;
            m += config.defaultLoginMessage;

            if (msg != null && msg.Trim().Length > 0)
            {
                m += string.Format("<div style='color:red;font-size:10;font-weight:bolder;'>{0}</div>", msg);
                Response.Write("<div class='clsLoginMsg'>" + m + "</div>");
            }
            else
            {
               Response.Write("<div class='clsLoginMsg'>" + config.defaultLoginMessage + "</div>");
            }


        }
    }
}
