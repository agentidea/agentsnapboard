using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdAddNewPageElementAndMap : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // add new page element to db.
            string val  = MacroUtils.getParameterString("Value", macro);
            string preJS64 = MacroUtils.getParameterString("preJS64", macro);
            string postJS64 = MacroUtils.getParameterString("postJS64", macro);

            string tags = MacroUtils.getParameterString("tags", macro);
            string sevTmpGUID = MacroUtils.getParameterString("sevTmpGUID", macro);
            int pageID  = MacroUtils.getParameterInt("PageID", macro);
            int gridX   = MacroUtils.getParameterInt("GridX", macro);
            int gridY   = MacroUtils.getParameterInt("GridY", macro);
            int gridZ = MacroUtils.getParameterInt("GridZ", macro);
            int typeID  = MacroUtils.getParameterInt("TypeID", macro);
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            int CurrentPageCursor = MacroUtils.getParameterInt("currentPageCursor", macro);
            int StoryOpenedBy = MacroUtils.getParameterInt("StoryOpenedBy", macro);

            //$todo: how to pass user context into commands???
            User by = macro.RunningMe;

            Story currStory = new Story(config.conn, StoryID, macro.RunningMe);

            Snip snip = TheUtils.ute.getSnippet(TheUtils.ute.decode64(val), 80);


            PageElement pe = new PageElement(config.conn, by);
            pe.TypeID = typeID;
            //pe.Code = code;
            pe.Value = val;
            pe.preJavascript = preJS64;
            pe.postJavascript = postJS64;
            pe.Tags = tags;
            pe.Save();

            currStory.AddPageElement(pe);

            PageElementMap pem = new PageElementMap(config.conn);
            pem.PageElementID = pe.ID;
            pem.GridX = gridX;
            pem.GridY = gridY;
            pem.GridZ = gridZ;
            pem.Save();

            Page p = new Page(config.conn, pageID);
            p.AddPageElementMap(pem,true);

            stats _stat = new stats();
            _stat.addEditorStoryHit(by, currStory);



            

            //$TODO: LOG EASIER
            utils ute = new utils();
            string storyNameH = ute.decode64(currStory.Title);
            string pageNameH = ute.decode64(p.Name);
            string msg = by.UserName + " added a new page element to page ( " + pageNameH + " )";
            if(snip.snippet.Length>0)
                msg += " [ " + snip.snippet + " ] ";

            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg + " userTX [" + macro.UserCurrentTxID + "]");
            sl = null;

            //audit content explicitly?
            if (config.auditMode.ToUpper()  == "FULL")
                Logger.log("user " + by.UserName + " id.guid[" + by.ID + "." + by.UserGUID + "] added [" + TheUtils.ute.decode64(val) + "]");

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("ProcessNewPageElement", 11);
            procPageID.addParameter("newPageElementID", "" + pe.ID);
            procPageID.addParameter("newPageElementMapID", "" + pem.ID);
            procPageID.addParameter("GridX", "" + gridX);
            procPageID.addParameter("GridY", "" + gridY);
            procPageID.addParameter("GridZ", "" + gridZ);
            procPageID.addParameter("CurrentPageCursor", "" + CurrentPageCursor);
            procPageID.addParameter("val64", val);
            procPageID.addParameter("GUID", pem.GUID);
            procPageID.addParameter("sevTmpGUID", sevTmpGUID);
            procPageID.addParameter("originUser", TheUtils.ute.encode64(pe.by.UserName));
            procPageID.addParameter("DateAdded", TheUtils.ute.encode64( Convert.ToString( pe.DateAdded)));


            me.addMacro(procPageID);

            Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            RenderChatMsg.addParameter("msg64", ute.encode64(msg));
            RenderChatMsg.addParameter("severity", 1 +"");
            
            me.addMacro(RenderChatMsg);

            MacroUtils.LogStoryTx(me, currStory.ID, macro);

            return me;
        }
    }
}
