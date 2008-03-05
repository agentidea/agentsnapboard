using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class StoryState
    {
        private string _stateName;
        private int _stateID;

        public int StateID
        {
            get { return _stateID; }
            set { _stateID = value; }
        }

        public string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }

        private utils ute = new utils();
        private string _connectionString = null;

        public StoryState()
        {
        }

        public StoryState(string asConnectionString, int stateID)
        {
            this._connectionString = asConnectionString;
            this.StateID = stateID;
            this.loadFromDB();
        }

        private void loadFromDB()
        {
            string sql = "";
            sql += "select * from StoryState where id=" + this.StateID;

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    //this.StateID = Convert.ToInt32(dbHelper.reader["ID"]);
                    this.StateName = Convert.ToString(dbHelper.reader["StateName"]);
                }
            }

            dbHelper.cleanup();
            return;

        }
    }


    public class StoryStates
    {
        private utils ute = new utils();
        private string _connectionString = null;

        public StoryStates(string asConnectionString)
        {
            this._connectionString = asConnectionString;
        }

        public List<StoryState> StoryStateList
        {
            get
            {
                List<StoryState> storyStateList
                    = new List<StoryState>();

                string sql = "";
                sql += "select * from StoryState";

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    while (dbHelper.reader.Read())
                    {
                        int stateID = Convert.ToInt32(dbHelper.reader["ID"]);
                        string stateName = Convert.ToString(dbHelper.reader["StateName"]);
                        StoryState ss = new StoryState();
                        ss.StateID = stateID;
                        ss.StateName = stateName;
                        storyStateList.Add(ss);

                    }

                }

                dbHelper.cleanup();
                return storyStateList;

            }
        }

    }
}
