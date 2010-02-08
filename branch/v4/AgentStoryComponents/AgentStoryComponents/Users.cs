using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Users
    {
        private utils ute = new utils();
        private string _connectionString = null;

        public Users(string asConnectionString)
        {
            this._connectionString = asConnectionString;
        }

        public List<User> UserList
        {
            get
            {
                //count number of users
                int numUsers = this.NumberUsers;
                List<User> userList = new List<User>(numUsers);

                string sql = "";
                sql += "select * from users order by dateAdded DESC";

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    while (dbHelper.reader.Read())
                    {
                        int userID = Convert.ToInt32( dbHelper.reader["ID"] );
                        User u = new User(this._connectionString, userID);
                        userList.Add(u);

                    }
                    
                }
                
                dbHelper.cleanup();
                return userList;

            }
        }

        /// <summary>
        /// return number of users in users table.
        /// </summary>
        public int NumberUsers
        {
            get
            {
                string sql = "";
                int count = -1;
                sql += "select count(id) from users";

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    dbHelper.reader.Read();
                    count = Convert.ToInt32( dbHelper.reader[0] );
                }
                else
                {
                    count = 0;
                }

                dbHelper.cleanup();

                return count;
            }
        }
    }
}
