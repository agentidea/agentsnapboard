using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdTakeToPage : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
           
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            int pageIndex = MacroUtils.getParameterInt("pageIndex", macro);
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
           // string tx_id = TheUtils.ute.decode64(tx_id64);
     
           // Story currStory = new Story(config.conn, StoryID, macro.RunningMe);
     


            MacroEnvelope me = new MacroEnvelope();

            Macro TakeToPage = new Macro("TakeToPage", 2);
            TakeToPage.addParameter("tx_id64", "" + tx_id64);
            TakeToPage.addParameter("pageIndex", "" + pageIndex);
            me.addMacro(TakeToPage);

            MacroUtils.LogStoryTx(me, StoryID, macro);                  //broadcast cmdTakeToPage

            return me;
        }
    }
}
