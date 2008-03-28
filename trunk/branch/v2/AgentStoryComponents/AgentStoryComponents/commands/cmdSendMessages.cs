using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdSendMessages : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();
            string msg = " emails sent ";

            string keysPiped64 = MacroUtils.getParameterString("keysPiped64", macro);
            string keysPiped = ute.decode64(keysPiped64);

            PostMan pm = new PostMan(config.conn);

            string[] keys = keysPiped.Split('|');
            int messagesSent =0;
            int messagesFailedToSend =0;

            foreach (string chkID in keys)
            {
                string[] chkBits = chkID.Split('_');

                string folderName = chkBits[1];
                string sEmailID = chkBits[2];
                int emailID = Convert.ToInt32(sEmailID);
                EmailMsg eMsg = new EmailMsg(config.conn, emailID);

                if (eMsg.state == (int)EmailStates.draft || eMsg.state == (int)EmailStates.sending )
                {
                    try
                    {

                        eMsg.changeState(EmailStates.sending);
                        pm.SendMessage(eMsg.ID);
                        eMsg.changeState(EmailStates.sent);
                        messagesSent++;

                        msg = " message [" + eMsg.ID + "] sent AT:" + ute.getDateStamp();
                        Logger.log(msg);
                        eMsg.LastError = ute.encode64(msg);

                    }
                    catch (MessageNotSentException mnsex)
                    {
                        msg = "message [" + eMsg.ID + "] NOT sent AT:" + ute.getDateStamp() + " REASON : " + mnsex.Message;
                        Logger.log(msg);
                        eMsg.LastError = ute.encode64(msg);
                        messagesFailedToSend++;
                    }
                    finally
                    {
                        eMsg.Save();
                    }
                }
                else
                {
                    msg = "message " + eMsg.ID + " not sent as state was " + eMsg.StateHr;
                    Logger.log(msg);
                }
          
            }

            //$TODO: LOG EASIER
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            if(messagesSent>0)
                msg = messagesSent + " emails sent";
            if (messagesFailedToSend > 0)
                msg += " \r\n\r\n " + messagesFailedToSend + " emails NOT sent ";
            


            MacroEnvelope me = new MacroEnvelope();

            //group already added
            Macro m = new Macro("ProcessEmailsSent", 3);
            m.addParameter("msg", ute.encode64(msg));
            m.addParameter("messagesSent", messagesSent + "");
            m.addParameter("messagesFailedToSend", messagesFailedToSend + "");
            me.addMacro(m);

            return me;
        }
    }
}
