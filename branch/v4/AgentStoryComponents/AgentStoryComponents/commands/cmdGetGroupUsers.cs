using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdGetGroupUsers : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int GroupID = MacroUtils.getParameterInt("GroupID", macro);

            StringBuilder userIDs = new StringBuilder("");
            StringBuilder userNames = new StringBuilder("");

            Group g = new Group(config.conn, GroupID);
            List<User> users = g.GroupUsers;

            foreach (User u in users)
            {
                userIDs.Append(u.ID);
                userIDs.Append ("|");

                userNames.Append(u.UserName);
                userNames.Append ("|");
            }

            if (users.Count > 0)
            {
                userIDs.Remove(userIDs.Length - 1, 1);
                userNames.Remove(userNames.Length - 1, 1);
            }

            MacroEnvelope me = new MacroEnvelope();

            Macro procPageID = new Macro("LoadUsersGroups", 3);
            procPageID.addParameter("userIDs",userIDs.ToString() );
            procPageID.addParameter("userNames",userNames.ToString() );
            procPageID.addParameter("count", "" + users.Count );
            me.addMacro(procPageID);

            return me;
        }
    }
}
