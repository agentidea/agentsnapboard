using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdMarkStoryState : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // mark the state of the story
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            int OperatorID = MacroUtils.getParameterInt("OperatorID", macro);
            int StoryState = MacroUtils.getParameterInt("StoryState", macro);

            User operatorUser = macro.RunningMe;

            Story currStory = new Story(config.conn, StoryID, macro.RunningMe);
            string storyNameH = TheUtils.ute.decode64( currStory.Title);
            string msg = "";

            //validate, if owner can change state
            //if root can change state
            if (operatorUser.isRoot() || operatorUser.isAdmin() || currStory.by.ID == operatorUser.ID)
            {
                currStory.State = StoryState;
                currStory.Save();
                msg = "story  ( " + storyNameH + " ) change story state to ( " + currStory.StateH + " ) operatorUser " + operatorUser.UserName;
                Logger.log(msg);
            }
            else
            {
                msg = "story  ( " + storyNameH + " ) did not have sufficient permissions to change story state to ( " + currStory.GetStoryStateHR(StoryState) + " ) operatorUser " + operatorUser.UserName;
                Logger.log(msg);
                throw new InvalidOperationException("Only Story owners, admins or root can change Story State");
            }

            StoryState storyState = new StoryState(config.conn, StoryState);

            

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("ProcessStoryState", 4);
            procPageID.addParameter("StoryID", "" + StoryID);
            procPageID.addParameter("StoryState", "" + StoryState);
            procPageID.addParameter("StateName", "" + storyState.StateName);
            procPageID.addParameter("OperatorUserName", "" + operatorUser.UserName );
            me.addMacro(procPageID);

            return me;
        }

        
    }
}
