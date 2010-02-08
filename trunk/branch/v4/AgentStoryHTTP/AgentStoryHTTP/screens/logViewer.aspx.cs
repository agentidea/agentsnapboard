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
    public partial class logViewer : SessionAwareWebForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e, null, null);

            if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
            {
                Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
                Response.End();
            }

        }
    }
}
