using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdRemoveUsersFromGroup : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int GroupID = MacroUtils.getParameterInt("GroupID", macro);
            string UserID = MacroUtils.getParameterString("UserIDs", macro);



            string msg = null;
            utils ute = new utils();
            Group g = new Group(config.conn, GroupID);

            User runningMe = macro.RunningMe;



            


            string[] users = UserID.Split('|');

            int userCounter = 0;

            foreach (string  usrID in users)
            {
                if (usrID.Trim().Length > 0)
                {
                    User u = new User(config.conn, Convert.ToInt32(usrID) );

                    if (runningMe.ID == u.ID)
                    {
                        //its me, I can leave groups at will.
                        if (g.By.ID == runningMe.ID)
                        {
                            //but wait I'm the group owner, can't leave.
                            Logger.log("group owner " + g.By.UserName + " tried to leave their group " + ute.decode64( g.Name));
                            continue;
                        }
                    }
                    else
                    {
                        if (g.By.ID == runningMe.ID)
                        {

                            //owner of group, ok
                        }
                        else
                        {
                            //i have no right to do this
                            continue;
                        }
                    }

                    g.RemoveUser(u);
                    userCounter++;

                    //$TODO: LOG EASIER
                    
                    //string storyNameH = ute.decode64(macro.Parameters[0].Val);
                    msg = "removed (" + u.UserName + ") from group ( " + ute.decode64(g.Name) + " ) total removed (" + userCounter + ")";
                    StoryLog sl = new StoryLog(config.conn);
                    sl.AddToLog(msg);
                    sl = null;

                }
            }



            msg = "removed (" + userCounter + ") users from group (" + ute.decode64(g.Name) + ")";
            msg = ute.encode64(msg);
           

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("RemoveUsersFromGroup", 1);
            //procPageID.addParameter("numberOfUsersRemoved", "" + userCounter );
            procPageID.addParameter("msg", msg );

            me.addMacro(procPageID);

            return me;
        }
    }
}


