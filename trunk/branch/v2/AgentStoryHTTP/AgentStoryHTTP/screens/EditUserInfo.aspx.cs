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
using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryHTTP.screens
{
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
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

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

    }
}
