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

public partial class StoryEditor : SessionAwareWebForm
{
    private Story _story;

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
        base.Page_Load(sender, e,this.divToolBarAttachPoint,this.divFooter);


        this._currUserID = base.currentUser.ID;

        string sStoryID = Request["StoryID"];
        int nStoryID = -1;
        if (sStoryID != null)
        {
            nStoryID = Convert.ToInt32(sStoryID);

            this.oStory = new Story(config.conn, nStoryID);
            

        }
        
    }
}
