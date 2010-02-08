using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdOrderPages : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int StoryID = MacroUtils.getParameterInt("StoryID", macro);
            string PageMap = MacroUtils.getParameterString("PageMap", macro);

            User operatorUser = macro.RunningMe;
            Story currStory = new Story(config.conn, StoryID, macro.RunningMe);

            string[] aPages = PageMap.Split('|');

            List<string> sqlCmds = new List<string>();

            int newSeqNumber = 0;

            foreach (string sPageID in aPages)
            {
                if(sPageID.Trim().Length == 0) break;   
                newSeqNumber++;

                sqlCmds.Add("UPDATE page SET seq = " + newSeqNumber + " WHERE id = " + sPageID);
                
                //$todo: updates if page is from another story!!!
                //sqlCmds.Add("UPDATE StoryPage SET story_id = " + StoryID + " WHERE page_id = " + sPageID);

                //Page p = new Page(config.conn, Convert.ToInt32(sPageID));
                ////if (p == null) break;

                //foreach (PageElementMap pem in p.PageElementMaps)
                //{
                //    //if (pem == null) break;
                //    sqlCmds.Add("UPDATE StoryPageElement SET story_id = " + StoryID + " WHERE pageElement_ID = " + pem.PageElementID);
                //}


            }

            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);

            int counter = 0;

            foreach (string sqlCmd in sqlCmds)
            {
                dbHelper.cmd.CommandText = sqlCmd;
                int numRows = dbHelper.cmd.ExecuteNonQuery();
                counter = numRows + counter;

            }

            dbHelper.cleanup();



           Logger.log(operatorUser.UserName + " changed the page order for story " + currStory.ID + " for a total of " + counter + " pages");

           
            MacroEnvelope me = new MacroEnvelope();
            Macro procPageID = new Macro("ProcessStoryPageOrder", 1);
            procPageID.addParameter("StoryID", "" + StoryID);
            me.addMacro(procPageID);

            return me;
        }
    }
}
