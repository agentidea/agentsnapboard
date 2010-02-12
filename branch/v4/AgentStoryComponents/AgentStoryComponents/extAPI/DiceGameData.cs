using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI
{
    public class DiceGameData
    {


        public int id { get; set; }
        public string tx_id { get; set; }
        public string alias { get; set; }
        public int pearl { get; set; }
        public int oyster { get; set; }
        public int breadAndButter { get; set; }
        public int whiteElephant { get; set; }
        public int funded_success { get; set; }
        public int funded_points { get; set; }
        public int unfunded_success { get; set; }
        public int unfunded_points { get; set; }
        public int best5_success { get; set; }
        public int best5_points { get; set; }


        private utils ute = new utils();
        private string _connectionString = null;
        public DiceGameData(string connectionString)
        {
            //new
            this._connectionString = connectionString;
            this.ResetDiceGameData(); //set all values to -1
        }

        public DiceGameData(string connectionString,string transactionID)
        {
            //existing
            this._connectionString = connectionString;
            this.loadFromDB(transactionID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <returns>-1 false id of tx record if true</returns>
        public int existsTxRecord(string transactionID)
        {
            int ret = -1;

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = @"SELECT id
                            FROM  DiceGameData
                            WHERE     tx_id ='" + transactionID + "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            while (dbHelper.reader.Read())
            {
                ret = Convert.ToInt32(dbHelper.reader[0]);
            }

            dbHelper.cleanup();
            dbHelper = null;


            return ret;
        }

        /// <summary>
        /// load the DiceGameData object from db
        /// </summary>
        /// <param name="transactionID"></param>
        public void loadFromDB(string transactionID)
        {

            this.tx_id = transactionID;

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = @"SELECT *
                            FROM  DiceGameData
                            WHERE     tx_id ='" + transactionID + "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            while (dbHelper.reader.Read())
            {
                this.alias = Convert.ToString(dbHelper.reader["alias"]);
                this.id = Convert.ToInt32(dbHelper.reader["id"]);

               
                this.pearl = Convert.ToInt32(dbHelper.reader["pearl"]);
                this.oyster = Convert.ToInt32(dbHelper.reader["oyster"]);
                this.breadAndButter = Convert.ToInt32(dbHelper.reader["breadAndButter"]);
                this.whiteElephant = Convert.ToInt32(dbHelper.reader["whiteElephant"]);
                this.funded_success = Convert.ToInt32(dbHelper.reader["Funded_Success"]);
                this.funded_points = Convert.ToInt32(dbHelper.reader["Funded_Points"]);
                this.unfunded_points = Convert.ToInt32(dbHelper.reader["UnFunded_Points"]);
                this.unfunded_success = Convert.ToInt32(dbHelper.reader["UnFunded_Success"]);
                this.best5_success = Convert.ToInt32(dbHelper.reader["Best5_Success"]);
                this.best5_points = Convert.ToInt32(dbHelper.reader["Best5_Points"]);
            }

            dbHelper.cleanup();
            dbHelper = null;



        }

        public void Save()
        {

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            var day = 0;
            var month = 0;
            var year = 0;
           
            var now = DateTime.Now;
            day = now.Day;
            month = now.Month;
            year = now.Year;
            string dateStamp = now.ToString();


            if (id == 0)
            {
                //new
                System.Guid newGUID = ute.getGUID();

                sql += @"INSERT INTO DiceGameData
           ([guid]
            ,[alias]
           ,[tx_id]
           ,[lastEditedDay]
           ,[lastEditedMonth]
           ,[lastEditedYear]
           ,[lastEditedWhen]
           ,[pearl]
           ,[oyster]
           ,[breadAndButter]
           ,[whiteElephant]
           ,[Funded_Success]
           ,[Funded_Points]
           ,[UnFunded_Success]
           ,[UnFunded_Points]
           ,[Best5_Success]
           ,[Best5_Points])
            VALUES ( ";

                sql += "'";
                sql += newGUID.ToString();
                sql += "'";
                sql += ",";

                
                sql += "'";
                sql += this.alias;
                sql += "'";
                sql += ",";

                sql += "'";
                sql += this.tx_id;
                sql += "'";
                sql += ",";

                sql += day;
                sql += ",";
                sql += month;
                sql += ",";
                sql += year;
                sql += ",";

                sql += "'";
                sql += dateStamp;
                sql += "'";
                sql += ",";

                sql += this.pearl;
                sql += ",";
                sql += this.oyster;
                sql += ",";
                sql += this.breadAndButter;
                sql += ",";
                sql += this.whiteElephant;
                sql += ",";

                sql += this.funded_success;
                sql += ",";
                sql += this.funded_points;
                sql += ",";
                sql += this.unfunded_success;
                sql += ",";
                sql += this.unfunded_points;
                sql += ",";
                sql += this.best5_success;
                sql += ",";
                sql += this.best5_points;

                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                //load object by way of transactionid
                this.loadFromDB(this.tx_id);

            }
            else
            {

                //this.loadFromDB(this.tx_id);

                //update
                sql += "UPDATE DiceGameData SET ";
                sql += " lastEditedWhen =";
                sql += "'";
                sql += dateStamp;
                sql += "'";
                sql += ",";
                sql += " pearl =";
                sql += this.pearl;
                sql += ",";
                sql += " oyster =";
                sql += this.oyster;
                sql += ",";
                sql += " breadAndButter =";
                sql += this.breadAndButter;
                sql += ",";
                sql += " whiteElephant =";
                sql += this.whiteElephant;
                sql += ",";
                sql += " funded_success =";
                sql += this.funded_success;
                sql += ",";
                sql += " funded_points =";
                sql += this.funded_points;
                sql += ",";
                sql += " unfunded_success =";
                sql += this.unfunded_success;
                sql += ",";
                sql += " unfunded_points =";
                sql += this.unfunded_points;
                sql += ",";
                sql += " best5_success =";
                sql += this.best5_success;
                sql += ",";
                sql += " best5_points =";
                sql += this.best5_points;

                sql += " WHERE tx_id='" + this.tx_id + "'";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

            }
        }

        public void ResetDiceGameData()
        {
            this.pearl = -1;
            this.oyster = -1;
            this.whiteElephant = -1;
            this.breadAndButter = -1;
            this.unfunded_points = -1;
            this.funded_points = -1;
            this.best5_points = -1;
            this.funded_success = -1;
            this.unfunded_success = -1;
            this.best5_success = -1;
        }
    }
}
