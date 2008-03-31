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
    public partial class StoryReplay : PossibleSessionAwareWebForm
    {
        private Story _story;
        public Story oStory
        {
            get { return _story; }
            set { _story = value; }
        }

        public string PageTitle
        {
            get
            {
                return TheUtils.ute.decode64(this.oStory.Title);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl toolBarREF = null;
            base.Page_Load(sender, e, toolBarREF, null);

            string sStoryID = Request["StoryID"];
            int nStoryID = Convert.ToInt32(sStoryID);

            try
            {
                this.oStory = new Story(config.conn, nStoryID, base.currentUser);
                if (this.oStory.CanView || this.oStory.CanEdit)
                {
                    //good proceed
                    Response.Write("<script language='JavaScript'>var bProceed = true; </script>");
                }
                else
                {
                    Response.Write("<script language='JavaScript'>var bProceed = false; </script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='JavaScript'>var bProceed = false; alert('"+ ex.Message +"');</script>");
            }
        }
    }
}
