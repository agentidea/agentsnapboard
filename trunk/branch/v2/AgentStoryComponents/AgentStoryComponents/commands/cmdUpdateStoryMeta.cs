using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdUpdateStoryMeta : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();
            // updates story meta infor

            string storyTitle = MacroUtils.getParameterString("txtStoryName", macro);
            string decodedTitle = ute.decode64(storyTitle);

            if (decodedTitle.Trim().Length == 0)
                throw new Exception("Try to be a bit more original with your story title :) ");

            string storyDescription = MacroUtils.getParameterString("txtStoryDescription", macro);
            string decodedDesc = ute.decode64(storyDescription);
           // int userID = MacroUtils.getParameterInt("UserID", macro);
            int storyID = MacroUtils.getParameterInt("StoryID", macro);

            User by = macro.RunningMe;

            //open existing story
            Story s = new Story(config.conn, storyID, macro.RunningMe);

            if (decodedTitle != "bah")
                s.Title = storyTitle;
            if (decodedDesc != "bah")
                s.Description = storyDescription;

           
            s.Save();


            //$TODO: LOG EASIER

            string storyNameH = ute.decode64(macro.Parameters[0].Val);
            string msg = "existing story metadata ( " + storyNameH.ToString() + " ) modified by " + by.UserName;
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            stats _stat = new stats();
            _stat.addEditorStoryHit(by, s);

            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("DisplayAlert", 2);  //change to update story model ...
            m.addParameter("msg",ute.encode64( msg));
            m.addParameter("severity", "1");

            me.addMacro(m);

            MacroUtils.LogStoryTx(me, s.ID, macro);
            
            return me;
        }
    }
}
