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


using System.IO;

using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryHTTP.screens
{
    public partial class SlideNavigator : PossibleSessionAwareWebForm
    {
        private Story _story;

        private int _pageCursor;
        private string _toolBarVisible;
        private string _includeCodeDirName;

        public string ToolBarVisible
        {
            get { return _toolBarVisible; }
            set { _toolBarVisible = value; }
        }


        public string PageTitle
        {
            get
            {
                string storyName = TheUtils.ute.decode64(this.oStory.Title);
                //$TODO: filter? dissallowed chars in <title></title> ?

                // return storyName + " - " + this.currentUser.UserName.ToUpper();
                return storyName + " by " + this.oStory.by.UserName.ToUpper();
            }
        }


        public string GameSpecificIncludes
        {
            get
            {

                if (_includeCodeDirName == null)
                {
                    return @"<!-- no JS specific includes for this story -->";
                }
                else
                {
                    //build paths for each .js script file under the extras directory
                    // auto addin code injxn!!
                    var clearIncludeCodeDirPath = TheUtils.ute.decode64(_includeCodeDirName);
                  
                    string pathToSearch = string.Format("../extras/{0}", clearIncludeCodeDirPath);
                    pathToSearch = Server.MapPath(pathToSearch);

                    System.Text.StringBuilder sb = new StringBuilder();
                    sb.Append(string.Empty);

                    if (System.IO.Directory.Exists(pathToSearch) == true)
                    {

                        sb.AppendFormat("<!-- Story auto generated [{0}] Specific Code JS object includes -->", TheUtils.ute.decode64(oStory.Title));

                        //read out all JS file names
                        string pattern = "*.js";
                        //string[] files = System.IO.Directory.GetFiles(pathToSearch, pattern);
                        DirectoryInfo di = new DirectoryInfo(pathToSearch);
                        FileInfo[] fi = di.GetFiles(pattern);
                        int fileCount = 0;
                        foreach (FileInfo info in fi )
                        {
                            fileCount++;
                           
                            //  <script src='../extras/SmartOrg/PortfolioGame/js/jquery.js' type='text/javascript'></script>
                            sb.AppendFormat("  <script src='../extras{0}{1}' type='text/javascript'></script>", clearIncludeCodeDirPath, info.Name);
                        }

                        sb.AppendFormat("<!-- {0} files found -->", fileCount);
                    }

                    return sb.ToString();

                }
            }
        }



        public int PageCursor
        {
            get { return _pageCursor; }
            set { _pageCursor = value; }
        }

        public Story oStory
        {
            get { return _story; }
            set { _story = value; }
        }

        public string currTx
        {
            get
            {
                return base.currentUser.TxSessionID;
            }
        }

        public string IncludeMaster
        {
            get { return config.includeMasterURL; }
        }

        //public int CurrUserID
        //{
        //    get { return _currUserID; }

        //}

        private string _viewMode;

        public User currUser
        {
            get { return base.currentUser; }
        }
        public string ViewMode
        {
            get { return _viewMode; }
            set { _viewMode = value; }
        }


        public string currUserStoriesJSON
        {
            get
            {
                MyStories ms = new MyStories(base.currentUser.ID);


                //sort by story id with lamda
                ms.Stories.Sort(

                    delegate(Story s1, Story s2)
                    {
                        return s2.ID.CompareTo(s1.ID);
                    }
                );

                //filter out deleted stories.
                ms.Stories = ms.Stories.FindAll(

                    delegate(Story s) { return s.State < 5; }

                    );




                return ms.StoriesMetaJSON;
            }

        }


        public string currUserJSON
        {
            get
            {
                StringBuilder json = new StringBuilder();
                User u = base.currentUser;


                json.Append("{");
                if (u != null)
                {
                    json.Append("'ID':");
                    json.Append(u.ID);
                    json.Append(",");
                    json.Append("'UserName':");
                    json.Append("'");
                    json.Append(u.UserName);
                    json.Append("'");

                }
                json.Append("}");

                return json.ToString();
            }
        }

        public bool DebugMode
        {
            get { return config.bDebug; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl toolBarREF = null;
            base.Page_Load(sender, e, toolBarREF, null);

            //get the user context
            string userContextInfo = "";
            userContextInfo += Session.SessionID;
            userContextInfo += " | ";
            userContextInfo += Request.UserHostAddress;
            userContextInfo += " | ";
            userContextInfo += Request.Browser.Platform;
            userContextInfo += " | ";
            userContextInfo += Request.Browser.Type;
            userContextInfo += " | ";
            userContextInfo += Request.UserAgent;


            string sStoryID = Request["StoryID"];
            string sPageCursor = Request["PageCursor"];
            string sViewMode = Request["Mode"];
            string sToolBarVisible = Request["toolBR"];
            if (sToolBarVisible != null) sToolBarVisible.ToUpper();

            if (sStoryID == null)
            {
                //StoryID parameter on URL expected
                Response.Redirect("./Platform2.aspx");
                return;
            }

            if (sToolBarVisible == null)
            {
                sToolBarVisible = config.storyToolbarStartMode;

            }

            this.ToolBarVisible = sToolBarVisible;

            int nPageCursor = 0;
            if (sPageCursor != null)
            {
                nPageCursor = Convert.ToInt32(sPageCursor);
            }

            this.PageCursor = nPageCursor;

            if (sViewMode == null)
            {
                sViewMode = "LOCKED";
            }

            if (sViewMode.ToUpper() == "LOCKED" || sViewMode.ToUpper() == "UNLOCKED")
            {
                //GOOD
            }
            else
            {
                throw new Exception("invalid use of Mode - valid values are LOCKED or UNLOCKED");
            }

            ViewMode = sViewMode;


            string msg = null;

            int nStoryID = -1;

            if (sStoryID != null)
            {
                nStoryID = Convert.ToInt32(sStoryID);

                this.oStory = new Story(config.conn, nStoryID, base.currentUser);

                //check if story state is active?
                if (this.oStory.State == 1)
                {
                    //show proceed
                }
                else
                {
                    msg = "Story no longer active, current state is [" + this.oStory.StateH + "]";
                    Logger.log(base.currentUser.UserName + " looking at a " + msg);
                    //throw new Exception(config.storyUnavailible);
                    Response.Redirect("./platform2.aspx?msg=" + config.storyUnavailible);
                    Response.End();
                    return;
                }

                //decorate permissions for viewer tool
                //this.oStory.decorateStoryACL2(base.currentUser);
                //pass current user into the opened by flags.

                //can the user proceed to the story?
                if (this.oStory.CanView || this.oStory.CanEdit)
                {
                    //good proceed
                }
                else
                {
                    //unauthorized to view this story
                    msg = "Story [" + TheUtils.ute.decode64(this.oStory.Title) + "] not intended for [" + base.currentUser.UserName + " (" + base.currentUser.ID + ")] eyes, if you know the story owner, please ask them to add you as a Viewer or Editor.";
                    Logger.log(base.currentUser.UserName + " looking at a " + msg + " [" + userContextInfo + "]");
                    //throw new Exception(config.storyUnavailible);
                    Response.Redirect("./platform2.aspx?msg=" + config.storyUnavailible);
                    Response.End();
                    return;
                }



                if (oStory.IncludeCodeDirName != string.Empty)
                {
                    this._includeCodeDirName = oStory.IncludeCodeDirName;
                }


                if (base.currentUser != null)
                {
                    //record unique view
                    stats stat = new stats();
                    stat.addUserStory_hit(base.currentUser, this.oStory);

                    msg = base.currentUser.UserName + " is looking at ( " + TheUtils.ute.decode64(this.oStory.Title) + " ) [" + userContextInfo + "]";

                }
                else
                {
                    Group publicGroup = new Group(config.conn, TheUtils.ute.encode64("public"));
                    //cHVibGlj

                    //is this story viewable by public?

                    // if (oStory.StoryViewerGroups.Contains(publicGroup))
                    if (containsGroup(oStory.StoryViewerGroups, publicGroup))
                    {
                        //public can view
                        msg = "Not logged in viewer is looking at ( " + TheUtils.ute.decode64(this.oStory.Title) + " )";
                    }
                    else
                    {
                        //story not viewable by public.
                        msg = "Not logged in viewer is trying to view a non public story - " + TheUtils.ute.decode64(this.oStory.Title);
                        Response.Redirect("./login.aspx?msg=Story%20not%20public,%20please%20login%20to%20view");
                        Response.End();
                        return;
                    }


                }


                StoryLog sl = new StoryLog(config.conn);
                sl.AddToLog(msg);
                sl = null;
            }
            //


        }    //~end function


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
