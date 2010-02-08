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
using System.Collections.Generic;

using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryHTTP.screens
{
    public partial class StoryOrig : PossibleSessionAwareWebForm
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

        if (base.currentUser != null)
        {
            this._currUserID = base.currentUser.ID;
        }

        string sStoryID = Request["StoryID"];

        if (sStoryID == null)
        {
            Response.Write("StoryID parameter on URL expected");
            Response.End();
            return;
        }


        string msg = null;

        int nStoryID = -1;

        if (sStoryID != null)
        {
            nStoryID = Convert.ToInt32(sStoryID);

            this.oStory = new Story(config.conn, nStoryID,base.currentUser);
            //decorate permissions for viewer tool
            this.oStory.decorateStoryACL2(base.currentUser);

            if (base.currentUser != null)
            {
                //record unique view
                stats stat = new stats();
                stat.addUserStory_hit(base.currentUser, this.oStory);
               
                msg = base.currentUser.UserName + " is looking at ( " + TheUtils.ute.decode64(this.oStory.Title) + " )";
               
            }
            else
            {
                Group publicGroup = new Group(config.conn, TheUtils.ute.encode64("public"));
                //cHVibGlj

                //is this story viewable by public?

               // if (oStory.StoryViewerGroups.Contains(publicGroup))
                if( containsGroup( oStory.StoryViewerGroups, publicGroup ) )
                {
                    //public can view
                    msg = "Not logged in viewer is looking at ( " + TheUtils.ute.decode64(this.oStory.Title) + " )";    
                }
                else
                {
                    //story not viewable by public.
                    msg = "Not logged in viewer is trying to view a non public story - " + TheUtils.ute.decode64(this.oStory.Title);
                    Response.Write("Story not public, please login to view");
                    Response.End();
                    return;
                }

               
            }

           
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

        }

        
        
    }


    private bool containsGroup(List<Group> grp, Group target)
    {
        foreach (Group g in grp)
        {
            if (target.ID == g.ID)
            {
                return true;
            }

        }
        return false;
    }
 }

}
