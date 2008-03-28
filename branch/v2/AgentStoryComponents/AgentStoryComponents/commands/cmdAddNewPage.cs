using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdAddNewPage : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // add new page to db.
            string pageName = MacroUtils.getParameterString("pageName", macro);
            int gridCols = MacroUtils.getParameterInt("gridCols", macro);
            int gridRows = MacroUtils.getParameterInt("gridRows", macro);
            
            int gridLayers = MacroUtils.getParameterInt("gridLayers", macro);

            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            int StoryOpenedBy = MacroUtils.getParameterInt("StoryOpenedBy", macro);

            //$todo: how to pass user context into commands???
            User by = macro.RunningMe;

            Story currStory = new Story(config.conn, StoryID, macro.RunningMe);
            Page p = new Page(config.conn, by);

            p.Name = pageName;
            //p.Code = pageName.ToUpper();
            p.Seq = currStory.Pages.Length + 1;
            
            p.GridX = gridCols;
            p.GridY = gridRows;
            p.GridZ = gridLayers;
            p.Save();

            int pageID = p.ID;


            currStory.AddPage(p);
            currStory.CurrPageCursor = ( currStory.Pages.Length-1 );

            stats _stat = new stats();
            _stat.addEditorStoryHit(by, currStory);

            //$TODO: LOG EASIER
            utils ute = new utils();
            string storyNameH = ute.decode64(currStory.Title);
            string pageNameH = ute.decode64(p.Name );
            string msg = by.UserName + " added a new page ( " + pageNameH + " ) to story ( " + storyNameH + " )";
            StoryLog sl = new StoryLog(config.conn);

            sl.AddToLog(msg + " userTX [" + macro.UserCurrentTxID + "]");
            sl = null;

            MacroUtils.LogStoryEvent(msg, currStory, macro, 9);

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("ProcessNewPage", 6);
            procPageID.addParameter("newPageID","" + pageID);
            procPageID.addParameter("pageName", "" + pageName);
            procPageID.addParameter("pageGUID", "" + p.GUID);
            procPageID.addParameter("gridCols", "" + gridCols);
            procPageID.addParameter("gridRows", "" + gridRows);
            procPageID.addParameter("GridZ", "" + gridLayers);

            me.addMacro(procPageID);

            Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            RenderChatMsg.addParameter("msg64",ute.encode64( msg ));
            RenderChatMsg.addParameter("severity", 1 + "");

            me.addMacro(RenderChatMsg);


            MacroUtils.LogStoryTx(me, currStory.ID, macro);

            return me;
        }
    }
}
