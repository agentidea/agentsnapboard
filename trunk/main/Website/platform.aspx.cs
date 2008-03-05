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

using AgentStoryComponents;

public partial class platform : SessionAwareWebForm
{

    private string _storiesJSON;

    public string getStoriesJSON()
    {
        return _storiesJSON;
        
    }


    public User CurrentUser
    {
        get { return base.currentUser; }
        
    }
	
	
    protected void Page_Load(object sender, EventArgs e)
    {
        //call base first
        
        base.Page_Load(sender, e,this.divToolBarAttachPoint,this.divFooter);

       
        string html = "";
        html += config.logoText +" story table of contents:";



        Stories stories = null;

        //filter with to story permissions
        stories = new Stories(config.conn, base.currentUser);


        //html += "<table>";

        int storyCursor = 0;
        System.Text.StringBuilder storyMetaJSON = new System.Text.StringBuilder();

        storyMetaJSON.Append("{");
        

        storyMetaJSON.Append("'stories':{");
        foreach (Story story in stories.StorieList)
        {
            storyCursor++;
            storyMetaJSON.Append("'story_" + storyCursor);
            storyMetaJSON.Append( "':{");
                storyMetaJSON.Append( "'Title':");
                storyMetaJSON.Append( "'");
                storyMetaJSON.Append( story.Title);
                storyMetaJSON.Append( "'");
                storyMetaJSON.Append( ",");
                storyMetaJSON.Append( "'ID':");
                storyMetaJSON.Append( story.ID);
                storyMetaJSON.Append(",");
                storyMetaJSON.Append("'TypeStory':");
                storyMetaJSON.Append(story.TypeStory);
                storyMetaJSON.Append(",");
                storyMetaJSON.Append( "'UniqueHits':");
                storyMetaJSON.Append( story.statBag.numViews);
                storyMetaJSON.Append(",");

                if (story.statBag.lastEditedBy != null)
                {
                    storyMetaJSON.Append("'LastEditedBy':'");
                    storyMetaJSON.Append(story.statBag.lastEditedBy.UserName);
                    storyMetaJSON.Append("',");
                    storyMetaJSON.Append("'LastEditedWhen':'");
                    storyMetaJSON.Append(story.statBag.dayLastEdited);
                    storyMetaJSON.Append("',");
                }

                if (story.CanEdit == true)
                {
                    storyMetaJSON.Append("'CanEdit':1,");
                }
                else
                {
                    storyMetaJSON.Append("'CanEdit':0,");
                }

                if (story.CanView == true)
                {
                    storyMetaJSON.Append("'CanView':1,");
                }
                else
                {
                    storyMetaJSON.Append("'CanView':0,");
                }

                storyMetaJSON.Append("'Author':");
                storyMetaJSON.Append("'");
                storyMetaJSON.Append(story.by.UserName );
                storyMetaJSON.Append("'");
                storyMetaJSON.Append(",");
                storyMetaJSON.Append("'AuthorID':");
                storyMetaJSON.Append(story.by.ID);
                storyMetaJSON.Append(",");
                storyMetaJSON.Append("'Added':");
                storyMetaJSON.Append("'");
                storyMetaJSON.Append( story.DateAdded );
                storyMetaJSON.Append("'");



           storyMetaJSON.Append( "}");
           storyMetaJSON.Append( ",");
        }
        if( stories.StorieList.Count > 0)
            storyMetaJSON.Remove(storyMetaJSON.Length - 1, 1);

        storyMetaJSON.Append( "}");
        storyMetaJSON.Append(",");
        storyMetaJSON.Append("'count':");
        storyMetaJSON.Append(storyCursor);
        storyMetaJSON.Append( "}");

        this._storiesJSON = storyMetaJSON.ToString();
        
        this.divBodyAttachPoint.InnerHtml = html;


    }
}
