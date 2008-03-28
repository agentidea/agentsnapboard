using System;
using System.Collections.Generic;
using System.Text;

namespace AgentStoryComponents
{
    public class PostMan
    {

        AgentStoryComponents.core.utils ute = new AgentStoryComponents.core.utils();

        private string _conn;

        public string conn
        {
            get { return _conn; }
            
        }
	
        public PostMan(string asConn)
        {
            //read off config
            this._conn = asConn;
            aspNetEmail.EmailMessage.LoadLicenseFile(config.aspNetEmailLicensePath );
        }

        public void SendMessages(EmailStates state)
        {

            StoryLog sl = new StoryLog(config.conn);
            Emails emails = new Emails(this._conn);
            List<EmailMsg> messagesToSend = emails.getEmailMessages(state);

            foreach (EmailMsg emsg in messagesToSend)
            {
                try
                {
                    sendMail(emsg);

                    //$TODO: LOG EASIER
                    string successMsg = "EMAIL [" + emsg.ID + "] to [ " + emsg.to + " ]  was SENT ";
                    
                    sl.AddToLog(successMsg);
                   

                    emsg.LastError = ute.encode64(successMsg);
                    emsg.Save();

                }
                catch (Exception ex)
                {
                    //$TODO: LOG EASIER
                    string errMsg = "EMAIL [" + emsg.ID + "] to [ "+emsg.to+" ]  was NOT SENT :" + ex.Message;
                   
                    sl.AddToLog(errMsg);
                   

                    emsg.LastError = ute.encode64(errMsg);
                    emsg.Save();

                }
            }

            sl = null;
               
                

        }

        private void sendMail( EmailMsg emsg)
        {

            
            aspNetEmail.EmailMessage msg = new aspNetEmail.EmailMessage(config.smtpServer);
            
            msg.ValidateAddress = false;

            User from = null;
            try
            {
                from = new User(config.conn, emsg.from);
            }
            catch (Exception ex)
            {
                throw new UserDoesNotExistException("Invalid sender [" + emsg.from + "].  Can only send emails from users that are registered on this system");
            }

            User reply = null;
            try
            {
                reply = new User(config.conn, emsg.ReplyToAddress);
            }
            catch (Exception ex)
            {
                throw new UserDoesNotExistException("Invalid replyto [" + emsg.ReplyToAddress + "].  Can only reply to emails from users that are registered on this system");
            }

            User to = null;
            try
            {
                to = new User(config.conn, emsg.to);
            }
            catch (Exception ex)
            {
                throw new UserDoesNotExistException("Invalid recipient [" + emsg.to + "]. Can only send emails to users that are registered on this system");
            }

            msg.BodyFormat = aspNetEmail.MailFormat.Text;

            //if (to.NotificationTypes.IndexOf("html") != -1)
            //{
            //    msg.BodyFormat = aspNetEmail.MailFormat.Html;
            //}

            msg.AddTo(to.Email,"<" + to.FirstName + ", " + to.LastName + "> ");
           // msg.ReturnReceipt = true;
            //msg.ReturnReceiptAddress = emsg.ReplyToAddress;
            msg.FromAddress = emsg.from;
            //msg.FromName = from.FirstName + " " + from.LastName + " ( aka. " + from.UserName + "  ) ";
            msg.FromName = reply.FirstName + " " + reply.LastName + " ( aka. " + reply.UserName + "  ) ";
            msg.ReplyTo = emsg.ReplyToAddress;
            msg.Importance = aspNetEmail.MailPriority.High;
            msg.Priority = aspNetEmail.MailPriority.High;
            msg.Organization = config.Orginization;

            if (emsg.ReplyToAddress.Trim().ToLower() == config.anonEmailReplyTo.Trim().ToLower())
            {
                //ANON REPLY TO
                msg.Subject = "[" +  from.UserName + " | " + from.UserGUID + "] " + ute.decode64(emsg.subject);
                msg.Body = ute.decode64(emsg.body) + config.AnonEmailBodyFooter + " " + config.GeneralEmailBodyFooter + config.HelpEmailBodyFooter;
            }
            else
            {
                //NON ANON REPLY TO
                msg.Subject = ute.decode64(emsg.subject);
                msg.Body = ute.decode64(emsg.body) + config.GeneralEmailBodyFooter + config.HelpEmailBodyFooter;
            }


            
            
            msg.Username = config.smtpUser;
            msg.Password = config.smtpPwd;

            try
            {
                emsg.changeState(EmailStates.sending);
                msg.Send();
                emsg.changeState(EmailStates.sent);
            }
            catch (Exception ex)
            {
                //$TODO: LOG EASIER
                string errMsg = "EMAIL [" + emsg.ID + "]  was NOT SENT :" + ex.Message;
                StoryLog sl = new StoryLog(config.conn);
                sl.AddToLog(errMsg);
                sl = null;

                emsg.LastError = ute.encode64(errMsg);
                emsg.Save();

                throw new MessageNotSentException(errMsg);

            }
        
        
        }


        public void SendMessage(int msgID)
        {
            EmailMsg msgToSend = new EmailMsg(config.conn, msgID);




            sendMail(msgToSend);
        }
    }




}
