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
using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;


namespace AgentStoryHTTP.screens
{
    public partial class addSingleUser : SessionAwareWebForm
    {
        public UserGroupHelper ugh = null;

        public User CurrentUser
        {
            get { return base.currentUser; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

            

        }

    }
}
