using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class stats
    {
        private utils ute = new utils();

        public void addUserStory_hit(User u, Story s)
        {
            if (this.userStoryHitExists(u, s))
            {
                //user hit already registered, update his total views
                this.updateUserHit(u, s);
            }
            else
            {
                this.addUserHit(u, s);
                this.updateStoryHit(s);
            }
        }
        public StatsBag storyHits(Story s)
        {
            
            StatsBag _statsBag = new StatsBag();

            string sql = "";
            sql += "SELECT * FROM vStoryPlatform WHERE ";
            sql += " story_id = ";
            sql += s.ID;

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    _statsBag.numViews = (int)dbHelper.reader["views"];
                    _statsBag.rating = (int)dbHelper.reader["rating"];

                    if (dbHelper.reader["lastEditedBy"] is System.DBNull)
                    {
                        //do nothing
                    }
                    else
                    {
                        int lastEditedByID = (int)dbHelper.reader["lastEditedBy"];
                        User u = new User(config.conn, lastEditedByID);

                        _statsBag.lastEditedBy = u;
                        DateTime _lastEditedWhen = Convert.ToDateTime(dbHelper.reader["lastEditedWhen"]);
                        _statsBag.lastEditedWhen = _lastEditedWhen;


                        DateTime _today = DateTime.Now;

                        if (_lastEditedWhen.ToShortDateString() == _today.ToShortDateString())
                        {
                            _statsBag.dayLastEdited = "TODAY at " + _lastEditedWhen.ToShortTimeString();
                        }
                        else
                        {
                            _statsBag.dayLastEdited = _lastEditedWhen.ToString();
                        }



                    }

                }
            }

            dbHelper.cleanup();


            return _statsBag;

        }

        public void addEditorStoryHit(User u, Story s)
        {
            if (storyHitExists(s))
            {
                this.updateEditorStoryHit(s,u);
            }
            else
            {
                this.addStoryHitDB(s);
                this.updateEditorStoryHit(s, u);
            }
        }

        public void updateEditorStoryHit(Story s,User u)
        {
            string sql = "";
            sql += "UPDATE FACT_StoryView ";
            sql += " SET lastEditedBy = " + u.ID;
            sql += " ,lastEditedWhen = '" + ute.getDateStamp() + "'";
            sql += " WHERE story_id = " + s.ID;

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception(" Poor sql " + sql);

            dbHelper.cleanup();
        }


        

        private void updateStoryHit(Story s)
        {
            if (this.storyHitExists(s))
            {
                //update story hit
                this.updateStoryHitDB(s);
            }
            else
            {
                this.addStoryHitDB(s);
            }
        }

        private void addStoryHitDB(Story s)
        {
            string sql = "";
            sql += "INSERT INTO FACT_StoryView ( story_id,views,rating) VALUES ";
            sql += "(";
            sql += s.ID;
            sql += ",1,-1";
            sql += ")";

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception(" Poor sql " + sql);

            dbHelper.cleanup();
        }

        private void updateStoryHitDB(Story s)
        {
            string sql = "";
            sql += "UPDATE FACT_StoryView ";
            sql += " SET views = views + 1";
            sql += " WHERE story_id =";
            sql += s.ID;

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception(" Poor sql " + sql);

            dbHelper.cleanup();
        }

        private bool storyHitExists(Story s)
        {
            bool ret = false;

            string sql = "";
            sql += "SELECT * FROM FACT_StoryView WHERE ";
            sql += " story_id = ";
            sql += s.ID;

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                ret = true;
            }

            dbHelper.cleanup();


            return ret;
        }

        private void addUserHit(User u, Story s)
        {
            string sql = "";
            sql += "INSERT INTO FACT_StoryUserView ( user_id,story_id,views,rating) VALUES ";
            sql += "(";
            sql += u.ID;
            sql += ",";
            sql += s.ID;
            sql += ",1,-1";
            sql += ")";

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception(" Poor sql " + sql);

            dbHelper.cleanup();
        }

        private void updateUserHit(User u, Story s)
        {
            string sql = "";
            sql += "UPDATE FACT_StoryUserView ";
            sql += " SET views = views + 1";
            sql += " WHERE user_id = ";
            sql += u.ID;
            sql += " AND story_id =";
            sql += s.ID;

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception(" Poor sql " + sql);

            dbHelper.cleanup();
        }

        private bool userStoryHitExists(User u, Story s)
        {
            bool ret = false;

            string sql = "";
            sql += "SELECT * FROM FACT_StoryUserView WHERE ";
            sql += " user_id = ";
            sql += u.ID;
            sql += " AND story_id = ";
            sql += s.ID;

            OleDbHelper dbHelper = ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                ret = true;
            }

            dbHelper.cleanup();


            return ret;

        }



       

    }

    public class StatsBag
    {
        public int numViews = -1;
        public User lastEditedBy = null;
        public int rating = -1;
        public DateTime lastEditedWhen;
        public string dayLastEdited;
    }
}
