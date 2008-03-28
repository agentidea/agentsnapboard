using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    /// <summary>
    /// invite code deprecated from system
    /// open to public
    /// </summary>
    public class cmdSaveUser : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {


            User operatorUser = macro.RunningMe;
            //if (operatorUser.isObserver())
            //    throw new InvalidOperationException("Observers may not add nor save user profiles");

        

            string firstName64    = MacroUtils.getParameterString("firstName", macro);
            string lastName64     = MacroUtils.getParameterString("lastName", macro);
            string username64     = MacroUtils.getParameterString("username", macro);
            string password64     = MacroUtils.getParameterString("password", macro);
            string email64        = MacroUtils.getParameterString("email", macro);
            string roles64        = MacroUtils.getParameterString("roles", macro);
            string tags64         = MacroUtils.getParameterString("tags", macro);
            //string inviteCode64   = MacroUtils.getParameterString("inviteCode", macro);
            int operatorID      = MacroUtils.getParameterInt("operatorID", macro);
            string action =         MacroUtils.getParameterString("action", macro);
            int notificationSchedule = MacroUtils.getParameterInt("notificationSchedule", macro);

            utils ute = new utils();

            string firstName    = ute.decode64( firstName64 );
            string lastName     = ute.decode64( lastName64  ); 
            string username     = ute.decode64( username64   );
            string password     = ute.decode64( password64   );
            string email        = ute.decode64( email64      );
            string roles        = ute.decode64( roles64      );

           


            string tags   = "";

            if (tags64 == "AA==") // to do hack 'empty' tags in base 64 tranlate to \0
            {
                tags = null;
            }
            else
            {
                tags =  ute.decode64( tags64 );
            }

           // string inviteCode   = ute.decode64( inviteCode64 );
            string inviteCode = ute.encode64( ute.getGUID().ToString() );

            string returnMacroToRun = "";

            string msg = "";

            int currUserSponsorID = macro.RunningMe.ID;
            if (currUserSponsorID != operatorID)
            {
                throw new PossibleHackException("operatorID may have changed " + currUserSponsorID + " NE " + operatorID);
            }


            User newUser = null;


            if (action.Trim().ToLower() == "save_new")
            {
                //save a new user.
                newUser = new User(config.conn);
                if (newUser.UsernameExists(username.Trim()))
                {
                    throw new UserExistsException(" username " + username.Trim() + " in use");
                }

                if (newUser.UserExists(email.Trim()))
                {

                    //if (email.Trim() == config.webMasterEmail.Trim() && config.allowMulipleWebmasterAliases)
                    //{
                        //allow webmaster to hold muliple aliases
                    //    Logger.log(macro.RunningMe.UserName + " added an alias " + email.Trim() + "");
                   // }
                   // else
                   // {
                        throw new UserExistsException("user with email email " + email.Trim() + " already registered");
                   // }
                }

                newUser.FirstName = firstName.Trim();
                newUser.LastName = lastName.Trim();
                newUser.UserName = username.Trim();
                newUser.Password = password.Trim();
                newUser.Email = email.Trim();
                newUser.Roles = roles.Trim();

                if (tags != null && tags.Trim().Length > 0)
                {
                    newUser.Tags = tags.Trim();
                }

                newUser.SponsorID = operatorID;
                newUser.OrigInviteCode = inviteCode.Trim();
                
                newUser.NotificationFrequency = 1;  //immediate?

                int newUserID = newUser.Save();


                if (config.bRequireVerificationForUserReg)
                {
                    newUser.State = (int)UserStates.pending_email_confirm;

                    EmailMsg eMsg = new EmailMsg(config.conn, macro.RunningMe);


                    string subject = config.Orginization + " - account activation instructions";
                    eMsg.subject = ute.encode64(subject);

                    eMsg.ReplyToAddress = config.webMasterEmail;

                    string activationCode = ute.encode64(newUser.PendingGUID);

                    string body = "Dear " +  newUser.UserName;
                    body += @"
                            ";
                    body += @"
                            ";
                    body += @"
Welcome to " + config.Orginization;
                    body += @"
                            ";
                    body += @"
                            ";
                    body += @"
Please activate your account by clicking on the url below ";

                    body += config.protocol + "://" + config.host + "/" + config.app + "/AccountActivation.aspx?ac=" + activationCode;
                    body += @"
                            ";
                    body += @"
Key:";
                    body += activationCode;
                    body += @"
                            ";
                    body += @"
                            ";
                    body += @"
If you can't activate your account using the URL above,
try to the above Key at: ";
                    body += config.protocol + "://" + config.host + "/" + config.app + "/AccountActivation.aspx";
                    body += @"
                            ";
                    body += @"Thanks, 

                              The Webmaster";




                    eMsg.body = ute.encode64( body );

                    eMsg.from = config.webMasterEmail;
                    eMsg.to = newUser.Email;

                    eMsg.Save();


                    try
                    {

                        PostMan postino = new PostMan(config.conn);
                        postino.SendMessage(eMsg.ID);

                    }
                    catch (Exception ex)
                    {

                        Logger.log("error sending account activation email " + ex.Message);
                    }



                }
                else
                {
                    newUser.State = (int)UserStates.pre_approved;
                }

                newUser.Save();



                returnMacroToRun = "AddNewUser";

                msg = macro.RunningMe.UserName + " [" + macro.UserCurrentTxID +  " ]("+macro.RunningMe.ID +") added user profile for (" + newUser.UserName + ") to system with an ID of " + newUser.ID + " GUID of " + newUser.UserGUID + " sponsored by user of id of " + operatorID;
            
            }
            else
                if (action.Trim().ToLower() == "save_existing")
                {
                    try
                    {
                        newUser = new User(config.conn, email.Trim());
                    }
                    catch (UserDoesNotExistException unex)
                    {

                        //try lookup user via username
                        try
                        {
                            newUser = new User(config.conn, username.Trim(), true);
                        }
                        catch (UserDoesNotExistException unex2)
                        {

                            //try lookup via user operator ID
                            newUser = new User(config.conn, macro.RunningMe.ID);

                            //smell and slippery when wet, what if other than user is been 
                            //changed say by admin, no worries pass up user id then
                            //$todo:

                        }
                    }



                    newUser.FirstName = firstName.Trim();
                    newUser.LastName = lastName.Trim();
                    newUser.UserName = username.Trim();
                    newUser.Password = password.Trim();

                    if (newUser.Password.ToLower() == config.defaultPassword)
                    {
                        newUser.State = (int)UserStates.signed_in;
                        Logger.log(" user changed default password ");
                    }


                    newUser.Email = email.Trim();
                    newUser.Roles = roles.Trim();
                    newUser.Tags = tags.Trim();
                    newUser.NotificationFrequency = notificationSchedule;

                    

                   

                    newUser.Save();
                    returnMacroToRun = "SaveExistingUser";


                    if (macro.RunningMe.Email == config.publicUserEmail)
                    {
                        msg = "Thank you, " + newUser.UserName + ") - please check your email for account activation instructions.";
                    }
                    else
                    {
                        msg = "Saved user profile for (" + newUser.UserName + ") - they will be sent an email with account activation instructions.";
                    }

                    Logger.log(msg);
                  
                }


           

           StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;

         

            msg = ute.encode64(msg);

            MacroEnvelope me = new MacroEnvelope();

            Macro procAddNewUser = new Macro(returnMacroToRun, 1);
            procAddNewUser.addParameter("msg", msg);
            me.addMacro(procAddNewUser);

            return me;
        }
    }
}
