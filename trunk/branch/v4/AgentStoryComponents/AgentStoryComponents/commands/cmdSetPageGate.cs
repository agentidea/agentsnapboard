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
           
            User by = macro.RunningMe;

            Story currStory = new Story(config.conn, StoryID, macro.RunningMe);
            int curStoryStateCursor = currStory.StateCursor;

            int maxPages = currStory.Pages.Length;

            




            utils ute = new utils();
            string storyNameH = ute.decode64(currStory.Title);

            string msg = by.UserName + " set gate ( " + pageGate + " ) to story ( " + storyNameH + " )";

            if (curStoryStateCursor == maxPages && pageGate == 0)
            {
                //at 'end' of state transition
                msg = "story state reset to beginning";
            }
            else
            if (pageGate < curStoryStateCursor)
            {
                msg = "story state set to earlier state, is this wise?";
            }
            else
                if (pageGate > maxPages)
                {
                    throw new Exception("There are only " + maxPages + " pages in this story");
                }

            //persist story state.
            currStory.StateCursor = pageGate;
            currStory.Save();
            
            
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg + " userTX [" + macro.UserCurrentTxID + "]");
            sl = null;

            MacroUtils.LogStoryEvent(msg, currStory, macro, 9);

            MacroEnvelope me = new MacroEnvelope();

            Macro ProcessPageGate = new Macro("ProcessPageGate", 2);
            ProcessPageGate.addParameter("StoryID", "" + StoryID);
            ProcessPageGate.addParameter("pageGate", "" + pageGate);


            me.addMacro(ProcessPageGate);

            Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            RenderChatMsg.addParameter("msg64",ute.encode64( msg ));
            RenderChatMsg.addParameter("severity", 1 + "");

            me.addMacro(RenderChatMsg);


            MacroUtils.LogStoryTx(me, currStory.ID, macro);

            return me;
        }
    }
}
