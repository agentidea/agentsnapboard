using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdSetPageGate : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // change the page gate
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            int pageGate = MacroUtils.getParameterInt("pageGate", macro);
     
            Story currStory = new Story(config.conn, StoryID, macro.RunningMe);
     
            //persist story state.
            currStory.StateCursor = pageGate;
            currStory.Save();

            MacroEnvelope me = new MacroEnvelope();

            Macro ProcessPageGate = new Macro("ProcessPageGate", 2);
            ProcessPageGate.addParameter("StoryID", "" + StoryID);
            ProcessPageGate.addParameter("pageGate", "" + pageGate);
            me.addMacro(ProcessPageGate);
            //pass on gate change to all currently present users
            MacroUtils.LogStoryTx(me, currStory.ID, macro);

            return me;
        }
    }
}
