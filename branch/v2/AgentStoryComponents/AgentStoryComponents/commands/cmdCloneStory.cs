using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdCloneStory : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // mark the state of the story
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            int OperatorID = MacroUtils.getParameterInt("OperatorID", macro);
            string NewStoryName64 = MacroUtils.getParameterString("NewStoryName64", macro);


           
            User operatorUser = macro.RunningMe;

            Story currStory = new Story(config.conn, StoryID,operatorUser);
            
            string msg = "";


            Scribe scribe = new Scribe(macro.RunningMe, currStory);
            Story newStory = scribe.CopyStory();

            if (NewStoryName64 != newStory.Title)
            {
                newStory.Title = NewStoryName64;
                newStory.Save();
            }

            string storyNameH = TheUtils.ute.decode64(newStory.Title);

            Logger.log(macro.RunningMe.UserName + " [" + macro.RunningMe.ID +  "] cloned '" + TheUtils.ute.decode64(currStory.Title) + "' [" + currStory.ID + "] as '" + TheUtils.ute.decode64(newStory.Title) + "'" + "[" + newStory.ID + "]");

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("ProcessStoryClone", 1);
            procPageID.addParameter("NewStoryID", "" + newStory.ID );
            me.addMacro(procPageID);

            return me;
        }


    }
}
