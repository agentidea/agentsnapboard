using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
   
    public class StoryChangeEvent
    {

        private utils ute = new utils();
        private string _connectionString = null;
        private int _id = -1;
        private int _seq = -1;
        private int _priority = -1;

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
	
        private DateTime _dateAdded;
        private User _by;
        private string _changeEvent;


        public int getMaxLastInfo()
        {
            int ret = 0;
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "SELECT max(seq) FROM StoryChangeLog WHERE story_id =" + this.StoryID;
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();
            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                if (dbHelper.reader[0] is System.DBNull)
                    ret = 0;
                else
                    ret = Convert.ToInt32(dbHelper.reader[0]);

            }

            dbHelper.cleanup();
            return ret;
        }


        private void loadFromDB(string conn, int storyID, int storySeq)
        {
            string sql = "SELECT changeEvent, dateadded, user_ID FROM storyChangeLog ";
            sql += " WHERE ";
            sql += " Story_ID = " + storyID;
            sql += " AND ";
            sql += " seq = " + storySeq;

            int userID = -1;


            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();
            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();

                this.ChangeEvent = Convert.ToString(dbHelper.reader[0]);
                this.DateAdded = Convert.ToDateTime(dbHelper.reader[1]);
                userID = Convert.ToInt32(dbHelper.reader[2]);
            }

            dbHelper.cleanup();

            if (userID != -1)
            {
                this.by = new User(this._connectionString, userID);
            }



        }

        private int _storyID;
        public int StoryID
        {
            get { return _storyID; }
            set { _storyID = value; }
        }


       
        public string ChangeEvent
        {
            get { return _changeEvent; }
            set { _changeEvent = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Seq
        {
            get { return _seq; }
            set { _seq = value; }
        }
        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
        public User by
        {
            get { return _by; }
            set { _by = value; }
        }


        //public StoryChangeEvent(string conn, int storyID)
        //{
        //    this._connectionString = conn;
        //    this.StoryID = storyID;
        //    this.loadFromDB(conn, storyID);
            
        //}

        public StoryChangeEvent(string conn, User by, string changeEvent, Story story, int priorityLevel,bool notify)
        {
            this._by = by;
            this._connectionString = conn;
            this.StoryID = story.ID;
            this.ChangeEvent = TheUtils.ute.encode64(changeEvent);
            this.Priority = priorityLevel;
            this.Save();

            if (notify)
            {
                // here would be an ASYNC put say to TIB
                
                    //send email to all story editors for now.
                    PostMan pm = new PostMan(config.conn);

                    foreach (User u in story.StoryEditorUsers)
                    {
                        try
                        {
                            this.SendStoryChangeEventEmail(by, changeEvent, story, pm, u);
                        }
                        
                        catch (Exception ex)
                        {
                           // throw new Exception("error while sending notifications " + ex.Message);
                           // should not hold up executions
                            Logger.log("error while sending email notifications " + ex.Message);
                        }


                    }//for

            }

            
        }

        private void SendStoryChangeEventEmail(User by, string changeEvent, Story story, PostMan pm, User u)
        {

            string clickBackURL = "";
            if (story.CurrPageCursor != -1)
            {
                clickBackURL += "\r\n";
                clickBackURL += "\r\n";
                clickBackURL += config.protocol + "://" + config.host;
                if (config.app.Trim().Length > 0)
                    clickBackURL += "/" + config.app;
                clickBackURL += "/";
                clickBackURL += "StoryEditor4.aspx?StoryID=";
                clickBackURL += story.ID;
                clickBackURL += "&PageCursor=";
                clickBackURL += story.CurrPageCursor;

                clickBackURL += "\r\n";
                clickBackURL += "\r\n";
            }

            //http://localhost:2014/AgentStory/StoryEditor4.aspx?StoryID=83&PageCursor=233
            
            EmailMsg msg = new EmailMsg(config.conn, by);
            msg.from = config.allEmailFrom;
            msg.to = u.Email;
            msg.subject = TheUtils.ute.encode64(config.Orginization + " - '" + TheUtils.ute.decode64(story.Title) + "' - Change Notification");
            msg.body = TheUtils.ute.encode64(changeEvent + clickBackURL);
            msg.ReplyToAddress = by.Email;

            //send an email
            int newMsgID = msg.Save();
            pm.SendMessage(newMsgID);
        }

        public void Save()
        {

           int lastMaxSeq = getMaxLastInfo();

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "";

            if (this.ID == -1)
            {
                sql = "INSERT INTO StoryChangeLog ( user_id,dateAdded,Story_ID,changeEvent,seq,priority) VALUES ( ";
                sql += this.by.ID;
                sql += ",";
                sql += "'";
                sql += ute.getDateStamp();
                sql += "'";
                sql += ",";
                sql += this.StoryID;
                sql += ",";
                sql += "'";
                sql += this.ChangeEvent;
                sql += "'";
                sql += ",";
                sql += lastMaxSeq + 1;
                sql += ",";
                sql += this.Priority;
                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

            }
            else
            {
                throw new NotImplementedException(" update not yet implemented ");
            }

        }





    }

}
