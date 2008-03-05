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
using AgentStoryComponents.core;
using AgentStoryComponents;

public partial class _Default : System.Web.UI.Page
{

    
    //public string PageTitle
    //{
    //    get { return config.HomePageTitle; }
        
    //}
    //public string MailContact
    //{
    //    get { return config.webMasterEmail; }
        
    //}
    //public string commaSeperatedTickerContent
    //{

    //    get
    //    {
    //        StringBuilder content = new StringBuilder();
    //        utils ute = new utils();

    //        User publicUser = new User(config.conn, config.publicUserEmail);
    //        content.Append("\"");
    //        content.Append(config.welcomeTo);
    //        content.Append(" ");
    //        content.Append(config.logoText);
    //        content.Append("\"");
    //        content.Append(" , ");

    //        content.Append("\"");
    //        content.Append(" These are some of our stories: ");
    //        content.Append("\"");
    //        content.Append(" , ");

    //        Stories stories = new Stories(config.conn,publicUser);
    //        foreach (Story  story in stories.StorieList)
    //        {
    //            content.Append("\"");
    //            content.Append(ute.decode64(story.Title));
    //            content.Append("\"");
    //            content.Append(",");
    //        }
    //        if (stories.StorieList.Count > 0)
    //        {
    //            content.Remove(content.Length - 1, 1);
    //        }
    //        else
    //        {
    //            content.Append("\"");
    //            content.Append("No no go not to lethe!  Ah no, no public stories found in db, please intialize :) ");
    //            content.Append("\"");
               
    //        }




    //        content.Append(",");
    //        content.Append("\"");
    //        content.Append("TOTAL number of users ");

    //        //content.Append(DateTime.Now.ToLongDateString());
    //        //content.Append(" ");
    //        //content.Append(DateTime.Now.ToLongTimeString() );
    //        Users usrs = new Users(config.conn);
    //        content.Append(" '");
    //        content.Append( usrs.UserList.Count );
    //        content.Append(" '!!!");
    //        content.Append("\"");
            

    //        return content.ToString();

    //    }

    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //while waiting for Sausalito Ferry 
        string url = "./StoryEditor4.aspx";
        url += "?StoryID=";
        url += config.startStoryID;
        url += "&PageCursor=";
        url += config.startStoryPage;
        url += "&toolBR=";
        url += config.startStoryToolBR;

        Response.Redirect(url);

    }
}
