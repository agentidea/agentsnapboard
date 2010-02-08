using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{


    public class StoryTxLog
    {

        private utils ute = new utils();
        private string _connectionString = null;
        private int _id = -1;
        private int _seq = -1;
        private DateTime _dateAdded;
        private User _by;
        private string _command;
        private MacroEnvelope _macroEnv;

        

        public int getMaxLastInfo()
        {
            int ret = 0;
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "SELECT max(seq) FROM StoryTxLog WHERE story_id =" + this.StoryID;
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

        public retDelta getDeltasJSON64(int storyID, int lastStorySeqPassed)
        {
            retDelta retD = new retDelta();

            

            StringBuilder ret = new StringBuilder();
            ret.Append("{'cmds':[");

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "SELECT command,seq FROM storyTxLog";
            sql += " WHERE ";
            sql += " Story_ID = " + storyID;
            sql += " AND ";
            sql += " seq > " + lastStorySeqPassed;


            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            int numRows = 0;
            int lastUpdateSeq = lastStorySeqPassed;

            while (dbHelper.reader.Read())
            {
                string tmpCommand  = Convert.ToString( dbHelper.reader[0] );
                tmpCommand = TheUtils.ute.decode64(tmpCommand);

                lastUpdateSeq = Convert.ToInt32(dbHelper.reader[1]);

                ret.Append(tmpCommand);
                ret.Append(",");
                numRows++;
            }
            
            dbHelper.cleanup();


            if (numRows > 0)
            {
                ret.Remove(ret.Length - 1, 1); //remove trailing ,
            }

            

            ret.Append("],");
            ret.Append("'numMacroEnvelopes':");
            ret.Append(numRows);
            ret.Append(",'lastUpdateSeq':");
            ret.Append(lastUpdateSeq);
            ret.Append("}");

            string s = ret.ToString();
            retD.cmdJSON64 = TheUtils.ute.encode64(s);
            retD.numCmds = numRows;

            return retD;

            
        }



        private int _storyID;

        private void loadFromDB(string conn, int storyID, int storySeq)
        {
            string sql = "SELECT command, dateadded, user_ID FROM storyTxLog";
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
                
                this.Command = Convert.ToString( dbHelper.reader[0] );
                this.DateAdded = Convert.ToDateTime(dbHelper.reader[1]);
                userID = Convert.ToInt32(dbHelper.reader[2]);
            }

            dbHelper.cleanup();

            if (userID != -1)
            {
                this.by = new User(this._connectionString, userID);
            }



        }
        public int StoryID
        {
            get { return _storyID; }
            set { _storyID = value; }
        }
	

        public MacroEnvelope MacroEnv
        {
            get { return _macroEnv; }
            set { _macroEnv = value; }
        }
	
        public string Command
        {
            get { return _command; }
            set { _command = value; }
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


        public StoryTxLog(string conn, int storyID)
        {
            this._connectionString = conn;
            this.StoryID = storyID;
            int lastSeq = getMaxLastInfo();
            if(lastSeq != -1)
            {
                this.loadFromDB(conn,storyID,lastSeq);
            }
        }

        public StoryTxLog(string conn, User by,MacroEnvelope me,int storyID)
        {
            this._by = by;
            this._connectionString = conn;
            this._macroEnv = me;
            this.StoryID = storyID;
        }

        public void Save()
        {

            int lastMaxSeq = getMaxLastInfo();

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "";

            if (this.ID == -1)
            {
                sql = "INSERT INTO StoryTxLog ( user_id,dateAdded,Story_ID,command,seq) VALUES ( ";
                sql += this.by.ID;
                sql += ",";
                sql += "'";
                sql += ute.getDateStamp();
                sql += "'";
                sql += ",";
                sql += this.StoryID;
                sql += ",";
                sql += "'";
                sql += ute.encode64( MacroUtils.serializeMacroEnvelopeJSON(this.MacroEnv));
                sql += "'";
                sql += ",";
                sql += ( lastMaxSeq + 1 ) ;
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

    public class retDelta
    {
        public string cmdJSON64 = null;
        public int numCmds = 0;
    }
}
