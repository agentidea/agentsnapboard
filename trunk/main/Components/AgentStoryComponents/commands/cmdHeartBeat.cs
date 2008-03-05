using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdHeartBeat : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int storyID = MacroUtils.getParameterInt("storyID", macro);
            int heartSeq = MacroUtils.getParameterInt("seq", macro);
            int storySeq = MacroUtils.getParameterInt("storySeq", macro);

            string sMsg = "unchanged";
            string sCMD = "";
            int bChanged = 0;
            int lastSeq = -1;

            try
            {




                StoryTxLog stl = new StoryTxLog(config.conn, storyID);
                retDelta rd = stl.getDeltasJSON64(storyID, storySeq);
                if (rd.numCmds == 0)
                {
                    //no change
                }
                else
                {
                    bChanged = 1;
                }

                sCMD = rd.cmdJSON64;

                //lastSeq = stl.getMaxLastInfo();

                //if (lastSeq > storySeq)
                //{
                //    int gap = storySeq - lastSeq;
                //    if (gap > 1)
                //    {
                //        //treat multiple missed commands
                //        sMsg = " MULTI c h a n g e !!!";
                //    }
                //    else
                //    {

                //        sMsg = " c h a n g e !!!";
                //        bChanged = 1;
                //        sCMD = stl.Command;
                //    }
                //}

            }
            catch (Exception ex)
            {
                //do nothing ...
            }

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("HeartBeat", 6);
            procPageID.addParameter("timestamp", TheUtils.ute.encode64( TheUtils.ute.getDateStamp() ) );
            procPageID.addParameter("seq", (heartSeq + 1) + "");
            procPageID.addParameter("msg", TheUtils.ute.encode64(sMsg) );
            procPageID.addParameter("cmd64", sCMD );
            procPageID.addParameter("bChanged", bChanged + "");
            procPageID.addParameter("lastStorySeq", lastSeq + "");
            me.addMacro(procPageID);

            return me;
        }
    }
}
