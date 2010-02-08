using System;
using System.Collections.Generic;
using System.Text;

namespace AgentStoryComponents
{
    public class UserGroupHelper
    {

        private int _currentGroupID = -1;
        public int getPlistCurrentGroupID()
        {
            return _currentGroupID;
        }
        public string getPlistGroupsID()
        {
            Groups groups = new Groups(config.conn);

            StringBuilder pList_GroupsID = new StringBuilder();
            int groupCounter = 0;

            foreach (Group g in groups.GroupList)
            {
                if (groupCounter == 0)
                    _currentGroupID = g.ID;
                groupCounter++;

                pList_GroupsID.Append(g.ID);
                pList_GroupsID.Append("|");
            }
            pList_GroupsID.Remove(pList_GroupsID.Length - 1, 1);

            return pList_GroupsID.ToString();
        }
        public string getPlistGroupsNames()
        {
            Groups groups = new Groups(config.conn);

            StringBuilder pList_GroupsID = new StringBuilder();
            foreach (Group g in groups.GroupList)
            {
                pList_GroupsID.Append(g.Name);
                pList_GroupsID.Append("|");
            }
            pList_GroupsID.Remove(pList_GroupsID.Length - 1, 1);

            return pList_GroupsID.ToString();
        }
        public string getPlistUsersID()
        {
            Users users = new Users(config.conn);

            StringBuilder pList_UsersID = new StringBuilder();
            foreach (User usr in users.UserList)
            {
                pList_UsersID.Append(usr.ID);
                pList_UsersID.Append("|");
            }
            pList_UsersID.Remove(pList_UsersID.Length - 1, 1);

            return pList_UsersID.ToString();
        }
        public string getPlistUsersNames()
        {
            Users users = new Users(config.conn);

            StringBuilder pList_UsersID = new StringBuilder();
            foreach (User usr in users.UserList)
            {
                pList_UsersID.Append(usr.UserName);
                pList_UsersID.Append("|");
            }
            pList_UsersID.Remove(pList_UsersID.Length - 1, 1);

            return pList_UsersID.ToString();
        }


    }
}
