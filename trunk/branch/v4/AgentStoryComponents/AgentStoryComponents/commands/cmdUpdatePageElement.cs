using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdUpdatePageElement : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // update page element to db.
            string val = MacroUtils.getParameterString("Value", macro);
            string preJS64 = MacroUtils.getParameterString("preJS64", macro);
            string postJS64 = MacroUtils.getParameterString("postJS64", macro);
            string tags = MacroUtils.getParameterString("tags", macro);
            int pageElementID = MacroUtils.getParameterInt("PageElementID", macro);
            string PageElementMapGUID = MacroUtils.getParameterString("PageElementMapGUID", macro); 
            int typeID = MacroUtils.getParameterInt("TypeID", macro);
            int StoryOpenedBy = MacroUtils.getParameterInt("StoryOpenedBy", macro);
            int gridX = MacroUtils.getParameterInt("GridX", macro);
            int gridY = MacroUtils.getParameterInt("GridY", macro);
            int gridZ = MacroUtils.getParameterInt("GridZ", macro);
            int storyID = MacroUtils.getParameterInt("StoryID", macro);
            int currentPageCursor = MacroUtils.getParameterInt("currentPageCursor", macro);

            User by = macro.RunningMe;

            Snip snip = TheUtils.ute.getSnippet(TheUtils.ute.decode64(val), 80);

            PageElement pe = new PageElement(config.conn, pageElementID);
            pe.TypeID = typeID;
            pe.Value = val;
            pe.preJavascript = preJS64;
            pe.postJavascript = postJS64;
            pe.Tags = tags;
            pe.Save();

            PageElementMap pem = null;

            if (gridZ != -1)
            {
                //update cartesian map
                System.Guid guid = new Guid( PageElementMapGUID );
                pem = new PageElementMap(config.conn,guid);
                pem.GridX = gridX;
                pem.GridY = gridY;
                pem.GridZ = gridZ;  
                pem.Save();
            }

            //$TODO: LOG EASIER
            utils ute = new utils();
            string msg = "page element id ( " + pageElementID + " ) updated at " + ute.getDateStamp() + " BY userTX [" + macro.UserCurrentTxID + "]";
            if (snip.snippet.Length > 0)
                msg += " [ " + snip.snippet + " ] ";

            StoryLog sl = new StoryLog(config.conn);
            
            sl.AddToLog(msg);
            sl = null;

            Story currStory = new Story(config.conn, storyID, macro.RunningMe);
            stats _stat = new stats();
            _stat.addEditorStoryHit(by, currStory);
          
            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("UpdatePageElement", 7);
            procPageID.addParameter("pageElementID", "" + pageElementID);
            procPageID.addParameter("GridX", "" + gridX);
            procPageID.addParameter("GridY", "" + gridY);
            procPageID.addParameter("GridZ", "" + gridZ);
            procPageID.addParameter("pemGUID", "" + pem.GUID);
            procPageID.addParameter("val64", val);
            procPageID.addParameter("CurrentPageCursor", currentPageCursor + "");

            me.addMacro(procPageID);

            msg = macro.RunningMe.UserName + " updated page element on page " + (currentPageCursor + 1);
            if (snip.snippet.Length > 0)
                msg += " [ " + snip.snippet + " ] ";

            Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            RenderChatMsg.addParameter("msg64",ute.encode64( msg ));
            RenderChatMsg.addParameter("severity", 1 + "");
            me.addMacro(RenderChatMsg);

            MacroUtils.LogStoryTx(me, storyID, macro);

            return me;
        }
    }
}
