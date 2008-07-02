﻿using System;
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


using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryHTTP.screens
{
    public partial class PageElementViewer : SessionAwareWebForm
    {
        public User CurrentUser
        {
            get { return base.currentUser; }
        }

        public string PageElementJSON
        {

            get
            {
                PageElements pageElements = new PageElements(config.conn);
                return pageElements.getPageElementsJSON(base.currentUser.ID);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

            if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
            {
                Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
                Response.End();
            }


        }
    }

}
