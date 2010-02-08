using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdAddUsersToGroup : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int GroupID = MacroUtils.getParameterInt("GroupID", macro);
            string UserID = MacroUtils.getParameterString("UserIDs", macro);
            string msg = null;

            utils ute = new utils();

            Group g = new Group(config.conn, GroupID);

            User runningMe = macro.RunningMe;
            if (g.By.ID != runningMe.ID)
            {
                throw new InvalidGroupActionException(" you need to be group owner " + g.By.UserName + "  in order to do that");
            }

            string[] users = UserID.Split('|');

            int userCounter = 0;

            foreach (string  usrID in users)
            {
                if (usrID.Trim().Length > 0)
                {
                    User u = new User(config.conn, Convert.ToInt32(usrID) );
                    userCounter++;
                    g.AddUser(u);

                    //$TODO: LOG EASIER
                    
                    //string storyNameH = ute.decode64(macro.Parameters[0].Val);
                    msg = "added (" + u.UserName + ") to group ( " + ute.decode64(g.Name) + " ) total added (" + userCounter + ")";
                    StoryLog sl = new StoryLog(config.conn);
                    sl.AddToLog(msg);
                    sl = null;

                }
            }
            

            msg = "added (" + userCounter + ") users to group (" +  ute.decode64(g.Name) + ")";

            msg = ute.encode64(msg);

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("AddUsersToGroup", 1);
            //procPageID.addParameter("numberOfUsersAdded", "" + userCounter );
            procPageID.addParameter("msg", msg );

            me.addMacro(procPageID);

            return me;
        }
    }
}
