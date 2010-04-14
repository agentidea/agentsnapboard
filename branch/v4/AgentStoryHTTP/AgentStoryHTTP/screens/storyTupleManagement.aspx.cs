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
    public partial class storyTupleManagement : SessionAwareWebForm
    {
        private Story _story;

        public UserGroupHelper ugh = new UserGroupHelper();

        public Story oStory
        {
            get { return _story; }
            set { _story = value; }
        }

        private int _currUserID;

        public int CurrUserID
        {
            get { return _currUserID; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);


            this._currUserID = base.currentUser.ID;

            string sStoryID = Request["StoryID"];
            int nStoryID = -1;
            if (sStoryID != null)
            {
                nStoryID = Convert.ToInt32(sStoryID);

                this.oStory = new Story(config.conn, nStoryID, base.currentUser);


            }

        }
    }

}
