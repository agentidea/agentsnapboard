using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Page
    {
        private utils ute = new utils();
        private string _connectionString = null;
        private int _id = -1;
        private string _code;
        private string _name;
        private int _seq = -1;
        private DateTime _dateAdded;
       
        private int _gridX = -1;
        private int _gridY = -1;
        private int _gridZ = -1;

        private string _guid;
        private User _by;
        public User OpenedBy = null;

        private PageElementMap[] _pageElementMaps;

        #region visibility
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

#endregion




        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public int GridY
        {
            get { return _gridY; }
            set { _gridY = value; }
        }
        public int GridX
        {
            get { return _gridX; }
            set { _gridX = value; }
        }
        public int GridZ
        {
            get { return _gridZ; }
            set { _gridZ = value; }
        }

        private List<User> _pageEditorUsers;
        private List<User> _pageViewerUsers;
        private List<Group> _pageEditorGroups;
        private List<Group> _pageViewerGroups;

        public List<Group> PageViewerGroups
        {

            get
            {
                if (_pageViewerGroups == null)
                    _pageViewerGroups = new List<Group>();

                return _pageViewerGroups;
            }

        }


        public List<Group> PageEditorGroups
        {

            get
            {
                if (_pageEditorGroups == null)
                    _pageEditorGroups = new List<Group>();

                return _pageEditorGroups;
            }

        }


        public List<User> PageViewerUsers
        {
            get
            {
                if (_pageViewerUsers == null)
                    _pageViewerUsers = new List<User>();

                return _pageViewerUsers;
            }

        }


        public List<User> PageEditorUsers
        {

            get
            {
                if (_pageEditorUsers == null)
                    _pageEditorUsers = new List<User>();

                return _pageEditorUsers;
            }

        }
	



        public PageElementMap[] PageElementMaps
        {
            get { return _pageElementMaps; }
            
        }
        public Page(string conn, User by)
        {
            this._by = by;
            this._connectionString = conn;

            this.OpenedBy = by;

        }
        public Page(string conn,int id)
        {
            this._connectionString = conn;
            this.ID = id;
            this.loadPageFromDB(id);
            this.loadPageElementMapsFromDB(id);
           
        }

        public Page(string conn, int id,User openedBy)
        {
            this._connectionString = conn;
            this.ID = id;
            this.OpenedBy = openedBy;

            this.loadPageFromDB(id);
            this.loadPageElementMapsFromDB(id);

            this._canEdit = true;
            this._canView = true;

            //this.LoadPagePermissions(id);
            //this.decoratePageACL2(openedBy);


        }


        private void loadPageElementMapsFromDB(int pageID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            
            //string sql = "Select pageElementMap_id from PagePageElementMap WHERE page_id = ";
            //sql += pageID;

            //need to sort by gridZ / z index

            string sql = @"SELECT dbo.PagePageElementMap.pageElementMap_id, dbo.PageElementMap.gridZ
                            FROM  dbo.PageElementMap INNER JOIN
                            dbo.PagePageElementMap ON dbo.PageElementMap.id = 
                                            dbo.PagePageElementMap.pageElementMap_id
                            WHERE     (dbo.PagePageElementMap.page_id =" + pageID + @" )
                            ORDER BY dbo.PageElementMap.gridZ ASC";


            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            while (dbHelper.reader.Read())
            {
                int pageElementMapID = Convert.ToInt32(dbHelper.reader["pageElementMap_id"]);
                PageElementMap pem = new PageElementMap(config.conn, pageElementMapID);
                this.AddPageElementMap(pem,false);
            }

            dbHelper.cleanup();
            dbHelper = null;



        }

        private void loadPageFromDB(int pageID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from Page WHERE id = ";
            sql += pageID;

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            loadPageFromDbHelper(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }
        private void loadPageFromDB(System.Guid guid)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from Page WHERE guid = ";
            sql += "'";
            sql += guid.ToString();
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            loadPageFromDbHelper(dbHelper);

            dbHelper.cleanup();
            dbHelper = null;
        }
        private void loadPageFromDbHelper( OleDbHelper dbHelper)
        {
            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                this.Name = (string)dbHelper.reader["name"];
                this.ID = Convert.ToInt32(dbHelper.reader["id"]);
                this.GUID = Convert.ToString( dbHelper.reader["guid"]);
                this.GridX = Convert.ToInt32(dbHelper.reader["gridX"]);
                this.GridY = Convert.ToInt32(dbHelper.reader["gridY"]);
                this.Seq = Convert.ToInt32(dbHelper.reader["seq"]);

                if (dbHelper.reader["code"] is System.DBNull)
                {
                    this.Code = null;
                }
                else
                {
                    this.Code = (string)dbHelper.reader["code"];
                }

                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);

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
                //new page INSERT
                System.Guid newGUID = ute.getGUID();

                sql += "INSERT INTO Page ( name,user_id_originator,dateAdded,guid,gridX,gridY,gridZ,seq ) VALUES ( ";

                sql += "'";
                sql += this.Name;
                sql += "'";
                sql += ",";

                sql += this.by.ID;
                sql += ",";

                sql += "'";
                sql += ute.getDateStamp();
                sql += "'";
                sql += ",";

                sql += "'";
                sql += newGUID.ToString();
                sql += "'";
                sql += ",";

                sql += this.GridX;
                sql += ",";
                sql += this.GridY;
                sql += ",";
                sql += this.GridZ;
                sql += ",";
                sql += this.Seq;
                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                //load object by way of guid
                this.loadPageFromDB(newGUID);

                //this.AddUserEditor(this.by);
                //this.AddUserViewer(this.by);

            }
            else
            {
                //existing PAGE update!
                sql += "UPDATE Page SET ";
                //name,user_id_originator,dateAdded,guid,gridX,gridY,seq ) VALUES ( ";

                sql += " name =";
                sql += "'";
                sql += this.Name;
                sql += "'";
                sql += ",";
                sql += " gridX =";
                sql += this.GridX;
                sql += ",";
                sql += " gridY =";
                sql += this.GridY;
                sql += ",";
                sql += " gridZ =";
                sql += this.GridZ;
                sql += " WHERE id=" + this.ID;

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

               

            }
        }
        public void Delete()
        {

            //delete any page element maps.
            if (this.PageElementMaps != null)
            {
                foreach (PageElementMap pem in this.PageElementMaps)
                {
                    this.RemoveAssociation(pem);
                    pem.Delete();
                }
                this._pageElementMaps = null;
            }

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM page ";
            sql += " WHERE guid = '" + this.GUID + "'";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();
            dbHelper = null;
        }
        public void AddPageElementMap(PageElementMap pem,bool associate)
        {
            //_pageElementMaps
            if (this._pageElementMaps == null)
            {
                this._pageElementMaps = new PageElementMap[1];
                this._pageElementMaps[0] = pem;
            }
            else
            {
                //copy array into new array.
                PageElementMap[] tmpPageElementMaps = new PageElementMap[this._pageElementMaps.Length + 1];
                int tmpPageElemMapCursor = 0;

                foreach (PageElementMap tmpPEM in this._pageElementMaps)
                {
                    tmpPageElementMaps[tmpPageElemMapCursor] = tmpPEM;
                    tmpPageElemMapCursor++;
                }

                tmpPageElementMaps[tmpPageElemMapCursor] = pem;
                this._pageElementMaps = null;
                this._pageElementMaps = tmpPageElementMaps;
            }

            //associate pageElementMap with Page
            if(associate)
                this.Associate(pem);
        }
        private void Associate(PageElementMap pem)
        {
            //associate page with this story.
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO PagePageElementMap ( page_id,pageElementMap_id ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += pem.ID;
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null;

        }
        private void RemoveAssociation(PageElementMap pem)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM PagePageElementMap WHERE pageElementMap_id =";
            sql += pem.ID;
            sql += " AND page_id =";
            sql += this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null;
        }


        /// <summary>
        /// make a deep copy of this page
        /// </summary>
        /// <returns></returns>
        public Page Clone(User cloner,Story newStory, bool deep)
        {
            Page newPage = new Page(config.conn, cloner);

            newPage.Name = this.Name;
            newPage.GridX = this.GridX;
            newPage.GridY = this.GridY;
            newPage.GridZ = this.GridZ;
            newPage.Seq = this.Seq;
            newPage.Save();


            //copy page elements
            if (deep)
            {
                foreach (PageElementMap pem in this.PageElementMaps)
                {

                    PageElement existingPe = new PageElement(config.conn, pem.PageElementID);
                    PageElement newPe = existingPe.Clone(cloner);
                    newStory.AddPageElement(newPe);

                    PageElementMap newPem = new PageElementMap(config.conn);

                    newPem.GridX = pem.GridX;
                    newPem.GridY = pem.GridY;
                    newPem.GridZ = pem.GridZ;
                    newPem.PageElementID = newPe.ID;
                    newPem.Save();

                    newPage.AddPageElementMap(newPem, true);
                }
            }

            return newPage;
        }

        private void LoadPagePermissions(int pageID)
        {
            /*
            LoadUserViewers(pageID);
            LoadUserEditors(pageID);
            LoadGroupViewers(pageID);
            LoadGroupEditors(pageID);
            */
        }

        
    }

    
}
