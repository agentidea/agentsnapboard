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

using System.Collections.Generic;

using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryHTTP.screens
{

    public partial class logReader : System.Web.UI.Page
    {

        private utils ute = new utils();

        protected void Page_Load(object sender, EventArgs e)
        {


            //build simple log reader table
            StoryLog storyLog = new StoryLog(config.conn);
            List<StoryLogMessage> storyLogMessages = storyLog.StoryLogList;

            System.Text.StringBuilder html = new System.Text.StringBuilder();
            html.Append("<table border='1' cellpadding='0' cellspcing='0' class='clsTableStoryLogMessage'>");

            foreach (StoryLogMessage slm in storyLogMessages)
            {

                string msg = null;

                try
                {
                    msg = ute.decode64(slm.msg);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                html.Append("<tr>");
                html.Append("<td><div class='clsStoryLogMessage'>");
                html.Append(slm.id);
                html.Append("</div></td>");
                html.Append("<td><div class='clsStoryLogMessage' title='");
                html.Append(msg);
                html.Append("'>");
                html.Append(msg);
                html.Append("</div></td>");
                html.Append("<td><div class='clsStoryLogMessage'>");
                html.Append(slm.dateAdded);
                html.Append("</div></td>");
                html.Append("</tr>");
            }

            html.Append("</table>");

            this.procAttachPoint.InnerHtml = html.ToString() + "<br><br><i>" + System.DateTime.Now.ToString() + "</i>";
        }




    }


}