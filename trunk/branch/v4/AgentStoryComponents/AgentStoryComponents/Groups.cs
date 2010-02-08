using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Groups
    {
        private utils ute = new utils();
        private string _connectionString = null;

        public Groups(string asConnectionString)
        {
            this._connectionString = asConnectionString;
        }

        public List<Group> GroupList
        {
            get
            {
                //count number of Groups
                int numGroups = this.NumberGroups;
                List<Group> GroupList = new List<Group>(numGroups);

                string sql = "";
                sql += "select * from Groups";

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    while (dbHelper.reader.Read())
                    {
                        int GroupID = Convert.ToInt32(dbHelper.reader["ID"]);
                        Group u = new Group(this._connectionString, GroupID);
                        GroupList.Add(u);

                    }

                }

                dbHelper.cleanup();
                return GroupList;

            }
        }

        /// <summary>
        /// return number of Groups in Groups table.
        /// </summary>
        public int NumberGroups
        {
            get
            {
                string sql = "";
                int count = -1;
                sql += "select count(id) from Groups";

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    dbHelper.reader.Read();
                    count = Convert.ToInt32(dbHelper.reader[0]);
                }
                else
                {
                    count = 0;
                }

                dbHelper.cleanup();

                return count;
            }
        }


        public List<Group> getMyGroupList(User whose)
        {

                List<Group> GroupList = new List<Group>();

                string sql = "";
                sql += "select * from UsersGroups where user_id = " + whose.ID;

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    while (dbHelper.reader.Read())
                    {
                        int GroupID = Convert.ToInt32(dbHelper.reader["group_id"]);
                        Group u = new Group(this._connectionString, GroupID);
                        GroupList.Add(u);

                    }

                }

                dbHelper.cleanup();
                return GroupList;

            
        }


    }
}