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

using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;
using AgentStoryComponents.core;

public partial class StoryPageView : PossibleSessionAwareWebForm
{

    public string IncludeMaster
    {
        get { return config.includeMasterURL; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        System.Web.UI.HtmlControls.HtmlGenericControl toolBarREF = null;
        base.Page_Load(sender, e, toolBarREF, null);
        string sStoryID = Request["StoryID"];

        Story currStory = new Story(config.conn, System.Convert.ToInt32(sStoryID));

        if( currStory.Title != null)
            Response.Write(" curr story is <i>" + TheUtils.ute.decode64( currStory.Title ) + "</i>");


    }
}
