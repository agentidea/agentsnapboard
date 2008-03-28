using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdTerminateProfile : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            User operatorUser = macro.RunningMe;
            if (operatorUser.isObserver())
                throw new InvalidOperationException("Observers may not alter user profiles");


            int userID = MacroUtils.getParameterInt("UserID", macro);
            
            User userToTerm = new User(config.conn, userID);
            userToTerm.State = (int)UserStates.terminated;
            userToTerm.Save();

            
            //terminate this user profile
            User by = macro.RunningMe;

          
            //$TODO: LOG EASIER
            utils ute = new utils();
            string msg = "user with username " + userToTerm.UserName + " and id [" + userToTerm.ID + "] has been terminated by user id " + by.ID + " " + by.UserName;
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            MacroEnvelope me = new MacroEnvelope();

            Macro ProcessTerminateProfile = new Macro("ProcessTerminateProfile", 1);
            ProcessTerminateProfile.addParameter("msg", "" + ute.encode64(msg));
            me.addMacro(ProcessTerminateProfile);

            return me;
        }
    }
}



 //                   Session["user"] = null;
 //                   Session["loggedIn"] = false;
 //                   Session.Clear();
 //                   Session.Abandon();
 //                   Response.Redirect("default.aspx?msg=" + Server.UrlEncode(" Goodbye! ('" + base.currentUser.UserName + "')."));
