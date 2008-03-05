using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class StoryLog
    {
        private utils ute = new utils();
        private string _connectionString = null;

        public StoryLog(string asConnectionString)
        {
            this._connectionString = asConnectionString;
        }

        public void AddToLog64(string msg)
        {
            //input is already in base 64
            this.AddToLogDB(msg.Trim());
        }

        public void AddToLog(string msg)
        {
            string enc = ute.encode64(msg.Trim());
            this.AddToLogDB( enc );
        }

        private void AddToLogDB( string message )
        {
            string sql = "INSERT INTO SystemLog ( msg, dateAdded ) VALUES ( ";
            sql += "'";
            sql += message;
            sql += "'";
            sql += ",";
            sql += "'";
            sql += ute.getDateStamp();
            sql += "'";
            sql += ")";

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            dbHelper.cmd.CommandText = sql;

            int numrows = dbHelper.cmd.ExecuteNonQuery();

        }

        public void TruncateLog()
        {
            string sql = "Delete FROM SystemLog";

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            dbHelper.cmd.CommandText = sql;

            int numrows = dbHelper.cmd.ExecuteNonQuery();
        }

        public List<StoryLogMessage> StoryLogList
        {

            get
            {
                List<StoryLogMessage> storyList = new List<StoryLogMessage>();

               
                DateTime n = DateTime.Now;
                n = n.AddDays(1.0);

                int day = n.Day;
                int month = n.Month;
                int year = n.Year;

                string todayDate = year + "-" + month + "-" + day;

                DateTime y = n.Subtract(new TimeSpan(3, 0, 0, 0, 0));

                string yesterdayDate = y.Year + "-" + y.Month + "-" + y.Day;

                string sql = "SELECT * From SystemLog  ";

                sql += " WHERE     (dateAdded < '" + todayDate + "') AND (dateAdded >= '" + yesterdayDate + "') ";
                sql += " Order by dateAdded DESC";


                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                while (dbHelper.reader.Read())
                {
                    StoryLogMessage slm = new StoryLogMessage();
                    slm.id = Convert.ToInt32( dbHelper.reader["id"] );
                    slm.msg = (string)dbHelper.reader["msg"];
                    slm.dateAdded = Convert.ToDateTime(dbHelper.reader["dateAdded"] );
                    storyList.Add(slm);
                }

                dbHelper.cleanup();

                return storyList;




            }
        }



        
    }

    public class StoryLogMessage
    {
        public int id;
        public string msg;
        public DateTime dateAdded;
    }
}
