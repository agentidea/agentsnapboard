using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class User
    {
       private utils ute = new utils();
       private string _connectionString = null;

       private int _notificationFrequency =1;

       private string _tags;

       private string _txSessionID;

       public string TxSessionID
       {
           get { return _txSessionID; }
           set { _txSessionID = value; }
       }
	

       public string Tags
       {
           get { return _tags; }
           set { _tags = value; }
       }


       public string GroupsAsPipe
       {
           get { 
      
               StringBuilder res = new StringBuilder();
                foreach (Group grp in MyGroups)
                {
                    res.Append(ute.decode64(grp.Name));
                    res.Append("|");
                }
                if (MyGroups.Count > 0)
                    res.Remove(res.Length - 1, 1);
                return res.ToString(); 
               }
          
       }
	
	

       public int NotificationFrequency
       {
           get { return _notificationFrequency; }
           set { _notificationFrequency = value; }
       }
	
       private string _notificationTypes;

       public string NotificationTypes
       {
           get
           {
               if (_notificationTypes == null)
                   _notificationTypes = "email";

               return _notificationTypes; 
           }

           set { _notificationTypes = value; }
       }
	
        private List<Group> _myGroups;

        public List<Group> MyGroups
        {
            get
            {
                if (_myGroups == null)
                {
                    //delayed getting of my groups
                    //lazy
                    Groups gps = new Groups(config.conn);
                    _myGroups = gps.getMyGroupList(this);


                }

                return _myGroups; 
            
            }
          
        }
	

       private DateTime _dateAdded;
       private DateTime _dateActivated;

       private string _userGUID;
       private int _ID = -1;

       public string StateHR
       {
           get 
           {
               string lsStateHR = "";

               switch (this.State)
               {
                   case 0:
                       lsStateHR = "added to db";
                       break;
                   case 1:
                       lsStateHR = "signed in";
                       break;
                   case 2:
                       lsStateHR = "pre-approved";
                       break;
                   case 3:
                       lsStateHR = "accepted invitation";
                       break;
                   case 4:
                       lsStateHR = "declined invitation";
                       break;
                   case 5:
                       lsStateHR = "account disabled";
                       break;
                   case 6:
                       lsStateHR = "account suspended";
                       break;
                   case 7:
                       lsStateHR = "account terminated";
                       break;
                   case 8:
                       lsStateHR = "pending email confirmation";
                       break;
                   case 9:
                       lsStateHR = "viewed invite";
                       break;

               }

               return lsStateHR;
           
           }
           
       }
       public int ID
       {
           get { return _ID; }
           set { _ID = value; }
       }
	

       public DateTime DateAdded
       {
           get { return _dateAdded; }
           set { _dateAdded = value; }
       }

        public DateTime DateActivated
        {
            get { return _dateActivated; }
            set { _dateActivated = value; }
        }

       

       public string UserGUID
       {
           get { return _userGUID; }
           set { _userGUID = value; }
       }
	
       private string _pendingGUID;

       public string PendingGUID
       {
           get 
           {  
               
               if( _pendingGUID == null && this.ID == -1 )
               {
                   _pendingGUID = Convert.ToString( ute.getGUID() );
               }
           
               return _pendingGUID;
           
           
           }
           set { _pendingGUID = value; }
       }
	

        private string _name;
        private string _firstName;
        private string  _lastName;
        private string  _nick;
        private int _sponsorID=-1;

        public int SponsorID
        {
            get { return _sponsorID; }
            set { _sponsorID = value; }
        }


        public User Sponsor
        {
            get
            {
                User sponsor = new User(config.conn, this.SponsorID);
                return sponsor;
            }
        }
        public string SponsorFullName
        {

            get
            {
                if (this.SponsorID == -1)
                {
                    return "self/system";
                }
                else
                {
                   return this.Sponsor.UserName;
                }
            }

        }
        
        private string _roles;

        public string Roles
        {
            get 
            {
                if (_roles == null)
                    _roles = "nullUser";

                return _roles;
            }
            set { _roles = value; }
        }
	

        public string  Nick
        {
            get { return _nick; }
            set { _nick = value; }
        }
	

        public string  LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
	

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
	
        private string _origInviteCode;

        public string OrigInviteCode
        {
            get { return _origInviteCode; }
            set { _origInviteCode = value; }
        }
	
        private int _state;

        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string  _password;

        public string  Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }



        public User(string asConnectionString, string username, bool errOnNotFound)
        {
            this._connectionString = asConnectionString;
            this.populateUsername64FromDB(username);
        }

        private void populateUsername64FromDB(string username)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from users WHERE username =";
            sql += "'";
            sql += TheUtils.ute.encode64(username);
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            processUserDataset(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asConnectionString"></param>
        /// <param name="Guid"></param>
        /// <param name="typeGUID">pendingGUID or userGUID</param>
        public User(string asConnectionString, System.Guid aGuid,string typeGUID)
        {
            this._connectionString = asConnectionString;
            this.populateUserFromDB(aGuid, typeGUID);
        }


        /// <summary>
        /// construct a new empty user
        /// </summary>
        /// <param name="asConnectionString"></param>
        public User(string asConnectionString)
        {
            this._connectionString = asConnectionString;
            this.DateAdded = System.DateTime.Now;
        }


        public User(string asConnectionString,int userID)
        {
            this._connectionString = asConnectionString;
            this.ID = userID;
            this.populateUserFromDB(userID);
        }

        /// <summary>
        /// construct an existing user
        /// </summary>
        /// <param name="asConnectionString"></param>
        /// <param name="email"></param>
        public User(string asConnectionString,string email)
        {
            this._connectionString = asConnectionString;

            email = email.Trim();

            if (this.UserExists(email) == true)
            {
                //good populate user object with values from db
                this.populateUserFromDB(email);
            }
            else
            {
                //this user does not exist.
                throw new UserDoesNotExistException("user for email " + email + " not found");
            }

        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        #region systemRoles
        public bool isRoot()
        {
            return this.hasRole("root");
        }

        public bool isAdmin()
        {
            return this.hasRole("admin");
        }

        public bool isEditor()
        {
            return this.hasRole("editor");
        }

        public bool isObserver()
        {
            return this.hasRole("observer");
        }

        private bool hasRole(string roleName)
        {
            string[] roles = this.Roles.Split('|');
            bool bHasRole = false;

            foreach (string role in roles)
            {
                if (roleName.Trim().ToLower() == role.Trim().ToLower())
                {
                    bHasRole = true;
                    break;
                }
            }

            return bHasRole;
        }


        #endregion

        private void populateUserFromDB(int userID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from users WHERE id = ";

            sql += userID;
            

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            processUserDataset(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;

        }

        private void processUserDataset(OleDbHelper dbHelper )
        {
             if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.UserName = ute.decode64( (string) dbHelper.reader["username"]);
                this.Password = ute.decode64( (string)dbHelper.reader["password"]);
                this.OrigInviteCode = (string)dbHelper.reader["OrigInvitationCode"];
                this.State = System.Convert.ToInt32( dbHelper.reader["state"] );
                this.ID = System.Convert.ToInt32(dbHelper.reader["ID"]);
                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"] );
                this.NotificationFrequency = System.Convert.ToInt32(dbHelper.reader["notificationFrequency"]);
                this.NotificationTypes = (string)dbHelper.reader["notificationTypes"];
                 
                 
                 //optional fields
                if (dbHelper.reader["email"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.Email =  (string)dbHelper.reader["email"];
                }
                
                if (dbHelper.reader["tags"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.Tags = (string)dbHelper.reader["tags"];
                }
                if (dbHelper.reader["sponsorID"] is System.DBNull)
                {
                    //this._sponsorID = -1;
                }
                else
                {
                    this._sponsorID = Convert.ToInt32(dbHelper.reader["sponsorID"]);
                }

                if (dbHelper.reader["nick"] is System.DBNull)
                {
                   //do nothing
                }
                else
                {
                    this.Nick = ute.decode64( (string)dbHelper.reader["nick"]);
                }
                if (dbHelper.reader["firstName"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.FirstName = ute.decode64( (string)dbHelper.reader["firstName"]);
                }
                if (dbHelper.reader["lastName"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.LastName = ute.decode64( (string)dbHelper.reader["lastName"]);
                }

                if (dbHelper.reader["roles"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.Roles = (string)dbHelper.reader["roles"];
                }
                if (dbHelper.reader["pendingGUID"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.PendingGUID = System.Convert.ToString(dbHelper.reader["pendingGUID"]);
                }
                if (dbHelper.reader["userGUID"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.UserGUID = System.Convert.ToString(dbHelper.reader["userGUID"]);
                }


            }


        }


        private void populateUserFromDB(string email)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from users WHERE email = ";
            sql += "'";
            sql += ute.prepSQLstring(email);
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            processUserDataset(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;

        }

        public User populateUserViaInviteCode(string inviteCode)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from users WHERE OrigInvitationCode = ";
            sql += "'";
            sql += inviteCode;
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            processUserDataset(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;

            return this;

        }

        private void populateUserFromDB(System.Guid GUID,string GuidCol)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from users WHERE " + GuidCol + " = ";
            sql += "'";
            sql += GUID.ToString();
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();
            
            processUserDataset(dbHelper);
            
            dbHelper.cleanup();
            dbHelper = null;

        }


        public bool UserExists(string email)
        {
            bool exists = false;
            if (email == null)
                return exists;


            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select ID from users WHERE email = ";
            sql += "'";
            sql += ute.prepSQLstring(email);
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                exists = true;
            }

            dbHelper.cleanup();
            dbHelper = null;

            return exists;
        }
        public bool UsernameExists(string username)
        {
            bool exists = false;
            if (username == null)
                return exists;

            string base64username = ute.encode64(username);


            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select ID from users WHERE username = ";
            sql += "'";
            sql += base64username;
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                exists = true;
            }

            dbHelper.cleanup();
            dbHelper = null;

            return exists;
        }


        /// <summary>
        /// persist user
        /// </summary>
        /// <returns>user id</returns>
        public int Save()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";
            System.Guid newGUID = ute.getGUID();
            
            bool newUser = false;

 
            if(this.ID == -1)
            {
                //new user
                newUser = true;
    
                sql += "INSERT INTO users ( firstname,lastname,username,password,email,OrigInvitationCode,dateAdded,state,roles,userGUID,pendingGUID,sponsorID,notificationTypes,notificationFrequency,tags ) VALUES ( ";

                if (this.FirstName != null)
                {
                    sql += "'";
                    sql += ute.encode64( this.FirstName );
                    sql += "'";
                    sql += ",";
                }
                else
                {
                    sql += " null ";
                    sql += ",";
                }

                if (this.LastName != null)
                {
                    sql += "'";
                    sql += ute.encode64( this.LastName);
                    sql += "'";
                    sql += ",";
                }
                else
                {
                    sql += " null ";
                    sql += ",";
                }


                sql += "'";
                sql += ute.encode64( this.UserName );
                sql += "'";
                sql += ",";

                sql += "'";
                sql += ute.encode64( this.Password );
                sql += "'";
                sql += ",";

                if (this.Email != null)
                {
                    sql += "'";
                    sql += this.Email;
                    sql += "'";
                    sql += ",";
                }
                else
                {
                    sql += " null ";
                    sql += ",";
                }


                if (this.OrigInviteCode != null)
                {
                    sql += "'";
                    sql += this.OrigInviteCode;
                    sql += "'";
                    sql += ",";
                }
                else
                {
                    sql += " null ";
                    sql += ",";
                }



                sql += "'";
                sql += Convert.ToString(this.DateAdded);
                sql += "'";
                sql += ",";
                sql += (int)this.State;
                sql += ",";
                sql += "'";
                sql += this.Roles;
                sql += "'";

                sql += ",";
                sql += "'";
                sql += newGUID.ToString();
                sql += "'";
                sql += ",";
                
                sql += "'";
                sql += this.PendingGUID.ToString();
                sql += "'";
                sql += ",";

                if (this.SponsorID != -1)
                {
                    sql += this.SponsorID;
                }
                else
                {
                    sql += "null";
                }

                sql += ",";
                
                sql += "'";
                sql += this.NotificationTypes;
                sql += "'";
                sql += ",";

                sql += this.NotificationFrequency;
                sql += ",";

                if (this.Tags != null)
                {
                    sql += "'";
                    sql += this.Tags;
                    sql += "'";
//                    sql += ",";
                }
                else
                {
                    sql += " null ";
//                    sql += ",";
                }

                sql += " )";

               

            }
            else
            {
                //existing user, update user

                //newGUID = null;
                newGUID = new Guid(this.UserGUID);

                sql = "UPDATE users ";
                sql += " SET ";
                sql += " Email = ";
                sql += "'";
                sql +=  this.Email;
                sql += "'";
                sql += " , ";
                sql += " Username = ";
                sql += "'";
                sql += ute.encode64( this.UserName );
                sql += "'";
                sql += " , ";
                sql += " Password = ";
                sql += "'";
                sql += ute.encode64( this.Password );
                sql += "'";
                sql += " , ";
                sql += " Roles = ";
                sql += "'";
                sql += this.Roles;
                sql += "'";
                sql += " , ";

                sql += " State = ";
                sql += this.State;

                if (this.Nick != null)
                {
                    sql += " , ";
                    sql += " Nick = ";
                    sql += "'";
                    sql += ute.encode64( this.Nick );
                    sql += "'";
                }

                if (this.DateActivated != null && this.DateActivated.Ticks > 0)
                {
                    sql += " , ";
                    sql += " dateActivated = ";
                    sql += "'";
                    sql += this.DateActivated.ToString();
                    sql += "'";
                }




                if (this.FirstName != null)
                {
                    sql += " , ";
                    sql += " firstName = ";
                    sql += "'";
                    sql += ute.encode64( this.FirstName );
                    sql += "'";
                }
                if (this.LastName != null)
                {
                    sql += " , ";
                    sql += " lastName = ";
                    sql += "'";
                    sql += ute.encode64( this.LastName );
                    sql += "'";
                }
                if (this.Tags != null)
                {
                    sql += " , ";
                    sql += " tags = ";
                    sql += "'";
                    sql += this.Tags;
                    sql += "'";
                }
                if (this.PendingGUID != null)
                {
                    sql += " , ";
                    sql += " pendingGUID = ";
                    sql += "'";
                    sql += this.PendingGUID;
                    sql += "'";
                }
                else
                {
                    sql += " , ";
                    sql += " pendingGUID = ";

                    sql += " null ";
                    

                }

                sql += ",";
                sql += "notificationTypes = ";
                sql += "'";
                sql += this.NotificationTypes;
                sql += "'";
                sql += ",";
                sql += "notificationFrequency = ";
                sql += this.NotificationFrequency;

                sql += " WHERE ";
                sql += " ID = ";
                sql += this.ID;
                
            }


            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();

            this.populateUserFromDB(this.Email);

            if (newUser)
            {
                //TRY to add new user to the everyone group
                try
                {
                    Group grp = new Group(config.conn, "ZXZlcnlvbmU=");
                    grp.AddUser(this);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            return this.ID;
        }

        public void Delete()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "Delete from Users Where id=" + this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();

            if (numRows != 1)
                throw new UnableToDeleteUserException("unable to delelte user " + this.ID + " from DB - " + sql);

           


        }

        public string ToJSONString()
        {
           StringBuilder json = new StringBuilder();

           json.Append("{");

            json.Append("'id':");
           json.Append(this.ID);
           json.Append(",");
         
           json.Append("'username':");
           json.Append("'");
           json.Append( this.UserName);
           json.Append("'");
           json.Append(",");

           json.Append("'sponsorFullName':");
           json.Append("'");
           json.Append(ute.encode64( this.SponsorFullName));
           json.Append("'");
           json.Append(",");
           json.Append("'sponsorID':");
           json.Append(this.SponsorID);
            json.Append(",");            

           json.Append("'password':");
           json.Append("'");
           json.Append(ute.encode64( this.Password));
           json.Append("'");
           json.Append(",");

           json.Append("'firstName':");
           json.Append("'");
           json.Append(ute.encode64( this.FirstName));
           json.Append("'");
           json.Append(",");

           json.Append("'lastName':");
           json.Append("'");
           json.Append(ute.encode64( this.LastName));
           json.Append("'");
           json.Append(",");

           json.Append("'roles':");
           json.Append("'");
           json.Append(this.Roles);
           json.Append("'");
           json.Append(",");

           json.Append("'email':");
           json.Append("'");
           json.Append(ute.encode64( this.Email));
           json.Append("'");
           json.Append(",");

           json.Append("'tags':");
           json.Append("'");
            if(this.Tags != null && this.Tags.Trim().Length > 0 )
                json.Append(ute.encode64(this.Tags));
           json.Append("'");
           json.Append(",");

           json.Append("'groupAsPipe':");
           json.Append("'");
           json.Append(ute.encode64( this.GroupsAsPipe ));
           json.Append("'");
           json.Append(",");

           json.Append("'notificationFrequency':");
           json.Append(this.NotificationFrequency);
           json.Append(",");

           json.Append("'origInviteCode':");
           json.Append("'");
           json.Append(ute.encode64(this.OrigInviteCode));
           json.Append("'");

           json.Append(",");

           json.Append("'dateAdded':");
           json.Append("'");
           json.Append(ute.encode64(this.DateAdded.ToString() ));
           json.Append("'");

           json.Append("}");


            return json.ToString();
        }
    }

    public enum UserStates : int
    {
        added = 0,
        signed_in = 1,
        pre_approved = 2,
        accepted = 3,
        declined = 4,
        disabled = 5,
        suspended = 6,
        terminated = 7,
        pending_email_confirm = 8,
        viewed_invite = 9
    }
}
