using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdAddNewGroup : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // add new page to db.
            string groupName = MacroUtils.getParameterString("groupName", macro);
            string groupDescription = MacroUtils.getParameterString("groupDescription", macro);
            int UserID = MacroUtils.getParameterInt("UserID", macro);

            utils ute = new utils();

            //$todo: how to pass user context into commands???
            User by = new User(config.conn, UserID);

            if (groupName.Trim().Length == 0) throw new FormatException("Please supply a vaild group name");

            Group groupNew = new Group(config.conn,by);

            groupNew.Description = groupDescription;
            groupNew.Name = groupName;

            MacroEnvelope me = new MacroEnvelope();

            try
            {
                groupNew.Save();
            }
            catch ( GroupExistsException gee )
            {
                //group already added
                Macro m = new Macro("DisplayAlert", 2);
                m.addParameter("msg", ute.encode64("group already exists, try a different name."));
                m.addParameter("severity", "1");
                me.addMacro(m);

                return me;
            }
            catch (Exception ex)
            {
                throw new Exception(" error creating group " + ex.Message);
            }

            //? add creator to the group? no!
            //yes!
            groupNew.AddUser(by);
           
            //$TODO: LOG EASIER
            
            string groupNameH = ute.decode64( groupNew.Name );
            string msg = "new group (" + groupNew.ID + ") [" + groupNameH + "] added by " + by.UserName;
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            Macro procPageID = new Macro("ProcessNewGroup", 3);
            procPageID.addParameter("newGroupID", "" + groupNew.ID );
            procPageID.addParameter("groupName", "" + groupNew.Name );
            procPageID.addParameter("groupGUID", "" + groupNew.GUID );

            me.addMacro(procPageID);

            return me;
        }
    }
}
