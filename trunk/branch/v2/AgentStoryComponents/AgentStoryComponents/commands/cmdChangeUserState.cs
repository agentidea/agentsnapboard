using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdChangeUserState : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            int userID = MacroUtils.getParameterInt("UserID", macro);
            int userState = MacroUtils.getParameterInt("State", macro);
            User userToChangeState = new User(config.conn, userID);
            User operatorUser = macro.RunningMe;

            if (operatorUser.isRoot() || operatorUser.isAdmin()  || userToChangeState.SponsorID == operatorUser.ID )
            {
                //good
            }
            else
            {
                throw new InvalidOperationException("Only root/admin can do this operation");
            }

        
            
            string OrigStateName = userToChangeState.StateHR;

            userToChangeState.State = userState;
            userToChangeState.Save();


            //who is changing this user profile
            User by = macro.RunningMe;


            //$TODO: LOG EASIER
            utils ute = new utils();
            string msg = "user with username " + userToChangeState.UserName + " [" + userToChangeState.ID + "] has had a state change from " + OrigStateName  + " to " + userToChangeState.StateHR + " - operation performed by " + by.UserName + "[" + by.ID + "]";
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            MacroEnvelope me = new MacroEnvelope();

            Macro ProcessTerminateProfile = new Macro("ProcessChangeOfProfile", 1);
            ProcessTerminateProfile.addParameter("msg", "" + ute.encode64(msg));
            me.addMacro(ProcessTerminateProfile);

            return me;
        }
    }
}



