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


        public string preJavascript { get; set; }
        public string postJavascript { get; set; }

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

                if (dbHelper.reader["preJavaScript"] is DBNull)
                {
                    this.preJavascript = null;
                }
                else
                {
                    this.preJavascript = (string)dbHelper.reader["preJavaScript"];
                }

                if (dbHelper.reader["postJavaScript"] is DBNull)
                {
                    this.postJavascript = null;
                }
                else
                {
                    this.postJavascript = (string)dbHelper.reader["postJavaScript"];
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

                sql = "INSERT INTO PageElement ( code,typeID,value,preJavaScript,postJavaScript,guid,tags,user_id_originator,dateAdded ) VALUES ( ";

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

                if (this.preJavascript == null)
                {
                    sql += "null";
                    sql += ",";
                }
                else
                {
                    sql += "'";
                    sql += this.preJavascript;
                    sql += "'";
                    sql += ",";
                }
                if (this.postJavascript == null)
                {
                    sql += "null";
                    sql += ",";
                }
                else
                {
                    sql += "'";
                    sql += this.postJavascript;
                    sql += "'";
                    sql += ",";
                }

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

                //if(this.Code == null)
                //{
                //    sCode = "null";
                //}
                //else
                //{
                //    sCode = "'" + this.Code + "'";
                //}
                sql += " typeID = " + this.TypeID;
                sql += ",";
                sql += " value = '" + this.Value + "'";
                sql += ",";

                if (this.preJavascript != null)
                {
                    sql += " preJavaScript = '" + this.preJavascript + "'";
                }
                else
                {
                    sql += " preJavaScript = null";
                }
                sql += ",";

                if (this.postJavascript != null)
                {
                    sql += " postJavaScript = '" + this.postJavascript + "'";
                }
                else
                {
                    sql += " postJavaScript = null";
                }
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
            //deprecated
        }
     }
}
