using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public enum EmailStates : int
    {
        draft=0,tobesent=1,sent=2,failed=3,deleted=4,sending=5
    }


    public class EmailMsg
    {
        private utils ute = new utils();
        private int _id = -1;
        private User _by;
        private string _guid;
        private string _lastError;

        public string LastError
        {
            get { return _lastError; }
            set { _lastError = value; }
        }
	
        private DateTime _lastModified;

        public DateTime LastModified
        {
            get { return _lastModified; }
            set { _lastModified = value; }
        }
	

        public string guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
	

        public User by
        {
            get { return _by; }
        }
	
        private string _subject;
        

        public string StateHr
        {
            get 
            {
                string lsStateHR = "";

                switch (this.state)
                {
                    case 0:
                        lsStateHR = "draft";
                        break;
                    case 1:
                        lsStateHR = "to be sent";
                        break;
                    case 2:
                        lsStateHR = "sent";
                        break;
                    case 3:
                        lsStateHR = "failed";
                        break;
                    case 4:
                        lsStateHR = "deleted";
                        break;
                    case 5:
                        lsStateHR = "sending";
                        break;
                    default:
                        throw new InvalidOperationException(" state "+ this.state +" for email not defined as Human Readable");
                        break;
                }

                return lsStateHR;

            }
        }
	
        private int _state;
        private string _to;
        private string _from;
        private string _body;
        private DateTime _dateAdded;
        private int _userAddedID;
        private string _connectionString = null;
        private string _replyTo;

        public string ReplyToAddress
        {
            get { return _replyTo; }
            set { _replyTo = value; }
        }
	

        public EmailMsg(string asConnectionString, User by)
        {
            //new message
            this._connectionString = asConnectionString;
            this._by = by;
        }

        public EmailMsg(string asConnectionString, int id)
        {
            //retrieve existing message
            this._connectionString = asConnectionString;
            loadEmailFromDB(id);
        }

        public EmailMsg(string asConnectionString, Guid guid)
        {
            //retrieve existing message
            this._connectionString = asConnectionString;
            loadEmailFromDB(guid);
        }

        public void changeState(EmailStates state)
        {
            this._state = (int)state;
            this.Save();

        }
        private void loadEmailFromDB(int id)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from emailMessage WHERE id = ";
            sql += id;


            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.subject = (string)dbHelper.reader["subject"];
                this.ID = System.Convert.ToInt32(dbHelper.reader["id"]);
                this.dateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);
                this.LastModified = System.Convert.ToDateTime(dbHelper.reader["lastModified"]);
                this.guid = System.Convert.ToString(dbHelper.reader["guid"]);
                this.to = (string)dbHelper.reader["to"];
                this.from = (string)dbHelper.reader["from"];
                this.body = (string)dbHelper.reader["body"];
                this._state = Convert.ToInt32(dbHelper.reader["state"]);
                this.ReplyToAddress = (string)dbHelper.reader["reply_to"]; 
                int userAddedID = System.Convert.ToInt32(dbHelper.reader["userAddedID"]);

                if (dbHelper.reader["lastError"] is DBNull)
                {
                    this.LastError = null;
                }
                else
                {
                    this.LastError = (string)dbHelper.reader["lastError"];
                }

                //load the by user if null
                if (this._by == null)
                {
                    this._by = new User(this._connectionString, userAddedID);
                }

            }

            dbHelper.cleanup();
            dbHelper = null;
        }
        private void loadEmailFromDB(System.Guid guid)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from emailMessage WHERE guid = '";
            sql += guid.ToString();
            sql += "'";


            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.subject = (string)dbHelper.reader["subject"];
                this.ID = System.Convert.ToInt32(dbHelper.reader["id"]);
                this.dateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);
                this.LastModified = System.Convert.ToDateTime(dbHelper.reader["lastModified"]);
                this.guid = System.Convert.ToString(dbHelper.reader["guid"]);
                this.to = (string)dbHelper.reader["to"];
                this.from = (string)dbHelper.reader["from"];
                this.body = (string)dbHelper.reader["body"];
                this._state = Convert.ToInt32(dbHelper.reader["state"]);
                this.ReplyToAddress = (string)dbHelper.reader["reply_to"]; 
                int userAddedID = System.Convert.ToInt32(dbHelper.reader["userAddedID"]);
                if (dbHelper.reader["lastError"] is DBNull)
                {
                    this.LastError = null;
                }
                else
                {
                    this.LastError = (string)dbHelper.reader["lastError"];
                }
                //load the by user if null
                if (this._by == null)
                {
                    this._by = new User(this._connectionString, userAddedID);
                }

            }

            dbHelper.cleanup();
            dbHelper = null;
        }

        public int userAddedID
        {
            get { return _userAddedID; }
            set { _userAddedID = value; }
        }
	

        public DateTime dateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
	

        public string body
        {
            get { return _body; }
            set { _body = value; }
        }
	

        public string from
        {
            get { return _from; }
            set { _from = value; }
        }
	

        public string to
        {
            get { return _to; }
            set { _to = value; }
        }
	

        public int state
        {
            get { return _state; }
            
        }
	

        public string subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
	

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }



        public int Save()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";
            string currentTime = ute.getDateStamp();

            if (this.ID == -1)
            {
                //new email INSERT
                System.Guid newGUID = ute.getGUID();
                sql += "INSERT INTO emailMessage ( subject,[to],[from],reply_to,body,state,dateAdded,lastModified,userAddedID,guid) VALUES ( ";

                sql += "'";
                sql += this.subject;
                sql += "'";
                sql += ",";
                sql += "'";
                sql += this.to;
                sql += "'";
                sql += ",";
                sql += "'";
                sql += this.from;
                sql += "'";
                sql += ",";
                sql += "'";
                sql += this.ReplyToAddress;
                sql += "'";
                sql += ",";
                sql += "'";
                sql += this.body;
                sql += "'";
                sql += ",";
                sql += this.state;
                sql += ",";
                sql += "'";
                sql += currentTime;
                sql += "'";
                sql += ",";
                sql += "'";
                sql += currentTime;
                sql += "'";
                sql += ",";
                sql += this._by.ID;
                sql += ",";
                sql += "'";
                sql += newGUID.ToString();
                sql += "'";
                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                //load object by way of guid
                this.loadEmailFromDB(newGUID);
            }
            else
            {
                //existing email update!
                sql = "UPDATE emailMessage SET ";
                sql += " subject = '"+ this.subject +"'";
                sql += ",";
                sql += " [to] = '" + this.to + "'";
                sql += ",";
                sql += " [body] = '" + this.body + "'";
                sql += ",";
                sql += " [from] = '" + this.from + "'";
                sql += ",";
                sql += " [lastModified] = '" + currentTime + "'";
                sql += ",";
                sql += " [reply_to] = '" + this.ReplyToAddress + "'";
                sql += ",";
                
                if (this.LastError != null)
                {
                    sql += " [lastError] = '" + this.LastError + "'";
                    sql += ",";
                }
                
                sql += " state = " + this.state;
                sql += " WHERE id = " + this.ID;

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

            }

            dbHelper.cleanup();

            return this.ID;
        }

        public void Delete()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";
            sql = "DELETE FROM emailMessage ";
            sql += " WHERE id = " + this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();
            dbHelper = null;
        }

        public string GetJSON()
        {
            StringBuilder json = new StringBuilder();

            json.Append("{");
            json.Append("'id':");
            json.Append(this.ID);
            json.Append(",");
            json.Append("'state':");
            json.Append(this.state);
            json.Append(",");
            json.Append("'guid':");
            json.Append("'");
            json.Append(this.guid);
            json.Append("'");
            json.Append(",");
            json.Append("'subject':");
            json.Append("'");
            json.Append(this.subject);
            json.Append("'"); 
            json.Append(",");
            json.Append("'body':");
            json.Append("'");
            json.Append(this.body);
            json.Append("'");
            json.Append(",");
            json.Append("'to':");
            json.Append("'");
            json.Append(ute.encode64( this.to ));
            json.Append("'");
            json.Append(",");


            json.Append("'from':");
            json.Append("'");
            json.Append(ute.encode64( this.from ));
            json.Append("'");
            json.Append(",");

            json.Append("'reply_to':");
            json.Append("'"); 
            json.Append(ute.encode64( this.ReplyToAddress ));
            json.Append("'");
            json.Append(",");

            User toUsr = new User(config.conn, this.to);
            User fromUsr = null;
            
            string sFromUser = "";
            int nAnon = -1;

            try
            {
                fromUsr = new User(config.conn, this.from);
                sFromUser = fromUsr.UserName;
                nAnon = 0;
                

            }
            catch (UserDoesNotExistException udneex)
            {
                sFromUser = by.UserName;
                nAnon = 1;
            }
           

            json.Append("'toUsername':");
            json.Append("'");
            json.Append(ute.encode64(toUsr.UserName));
            json.Append("'");
            json.Append(",");
            json.Append("'fromUsername':");
            json.Append("'");
            json.Append(ute.encode64(sFromUser));
            json.Append("'");
            json.Append(",");
            json.Append("'anon':");
            json.Append(nAnon);
            json.Append(",");

            json.Append("'originatorUsername':");
            json.Append("'");
            json.Append(ute.encode64(this.by.UserName));
            json.Append("'");
            json.Append("}");
            
            return json.ToString();
        }
    }
}
