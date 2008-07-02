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
using AgentStoryComponents;


namespace AgentStoryHTTP.screens
{
    public partial class sendEmail : SessionAwareWebForm
    {
        public User CurrentUser
        {
            get { return base.currentUser; }

        }

        private User _to;

        public User To
        {
            get { return _to; }
            set { _to = value; }
        }



        public int ToID
        {
            get
            {
                int id = -1;

                if (_to != null)
                    id = To.ID;

                return id;
            }

        }
        public string ToUserName
        {
            get
            {
                string usrname = "";

                if (_to != null)
                {
                    usrname = To.UserName;
                }

                return usrname;
            }

        }

        private EmailMsg _existingMessage;

        public EmailMsg existingMessage
        {
            get { return _existingMessage; }

        }

        private string _emailJSON;

        public string emailJSON
        {
            get { return _emailJSON; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            //call base first
            base.Page_Load(sender, e, this.divToolBarAttachPoint, this.divFooter);

            //sending to
            string sToID = Request["idTo"];
            if (sToID != null)
            {
                int nUserID = Convert.ToInt32(sToID);
                _to = new User(config.conn, nUserID);
            }

            //open existing via guid 
            string emailGuid = Request["emailGuid"];
            if (emailGuid != null)
            {


                _existingMessage = new EmailMsg(config.conn, new Guid(emailGuid));
                _to = new User(config.conn, _existingMessage.to);
                this._emailJSON = _existingMessage.GetJSON();

            }
            else
            {
                this._emailJSON = "{}";
            }







        }
    }
}
