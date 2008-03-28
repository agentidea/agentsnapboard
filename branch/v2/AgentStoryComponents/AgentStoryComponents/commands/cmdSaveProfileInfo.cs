


using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdSaveProfileInfo : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // add new page to db.
            string pageName = MacroUtils.getParameterString("pageName", macro);
            int gridCols = MacroUtils.getParameterInt("gridCols", macro);
            

            //$todo: how to pass user context into commands???
            User by = macro.RunningMe;

          

            //$TODO: LOG EASIER
            utils ute = new utils();
            string msg = "user ( " + by.UserName + " ) saved new profile info";
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("ProcessProfileSave", 1);
            m.addParameter("msg", "" + ute.encode64(msg) );
            me.addMacro(m);
            return me;
        }
    }
}


         //StringBuilder saveMsg = new StringBuilder();

         //       string newPassword = Request["txtPassword"];
         //       if (base.currentUser.Password.Trim() != newPassword.Trim())
         //       {
         //           base.currentUser.Password = newPassword.Trim();
         //           saveMsg.Append("New Password saved. Please re-login.");
         //           saveMsg.Append("<br>");
         //           Session["loggedIn"] = false;
         //       }
         //       string newUserName = Request["txtUserName"];
         //       if (base.currentUser.UserName != newUserName)
         //       {
         //           base.currentUser.UserName = newUserName;
         //           saveMsg.Append("New Username Name saved.");
         //           saveMsg.Append("<br>");
         //       }
         //       string newFirstName = Request["txtFirstName"];
         //       if (base.currentUser.FirstName != newFirstName)
         //       {
         //           base.currentUser.FirstName = newFirstName;
         //           saveMsg.Append("New First Name saved.");
         //           saveMsg.Append("<br>");
         //       }
         //       string newLastName = Request["txtLastName"];
         //       if (base.currentUser.LastName != newLastName)
         //       {
         //           base.currentUser.LastName = newLastName;
         //           saveMsg.Append("New Last Name saved.");
         //           saveMsg.Append("<br>");
         //       }
         //       string newEmail = Request["txtEmail"];
         //       if (base.currentUser.Email.Trim() != newEmail.Trim())
         //       {
         //           base.currentUser.Email = newEmail.Trim();
         //           saveMsg.Append("New Email saved. Please re-login.");
         //           saveMsg.Append("<br>");
         //           Session["loggedIn"] = false;
         //       }

         //       string newNotificationSchedule = Request["hdnNotificationSchedule"];
         //       if (base.currentUser.NotificationFrequency != Convert.ToInt32(newNotificationSchedule))
         //       {
         //           base.currentUser.NotificationFrequency = Convert.ToInt32(newNotificationSchedule);
         //           saveMsg.Append("notification schedule changed");
         //           saveMsg.Append("<br>");
         //       }

         //       if (saveMsg.Length > 0)
         //       {
         //           base.currentUser.Save();
         //           this.displayMsg(saveMsg.ToString());
         //       }
       