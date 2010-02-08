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
            int ExistingUserID = MacroUtils.getParameterInt("ProfileID", macro);
            int operatorID = MacroUtils.getParameterInt("operatorID", macro);
            string action =         MacroUtils.getParameterString("action", macro);  //$to do: updateUserCMD???
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


            User _user = null;


            if (action.Trim().ToLower() == "save_new")
            {
                #region save a new user.
                _user = new User(config.conn);
                if (_user.UsernameExists(username.Trim()))
                {
                    throw new UserExistsException(" username " + username.Trim() + " in use");
                }

                if (_user.UserExists(email.Trim()))
                {

                    //if (email.Trim() == config.webMasterEmail.Trim() && config.allowMulipleWebmasterAliases)
                    //{
                        //allow webmaster to hold muliple aliases
                    //    Logger.log(macro.RunningMe.UserName + " added an alias " + email.Trim() + "");
                   // }
                   // else
                   // {
                        throw new UserExistsException("user with email " + email.Trim() + " already registered");
                   // }
                }

                _user.FirstName = firstName.Trim();
                _user.LastName = lastName.Trim();
                _user.UserName = username.Trim();
                _user.Password = password.Trim();
                _user.Email = email.Trim();
                _user.Roles = roles.Trim();

                if (tags != null && tags.Trim().Length > 0)
                {
                    _user.Tags = tags.Trim();
                }

                _user.SponsorID = operatorID;
                _user.OrigInviteCode = inviteCode.Trim();
                
                _user.NotificationFrequency = 1;  //immediate?

                int newUserID = _user.Save();


                if (config.bRequireVerificationForUserReg)
                {
                    _user.State = (int)UserStates.pending_email_confirm;

                    EmailMsg eMsg = new EmailMsg(config.conn, macro.RunningMe);


                    string subject = config.Orginization + " - account activation instructions";
                    eMsg.subject = ute.encode64(subject);

                    eMsg.ReplyToAddress = config.webMasterEmail;

                    string activationCode = ute.encode64(_user.PendingGUID);

                    string body = "Dear " +  _user.UserName;
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

                    body += config.protocol + "://" + config.host + "/" + config.app + "/screens/AccountActivation.aspx?ac=" + activationCode;
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
                    body += config.protocol + "://" + config.host + "/" + config.app + "/screens/AccountActivation.aspx";
                    body += @"
                            ";
                    body += @"Thanks, 

                              The Webmaster";




                    eMsg.body = ute.encode64( body );

                    eMsg.from = config.webMasterEmail;
                    eMsg.to = _user.Email;

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
                    _user.State = (int)UserStates.pre_approved;
                }

                _user.Save();



                returnMacroToRun = "AddNewUser";

                msg = macro.RunningMe.UserName + " [" + macro.UserCurrentTxID +  " ]("+macro.RunningMe.ID +") added user profile for (" + _user.UserName + ") to system with an ID of " + _user.ID + " GUID of " + _user.UserGUID + " sponsored by user of id of " + operatorID;
                #endregion
            }
            else
            if (action.Trim().ToLower() == "save_existing")
            {
                #region update existing user

                
                _user = new User(config.conn, ExistingUserID);

                _user.FirstName = firstName.Trim();
                _user.LastName = lastName.Trim();
                _user.UserName = username.Trim();
                _user.Password = password.Trim();

                if (_user.Password.ToLower() == config.defaultPassword)
                {
                    _user.State = (int)UserStates.signed_in;
                    Logger.log(" user changed default password ");
                }


                _user.Email = email.Trim();
                _user.Roles = roles.Trim();
                _user.Tags = tags.Trim();
                _user.NotificationFrequency = notificationSchedule;

                

               

                _user.Save();
                returnMacroToRun = "SaveExistingUser";
  
               msg = "Saved user profile for (" + _user.UserName + ")";
               
                Logger.log(msg);
                #endregion
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
