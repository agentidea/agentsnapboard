using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Emails
    {
        private string _conn;
        private utils ute = new utils();

        public string conn
        {
            get { return _conn; }
        }
	
        public Emails(string asConnectionString)
        {
            this._conn = asConnectionString;
        }

        public List<EmailMsg> getEmailMessages(EmailStates state)
        {
            
                List<EmailMsg> msgs = new List<EmailMsg>();

                string sql = "";
                sql += "SELECT * FROM emailMessage WHERE state = " + (int)state + " ORDER BY dateAdded DESC ";

                OleDbHelper dbHelper = ute.getDBcmd(this._conn);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    while (dbHelper.reader.Read())
                    {
                        int id = Convert.ToInt32(dbHelper.reader["ID"]);

                        EmailMsg msg = new EmailMsg(this._conn, id);

                        msgs.Add(msg);

                    }
                }

                dbHelper.cleanup();


                return msgs;
            

        }

        public List<EmailMsg> getEmailMessages(User by)
        {
            List<EmailMsg> msgs = new List<EmailMsg>();

            string sql = "";
            sql += "SELECT * FROM vEmailMessage WHERE userAddedID = " + by.ID + " ORDER BY dateAdded DESC ";

            OleDbHelper dbHelper = ute.getDBcmd(this._conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    int id = Convert.ToInt32(dbHelper.reader["ID"]);

                    EmailMsg msg = new EmailMsg(this._conn, id);

                    msgs.Add(msg);

                }
            }

            dbHelper.cleanup();


            return msgs;

        }

        public List<EmailMsg> getEmailMessages(string toEmailAddress)
        {
            List<EmailMsg> msgs = new List<EmailMsg>();

            string sql = "";
            sql += "SELECT * FROM vEmailMessage WHERE toAddress = '" + toEmailAddress.Trim() + "'" + " ORDER BY dateAdded DESC ";

            OleDbHelper dbHelper = ute.getDBcmd(this._conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    int id = Convert.ToInt32(dbHelper.reader["ID"]);

                    EmailMsg msg = new EmailMsg(this._conn, id);

                    msgs.Add(msg);

                }
            }

            dbHelper.cleanup();


            return msgs;

        }


        public string getEmailMsgMetaJSON(User by)
        {

   /*
    * 
   {'draft_messages':
               {
                   { 'subject':'test','reply-to':'g@agentidea.com','to':'xx',stateHR:'sent',state:2  },
                   { 'subject':'testB', ... }
               }
               ,
    'sent_messages':
               {
                   { 'subject':'test','reply-to':'g@agentidea.com','to':'xx',stateHR:'sent',state:2  },
                   { 'subject':'testB', ... }
               }
               ,
    'inbox_messages':
               {
                   { 'subject':'test','reply-to':'g@agentidea.com','to':'xx',stateHR:'sent',state:2  },
                   { 'subject':'testB', ... }
               }
                
   }
   */

            List<EmailMsg> msgsByMe = this.getEmailMessages(by);
            List<EmailMsg> msgsToMe = this.getEmailMessages(by.Email);


            StringBuilder json = new StringBuilder();

            json.Append("{");

                json.Append("\r\n");
                prepEmailFolder(msgsByMe, json,"draft_messages",EmailStates.draft);
                json.Append(",");
                
                json.Append("\r\n");
                prepEmailFolder(msgsByMe, json, "sent_messages", EmailStates.sent);
                json.Append(",");
                
                json.Append("\r\n");
                prepEmailFolder(msgsByMe, json, "outbox_messages", EmailStates.sending);
                json.Append(",");
                
                json.Append("\r\n");
                prepEmailFolder(msgsToMe, json, "inbox_messages", EmailStates.sent);
                
            
            json.Append("}");

            return json.ToString();
        }

        private void prepEmailFolder(List<EmailMsg> msgs, StringBuilder json,string folderName,EmailStates state)
        {
            int msgsInFolder = 0;

            json.Append("\r\n'");
            json.Append(folderName);
            json.Append("':");
            json.Append("[");

            if (msgs.Count == 0)
            {
                //empty
            }
            else
            {
                foreach (EmailMsg msg in msgs)
                {
                    if (msg.state != (int)state) continue;
                    
                    msgsInFolder++;

                    json.Append("{");

                    json.Append("'id':");
                    json.Append(msg.ID);
                    json.Append(",");

                    json.Append("'dateTime':");
                    json.Append("'");
                    json.Append(ute.encode64(msg.dateAdded.ToShortDateString() + " " + msg.dateAdded.ToShortTimeString()));
                    json.Append("'");
                    json.Append(",");

                    json.Append("'subject':");
                    json.Append("'");
                    json.Append(msg.subject);
                    json.Append("'");
                    json.Append(",");

                    json.Append("'guid':");
                    json.Append("'");
                    json.Append(msg.guid);
                    json.Append("'");
                    json.Append(",");

                    json.Append("'status':");
                    json.Append("'");
                    json.Append(msg.LastError);
                    json.Append("'");
                    json.Append(",");

                    json.Append("'reply_to':");
                    json.Append("'");
                    json.Append(ute.encode64(msg.ReplyToAddress));
                    json.Append("'");
                    json.Append(",");

                    json.Append("'to':");
                    json.Append("'");
                    json.Append(ute.encode64(msg.to));
                    json.Append("'");

                    json.Append("}");
                    json.Append(",");
                }
                if (msgsInFolder > 0)
                {
                    json.Remove(json.Length - 1, 1);  //1/more mesages in folder remove trailing comma
                }

            }// end if

            json.Append("]");
        }



    }
}
