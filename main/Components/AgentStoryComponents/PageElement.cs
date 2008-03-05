using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class PageElement
    {
        private utils ute = new utils();
        private string _connectionString = null;
        private User _by;
        public User OpenedBy = null;
        private int _id = -1;
        private string _code;
        private int _typeID;
        private string _value;
        private string _guid;
        private string _tags;

        private string _viewableBy;
        private string _editableBy;

        public string EditableBy
        {
            get { return _editableBy; }
            set { _editableBy = value; }
        }

        public string ViewableBy
        {
            get { return _viewableBy; }
            set { _viewableBy = value; }
        }

        private bool _canEdit = false;
        private bool _canView = false;

        public bool CanView
        {
            get { return _canView; }
            set { _canView = value; }
        }


        public bool CanEdit
        {
            get { return _canEdit; }

        }

        public int canEditInt
        {
            get
            {
                if (this._canEdit)
                    return 1;
                else
                    return 0;
            }
        }
        public int canViewInt
        {
            get
            {
                if (this._canView)
                    return 1;
                else
                    return 0;
            }
        }
	

        private List<User> _pageElementEditorUsers;
        private List<User> _pageElementViewerUsers;
        private List<Group> _pageElementEditorGroups;
        private List<Group> _pageElementViewerGroups;

        public List<Group> PageElementViewerGroups
        {

            get
            {
                if (_pageElementViewerGroups == null)
                    _pageElementViewerGroups = new List<Group>();

                return _pageElementViewerGroups;
            }

        }


        public List<Group> PageElementEditorGroups
        {

            get
            {
                if (_pageElementEditorGroups == null)
                    _pageElementEditorGroups = new List<Group>();

                return _pageElementEditorGroups;
            }

        }


        public List<User> PageElementViewerUsers
        {
            get
            {
                if (_pageElementViewerUsers == null)
                    _pageElementViewerUsers = new List<User>();

                return _pageElementViewerUsers;
            }

        }


        public List<User> PageElementEditorUsers
        {

            get
            {
                if (_pageElementEditorUsers == null)
                    _pageElementEditorUsers = new List<User>();

                return _pageElementEditorUsers;
            }

        }
	
	
        

        public string TypeName
        {
            get
            {
                string typeName = "invalid";

                if (this.TypeID == 1)
                    typeName = "text";
                if (this.TypeID == 2)
                    typeName = "audio";
                if (this.TypeID == 3)
                    typeName = "video";
                if (this.TypeID == 4)
                    typeName = "image";
                if (this.TypeID == 5)
                    typeName = "random";

                return typeName; 
            }
            
        }

        private DateTime _dateAdded;

        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
	
        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        public int TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }
        public User by
        {
            get { return _by; }
            set { _by = value; }
        }
        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
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


        public PageElement(string conn, User by)
        {
            this.by = by;
            this._connectionString = conn;
        }

        public PageElement(string conn, int id)
        {
            this._connectionString = conn;
            this.loadPageElementFromDB(id);
        }

        public PageElement(string conn, int id,User openedBy)
        {
            this._connectionString = conn;
            this.loadPageElementFromDB(id);
            this.OpenedBy = openedBy;
            //this.decoratePageElementACL2(openedBy);
        }

        private void loadPageElementFromDB(int pageElementID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from PageElement WHERE id = ";
            sql += pageElementID;

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            loadPageElementFromDbHelper(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }

        private void loadPageElementFromDB(System.Guid guid)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from PageElement WHERE guid = ";
            sql += "'";
            sql += guid.ToString();
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            loadPageElementFromDbHelper(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }

        private void loadPageElementFromDbHelper(OleDbHelper dbHelper)
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
                this.TypeID = Convert.ToInt32(dbHelper.reader["typeID"]);
                this.Value = Convert.ToString(dbHelper.reader["value"]);
                this.GUID = Convert.ToString(dbHelper.reader["guid"]);
                this.DateAdded = Convert.ToDateTime(dbHelper.reader["DateAdded"]);
                if (dbHelper.reader["tags"] is System.DBNull)
                {
                    this.Tags = null;
                }
                else
                {
                    this.Tags = (string)dbHelper.reader["tags"];
                }
                int user_id_originator = System.Convert.ToInt32(dbHelper.reader["user_id_originator"]);

                //load the by user if null
                if (this.by == null)
                {
                    this.by = new User(this._connectionString, user_id_originator);
                }
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

                sql = "INSERT INTO PageElement ( code,typeID,value,guid,tags,user_id_originator,dateAdded ) VALUES ( ";

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

                sql += this.TypeID;
                sql += ",";

                sql += "'";
                sql += this.Value;
                sql += "'";
                sql += ",";

                sql += "'";
                sql += newGUID.ToString();
                sql += "'";
                sql += ",";

                if (this.Tags == null)
                {
                    sql += "null";
                    sql += ",";
                }
                else
                {
                    sql += "'";
                    sql += this.Tags;
                    sql += "'";
                    sql += ",";
                }

                sql += this.by.ID;
                sql += ",";

                sql += "'";
                sql += ute.getDateStamp();
                sql += "'";

                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                //load object by way of guid
                this.loadPageElementFromDB(newGUID);

               // this.AddUserEditor(this.by);
               // this.AddUserViewer(this.by);

            }
            else
            {
                //existing story update!
                sql = "UPDATE PageElement ";
                sql += " SET ";

                string sCode = null;

                if(this.Code == null)
                {
                    sCode = "null";
                }
                else
                {
                    sCode = "'" + this.Code + "'";
                }
                sql += " typeID = " + this.TypeID;
                sql += ",";
                sql += " value = '" + this.Value + "'";
                sql += ",";
                sql += " tags = '" + this.Tags + "'";
                sql += " WHERE id =" + this.ID;


                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();
                
                //( code,typeID,value,guid,tags,user_id_originator,dateAdded ) VALUES ( ";

                

            }


            dbHelper.cleanup();
        }


        public void Delete()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql = "DELETE FROM StoryPageElement ";
            sql += " WHERE pageElement_id = " + this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            sql = "DELETE FROM pageElement ";
            sql += " WHERE guid = '" + this.GUID + "'";

            dbHelper.cmd.CommandText = sql;
            numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();
            dbHelper = null;
        }

        public PageElement Clone(User user)
        {
            PageElement pe = new PageElement(config.conn, user);

            pe.Value = this.Value;
            pe.TypeID = this.TypeID;
            pe.Code = this.Code;
            pe.Tags = this.Tags;
            pe.Save();

            return pe;
        }

        private void LoadPageElementPermissions(int pageElementID)
        {
            //LoadUserViewers(pageElementID);
            //LoadUserEditors(pageElementID);
            //LoadGroupViewers(pageElementID);
            //LoadGroupEditors(pageElementID);
        }


        //#region loading viewers and editors
        //private void LoadUserViewers(int pageElementID)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "Select user_id from PageElementUserViewers  WHERE peID = " + pageElementID;

        //    dbHelper.cmd.CommandText = sql;

        //    dbHelper.reader = dbHelper.cmd.ExecuteReader();

        //    this.PageElementViewerUsers.Clear();

        //    while (dbHelper.reader.Read())
        //    {
        //        int userID = (int)dbHelper.reader["user_id"];
        //        User u = new User(config.conn, userID);
        //        this.PageElementViewerUsers.Add(u);
        //    }


        //    dbHelper.cleanup();
        //    dbHelper = null;
        //}
        //private void LoadUserEditors(int pageElementID)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "Select user_id from PageElementUserEditors  WHERE peID = " + pageElementID;

        //    dbHelper.cmd.CommandText = sql;

        //    dbHelper.reader = dbHelper.cmd.ExecuteReader();

        //    this.PageElementEditorUsers.Clear();

        //    while (dbHelper.reader.Read())
        //    {
        //        int userID = (int)dbHelper.reader["user_id"];
        //        User u = new User(config.conn, userID);
        //        this.PageElementEditorUsers.Add(u);
        //    }


        //    dbHelper.cleanup();
        //    dbHelper = null;
        //}

        //private void LoadGroupViewers(int pageElementID)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "Select group_id from PageElementGroupViewers  WHERE peID = " + pageElementID;

        //    dbHelper.cmd.CommandText = sql;

        //    dbHelper.reader = dbHelper.cmd.ExecuteReader();

        //    this.PageElementViewerGroups.Clear();

        //    while (dbHelper.reader.Read())
        //    {
        //        int groupId = (int)dbHelper.reader["group_id"];
        //        Group g = new Group(config.conn, groupId);
        //        this.PageElementViewerGroups.Add(g);
        //    }


        //    dbHelper.cleanup();
        //    dbHelper = null;
        //}
        //private void LoadGroupEditors(int pageElementID)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "Select group_id from PageElementGroupEditors  WHERE peID = " + pageElementID;

        //    dbHelper.cmd.CommandText = sql;

        //    dbHelper.reader = dbHelper.cmd.ExecuteReader();

        //    this.PageElementEditorGroups.Clear();

        //    while (dbHelper.reader.Read())
        //    {
        //        int groupId = (int)dbHelper.reader["group_id"];
        //        Group g = new Group(config.conn, groupId);
        //        this.PageElementEditorGroups.Add(g);
        //    }


        //    dbHelper.cleanup();
        //    dbHelper = null;
        //}


        //public void AddUserViewer(User u)
        //{

        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "INSERT INTO PageElementUserViewers ( peID,user_id,dateAdded ) VALUES ( ";

        //    sql += this.ID;
        //    sql += ",";
        //    sql += u.ID;
        //    sql += ",";
        //    sql += "'";
        //    sql += ute.getDateStamp();
        //    sql += "'";
        //    sql += ")";

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;

        //    LoadUserViewers(this.ID);
        //}
        //public void RemoveUserViewer(User u)
        //{

        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "DELETE FROM PageElementUserViewers WHERE ";
        //    sql += " peID = " + this.ID;
        //    sql += " AND user_id = " + u.ID;

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;
        //    LoadUserViewers(this.ID);
        //}
        //public void AddUserEditor(User u)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "INSERT INTO PageElementUserEditors ( peID,user_id,dateAdded ) VALUES ( ";

        //    sql += this.ID;
        //    sql += ",";
        //    sql += u.ID;
        //    sql += ",";
        //    sql += "'";
        //    sql += ute.getDateStamp();
        //    sql += "'";
        //    sql += ")";

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;
        //    LoadUserEditors(this.ID);
        //}
        //public void RemoveUserEditor(User u)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "DELETE FROM PageElementUserEditors WHERE ";
        //    sql += " peID = " + this.ID;
        //    sql += " AND user_id = " + u.ID;

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;

        //    LoadUserEditors(this.ID);
        //}

        //public void AddGroupViewer(Group g)
        //{

        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "INSERT INTO PageElementGroupViewers ( peID,group_id,dateAdded ) VALUES ( ";

        //    sql += this.ID;
        //    sql += ",";
        //    sql += g.ID;
        //    sql += ",";
        //    sql += "'";
        //    sql += ute.getDateStamp();
        //    sql += "'";
        //    sql += ")";

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;
        //    LoadGroupViewers(this.ID);
        //}
        //public void RemoveGroupViewer(Group g)
        //{

        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "DELETE FROM PageElementGroupViewers WHERE ";
        //    sql += " peID = " + this.ID;
        //    sql += " AND group_id = " + g.ID;

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;
        //    LoadGroupViewers(this.ID);
        //}
        //public void AddGroupEditor(Group g)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "INSERT INTO PageElementGroupEditors ( peID,group_id,dateAdded ) VALUES ( ";

        //    sql += this.ID;
        //    sql += ",";
        //    sql += g.ID;
        //    sql += ",";
        //    sql += "'";
        //    sql += ute.getDateStamp();
        //    sql += "'";
        //    sql += ")";

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;
        //    LoadGroupEditors(this.ID);
        //}
        //public void RemoveGroupEditor(Group g)
        //{
        //    OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

        //    string sql = "";

        //    sql += "DELETE FROM PageElementGroupEditors WHERE ";
        //    sql += " peID = " + this.ID;
        //    sql += " AND group_id = " + g.ID;

        //    dbHelper.cmd.CommandText = sql;
        //    int numRows = dbHelper.cmd.ExecuteNonQuery();

        //    if (numRows != 1)
        //        throw new Exception("unable to " + sql);

        //    dbHelper.cleanup();
        //    dbHelper = null;

        //    LoadGroupEditors(this.ID);
        //}

        //public selWidget getUserEditors()
        //{
        //    selWidget tmpSelWidget = new selWidget();
        //    StringBuilder tmpSB = new StringBuilder("");
        //    StringBuilder tmpSB2 = new StringBuilder("");

        //    foreach (User u in this.PageElementEditorUsers)
        //    {
        //        tmpSB.Append(u.ID);
        //        tmpSB.Append("|");

        //        tmpSB2.Append(u.UserName);
        //        tmpSB2.Append("|");
        //    }

        //    if (this.PageElementEditorUsers.Count > 0)
        //    {
        //        tmpSB.Remove(tmpSB.Length - 1, 1);
        //        tmpSB2.Remove(tmpSB2.Length - 1, 1);
        //    }


        //    tmpSelWidget.ids = tmpSB.ToString();
        //    tmpSelWidget.names = tmpSB2.ToString();

        //    return tmpSelWidget;
        //}
        //public selWidget getUserViewers()
        //{
        //    selWidget tmpSelWidget = new selWidget();
        //    StringBuilder tmpSB = new StringBuilder("");
        //    StringBuilder tmpSB2 = new StringBuilder("");

        //    foreach (User u in this.PageElementViewerUsers)
        //    {
        //        tmpSB.Append(u.ID);
        //        tmpSB.Append("|");

        //        tmpSB2.Append(u.UserName);
        //        tmpSB2.Append("|");
        //    }

        //    if (this.PageElementViewerUsers.Count > 0)
        //    {
        //        tmpSB.Remove(tmpSB.Length - 1, 1);
        //        tmpSB2.Remove(tmpSB2.Length - 1, 1);
        //    }


        //    tmpSelWidget.ids = tmpSB.ToString();
        //    tmpSelWidget.names = tmpSB2.ToString();

        //    return tmpSelWidget;
        //}

        //public selWidget getGroupEditors()
        //{
        //    selWidget tmpSelWidget = new selWidget();
        //    StringBuilder tmpSB = new StringBuilder("");
        //    StringBuilder tmpSB2 = new StringBuilder("");

        //    foreach (Group g in this.PageElementEditorGroups)
        //    {
        //        tmpSB.Append(g.ID);
        //        tmpSB.Append("|");

        //        tmpSB2.Append(g.Name);
        //        tmpSB2.Append("|");
        //    }

        //    if (this.PageElementEditorGroups.Count > 0)
        //    {
        //        tmpSB.Remove(tmpSB.Length - 1, 1);
        //        tmpSB2.Remove(tmpSB2.Length - 1, 1);
        //    }


        //    tmpSelWidget.ids = tmpSB.ToString();
        //    tmpSelWidget.names = tmpSB2.ToString();

        //    return tmpSelWidget;
        //}
        //public selWidget getGroupViewers()
        //{
        //    selWidget tmpSelWidget = new selWidget();
        //    StringBuilder tmpSB = new StringBuilder("");
        //    StringBuilder tmpSB2 = new StringBuilder("");

        //    foreach (Group g in this.PageElementViewerGroups)
        //    {
        //        tmpSB.Append(g.ID);
        //        tmpSB.Append("|");

        //        tmpSB2.Append(g.Name);
        //        tmpSB2.Append("|");
        //    }

        //    if (this.PageElementViewerGroups.Count > 0)
        //    {
        //        tmpSB.Remove(tmpSB.Length - 1, 1);
        //        tmpSB2.Remove(tmpSB2.Length - 1, 1);
        //    }


        //    tmpSelWidget.ids = tmpSB.ToString();
        //    tmpSelWidget.names = tmpSB2.ToString();

        //    return tmpSelWidget;
        //}





        //public void decoratePageElementACL2(User _who)
        //{

        //    this._canView = false;
        //    this._canEdit = false;

        //    if (_who == null) return;

        //    if (_who.isRoot() == true)  // short circuit for root is g-d
        //    {
        //        this._canView = true;
        //        this._canEdit = true;

        //        return;

        //    }

        //    //we know who
        //    int userID = _who.ID;
        //    List<Group> theirGroups = _who.MyGroups;

        //    #region VIEWER status
        //    foreach (Group userGroup in theirGroups)
        //    {
        //        foreach (Group storyGroup in this.PageElementViewerGroups)
        //        {
        //            if (userGroup.ID == storyGroup.ID)
        //            {
        //                this._canView = true;
        //                break;
        //            }
        //        }
        //        if (this.CanView) break;
        //    }

        //    if (this.CanView == false)
        //    {
        //        //lookup user specific

        //        foreach (User storyUser in this.PageElementViewerUsers)
        //        {
        //            if (userID == storyUser.ID)
        //            {
        //                this._canView = true;
        //                break;
        //            }
        //        }
        //    }
        //    #endregion

        //    #region EDITOR status
        //    foreach (Group userGroup in theirGroups)
        //    {
        //        foreach (Group storyGroup in this.PageElementEditorGroups)
        //        {
        //            if (userGroup.ID == storyGroup.ID)
        //            {
        //                this._canEdit = true;
        //                break;
        //            }
        //        }
        //        if (this._canEdit) break;
        //    }

        //    if (this._canEdit == false)
        //    {
        //        //lookup user specific

        //        foreach (User storyUser in this.PageElementEditorUsers)
        //        {
        //            if (userID == storyUser.ID)
        //            {
        //                this._canEdit = true;
        //                break;
        //            }
        //        }
        //    }
        //    #endregion
        //}

        //#endregion
    }

   
}
