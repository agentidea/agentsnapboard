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

namespace AgentStoryHTTP.screens
{
    public partial class platform2 : PossibleSessionAwareWebForm
    {

        private string _storiesJSON;

        public string StoriesJSON
        {
            get { return _storiesJSON; }
            // set { _storiesJSON = value; }
        }


        public string PageTitle
        {
            get
            {

                return config.Orginization + " - table of contents ";
            }
        }

        public string ClubName
        {
            get { return config.logoText; }
        }

        public string ToolBRdefault
        {
            get { return config.defaultStoryToolBR; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

            //Response.Write(" me " + base.currentUser.UserName);


            string msg = Request["msg"];
            if (msg != null)
            {
                this.divMsgAttachPoint.Visible = false; //$to do fix page level messaging!
                this.divMsgAttachPoint.InnerHtml = msg;
            }


            if (base.currentUser.State == (int)UserStates.pending_email_confirm)
            {
                string warn = "account%20requires%20user%20activation,%20please%20check%20your%20email%20for%20instructions :) ";
                Response.Redirect("./accountActivation.aspx?msg=" + warn);
                Response.End();
                return;

            }


            //show story list
            //simple
            MyStories ms = new MyStories(base.currentUser.ID);

            if (ms.Stories == null)
            {
                Response.Write("no stories for this user yet.");
                _storiesJSON = "{}";
                return;

            }



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



            _storiesJSON = ms.StoriesMetaJSON;




        }
    }
}