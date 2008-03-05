using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    /// <summary>
    /// adds a chat message to the story tx log
    /// </summary>
    public class cmdStoryPostMsg : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            
            string userName = MacroUtils.getParameterString("userName", macro);
            int storyID = MacroUtils.getParameterInt("StoryID", macro);
            string msg64 = MacroUtils.getParameterString("msg64", macro);

            msg64 = TheUtils.ute.decode64(msg64) + " " + TheUtils.ute.getDateStamp();
           
            User by = macro.RunningMe;
            Story currStory = new Story(config.conn, storyID, macro.RunningMe);
            MacroEnvelope me = new MacroEnvelope();
            Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            RenderChatMsg.addParameter("msg64",TheUtils.ute.encode64( msg64));
            RenderChatMsg.addParameter("severity", 1 +"");
            me.addMacro(RenderChatMsg);

            MacroUtils.LogStoryTx(me, currStory.ID, macro);

            return me;
        }
    }
}
