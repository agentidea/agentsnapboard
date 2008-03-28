using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdSaveEmailAndSend : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();

            int operatorID = MacroUtils.getParameterInt("operatorID", macro);
            int toID = MacroUtils.getParameterInt("toID", macro);
            string subject64 = MacroUtils.getParameterString("subject", macro);
            string body64 = MacroUtils.getParameterString("body", macro);
            string subject = ute.decode64(subject64);
            string body = ute.decode64(body64);
            string useAnon = MacroUtils.getParameterString("useAnon", macro);
            bool bUseAnon = Convert.ToBoolean(useAnon);

            string currentMessageGUID = MacroUtils.getParameterString("currentMessageGUID", macro);

            User by = new User(config.conn,operatorID);
            User to = null;
            string prefix = "";

            if (toID > 0)
            {
                to = new User(config.conn, toID);

            }

            EmailMsg email = null;

            if (currentMessageGUID.Trim().Length == 0)
            {
                //new message to someone
                email = new EmailMsg(config.conn, by);
                email.to = to.Email;
            }
            else
            {
                email = new EmailMsg(config.conn,new Guid(currentMessageGUID.Trim()));
                prefix = "re-";
            }

            if (bUseAnon)
            {
                email.from = config.allEmailFrom;
                email.ReplyToAddress = config.anonEmailReplyTo;
            }
            else
            {
                email.from = config.allEmailFrom;
                email.ReplyToAddress = by.Email;
            }

            email.subject = subject64;
            email.body = body64;
            email.Save();


            // SAVED now send ...
            PostMan pm = new PostMan(config.conn);
            string msg = "";

            try
            {

                email.changeState(EmailStates.sending);
                pm.SendMessage(email.ID);
                email.changeState(EmailStates.sent);
             
                msg = " message [" + email.ID + "] sent AT:" + ute.getDateStamp();
                email.LastError = ute.encode64(msg);

            }
            catch (MessageNotSentException mnsex)
            {
                msg = "message [" + email.ID + "] NOT sent AT:" + ute.getDateStamp() + " REASON : " + mnsex.Message;
                email.LastError = ute.encode64(msg);
            }
            finally
            {
                email.Save();
            }


            
            Logger.log(msg);

            // msg = "email draft " + prefix + "saved";
            msg = ute.encode64(msg);

            MacroEnvelope me = new MacroEnvelope();

            //group already added
            Macro m = new Macro("ProcessEmailSent", 3);
            m.addParameter("msg", msg);
            m.addParameter("id", email.ID + "");
            m.addParameter("guid", email.guid);
            me.addMacro(m);

            return me;
        }
    }
}
