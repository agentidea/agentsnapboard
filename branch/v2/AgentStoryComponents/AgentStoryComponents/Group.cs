using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Group
    {
        private utils ute = new utils();
        private string _connectionString = null;

       private List<User> _groupUsers;

        public List<User> GroupUsers
        {
            get
            {
                _groupUsers = new List<User>();

                string sql = " select user_id from usersgroups where group_id = " + this.ID;

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
              

                dbHelper.cmd.CommandText = sql;
                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                while(dbHelper.reader.Read())
                {
                    int uId = System.Convert.ToInt32(dbHelper.reader["user_id"]);
                    User tmpUsr = new User(this._connectionString, uId);
                    _groupUsers.Add(tmpUsr);
                }

                dbHelper.cleanup();
                dbHelper = null;

               return _groupUsers; 
            }
        }

        
	

        private int _ID = -1;
        private string _name;
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
	
        private string _guid;

        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }
	
        private DateTime _dateAdded;
        private User _by;

	public User By
	{
		get { return _by;}
		set { _by = value;}
	}
	
        #region basic properties
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        
        
        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
#endregion

        #region constructors
        public Group(string asConnectionString, System.Guid aGuid)
        {
            this._connectionString = asConnectionString;
            this.populateGroupFromDB(aGuid);
        }


        public Group(string asConnectionString,User by)
        {
            this._connectionString = asConnectionString;
            this.DateAdded = System.DateTime.Now;
            this.By = by;
        }


        public Group(string asConnectionString, int groupID)
        {
            this._connectionString = asConnectionString;
            this.ID = groupID;
            this.populateGroupFromDB(groupID);
        }

        public Group(string asConnectionString, string name)
        {
            this._connectionString = asConnectionString;
            this.Name = name;
            this.populateGroupFromDB(name);
        }

        #endregion 

        private void populateGroupFromDB(int groupID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from groups WHERE id = ";

            sql += groupID;


            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.Name = (string)dbHelper.reader["name"];
                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);
                this.GUID = System.Convert.ToString( dbHelper.reader["GUID"] );
                int groupStartedBy = System.Convert.ToInt32( dbHelper.reader["groupStartedBy"] );
                this.By = new User(this._connectionString, groupStartedBy);
                
            }

            dbHelper.cleanup();
            dbHelper = null;

        }

        private void populateGroupFromDB(string name)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from groups WHERE name = ";
            sql += "'";
            sql += name.Trim();
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.ID = System.Convert.ToInt32(dbHelper.reader["id"]);
                this.Name = System.Convert.ToString(dbHelper.reader["name"]);
                this.GUID = System.Convert.ToString(dbHelper.reader["GUID"]);
                if (dbHelper.reader["description"] is DBNull)
                {
                    this.Description = null;
                }
                else
                {
                    this.Description = System.Convert.ToString(dbHelper.reader["description"]);
                }
                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);

                int groupStartedBy = System.Convert.ToInt32(dbHelper.reader["groupStartedBy"]);
                this.By = new User(this._connectionString, groupStartedBy);
                dbHelper.cleanup();
                dbHelper = null;
            }
            else
            {
                throw new GroupDoesNotExistException(" group " + name + " does not exist");
            }

            
        }
        private void populateGroupFromDB(System.Guid GUID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from groups WHERE guid = ";
            sql += "'";
            sql += GUID.ToString();
            sql += "'";
            
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.ID = System.Convert.ToInt32(dbHelper.reader["id"]);
                this.Name = System.Convert.ToString( dbHelper.reader["name"]) ;
                this.GUID = System.Convert.ToString(dbHelper.reader["GUID"]);
                if (dbHelper.reader["description"] is DBNull)
                {
                    this.Description = null;
                }
                else
                {
                    this.Description = System.Convert.ToString(dbHelper.reader["description"]);
                }
                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);
               
                int groupStartedBy = System.Convert.ToInt32(dbHelper.reader["groupStartedBy"] );
                this.By = new User(this._connectionString, groupStartedBy);

            }

            dbHelper.cleanup();
            dbHelper = null;
    
        }
        public int Save()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";
            System.Guid newGUID = ute.getGUID();

           if(this.ID == -1)
           {

               if(GroupExists(this.Name.Trim()) )
                   throw new GroupExistsException(this.Name);

                sql += "INSERT INTO groups ( name,description,dateAdded,guid,groupStartedBy ) VALUES ( ";

             
                sql += "'";
                sql += this.Name;
                sql += "'";
                sql += ",";

                if (this.Description != null)
                {
                    sql += "'";
                    sql += this.Description;
                    sql += "'";
                    sql += ",";
                }
                else
                {
                    sql += "null";
                    sql += ",";

                }

                sql += "'";
                sql += ute.getDateStamp();
                sql += "'";
                sql += ",";
                
                sql += "'";
                sql += newGUID.ToString();
                sql += "'";

                sql += ",";
                sql += By.ID;
                sql += " )";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                if (numRows == 1)
                {
                    this.populateGroupFromDB(newGUID);
                }


              



            }
            else
            {
                //existing user, update user

                //newGUID = null;
                //newGUID = new Guid(this.UserGUID);

                //sql = "UPDATE users ";
                //sql += " SET ";
                //sql += " Email = ";
                //sql += "'";
                //sql += this.Email;
                //sql += "'";
                //sql += " , ";
                //sql += " Username = ";
                //sql += "'";
                //sql += this.UserName;
                //sql += "'";
               
                //sql += " WHERE ";
                //sql += " ID = ";
                //sql += this.ID;

            }



            dbHelper.cleanup();

            return this.ID;
        }

        private void removeUserAssociations(int groupID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "Delete from usersgroups Where group_id=" + groupID;
          

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
        }

        public void Delete()
        {
            //remove associations first
            this.removeUserAssociations(this.ID);

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "Delete from groups Where id=" + this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();

            if (numRows != 1)
                throw new Exception("unable to delelte group " + this.ID + " from DB - " + sql);




        }

        public void RemoveUser(User by)
        {
            int userID = by.ID;
            int groupID = this.ID;

            string sql = " DELETE FROM UsersGroups ";
            sql += " WHERE " ;
            sql += " group_id =";
            sql += groupID;
            sql += " AND ";
            sql += " user_id = ";
            sql += userID;
           

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to remove user from group " + groupID + " - " + sql);

            dbHelper.cleanup();
        

        }

        public void AddUser(User by)
        {
            //add a user to this group.

            int userID = by.ID;
            int groupID = this.ID;

            string sql = " INSERT INTO UsersGroups ( user_id , group_id, dateAdded ) VALUES ( ";
            sql += userID;
            sql += ",";
            sql += groupID;
            sql += ",";
            sql += "'";
            sql += ute.getDateStamp();
            sql += "'";
            sql += ")";

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            try
            {
                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();


                if (numRows != 1)
                    throw new Exception("unable to add to group " + groupID + " - " + sql);



            }
            catch (System.Data.OleDb.OleDbException dbex)
            {
                //possibly already inserted
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbHelper.cleanup();
            }




        }

        public bool GroupExists(string groupName)
        {
            bool ret = false;
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from groups WHERE name = ";
            sql += "'";
            sql += groupName.Trim();
            sql += "'";
            
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            
            ret = dbHelper.reader.HasRows;

            dbHelper.cleanup();

            return ret;
            
        }
    }

   
}
