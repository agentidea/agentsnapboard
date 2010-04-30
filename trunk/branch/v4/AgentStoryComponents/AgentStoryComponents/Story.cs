using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Story
    {
        private utils ute = new utils();
        private string _connectionString = null;
        public User by = null;
        public User openedBy = null;
        private int _ID = -1;
        private string _rating;
        private string _title;

        private string _description;
        private int _typeStory =0; //0 grid style 1 xyz ...


        private int _currPageID=-1;
/// <summary>
/// not saved in db for now
/// </summary>
        public int CurrPageCursor
        {
            get { return _currPageID; }
            set { _currPageID = value; }
        }
	


       

        public int LastSeq
        {
            get 
            { 

                StoryTxLog stl = new StoryTxLog(config.conn, this.ID);
                return stl.getMaxLastInfo();

            }
            
        }
	

        public int TypeStory
        {
            get { return _typeStory; }
            set { _typeStory = value; }
        }
	

       

        public StatsBag statBag
        {
            get
            {
                stats s = new stats();
                return s.storyHits(this); 
            
            }
           
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
	

        private List<User> _storyEditorUsers;
        private List<User> _storyViewerUsers;
        private List<Group> _storyEditorGroups;
        private List<Group> _storyViewerGroups;

        public List<Group> StoryViewerGroups
        {
            
             get 
            {
                if (_storyViewerGroups == null)
                    _storyViewerGroups = new List<Group>();

                return _storyViewerGroups; 
            }
           
        }
	

        public List<Group> StoryEditorGroups
        {
           
             get 
            {
                if (_storyEditorGroups == null)
                    _storyEditorGroups = new List<Group>();

                return _storyEditorGroups; 
            }
           
        }
	

        public List<User> StoryViewerUsers
        {
            get 
            {
                if (_storyViewerUsers == null)
                    _storyViewerUsers = new List<User>();

                return _storyViewerUsers; 
            }
            
        }
	

        public List<User> StoryEditorUsers
        {
            
             get 
            {
                if (_storyEditorUsers == null)
                    _storyEditorUsers = new List<User>();

                return _storyEditorUsers; 
            }
            
        }
	
       
        public string StateH
        {
            
           get 
           {
               return this.GetStoryStateHR(this.State);
           }
           
        }
	
        private int _state;

        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        private int _stateCursor;

        public int StateCursor
        {
            get { return _stateCursor; }
            set { _stateCursor = value; }
        }	
        private Page[] _pages;

        public Page[] Pages
        {
            get { return _pages; }
        }

        private PageElement[] _pageElements;

        public PageElement[] PageElements
        {
            get { return _pageElements; }
        }
	

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
	

      
	

        private string  _storyGUID;

        public string  StoryGUID
        {
            get { return _storyGUID; }
            set { _storyGUID = value; }
        }

        //added to allow for JS code injection to slideNavigator etc ...
        public string IncludeCodeDirName { get; set; }
	

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
	

        public string Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }
	
        
       

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        } 
        private DateTime _dateAdded;
        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
        /// <summary>
        /// new story
        /// </summary>
        /// <param name="asConnectionString"></param>
        /// <param name="by"></param>
        public Story(string asConnectionString, User by)
        {
            this.by = by;
            this._connectionString = asConnectionString;
           

            this.openedBy = by;
            this.decorateStoryACL2(openedBy);

        }

        /// <summary>
        /// load existing story
        /// </summary>
        /// <param name="asConnectionString"></param>
        /// <param name="storyID"></param>
        //public Story(string asConnectionString, int storyID)
        //{
        //    this.init(asConnectionString, storyID,false);
        //}

        public Story(string asConnectionString, int storyID,User openedBy)
        {
            this.openedBy = openedBy;
            this.init(asConnectionString, storyID, false);
            this.decorateStoryACL2(openedBy);

        }

        public Story(string asConnectionString, int storyID, bool lightWeight)
        {
            this.init(asConnectionString, storyID, lightWeight);
        }


        private void init(string asConnectionString, int storyID, bool lightWeight)
        {
            this._connectionString = asConnectionString;
            this.loadStoryFromDB(storyID);
            if (lightWeight == false)
            {
                this.loadStoryPagesFromDB(storyID);
                this.loadStoryPageElementsFromDB(storyID);
                this.LoadStoryPermissions(storyID);
            }
            
        }


        private void loadStoryFromDB(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from Story WHERE id = ";
            sql += storyID;
            

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.Title = (string)dbHelper.reader["title"];
                this.ID = System.Convert.ToInt32(dbHelper.reader["id"]);
                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);
                this.StoryGUID = System.Convert.ToString(dbHelper.reader["guid"]);
               
                this.State = Convert.ToInt32(dbHelper.reader["state"]);
                
                this.StateCursor = Convert.ToInt32(dbHelper.reader["StateCursor"]);

                if (dbHelper.reader["typeStory"] is System.DBNull)
                {
                    this.TypeStory = 0;
                }
                else
                {
                    this.TypeStory = Convert.ToInt32(dbHelper.reader["typeStory"]);
                }

                //added feature to include JS objects by way of include injection
                if (dbHelper.reader["IncludeCodeDirName"] is System.DBNull)
                {
                    this.IncludeCodeDirName = string.Empty;
                }
                else
                {
                    this.IncludeCodeDirName = Convert.ToString(dbHelper.reader["IncludeCodeDirName"]);
                }



                if (dbHelper.reader["description"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.Description = (string)dbHelper.reader["description"];
                }

                int user_id_originator = System.Convert.ToInt32(dbHelper.reader["user_id_originator"]);

                //load the by user if null
                if (this.by == null)
                {
                    this.by = new User(this._connectionString, user_id_originator);
                }

            }

            dbHelper.cleanup();
            dbHelper = null;

            

        }
        private void loadStoryPagesFromDB(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            //string sql = "Select page_id from StoryPage WHERE story_id = ";
            //sql += storyID;

            string sql = "Select page_id from vPagesByStory WHERE story_id = ";
            sql += storyID;
            sql += " ORDER BY seq";

            //
            

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            while( dbHelper.reader.Read())
            {
              int pageID = Convert.ToInt32( dbHelper.reader["page_id"] );
              Page p = new Page(config.conn, pageID,this.openedBy);

              if( p.CanView || p.CanEdit )
                  this.AddPage(p,false);
            }

            dbHelper.cleanup();
            dbHelper = null;

            

        }
        private void loadStoryPageElementsFromDB(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select pageElement_id from StoryPageElement WHERE story_id = ";
            sql += storyID;


            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            while (dbHelper.reader.Read())
            {
                int pageElementID = Convert.ToInt32(dbHelper.reader["pageElement_id"]);
                PageElement pe = new PageElement(config.conn, pageElementID,this.openedBy);

                
                this.AddPageElement(pe, false);
            }

            dbHelper.cleanup();
            dbHelper = null;

        }


        private void loadStoryFromDB( System.Guid guid )
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
            string sql = "Select * from Story WHERE guid = ";
            sql += "'";
            sql += guid.ToString();
            sql += "'";

            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();
                //set properties.
                this.Title = (string)dbHelper.reader["title"];
                this.ID = System.Convert.ToInt32(dbHelper.reader["id"]);
                this.DateAdded = System.Convert.ToDateTime(dbHelper.reader["dateAdded"]);
                this.StoryGUID = System.Convert.ToString(dbHelper.reader["guid"]);
               
                this.State = Convert.ToInt32(dbHelper.reader["state"]);
                this.StateCursor = Convert.ToInt32(dbHelper.reader["StateCursor"]);

                if (dbHelper.reader["typeStory"] is System.DBNull)
                {
                    this.TypeStory = 0;
                }
                else
                {
                    this.TypeStory = Convert.ToInt32(dbHelper.reader["typeStory"]);
                }

                if (dbHelper.reader["description"] is System.DBNull)
                {
                    //do nothing
                }
                else
                {
                    this.Description = (string)dbHelper.reader["description"];
                }

                int user_id_originator = System.Convert.ToInt32(dbHelper.reader["user_id_originator"]);

                //load the by user if null
                if (this.by == null)
                {
                    this.by = new User(this._connectionString, user_id_originator);
                }

            }

            dbHelper.cleanup();
            dbHelper = null;
        }

        public void Delete()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            if (this.Pages != null)
            {
                foreach (Page page in this.Pages)
                {
                    this.RemoveAssociation(page);
                    page.Delete();
                }

                //clear out pages
                //not strictly speaking necessary
                int numPages = this._pages.Length;
                for (int i = 0; i < numPages; i++)
                {
                    this._pages[i] = null;
                }
                this._pages = null;
            }

            if (this.PageElements != null)
            {
                foreach (PageElement pageElement in this.PageElements)
                {
                    this.RemoveAssociation(pageElement);
                    pageElement.Delete();
                }
                this._pageElements = null;
            }

            sql = "DELETE FROM Story ";
            sql += " WHERE guid = '"+ this.StoryGUID +"'";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();
            dbHelper = null;

            this.Reset();


        }

        private void Reset()
        {
            this.ID = -1;
            this.TypeStory = 0;
            this.StoryGUID = null;
            this.Description = null;
            this.Title = null;
            
            this.by = null;
            this.Rating = null;
          //???  this.DateAdded = new DateTime(1900,1,1);

        }

        public void DeletePage(Page page)
        {
            //remove page association first.
            this.RemoveAssociation(page);

            //delete the page
            page.Delete();

        }

        private void RemoveAssociation(Page page)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM StoryPage WHERE page_id =";
            sql += page.ID;
            sql += " AND story_id =";
            sql += this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null; 
        }

        private void RemoveAssociation(PageElement pe)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM StoryPageElement WHERE pageElement_id =";
            sql += pe.ID;
            sql += " AND story_id =";
            sql += this.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null; 
        }

        public int Save()
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            if (this.ID == -1)
            {
                //new story INSERT
                System.Guid newGUID = ute.getGUID();

                sql += "INSERT INTO Story ( title,user_id_originator,dateAdded,guid,description,state,typeStory) VALUES ( ";

                sql += "'";
                sql += ute.prepSQLstring(this.Title);
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

               
                if (this.Description != null)
                {
                    sql += "'";
                    sql += ute.prepSQLstring(this.Description);
                    sql += "'";
                }
                else
                {
                    sql += "null";
                }


                sql += ",1,";

                sql += this.TypeStory;
                
                sql += ")";

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

                //load object by way of guid
                this.loadStoryFromDB(newGUID);
            }
            else
            {
                //existing story update!
                sql = "UPDATE Story SET ";
                sql += " title = '"+ this.Title +"'";
                sql += ",";
                sql += " description = '" + this.Description + "'";
                sql += ",";
                sql += " state = " + this.State;
                 sql += ",";
                 sql += " stateCursor = " + this.StateCursor;
               

                sql += " WHERE id = " + this.ID;

                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();

            }

            dbHelper.cleanup();

            return this.ID;
                
        }

        private void Associate(Page p)
        {
            //associate page with this story.
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO StoryPage ( story_id,page_id ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += p.ID;
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null;

        }
        private void Associate(PageElement pe)
        {
            //associate page with this story.
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO StoryPageElement ( story_id,pageElement_id ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += pe.ID;
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null;

        }

        public void AddPageElement(PageElement pe)
        {
            this.AddPageElement(pe, true);
        }
        protected void AddPageElement(PageElement pe,bool associate)
        {

            if (this._pageElements == null)
            {
                this._pageElements = new PageElement[1];
                this._pageElements[0] = pe;
            }
            else
            {
                //copy array into new array.
                PageElement[] tmpPageElements = new PageElement[this._pageElements.Length + 1];
                int tmpPageElemCursor = 0;

                foreach (PageElement tmpPE in this._pageElements)
                {
                    tmpPageElements[tmpPageElemCursor] = tmpPE;
                    tmpPageElemCursor++;
                }

                tmpPageElements[tmpPageElemCursor] = pe;
                this._pageElements = null;
                this._pageElements = tmpPageElements;
            }

            //associate page with story
            if(associate)
                this.Associate(pe);
        }

        public void AddPage(Page p)
        {
            this.AddPage(p, true);
        }

        protected void AddPage(Page p,bool associate)
        {
            if (this._pages == null)
            {
                this._pages = new Page[1];
                this._pages[0] = p;
            }
            else
            {
                //copy array into new array.
                Page[] tmpPages = new Page[this._pages.Length + 1];
                int tmpPageCursor = 0;

                foreach (Page tmpP in this._pages)
                {
                    tmpPages[tmpPageCursor] = tmpP;
                    tmpPageCursor++;
                }

                tmpPages[tmpPageCursor] = p;
                this._pages = null;
                this._pages = tmpPages;
            }

            //associate page with story
            if(associate)
                this.Associate(p);



        }

        public string GetStoryJSON()
        {
            return this.GetStoryJSON(this);
        }

        public string GetStoryJSON2()
        {
            return this.GetStoryJSON2(this);
        }

        public string GetStoryJSON(Story st)
        {
            //gets the story JSON representation.
            System.Text.StringBuilder sbJSON = new StringBuilder();

            sbJSON.Append("{");
            sbJSON.Append("'ID':");
            sbJSON.Append(st.ID);
            sbJSON.Append(",");
            sbJSON.Append("'TypeStory':");
            sbJSON.Append(st.TypeStory);                //0 grid 1 cartesian layout
            sbJSON.Append(",");
            sbJSON.Append("'LastSeq':");
            sbJSON.Append(st.LastSeq);                // last update seq log
            sbJSON.Append(",");

            sbJSON.Append("'StateCursor':");
            sbJSON.Append(st.StateCursor);                // state cursor for keeping position of pages
            sbJSON.Append(",");




            sbJSON.Append("'StoryOpenedBy':");
            sbJSON.Append(st.by.ID);                //$TODO:  NEED TO GET THE REAL USER ID!!!!!
            sbJSON.Append(",");

            if (st._canEdit == true)
            {
                sbJSON.Append("'CanEdit':1,");
            }
            else
            {
                sbJSON.Append("'CanEdit':0,");
            }

            if (st._canView == true)
            {
                sbJSON.Append("'CanView':1,");
            }
            else
            {
                sbJSON.Append("'CanView':0,");
            }


            sbJSON.Append("'ByUserName':");
            sbJSON.Append("'");
            sbJSON.Append(TheUtils.ute.encode64( st.by.UserName));
            sbJSON.Append("'");
            sbJSON.Append(",");



            sbJSON.Append("'Title':");
            sbJSON.Append("'");
            sbJSON.Append( st.Title );
            sbJSON.Append("'");
            sbJSON.Append(",");
            sbJSON.Append("'Description':");
            sbJSON.Append("'");
            sbJSON.Append(st.Description);
            sbJSON.Append("'");
            sbJSON.Append(",");

            sbJSON.Append("'PageElements':");
            sbJSON.Append("{");

            int pageElementIndex = 0;
            if (st.PageElements != null)
            {
                foreach (PageElement pe in st.PageElements)
                {
                    pageElementIndex++;
                    sbJSON.Append("'pageElement_");
                    sbJSON.Append(pe.ID);
                    sbJSON.Append("':");
                    sbJSON.Append("{");

                        sbJSON.Append("'Type':");
                        sbJSON.Append("'");
                        sbJSON.Append(pe.TypeName);          
                        sbJSON.Append("'");
                        sbJSON.Append(",");

                        sbJSON.Append("'TypeID':");
                        sbJSON.Append(pe.TypeID);
                        sbJSON.Append(",");

                        sbJSON.Append("'ID':");
                        sbJSON.Append(pe.ID);
                        sbJSON.Append(",");

                        sbJSON.Append("'Value':");
                        sbJSON.Append("'");
                        sbJSON.Append(pe.Value);
                        sbJSON.Append("'");
                        sbJSON.Append(",");

                        if (pe.preJavascript != null)
                        {
                            sbJSON.Append("'preJavaScript':");
                            sbJSON.Append("'");
                            sbJSON.Append(pe.preJavascript.Trim());
                            sbJSON.Append("'");
                            sbJSON.Append(",");
                        }


                        if (pe.postJavascript != null)
                        {
                            sbJSON.Append("'postJavaScript':");
                            sbJSON.Append("'");
                            sbJSON.Append(pe.postJavascript.Trim());
                            sbJSON.Append("'");
                            sbJSON.Append(",");
                        }

                        sbJSON.Append("'DateAdded':");
                        sbJSON.Append("'");
                        sbJSON.Append(Convert.ToString( pe.DateAdded));
                        sbJSON.Append("'");
                        sbJSON.Append(",");

                        sbJSON.Append("'Tags':");
                        sbJSON.Append("'");
                        sbJSON.Append(pe.Tags);
                        sbJSON.Append("'");

                        sbJSON.Append(",");
                        sbJSON.Append("'GUID':");
                        sbJSON.Append("'");
                        sbJSON.Append(pe.GUID);
                        sbJSON.Append("'");

                        sbJSON.Append(",");
                        sbJSON.Append("'BY':");
                        sbJSON.Append("'");
                        sbJSON.Append(pe.by.UserName);
                        sbJSON.Append("'");


                        //bad place but hey ...
                        int canViewInt = 1;
                        int canEditInt = 1;

                        if (this.canEditInt == 0)
                        {
                            //storyviewer only; never can edit anything
                            canEditInt = 0;
                        }
                        else
                        {
                            //potential editor of element

                            if (pe.by.ID != this.openedBy.ID)
                            {
                                //not element of the user logged in
                                if (config.editorsElementsExclusive)
                                {
                                    //elements only editible by element owner
                                    canEditInt = 0;
                                }
                            }


                            if (this.openedBy.ID == this.by.ID)
                            {
                                if (config.ownerOfStoryCanModerate)
                                {
                                    //story owner IS allowed to edit everything
                                    // aka ( moderated )
                                    //story owner
                                    //must refresh to edit new stuff.
                                    canEditInt = 1;
                                }
                            }
                        }

                        sbJSON.Append(",");
                        sbJSON.Append("'CanView':");
                       // sbJSON.Append(pe.canViewInt);
                        sbJSON.Append(canViewInt);
                        sbJSON.Append(",");
                        sbJSON.Append("'CanEdit':");
                        sbJSON.Append(canEditInt);
                        


                    sbJSON.Append("}");
                    sbJSON.Append(",");
                    
                }
                //remove last comma
                sbJSON.Remove(sbJSON.Length - 1, 1);
                //sbJSON.Append("}");
            }

            sbJSON.Append("}");
            sbJSON.Append(",");

            sbJSON.Append("'PageElementCount':");
            sbJSON.Append(pageElementIndex);
            sbJSON.Append(",");



            sbJSON.Append("'Pages':");
            sbJSON.Append("{");
            int pageIndex = 0;
            if (st.Pages != null)
            {
                foreach (Page p in st.Pages)
                {
                    
                    sbJSON.Append("'page_");
                    sbJSON.Append(pageIndex);
                    sbJSON.Append("':");
                    sbJSON.Append("{");
                    sbJSON.Append("'Name':");
                    sbJSON.Append("'");
                    sbJSON.Append(p.Name);
                    sbJSON.Append("'");
                    sbJSON.Append(",");
                    sbJSON.Append("'ID':");
                    sbJSON.Append("'");
                    sbJSON.Append(p.ID);
                    sbJSON.Append("'");
                    sbJSON.Append(",");
                    sbJSON.Append("'GUID':");
                    sbJSON.Append("'");
                    sbJSON.Append(p.GUID);
                    sbJSON.Append("'");
                    sbJSON.Append(",");
                    sbJSON.Append("'gridCols':");
                    sbJSON.Append("'");
                    sbJSON.Append(p.GridX);
                    sbJSON.Append("'");
                    sbJSON.Append(",");
                    sbJSON.Append("'gridRows':");
                    sbJSON.Append("'");
                    sbJSON.Append(p.GridY);
                    sbJSON.Append("'");
                    sbJSON.Append(",");

                    sbJSON.Append("'CanView':");
                    sbJSON.Append(p.canViewInt);
                    sbJSON.Append(",");
                    sbJSON.Append("'CanEdit':"); 
                    sbJSON.Append(p.canEditInt);
                    sbJSON.Append(",");

                    sbJSON.Append("'PageElementMaps':");
                    sbJSON.Append("{");
                    int pageElementMapIndex = 0;
                    int pemMaxZ = 0;

                    if (p.PageElementMaps != null)
                    {
                        foreach (PageElementMap pem in p.PageElementMaps)
                        {
                            
                            sbJSON.Append("'pageElementMap_");


                            //here is the sticky differentiator between the table layout and the cartesian
                            if (pem.GridZ != null && pem.GridZ != -1)
                            {
                                sbJSON.Append(pageElementMapIndex);
                            }
                            else
                            {

                                sbJSON.Append(pem.GridX);
                                sbJSON.Append("_");
                                sbJSON.Append(pem.GridY);
                            }

                            sbJSON.Append("':");
                            sbJSON.Append("{");
                            sbJSON.Append("'PageElementID':");
                            sbJSON.Append(pem.PageElementID);
                            sbJSON.Append(",");
                            sbJSON.Append("'X':");
                            sbJSON.Append(pem.GridX);
                            sbJSON.Append(",");
                            sbJSON.Append("'Y':");
                            sbJSON.Append(pem.GridY);
                            sbJSON.Append(",");
                            sbJSON.Append("'Z':");
                            sbJSON.Append(pem.GridZ);

                            if (pem.GridZ > pemMaxZ)
                                pemMaxZ = pem.GridZ;


                            sbJSON.Append(",");
                            sbJSON.Append("'visible':true");
                            
                            sbJSON.Append(",");
                            sbJSON.Append("'GUID':");
                            sbJSON.Append("'");
                            sbJSON.Append(pem.GUID);
                            sbJSON.Append("'");
                            sbJSON.Append("}");
                            sbJSON.Append(",");

                            pageElementMapIndex++;
                        }
                        //remove last comma
                        sbJSON.Remove(sbJSON.Length - 1, 1);

                        
                    }

                    sbJSON.Append("}");

                   // sbJSON.Append(",");
                    //sbJSON.Append("'ElementCursor':-1");
                    sbJSON.Append(",");
                    sbJSON.Append("'PageElementMapCount':");
                    sbJSON.Append(pageElementMapIndex);

                    sbJSON.Append(",");
                    sbJSON.Append("'pemMaxZ':");
                    sbJSON.Append(pemMaxZ);

                    sbJSON.Append("}");
                    sbJSON.Append(",");

                    pageIndex++;
                }
                //remove last comma
                sbJSON.Remove(sbJSON.Length - 1, 1);
            }

            sbJSON.Append("}");
            sbJSON.Append(",");

            sbJSON.Append("'PageCount':");
            sbJSON.Append(pageIndex);

            sbJSON.Append(", 'storyTuples':[");
            getStoryTuples(sbJSON);
            sbJSON.Append("]");

            sbJSON.Append("}");

            return sbJSON.ToString();
        }

        private void getStoryTuples(StringBuilder sbJSON)
        {
            List<Tuple> storyTups = TupleElements.getStoryTuples(this.ID, config.sqlConn);

            if (storyTups.Count > 0)
            {
                foreach (Tuple t in storyTups)
	            {
		 
	
                sbJSON.Append("{");

                sbJSON.AppendFormat("'id':{0},", t.id);
                sbJSON.AppendFormat("'guid':'{0}',", t.guid);
                sbJSON.AppendFormat("'name':'{0}',", t.name);
                sbJSON.AppendFormat("'code':'{0}',", t.code);
                sbJSON.AppendFormat("'description':'{0}',", t.description);
                sbJSON.AppendFormat("'units':'{0}',", t.units);
                sbJSON.AppendFormat("'value':'{0}',", t.val);
                sbJSON.AppendFormat("'numValue':{0}", t.valNum);

                sbJSON.Append("}");
                sbJSON.Append(",");
                
                }
                 //remove last comma
                sbJSON.Remove(sbJSON.Length - 1, 1);
            }


        }


        public string GetStoryJSON2(Story st)
        {
            //gets the story JSON representation.
            System.Text.StringBuilder sbJSON = new StringBuilder();

            sbJSON.Append("{");
            sbJSON.Append("'ID':");
            sbJSON.Append(st.ID);
            sbJSON.Append(",");
            sbJSON.Append("'Title':");
            sbJSON.Append("'");
            sbJSON.Append(st.Title);
            sbJSON.Append("'");
            sbJSON.Append(",");
            sbJSON.Append("'Description':");
            sbJSON.Append("'");
            sbJSON.Append(st.Description);
            sbJSON.Append("'");
            sbJSON.Append(",");

            sbJSON.Append("'PageElements':[");
              int pageElementIndex = 0;
            if (st.PageElements != null)
            {
                foreach (PageElement pe in st.PageElements)
                {
                    pageElementIndex++;

                    sbJSON.Append("{");
                    sbJSON.Append("'ID':");
                    sbJSON.Append(pe.ID);
                    sbJSON.Append(",");

                    sbJSON.Append("'Value':");
                    sbJSON.Append("'");
                    sbJSON.Append(pe.Value);
                    sbJSON.Append("'");
                    sbJSON.Append(",");

                    sbJSON.Append("'DateAdded':");
                    sbJSON.Append("'");
                    sbJSON.Append(Convert.ToString(pe.DateAdded));
                    sbJSON.Append("'");
                    sbJSON.Append(",");

                  
                    sbJSON.Append("'GUID':");
                    sbJSON.Append("'");
                    sbJSON.Append(pe.GUID);
                    sbJSON.Append("'");

                    sbJSON.Append(",");
                    sbJSON.Append("'BY':");
                    sbJSON.Append("'");
                    sbJSON.Append(pe.by.UserName);
                    sbJSON.Append("'");
                    sbJSON.Append("}");
                    sbJSON.Append(",");

                }
                
                sbJSON.Remove(sbJSON.Length - 1, 1);
                
            }

            sbJSON.Append("]");
   
            sbJSON.Append("}");

            return sbJSON.ToString();
        }

        private void LoadStoryPermissions(int storyID)
        {
            LoadUserViewers(storyID);
            LoadUserEditors(storyID);
            LoadGroupViewers(storyID);
            LoadGroupEditors(storyID);
        }
        #region loading viewers and editors
        private void LoadUserViewers(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "Select user_id from StoryUserViewers  WHERE story_id = " + storyID;

            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            this.StoryViewerUsers.Clear();

            while (dbHelper.reader.Read())
            {
                int userID = (int) dbHelper.reader["user_id"];
                User u = new User(config.conn, userID);
                this.StoryViewerUsers.Add(u);
            }


            dbHelper.cleanup();
            dbHelper = null;
        }
        private void LoadUserEditors(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "Select user_id from StoryUserEditors  WHERE story_id = " + storyID;

            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            this.StoryEditorUsers.Clear();

            while (dbHelper.reader.Read())
            {
                int userID = (int)dbHelper.reader["user_id"];
                User u = new User(config.conn, userID);
                this.StoryEditorUsers.Add(u);
            }


            dbHelper.cleanup();
            dbHelper = null;
        }

        private void LoadGroupViewers(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "Select group_id from StoryGroupViewers  WHERE story_id = " + storyID;

            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            this.StoryViewerGroups.Clear();

            while (dbHelper.reader.Read())
            {
                int groupId = (int)dbHelper.reader["group_id"];
                Group g = new Group(config.conn, groupId);
                this.StoryViewerGroups.Add(g);
            }


            dbHelper.cleanup();
            dbHelper = null;
        }
        private void LoadGroupEditors(int storyID)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "Select group_id from StoryGroupEditors  WHERE story_id = " + storyID;

            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            this.StoryEditorGroups.Clear();

            while (dbHelper.reader.Read())
            {
                int groupId = (int)dbHelper.reader["group_id"];
                Group g = new Group(config.conn, groupId);
                this.StoryEditorGroups.Add(g);
            }


            dbHelper.cleanup();
            dbHelper = null;
        }


        public void AddUserViewer(User u)
        {

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO StoryUserViewers ( story_id,user_id,dateAdded ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += u.ID;
            sql += ",";
            sql += "'";
            sql += ute.getDateStamp();
            sql += "'";
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            
            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;

            LoadUserViewers(this.ID);
        }
        public void RemoveUserViewer(User u)
        {

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM StoryUserViewers WHERE ";
            sql += " story_id = " + this.ID;
            sql += " AND user_id = " + u.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;
            LoadUserViewers(this.ID);
        }
        public void AddUserEditor(User u)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO StoryUserEditors ( story_id,user_id,dateAdded ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += u.ID;
            sql += ",";
            sql += "'";
            sql += ute.getDateStamp();
            sql += "'";
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;
            LoadUserEditors(this.ID);
        }
        public void RemoveUserEditor(User u)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM StoryUserEditors WHERE ";
            sql += " story_id = " + this.ID;
            sql += " AND user_id = " + u.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;

            LoadUserEditors(this.ID);
        }

        public void AddGroupViewer(Group g)
        {

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO StoryGroupViewers ( story_id,group_id,dateAdded ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += g.ID;
            sql += ",";
            sql += "'";
            sql += ute.getDateStamp();
            sql += "'";
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;
            LoadGroupViewers(this.ID);
        }
        public void RemoveGroupViewer(Group g)
        {

            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM StoryGroupViewers WHERE ";
            sql += " story_id = " + this.ID;
            sql += " AND group_id = " + g.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;
            LoadGroupViewers(this.ID);
        }
        public void AddGroupEditor(Group g)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "INSERT INTO StoryGroupEditors ( story_id,group_id,dateAdded ) VALUES ( ";

            sql += this.ID;
            sql += ",";
            sql += g.ID;
            sql += ",";
            sql += "'";
            sql += ute.getDateStamp();
            sql += "'";
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;
            LoadGroupEditors(this.ID);
        }
        public void RemoveGroupEditor(Group g)
        {
            OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

            string sql = "";

            sql += "DELETE FROM StoryGroupEditors WHERE ";
            sql += " story_id = " + this.ID;
            sql += " AND group_id = " + g.ID;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            if (numRows != 1)
                throw new Exception("unable to " + sql);

            dbHelper.cleanup();
            dbHelper = null;

            LoadGroupEditors(this.ID);
        }

        public selWidget getUserEditors()
        {
            selWidget tmpSelWidget = new selWidget();
            StringBuilder tmpSB = new StringBuilder("");
            StringBuilder tmpSB2 = new StringBuilder("");

            foreach (User u in this.StoryEditorUsers )
            {
                tmpSB.Append(u.ID);
                tmpSB.Append("|");

                tmpSB2.Append(u.UserName);
                tmpSB2.Append("|");
            }

            if (this.StoryEditorUsers.Count > 0)
            {
                tmpSB.Remove(tmpSB.Length - 1, 1);
                tmpSB2.Remove(tmpSB2.Length - 1, 1);
            }


            tmpSelWidget.ids = tmpSB.ToString();
            tmpSelWidget.names = tmpSB2.ToString();

            return tmpSelWidget;
        }
        public selWidget getUserViewers()
        {
            selWidget tmpSelWidget = new selWidget();
            StringBuilder tmpSB = new StringBuilder("");
            StringBuilder tmpSB2 = new StringBuilder("");

            foreach (User u in this.StoryViewerUsers)
            {
                tmpSB.Append(u.ID);
                tmpSB.Append("|");

                tmpSB2.Append(u.UserName);
                tmpSB2.Append("|");
            }

            if (this.StoryViewerUsers.Count > 0)
            {
                tmpSB.Remove(tmpSB.Length - 1, 1);
                tmpSB2.Remove(tmpSB2.Length - 1, 1);
            }


            tmpSelWidget.ids = tmpSB.ToString();
            tmpSelWidget.names = tmpSB2.ToString();

            return tmpSelWidget;
        }

        public selWidget getGroupEditors()
        {
            selWidget tmpSelWidget = new selWidget();
            StringBuilder tmpSB = new StringBuilder("");
            StringBuilder tmpSB2 = new StringBuilder("");

            foreach (Group g in this.StoryEditorGroups)
            {
                tmpSB.Append(g.ID);
                tmpSB.Append("|");

                tmpSB2.Append(g.Name);
                tmpSB2.Append("|");
            }

            if (this.StoryEditorGroups.Count > 0)
            {
                tmpSB.Remove(tmpSB.Length - 1, 1);
                tmpSB2.Remove(tmpSB2.Length - 1, 1);
            }


            tmpSelWidget.ids = tmpSB.ToString();
            tmpSelWidget.names = tmpSB2.ToString();

            return tmpSelWidget;
        }
        public selWidget getGroupViewers()
        {
            selWidget tmpSelWidget = new selWidget();
            StringBuilder tmpSB = new StringBuilder("");
            StringBuilder tmpSB2 = new StringBuilder("");

            foreach (Group g in this.StoryViewerGroups)
            {
                tmpSB.Append(g.ID);
                tmpSB.Append("|");

                tmpSB2.Append(g.Name);
                tmpSB2.Append("|");
            }

            if (this.StoryViewerGroups.Count > 0)
            {
                tmpSB.Remove(tmpSB.Length - 1, 1);
                tmpSB2.Remove(tmpSB2.Length - 1, 1);
            }


            tmpSelWidget.ids = tmpSB.ToString();
            tmpSelWidget.names = tmpSB2.ToString();

            return tmpSelWidget;
        }





        public void decorateStoryACL2(User _who)
        {

            this._canView = false;
            this._canEdit = false;

            if (_who == null) return;

            if (_who.isRoot() == true )  // short circuit for root is g-d
            {
                this._canView = true;
                this._canEdit = true;

                return;

            }

            //we know who
            int userID = _who.ID;
            List<Group> theirGroups = _who.MyGroups;

            #region VIEWER status
            foreach (Group userGroup in theirGroups)
            {
                foreach (Group storyGroup in this.StoryViewerGroups)
                {
                    if (userGroup.ID == storyGroup.ID)
                    {
                        this._canView = true;
                        break;
                    }
                }
                if (this.CanView) break;
            }

            if (this.CanView == false)
            {
                //lookup user specific
               
                foreach (User storyUser in this.StoryViewerUsers)
                {
                    if (userID == storyUser.ID)
                    {
                        this._canView = true;
                        break;
                    }
                }
            }
            #endregion

            #region EDITOR status
            foreach (Group userGroup in theirGroups)
            {
                foreach (Group storyGroup in this.StoryEditorGroups)
                {
                    if (userGroup.ID == storyGroup.ID)
                    {
                        this._canEdit = true;
                        break;
                    }
                }
                if (this._canEdit) break;
            }

            if (this._canEdit == false)
            {
                //lookup user specific
                
                foreach (User storyUser in this.StoryEditorUsers)
                {
                    if (userID == storyUser.ID)
                    {
                        this._canEdit = true;
                        break;
                    }
                }
            }
             #endregion
        }

#endregion

        public string GetStoryStateHR(int StoryState)
        {
            string lsStateHR = "";

            switch (StoryState)
            {
                case 0:
                    lsStateHR = "inactive";
                    break;
                case 1:
                    lsStateHR = "active";
                    break;
                case 2:
                    lsStateHR = "archived";
                    break;
                case 3:
                    lsStateHR = "deactivated";
                    break;
                case 4:
                    lsStateHR = "suspect";
                    break;
                case 5:
                    lsStateHR = "deleted";
                    break;
            }

            return lsStateHR;
        }

        public static int associateTuple(string conn, int tupleID, int storyID)
        {
            //associate page with this story.
            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(conn);

            string sql = "";

            sql += "INSERT INTO StoryTuple( storyID,tupleID ) VALUES ( ";

            sql += storyID;
            sql += ",";
            sql += tupleID;
            sql += ")";

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null;

            return numRows;
        }

        public static int dissociateStoryTuple(string conn, int tupleID, int storyID)
        {
            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(conn);

            string sql = "";

            sql += "DELETE FROM StoryTuple WHERE ";
            sql += "storyID =";
            sql += storyID;
            sql += " AND ";
            sql += "tupleID =";
            sql += tupleID;
           

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();
            dbHelper.cleanup();
            dbHelper = null;

            return numRows;
        }
    }

    public class selWidget
    {
        public string ids = null;
        public string names = null;
    }
}
