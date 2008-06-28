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


using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;


namespace AgentStoryHTTP.screens
{
    public partial class ListMembers : SessionAwareWebForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

            if (base.currentUser.isObserver() && base.currentUser.ID == config.publicUserID)
            {
                Response.Redirect("./Platform2.aspx?msg=" + Server.UrlEncode("Please login to perform this operation."));
                Response.End();
            }

            Users us = new Users(config.conn);

            //filter only my sponsored contacts.

            //User u = new User();
            //u.SponsorID
            List<User> users = us.UserList;



            StringBuilder html = new StringBuilder();

            html.Append("<script language='JavaScript'>");
            html.Append(@" 
            
        function changeUserState(widget)
        {
            var userID = widget.id.split('_')[1];
            var stateID = widget.options[ widget.selectedIndex ].id;

            if(stateID == -1) return;

            var m = new macro();
            m.name = 'ChangeUserState';
            addParam(m,'UserID',userID);
            addParam(m,'State',stateID);
            processRequest( m );
        }

        ");
            html.Append("</script>");

            html.Append("<table border='1'>");

            html.Append("<tr class='clsGridHeadRow'>");

            html.Append("<td>");
            html.Append("Username");
            html.Append("</td>");
            html.Append("<td>");
            html.Append("Email");
            html.Append("</td>");
            html.Append("<td>");
            html.Append("Roles");
            html.Append("</td>");
            html.Append("<td>");
            html.Append("State");
            html.Append("</td>");
            html.Append("<td>");
            html.Append("Action");
            html.Append("</td>");
            html.Append("<td>");
            html.Append("send message");
            html.Append("</td>");

            html.Append("<td>");
            html.Append("Sponsor");
            html.Append("</td>");

            html.Append("</tr>");

            foreach (User u in users)
            {

                if (u.ID == base.currentUser.ID)
                {
                    //that's me, show!
                }
                else
                    if (base.currentUser.isAdmin() || base.currentUser.isRoot())
                    {
                        //no filtering
                    }
                    else
                    {
                        if (u.SponsorID != base.currentUser.ID)
                        {
                            if (u.ID == base.currentUser.SponsorID)
                            {
                                //show my sponsor!
                            }
                            else
                            {
                                continue;  //skip this user.
                            }

                        }
                        else
                        {
                            //let in
                        }
                    }


                if (u.ID == base.currentUser.SponsorID)
                    html.Append("<tr class='clsSponsorRow'>");
                else
                    html.Append("<tr class='clsGridRow'>");


                html.Append("<td>");
                html.Append("<div title='Tags: ");
                html.Append(u.Tags);
                html.Append("'>");

                if (base.currentUser.isRoot())
                {
                    html.Append(" [ " + u.ID + " ] ");
                }

                if (u.ID != base.currentUser.SponsorID)
                {
                    html.Append("<a href='./EditUserInfo.aspx?UserGUID=");
                    html.Append(u.UserGUID);
                    html.Append("' >");
                }


                html.Append(u.UserName);
                html.Append(" ( ");
                html.Append(u.LastName);
                html.Append(", ");
                html.Append(u.FirstName);
                html.Append(" ) ");

                if (u.ID != base.currentUser.SponsorID)
                {
                    html.Append("</a>");
                }





                html.Append("</div></td>");

                html.Append("<td>");
                html.Append(u.Email);
                html.Append("</td>");
                html.Append("<td>");
                html.Append(u.Roles);
                html.Append("</td>");

                html.Append("<td>");
                html.Append(u.StateHR);
                html.Append("</td>");

                if (u.ID == base.currentUser.SponsorID || u.ID == base.currentUser.ID)
                {
                    //hide state change action on sponsor and themself
                    html.Append("<td>");
                    html.Append("  ");
                    html.Append("</td>");
                }
                else
                {
                    #region states
                    html.Append("<td>");

                    html.Append("<select id='sel_" + u.ID + "' onchange='changeUserState(this);'>");


                    html.Append("<option id='-1'");

                    html.Append(">");
                    html.Append(" -- change state -- ");
                    html.Append("</option>");

                    html.Append("<option id='" + ((int)UserStates.signed_in) + "'");
                    if (u.State == ((int)UserStates.signed_in))
                    {
                        html.Append(" SELECTED ");
                    }
                    html.Append(">");
                    html.Append("signed-in");
                    html.Append("</option>");

                    html.Append("<option id='" + ((int)UserStates.added) + "'");
                    if (u.State == ((int)UserStates.added))
                    {
                        html.Append(" SELECTED ");
                    }
                    html.Append(">");
                    html.Append("added");
                    html.Append("</option>");

                    html.Append("<option id='" + ((int)UserStates.accepted) + "'");
                    if (u.State == ((int)UserStates.accepted))
                    {
                        html.Append(" SELECTED ");
                    }
                    html.Append(">");
                    html.Append("accepted");
                    html.Append("</option>");

                    html.Append("<option id='" + ((int)UserStates.suspended) + "'");
                    if (u.State == ((int)UserStates.suspended))
                    {
                        html.Append(" SELECTED ");
                    }
                    html.Append(">");
                    html.Append("suspended");
                    html.Append("</option>");

                    // only users can self terminate
                    //html.Append("<option id='" + ((int)UserStates.terminated) + "'");
                    //if (u.State == ((int)UserStates.terminated))
                    //{
                    //    html.Append(" SELECTED ");
                    //}
                    //html.Append(">");
                    //html.Append("terminated");
                    //html.Append("</option>");

                    html.Append("<option id='" + ((int)UserStates.pre_approved) + "'");
                    if (u.State == ((int)UserStates.pre_approved))
                    {
                        html.Append(" SELECTED ");
                    }
                    html.Append(">");
                    html.Append("preapproved");
                    html.Append("</option>");




                    html.Append("</select>");


                    html.Append("</td>");
                    #endregion
                }



                html.Append("<td>");
                html.Append("<a href='SendEmail.aspx?idTo=" + u.ID + "'>email</a>");
                html.Append("</td>");



                html.Append("<td>");
                html.Append("<div title='Sponsored by: ");
                html.Append(u.Sponsor.FirstName);
                html.Append(" ");
                html.Append(u.Sponsor.LastName);
                html.Append("'>");
                html.Append(u.SponsorFullName);
                html.Append("</div></td>");

                html.Append("</tr>");
            }

            html.Append("</table>");

            html.Append("<table><tr><td class='clsSponsorRow'>&nbsp;&nbsp;</td><td>Your Sponsor</td></tr></table>");




            this.divBodyAttachPoint.InnerHtml = html.ToString();


        }
    }
}
