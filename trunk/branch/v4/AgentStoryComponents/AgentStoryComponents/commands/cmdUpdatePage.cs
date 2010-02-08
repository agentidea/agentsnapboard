using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdUpdatePage : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // update page information
            int PageID = MacroUtils.getParameterInt("PageID", macro);
            int gridX = MacroUtils.getParameterInt("GridX", macro);
            int gridY = MacroUtils.getParameterInt("GridY", macro);
            int storyID = MacroUtils.getParameterInt("StoryID", macro);
            string PageName64 = MacroUtils.getParameterString("PageName", macro);
            string PageName = TheUtils.ute.decode64(PageName64);
            User by = macro.RunningMe;

            bool bSavedName = false;
            bool bSavedDimensions = false;
            
            string msg = "";

            Page p = new Page(config.conn, PageID);

            if (p.GridX == gridX && p.GridY == gridY)
            {
                bSavedDimensions = false;
            }
            else
            {
                p.GridX = gridX;
                p.GridY = gridY;

                bSavedDimensions = true;
                string xy = gridX + " x " + gridY;
                msg += "page  [" + PageName + "] updated grid size to cols x rows ( " + xy + " ) by " + by.UserName;
     
            }
            
            if (p.Name.Trim() != PageName64)
            {
                string pnameorig = p.Name;
                p.Name = PageName64;
                bSavedName = true;
                msg += "page  [" + p.ID + "] name changed from '" + TheUtils.ute.decode64(pnameorig) + "' to '" + PageName +"'" + by.UserName;
            }

            p.Save();

            Logger.log(msg);


           Story currStory = new Story(config.conn, storyID,macro.RunningMe);
           stats _stat = new stats();
           _stat.addEditorStoryHit(by, currStory);

            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("UpdatePage", 1);
            m.addParameter("dateUpdated", "page grid updated to db on server at " + System.DateTime.Now.ToString()  );
            

            me.addMacro(m);

           

            return me;
        }
    }
}
