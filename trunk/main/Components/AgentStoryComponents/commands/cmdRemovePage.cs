using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdRemovePage : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();

            // remove page
            int PageID = MacroUtils.getParameterInt("PageID", macro);
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);

            Story s = new Story(config.conn, StoryID, macro.RunningMe);
            User by = macro.RunningMe;

            Page p = new Page(config.conn, PageID);
            string pageNameH = ute.decode64(p.Name);
            s.DeletePage(p);

            string storyNameH = ute.decode64(s.Title);


            
            //$TODO: LOG EASIER
            string msg = "page ( " + pageNameH + " ) removed from story ( " + storyNameH + " ) by " + by.UserName;
            //string msg = "page  ( " + pageNameH + " ) removed by " + by.UserName;
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg + " userTX [" + macro.UserCurrentTxID + "]");
            sl = null;

            stats _stat = new stats();
            _stat.addEditorStoryHit(by, s);

            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("RemovePage", 2);
            m.addParameter("msg64", ute.encode64( msg) );
            m.addParameter("PageID", "" + p.ID);


            me.addMacro(m);

            MacroUtils.LogStoryEvent(msg, s, macro, 8);


            MacroUtils.LogStoryTx(me, s.ID, macro);

            return me;
        }
    }
}
