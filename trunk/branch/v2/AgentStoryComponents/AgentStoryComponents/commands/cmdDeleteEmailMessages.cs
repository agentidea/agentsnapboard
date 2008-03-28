using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdDeleteEmailMessages : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();
            string msg = " emails deleted ";

            string keysPiped64 = MacroUtils.getParameterString("keysPiped64", macro);
            string keysPiped = ute.decode64(keysPiped64);
            string[] keys = keysPiped.Split('|');
            
            int numEmailsTrashed = 0;

            foreach (string chkID in keys)
            {
                string[] chkBits = chkID.Split('_');

                string folderName = chkBits[1];
                string sEmailID = chkBits[2];
                int emailID = Convert.ToInt32(sEmailID);
                EmailMsg eMsg = new EmailMsg(config.conn, emailID);

               
                try
                {
                    eMsg.changeState(EmailStates.deleted);
                    msg = " message [" + eMsg.ID + "] trashed AT:" + ute.getDateStamp();
                    
                    numEmailsTrashed++;

                }
                catch (Exception ex)
                {
                    msg = "message [" + eMsg.ID + "] NOT trashed AT:" + ute.getDateStamp() + " REASON : " + ex.Message;
                    Logger.log(msg);
                    eMsg.LastError = ute.encode64(msg);

                }
                finally
                {
                    eMsg.Save();
                }
            }

            msg = numEmailsTrashed + " emails deleted";   //sent to the trash can";

            MacroEnvelope me = new MacroEnvelope();
            //group already added
            Macro m = new Macro("RefreshMessages", 1);
            m.addParameter("msg", ute.encode64(msg));
            me.addMacro(m);
            return me;
        }
    }
}
