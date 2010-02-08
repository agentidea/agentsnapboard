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
namespace AgentStoryHTTP
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //while waiting for Sausalito Ferry 
            string url = config.startEditor;
            url += "?StoryID=";
            url += config.startStoryID;
            url += "&PageCursor=";
            url += config.startStoryPage;
            url += "&toolBR=";
            url += config.startStoryToolBR;

            Response.Redirect(url);
        }
    }
}
