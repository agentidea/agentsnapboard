using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdUpdatePageElementXYZ : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // update page element to db.
            string pemGUID = MacroUtils.getParameterString("pemGUID", macro);
            string CurrentPageCursor = MacroUtils.getParameterString("CurrentPageCursor", macro);
            int gridX = MacroUtils.getParameterInt("X", macro);
            int gridY = MacroUtils.getParameterInt("Y", macro);
            int gridZ = MacroUtils.getParameterInt("Z", macro);
            int storyID = MacroUtils.getParameterInt("StoryID", macro);
            string tsx = MacroUtils.getParameterString("tsx", macro);

            User by = macro.RunningMe;

            

            PageElementMap pem = null;
            //update cartesian map
            System.Guid guid = new Guid(pemGUID);
            pem = new PageElementMap(config.conn, guid);
            pem.GridX = gridX;
            pem.GridY = gridY;

            int changeZ = 0;
            if (pem.GridZ != gridZ)
                changeZ = 1;

            pem.GridZ = gridZ;
            pem.Save();
           

            MacroEnvelope me = new MacroEnvelope();

            Macro UpdatePageElementXYZ = new Macro("UpdatePageElementXYZ", 7);
            UpdatePageElementXYZ.addParameter("GridX", "" + gridX);
            UpdatePageElementXYZ.addParameter("GridY", "" + gridY);
            UpdatePageElementXYZ.addParameter("GridZ", "" + gridZ);
            UpdatePageElementXYZ.addParameter("pemGUID",pem.GUID);
            UpdatePageElementXYZ.addParameter("tsx", tsx );
            UpdatePageElementXYZ.addParameter("CurrentPageCursor", CurrentPageCursor);
            UpdatePageElementXYZ.addParameter("changeZ", "" + changeZ );

            
            me.addMacro(UpdatePageElementXYZ);

            //tmi?
            //string msg = macro.RunningMe.UserName + " moved a page element on page " + ( Convert.ToInt32( CurrentPageCursor)  + 1);
            //Macro RenderChatMsg = new Macro("RenderChatMsg", 2);
            //RenderChatMsg.addParameter("msg64", TheUtils.ute.encode64(msg));
            //RenderChatMsg.addParameter("severity", 5 + "");
            //me.addMacro(RenderChatMsg);

            MacroUtils.LogStoryTx(me, storyID, macro);

            return me;
        }
    }
}
