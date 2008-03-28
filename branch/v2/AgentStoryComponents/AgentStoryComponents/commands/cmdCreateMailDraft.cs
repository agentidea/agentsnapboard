using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdCreateMailDraft : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();
            
            List<string> messages = new List<string>();

            string action = MacroUtils.getParameterString("action", macro);
            int operatorID = MacroUtils.getParameterInt("operatorID", macro);

            int fromID = MacroUtils.getParameterInt("fromID", macro);

            string toUserIDs = MacroUtils.getParameterString("toUserIDs", macro);
            string toGroupIDs = MacroUtils.getParameterString("toGroupIDs", macro);


            string greeting64 = MacroUtils.getParameterString("greeting", macro);
            string greeting = null;
            
            if (greeting64 == "AA==")
                greeting = "";
            else
                greeting = ute.decode64(greeting);

            string subject64 = MacroUtils.getParameterString("subject", macro);
            string bodyContent64 = MacroUtils.getParameterString("bodyContent", macro);
            
            string subject = ute.decode64(subject64);
            string bodyContent = ute.decode64(bodyContent64);

            string useAnonSender = MacroUtils.getParameterString("useAnonSender", macro);
            bool bAnonSender = Convert.ToBoolean(useAnonSender);
           
            string requestReadReciept = MacroUtils.getParameterString("requestReadReciept", macro);
            bool bRequestReadReciept = Convert.ToBoolean(requestReadReciept);
            
            string AllowHtml = MacroUtils.getParameterString("AllowHtml", macro);
            bool bAllowHtml = Convert.ToBoolean(AllowHtml);


            User by = macro.RunningMe;
            User fromUser = new User(config.conn, fromID);

            if (by.ID != fromUser.ID)
            {
                throw new PossibleHackException("Impersonation!  It is not possible to send out new emails as someone else ");
                //no exceptions, not even root!
            }

            List<int> userIDsTo = new List<int>();
            #region create a unique pool of user id's

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

            #endregion


            foreach (int uID in userIDsTo)
            {
                EmailMsg eMsg = new EmailMsg(config.conn, by);
                User to = new User(config.conn,uID);

                //eMsg.state = (int) EmailStates.draft;
                
                if (bAnonSender)
                    eMsg.ReplyToAddress = config.anonEmailReplyTo;
                else
                    eMsg.ReplyToAddress = by.Email;

                eMsg.from = config.allEmailFrom;

                string tmpBody = bodyContent;
                string tmpBody64 = null;

                if (greeting.Trim().Length > 0)
                {
                    tmpBody = greeting.Trim() + " " + to.UserName + "\r\n\r\n" + bodyContent;
                    
                }

                tmpBody64 = ute.encode64(tmpBody);

                eMsg.body = tmpBody64;
                eMsg.subject = subject64;
                eMsg.to = to.Email;
                eMsg.Save();
                eMsg.changeState(EmailStates.draft);

                messages.Add(" created draft " + eMsg.ID);
   
            }



            string msg = "" + messages.Count + " draft messages created by " + by.UserName + " [" + by.ID + "]";
            Logger.log(msg);
            msg = ute.encode64(msg);

            MacroEnvelope me = new MacroEnvelope();

            //group already added
            Macro m = new Macro("ProcessNewMails", 1);
            m.addParameter("msg", msg);
            me.addMacro(m);

            return me;
        }
    }
}
