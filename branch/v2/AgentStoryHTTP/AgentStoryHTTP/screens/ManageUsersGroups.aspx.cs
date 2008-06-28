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


namespace AgentStoryHTTP.screens
{
    public partial class ManageUsersGroups : SessionAwareWebForm
    {
        public UserGroupHelper ugh = null;

        public User CurrentUser
        {
            get { return base.currentUser; }
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

            if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
            {
                Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
                Response.End();
            }

            if (base.currentUser.isObserver())
            {
                string msg = Server.UrlEncode(" observers may not manage groups at this time");
                Response.Redirect("msg.aspx?msg=" + msg);
                Response.End();
            }

            try
            {
                ugh = new UserGroupHelper();
            }
            catch (Exception ex)
            {
                string msg = Server.UrlEncode(" there are no groups in the system yet, please add one.");
                Response.Redirect("AddNewGroup.aspx.aspx?msg=" + msg);
                Response.End();
            }




        }

    }
}
