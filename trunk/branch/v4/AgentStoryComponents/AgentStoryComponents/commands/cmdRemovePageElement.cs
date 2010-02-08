using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdRemovePageElement : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int pageElementID = MacroUtils.getParameterInt("PageElementID", macro);
            string PageElementMapGUID = MacroUtils.getParameterString("PageElementMapGUID", macro);
            int storyElementViewID = MacroUtils.getParameterInt("storyElementViewID", macro);
            int storyID = MacroUtils.getParameterInt("StoryID", macro);
            int PageID = MacroUtils.getParameterInt("PageID", macro);
            string msg = "";

            int gridX = -1, gridY = -1, gridZ = -1;
            string pGUID = null;

            //$todo: how to pass user context into commands???
            User by = macro.RunningMe;

            System.Guid pemGUID = new Guid( PageElementMapGUID );

            PageElementMap pem = new PageElementMap(config.conn, pemGUID);
            PageElement pe = new PageElement(config.conn, pageElementID);

            Snip snip = TheUtils.ute.getSnippet(TheUtils.ute.decode64(pe.Value),80);

            if (pem.GUID == null && pe.GUID == null)
            {

                //item already removed

            }
            else
            {

                gridY = pem.GridY;
                gridZ = pem.GridZ;
                pGUID = pem.GUID;

                pem.Delete();
                pe.Delete();

                //$TODO: LOG EASIER
                utils ute = new utils();
                msg = "page element id ( " + pageElementID + " ) removed by " + by.UserName + " BY userTX [" + macro.UserCurrentTxID + "]";
                if(snip.snippet.Trim().Length > 0)
                    msg += " [ " + snip.snippet + " ]";
                StoryLog sl = new StoryLog(config.conn);

                sl.AddToLog(msg);
                sl = null;
            }
          
            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("RemovePageElement", 7);
            procPageID.addParameter("pageElementID", pageElementID + "");
            procPageID.addParameter("PageID", PageID + "");
            procPageID.addParameter("GridX", "" + gridX);
            procPageID.addParameter("GridY", "" + gridY);
            procPageID.addParameter("GridZ", "" + gridZ);
            procPageID.addParameter("pemGUID", "" + pGUID);
            procPageID.addParameter("storyElementViewID", storyElementViewID + "");
            me.addMacro(procPageID);

            msg = by.UserName + " removed page element on page " + ( PageID +1);
            if(snip.snippet.Length>0)
                msg += " [ " + snip.snippet.Trim() + " ]";

            Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            RenderChatMsg.addParameter("msg64", TheUtils.ute.encode64(msg));
            RenderChatMsg.addParameter("severity", 1 + "");

            me.addMacro(RenderChatMsg);


            MacroUtils.LogStoryTx(me, storyID, macro);

            return me;
        }
    }
}
