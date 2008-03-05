using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class PageElementMap
    {
        private utils ute = new utils();
        private string _connectionString = null;
        private int _id = -1;
        private string _code;
        private int _pageElementID;
        private int _gridX;
        private int _gridY;
        private int _gridZ;

        private string _guid;
        private DateTime _dateAdded;
        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
        public int PageElementID
        {
            get { return _pageElementID; }
            set { _pageElementID = value; }
        }
        

        public int GridZ
        {
            get { return _gridZ; }
            set { _gridZ = value; }
        }
	    public int GridY
	{
		get { return _gridY;}
		set { _gridY = value;}
	}
	    public int GridX
	{
		get { return _gridX;}
		set { _gridX = value;}
	}

        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public PageElementMap(string conn)
        {
            this._connectionString = conn;
        }

        public PageElementMap(string conn, int id)
        {
            this._connectionString = conn;
            this.loadPageElementMapFromDB(id);
        }

        public PageElementMap(string conn, System.Guid guid)
        {
            this._connectionString = conn;
            this.loadPageElementMapFromDB(guid);
        }

        private void loadPageElementMapFromDB(int pageElementMapID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from PageElementMap WHERE id = ";
            sql += pageElementMapID;

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            loadPageElementMapFromDbHelper(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }

        private void loadPageElementMapFromDB(System.Guid guid)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from PageElementMap WHERE guid = ";
            sql += "'";
            sql += guid.ToString();
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            loadPageElementMapFromDbHelper(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }

        private void loadPageElementMapFromDbHelper(OleDbHelper dbHelper)
        {
            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                this.ID = Convert.ToInt32(dbHelper.reader["id"]);
                if (dbHelper.reader["code"] is System.DBNull)
                {
                    this.Code = null;
                }
                else
                {
                    this.Code = (string)dbHelper.reader["code"];
                }
                
                this.PageElementID = Convert.ToInt32(dbHelper.reader["pageElement_id"]);
                this.GridX = Convert.ToInt32(dbHelper.reader["gridX"]);
                this.GridY = Convert.ToInt32(dbHelper.reader["gridY"]);
                this.GridZ = Convert.ToInt32(dbHelper.reader["gridZ"]);

                this.GUID = Convert.ToString(dbHelper.reader["guid"]);
                this.DateAdded = Convert.ToDateTime(dbHelper.reader["dateAdded"]);
            }
        }


        public void Save()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            if (this.ID == -1)
            {
                //new pageElement INSERT
                System.Guid newGUID = ute.getGUID();

                sql += "INSERT INTO PageElementMap ( code,pageElement_id,gridX,gridY,gridZ,guid,dateAdded ) VALUES ( ";

                if (this.Code == null)
                {
                    sql += "null";
                    sql += ",";
                }
                else
                {
                    sql += "'";
                    sql += this.Code;
                    sql += "'";
                    sql += ",";
                }

                sql += this.PageElementID;
                sql += ",";

                sql += this.GridX;
                sql += ",";
                sql += this.GridY;
                sql += ",";
                sql += this.GridZ;
                sql += ",";

                sql += "'";
                sql += newGUID.ToString();
                sql += "'";
                sql += ",";

               
                sql += "'";
                sql += ute.getDateStamp();
                sql += "'";

                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                //load object by way of guid
                this.loadPageElementMapFromDB(newGUID);
            }
            else
            {
                //existing story update!
                sql = "UPDATE PageElementMap SET ";
                sql += " gridX = " + this.GridX;
                sql += ",";
                sql += " gridY = " + this.GridY;
                sql += ",";
                sql += " gridZ = " + this.GridZ;

                if (this.Code == null)
                {
                    sql += ",code=null";
                    
                }
                else
                {
                    sql += ",code='";
                    sql += this.Code;
                    sql += "'";
                }

                sql += " WHERE pageElement_id = " + this.PageElementID;

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();


            }
        }


        public void Delete()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM pagePageElementMap ";
            sql += " WHERE pageElementMap_id = " + this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            sql = "DELETE FROM pageElementMap ";
            sql += " WHERE guid = '" + this.GUID + "'";

            dbHelper.cmd.CommandText = sql;
            numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();
            dbHelper = null;
        }
    }
}
