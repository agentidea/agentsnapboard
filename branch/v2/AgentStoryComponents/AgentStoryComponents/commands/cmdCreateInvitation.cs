using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdCreateInvitation : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();

            int operatorID = MacroUtils.getParameterInt("operatorID", macro);

            int fromID = MacroUtils.getParameterInt("fromID", macro);
            
            string toUserIDs = MacroUtils.getParameterString("toUserIDs", macro);
            string toGroupIDs = MacroUtils.getParameterString("toGroupIDs", macro);


            string greeting64 = MacroUtils.getParameterString("greeting", macro);
            string subject64 = MacroUtils.getParameterString("subject", macro);
            string bodyContent64 = MacroUtils.getParameterString("bodyContent", macro);
            string inviteEventName64 = MacroUtils.getParameterString("inviteEventName", macro);

            string greeting = ute.decode64(greeting64);
            string subject = ute.decode64(subject64);
            string bodyContent = ute.decode64(bodyContent64);
            string inviteEventName = ute.decode64(inviteEventName64);

            string CreateInviteEmails = MacroUtils.getParameterString("CreateInviteEmails", macro);
            bool bCreateInviteEmails = Convert.ToBoolean(CreateInviteEmails);

            User by = new User(config.conn, operatorID);


            //create a unique pool of user id's
            List<int> userIDsTo = new List<int>();
            System.Collections.Hashtable userIDsToIndex = new System.Collections.Hashtable();
            
            //loop through user ID's
            string[] toUserIDsArray = toUserIDs.Split('|');
            foreach (string userID in toUserIDsArray)
            {
                if (userID == "-1") continue;
                if (userID.Trim().Length == 0) break;

                int idToADD = Convert.ToInt32(userID);

                if (userIDsToIndex.ContainsKey("u_" + idToADD))
                {
                    //ignore
                    continue;
                }
                else
                {
                    userIDsToIndex.Add("u_" + idToADD, idToADD);
                    userIDsTo.Add(idToADD);
                }

            }
            //loop through group ID's
            string[] toGroupsIDsArray = toGroupIDs.Split('|');
            foreach (string groupID in toGroupsIDsArray)
            {
                if (groupID == "-1") continue;
                if (groupID.Trim().Length == 0) break;

                int nGrpID = Convert.ToInt32(groupID);

                Group tmpGroup = new Group(config.conn, nGrpID);
                List<User> tmpUsers = tmpGroup.GroupUsers;

                foreach (User tmpUser in tmpUsers)
                {

                    int idToADD = tmpUser.ID;

                    if (userIDsToIndex.ContainsKey("u_" + idToADD))
                    {
                        //ignore
                        continue;
                    }
                    else
                    {
                        userIDsToIndex.Add("u_" + idToADD, idToADD);
                        userIDsTo.Add(idToADD);
                    }
                }

            }


            User fromUser = new User(config.conn,fromID);
            List<string> inviteGUIDS = new List<string>();

            int numInvites = 0;
            int numEmails = 0;

            if (userIDsTo.Count > 0)
            {
                //so there are some users to create invites for.

                for (int userCounter = 0; userCounter < userIDsTo.Count; userCounter++)
                {
                    User toUser = new User(config.conn, userIDsTo[userCounter]);
                    Invitation tmpInvite = new Invitation(config.conn, fromUser, toUser);
                    tmpInvite.InviteEvent = inviteEventName64;
                    tmpInvite.Title = subject64;

                    string inviteBody = greeting + " " + toUser.FirstName + ".";
                    inviteBody += " <br><br> " + bodyContent;
                    bodyContent64 = ute.encode64(inviteBody);
                    tmpInvite.InvitationText = bodyContent64;
                    tmpInvite.InviteCode = toUser.OrigInviteCode;
                    tmpInvite.Save();

                    string inviteGUID = tmpInvite.GUID;
                    inviteGUIDS.Add(inviteGUID);

                    numInvites++;

                    if (bCreateInviteEmails)
                    {
                         
                        string pickupURL = "http://";
                        
                        pickupURL += config.host + "/";
                        if (config.app.Length > 0)
                        {
                            pickupURL += config.app + "/";
                        }

                        pickupURL += "ViewInvitation2.aspx?guid=" + inviteGUID;

                        EmailMsg email = new EmailMsg(config.conn, fromUser);
                        email.to = toUser.Email;
                        email.from = config.allEmailFrom;
                        email.ReplyToAddress = fromUser.Email;
                        email.subject = subject64;
                        
                        //DEAR ...
                        email.body = greeting;
                        email.body += " ";
                        email.body += toUser.FirstName;
                        email.body += "\r\n";
                        email.body += "\r\n";
                        email.body += fromUser.FirstName;
                        email.body += " has sent you a personal invitation.";
                        
                        email.body += "\r\n";
                        email.body += "\r\n";
                        email.body += "To view it please use this link: ";
                        email.body += "\r\n";
                        email.body += "\r\n";
                        email.body += pickupURL;
                        email.body += "\r\n";
                        email.body += "\r\n";
                        email.body += "\r\n";
                        email.body += "\r\n";

                        
                        
                        email.body = ute.encode64(email.body);

                        int emailID = email.Save();
                        numEmails++;

                    }
                    
                }

            }

            /*

            Having trouble? If you are unable to open this Evite, try copying the entire URL below into your browser:
            http://www.evite.com/pages/invite/viewInvite.jsp?inviteId=DDVUOLVCVWSOYCMSCLRS&li=iq&src=email 

            Was this email unwanted?  Manage your communication preferences. 

            Replying to this email will reply directly to the sender. Your email address will be displayed. 

            If you found this email in your junk/bulk folder, please add info@evite.com to ensure that you'll receive all future Evite invitations in your Inbox. 
 

            */



            //$TODO: LOG EASIER
            string msg = numInvites + " invites created ";
            if (numEmails > 0) 
                msg += " with " + numEmails + " associated email messages.  Please go to your messages and send them from your drafts folder";
           
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            msg = ute.encode64(msg);

            MacroEnvelope me = new MacroEnvelope();

            //group already added
            Macro m = new Macro("ProcessNewInvites", 1);
            m.addParameter("msg", msg);
            me.addMacro(m);

            return me;
        }
    }
}
