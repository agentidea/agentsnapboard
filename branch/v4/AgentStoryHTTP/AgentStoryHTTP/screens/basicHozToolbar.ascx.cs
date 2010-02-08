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

namespace AgentStoryHTTP.screens
{
    public partial class basicHozToolbar : System.Web.UI.UserControl
    {
        public string IncludeMaster
        {
            get { return config.includeMasterURL; }
        }

        public string ClubLogo
        {
            get { return config.logoText; }

        }

            

        public int HelpStoryID
        {
            get { return config.helpStoryID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = Session["user"] as User;

            if (currentUser != null) ;
            this.divUserNameAttachPoint.InnerHtml = currentUser.UserName;

            if (currentUser.ID == config.publicUserID)
            {
                this.dvLogIn.Visible = true;
                this.dvLogOut.Visible = false;

            }
            else
            {
                this.dvLogIn.Visible = false;
                this.dvLogOut.Visible = true;
            }


        }
    }
}