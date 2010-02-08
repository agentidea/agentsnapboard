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
    public partial class msg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = Request["msg"];
            string userId = Request["userID"];
            int uid = System.Convert.ToInt32(userId);
            User u = new User(config.conn, uid);

            string html = "<b>" + msg + "</b>";
            html += "<br>";
            html += "<br>";
            html += "<br>";
            html += "<a href='mailto:" + config.webMasterEmail + "?subject=" + Server.UrlEncode(msg) + "'>email the webmaster for help with this message</a>";
            this.divMsgAttachPoint.InnerHtml = html;
        }
    }
}
