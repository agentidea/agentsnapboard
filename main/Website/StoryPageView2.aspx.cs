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

public partial class StoryPageView2 : PossibleSessionAwareWebForm
{
    private string _storyMetaJSON;

    public string StoryMetaJSON
    {
        get { return _storyMetaJSON; }

    }
    public string IncludeMaster
    {
        get { return config.includeMasterURL; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        System.Web.UI.HtmlControls.HtmlGenericControl toolBarREF = null;
        base.Page_Load(sender, e, toolBarREF, null);
        string sStoryIDs = Request["StoryIDs"];

        this.buildStoryEnvelope(sStoryIDs);
       



    }

    private void buildStoryEnvelope(string delimitedStoryIDs)
    {
        string[] aStories = delimitedStoryIDs.Split('|');

        StringBuilder json = new StringBuilder();

        json.Append("{");
        json.Append("'stories':");
        json.Append("[");
        

        foreach (string sID in aStories)
        {
            Story currStory = new Story(config.conn, System.Convert.ToInt32(sID),base.currentUser);
            json.Append("{");
            json.Append("'title':'");
            json.Append(currStory.Title);
            json.Append("',");
            json.Append("'id':'");
            json.Append(currStory.ID);
            json.Append("'");

            json.Append(",");
            json.Append("'pages':");
            json.Append("[");

            foreach (AgentStoryComponents.Page page in currStory.Pages)
            {
                json.Append("{");
                json.Append("'id':");
                json.Append(page.ID);
                json.Append(",");
                json.Append("'GUID':");
                json.Append("'");
                json.Append(page.GUID);
                json.Append("'");
                json.Append(",");
                json.Append("'title':");
                json.Append("'");
                json.Append(page.Name);
                json.Append("'");
                json.Append(",");

                json.Append("'seq':");
                json.Append(page.Seq);
                json.Append("}");
                json.Append(",");

            }

            if( currStory.Pages.Length > 0)
                json.Remove(json.Length - 1, 1);

            json.Append("]");


            json.Append("}");




            json.Append(",");
            //TheUtils.ute.decode64(currStory.Title);

        }

        if (aStories.Length > 0)
            json.Remove(json.Length - 1, 1);

        json.Append("]");
        json.Append("}");

        this._storyMetaJSON = json.ToString();

    }

    //private string buildListDataJSON(Story s)
    //{
    //    StringBuilder json = new StringBuilder();

    //    //var passedListStruc2 = {"listItems":[{"name":"guava","val":21,"seq":3},{"name":"kiwi","val":21,"seq":3},{"name":"quince","val":119,"seq":11},{"name":"star fruit","val":139,"seq":-1}]};


    //    json.Append("{");
    //    json.Append("'name':'");
    //    json.Append(TheUtils.ute.decode64(s.Title));
    //    json.Append("',");
    //    json.Append("val:'");
    //    json.Append( s.ID );
    //    json.Append("'");
    //    json.Append("");
    //    json.Append("}");



    //    return json.ToString();

    //}
}
