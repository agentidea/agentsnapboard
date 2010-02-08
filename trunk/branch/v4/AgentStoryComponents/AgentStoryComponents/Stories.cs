using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class Stories
    {

        private utils ute = new utils();
        private string _connectionString = null;
       // private int _count =0;
        private User _who = null;

        public int Count
        {
            get
            {
                return StorieList.Count; 
            }
        }
	

        public Stories(string asConnectionString)
        {
            this._connectionString = asConnectionString;
        }

        public Stories(string asConnectionString, User who)
        {
            this._connectionString = asConnectionString;
            if (who == null) throw new Exception(" null who ");

            this._who = who;

        }

        public List<Story> StorieList
        {
            get
            {
                List<Story> storyList = new List<Story>();

                string sql = "";
                sql += "SELECT * FROM Story WHERE state = 1 ORDER BY dateAdded DESC";

                OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
                dbHelper.cmd.CommandText = sql;

                dbHelper.reader = dbHelper.cmd.ExecuteReader();

                if (dbHelper.reader.HasRows)
                {
                    while (dbHelper.reader.Read())
                    {
                        int storyID = Convert.ToInt32(dbHelper.reader["ID"]);

                        Story s = new Story(this._connectionString, storyID, _who);


                        s.decorateStoryACL2(_who);

                        if(s.CanEdit || s.CanView)
                            storyList.Add(s);
                        
                    }
                }

                dbHelper.cleanup();

                return storyList;
            }
        }


    }



    public class MyStories
    {

        private List<Story> _stories;
        private int _userID;

        public int UserID
        {
            get { return _userID; }
        }
	
        public List<Story> Stories
        {
            get
            {
                return _stories; 
            }
            set
            {
                _stories = value;
            }
        }

        public List<int> StoryIDs
        {

            get {

                /*
                 * 
select story_id from StoryGroupEditors where group_id in ( select group_id from UsersGroups where user_id = 1 )
select story_id from StoryGroupViewers where group_id in ( select group_id from UsersGroups where user_id = 1 )
select story_id from StoryUserEditors where user_id = 1
select story_id from StoryUserViewers where user_id = 1
                 * 
                 */

                List<int> _stories = new List<int>();
                OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);

                string sql = "";

                sql = "select story_id from StoryGroupEditors where group_id in ( select group_id from UsersGroups where user_id = " + this._userID +   " )";
                GetStoryIDs(_stories, dbHelper, sql);

                sql = "select story_id from StoryGroupViewers where group_id in ( select group_id from UsersGroups where user_id = " + this._userID + " )";
                GetStoryIDs(_stories, dbHelper, sql);

                sql = "select story_id from StoryUserEditors where user_id = " + this._userID;
                GetStoryIDs(_stories, dbHelper, sql);

                sql = "select story_id from StoryUserViewers where user_id = " + this._userID;
                GetStoryIDs(_stories, dbHelper, sql);






                dbHelper.cleanup();




                return _stories;

            }

        }

        private static void GetStoryIDs(List<int> _stories, OleDbHelper dbHelper, string sql)
        {
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    int id = Convert.ToInt32(dbHelper.reader[0]);
                    if(_stories.Contains( id ) == false )
                        _stories.Add(id);

                }
            }

            dbHelper.reader.Close();
        }
	
        public MyStories(int userID)
        {
            this._userID = userID;

            List<int> _storyIDs = this.StoryIDs;
            
            if(_storyIDs.Count == 0) return;

            if (_stories == null) _stories = new List<Story>(_storyIDs.Count);

            foreach (int nStoryID in _storyIDs )
            {
                Story s = new Story(config.conn, nStoryID,true);
                _stories.Add(s);
             }

        }


        public string StoriesMetaJSON
        {
            get
            {
                int storyCursor = 0;
                System.Text.StringBuilder storyMetaJSON = new System.Text.StringBuilder();

                storyMetaJSON.Append("{");


                storyMetaJSON.Append("'stories':{");
                foreach (Story story in this._stories)
                {
                    storyCursor++;
                    storyMetaJSON.Append("'story_" + storyCursor);
                    storyMetaJSON.Append("':{");
                    storyMetaJSON.Append("'Title':");
                    storyMetaJSON.Append("'");
                    storyMetaJSON.Append(story.Title);
                    storyMetaJSON.Append("'");
                    storyMetaJSON.Append(",");
                    storyMetaJSON.Append("'ID':");
                    storyMetaJSON.Append(story.ID);
                    storyMetaJSON.Append(",");
                    storyMetaJSON.Append("'TypeStory':");
                    storyMetaJSON.Append(story.TypeStory);
                    storyMetaJSON.Append(",");
                    storyMetaJSON.Append("'UniqueHits':");
                    storyMetaJSON.Append(story.statBag.numViews);
                    storyMetaJSON.Append(",");

                    if (story.statBag.lastEditedBy != null)
                    {
                        storyMetaJSON.Append("'LastEditedBy':'");
                        storyMetaJSON.Append(story.statBag.lastEditedBy.UserName);
                        storyMetaJSON.Append("',");
                        storyMetaJSON.Append("'LastEditedWhen':'");
                        storyMetaJSON.Append(story.statBag.dayLastEdited);
                        storyMetaJSON.Append("',");
                    }

                    if (story.CanEdit == true)
                    {
                        storyMetaJSON.Append("'CanEdit':1,");
                    }
                    else
                    {
                        storyMetaJSON.Append("'CanEdit':0,");
                    }

                    if (story.CanView == true)
                    {
                        storyMetaJSON.Append("'CanView':1,");
                    }
                    else
                    {
                        storyMetaJSON.Append("'CanView':0,");
                    }

                    storyMetaJSON.Append("'Author':");
                    storyMetaJSON.Append("'");
                    storyMetaJSON.Append(story.by.UserName);
                    storyMetaJSON.Append("'");
                    storyMetaJSON.Append(",");
                    storyMetaJSON.Append("'AuthorID':");
                    storyMetaJSON.Append(story.by.ID);
                    storyMetaJSON.Append(",");
                    storyMetaJSON.Append("'Added':");
                    storyMetaJSON.Append("'");
                    storyMetaJSON.Append(story.DateAdded);
                    storyMetaJSON.Append("'");



                    storyMetaJSON.Append("}");
                    storyMetaJSON.Append(",");
                }
                if (_stories.Count > 0)
                    storyMetaJSON.Remove(storyMetaJSON.Length - 1, 1);

                storyMetaJSON.Append("}");
                storyMetaJSON.Append(",");
                storyMetaJSON.Append("'count':");
                storyMetaJSON.Append(storyCursor);
                storyMetaJSON.Append("}");

                return storyMetaJSON.ToString();
            }

        }



    }




}
