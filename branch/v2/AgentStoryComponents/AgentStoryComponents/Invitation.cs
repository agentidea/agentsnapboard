using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents
{

    public enum InviteStates
    {
        open,alreadyPickedUp

    }
   public class Invitation
    {
       private utils ute = new utils();
       private string _connectionString = null;

       private string _guid;

       private InviteStates _state;

       public InviteStates State
       {
           get { return _state; }
           set { _state = value; }
       }
	

       private string _InviteEvent;

       public string InviteEvent
       {
           get { return _InviteEvent; }
           set { _InviteEvent = value; }
       }
	

       public string GUID
       {
           get { return _guid; }
           set { _guid = value; }
       }
	
       private User _to;
       private User _from;

       public User from
       {
           get { return _from; }
           set { _from = value; }
       }
	

       public User to
       {
           get { return _to; }
           set { _to = value; }
       }
	
       private int _id;
       private DateTime _dateAdded;

       public DateTime DateAdded
       {
           get { return _dateAdded; }
           set { _dateAdded = value; }
       }
	

       public int ID
       {
           get { return _id; }
           set { _id = value; }
       }
	
       private string _title;
       private string _body;
       private string _imageUrl;
       private string _inviteCode;

       public string InviteCode
       {
           get { return _inviteCode; }
           set { _inviteCode = value; }
       }
	

       public string ImageUrl
       {
           get { return _imageUrl; }
           set { _imageUrl = value; }
       }
	

       public string InvitationText
       {
           get 
           {
               if (_body == null)
                   _body = "";

               return _body; 
           }
           set { _body = value; }
       }
	

       public string Title
       {
           get { return _title; }
           set { _title = value; }
       }
	
       /// <summary>
       /// create a new invitation from user A to user B
       /// </summary>
       /// <param name="from"></param>
       /// <param name="to"></param>
       public Invitation(string conn,User from,User to)
       {
           this.DateAdded = System.DateTime.Now;
           this.from = from;
           this.to = to;
           this.ID = -1;  //new invitation
           this._connectionString = conn;
           
       }

       /// <summary>
       /// opens an existing invitation by way of a GUID
       /// </summary>
       /// <param name="uuid"></param>
       public Invitation(string conn,string uuid)
       {
           this._connectionString = conn;
           this.populateInvitationFromDB(uuid);

       }

       private void populateInvitationFromDB(string guid)
       {
           OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);
           string sql = "Select * from invitation WHERE guid = ";
           sql += "'";
           sql += guid;
           sql += "'";

           dbHelper.cmd.CommandText = sql;
           dbHelper.reader = dbHelper.cmd.ExecuteReader();

           if (dbHelper.reader.HasRows)
           {
               dbHelper.reader.Read();
               //set properties.

               this.ID = System.Convert.ToInt32(dbHelper.reader["ID"]);
               this.InviteCode = (string)dbHelper.reader["invitationCode"];
               this.Title = (string)dbHelper.reader["title"];
               this.GUID = System.Convert.ToString( dbHelper.reader["guid"] );
               this.InviteEvent = System.Convert.ToString(dbHelper.reader["InviteEvent"]);

               if (dbHelper.reader["invitationText"] is System.DBNull)
               {
                   //do nothing
               }
               else
               {
                   this.InvitationText = (string)dbHelper.reader["invitationText"];
               }

               if (dbHelper.reader["imgUrl"] is System.DBNull)
               {
                   //do nothing
               }
               else
               {
                   this.ImageUrl = (string)dbHelper.reader["imgUrl"];
               }

               //bring up the user objects as well
               int toID = System.Convert.ToInt32(dbHelper.reader["user_id_to"]);
               int fromID = System.Convert.ToInt32(dbHelper.reader["user_id_from"]);


               this.to = new User(this._connectionString,toID);
               this.from = new User(this._connectionString, fromID);


           }
           else
           {
               throw new InvitationDoesNotExistException("no invite for guid " + guid);
           }

           dbHelper.cleanup();
           dbHelper = null;

       }

       public void Save()
       {
           //persists invitation to db
           OleDbHelper dbHelper = ute.getDBcmd(this._connectionString);

           string sql = "";

           if (this.ID == -1)
           {
               this.GUID = ute.getGUID().ToString();

               //insert
               sql = "INSERT into INVITATION ";
               sql += " ( ";
               sql += " invitationCode, user_id_from, user_id_to,title,invitationText,dateAdded,guid,InviteEvent";
               sql += " ) ";
               sql += " values ";
               sql += "(";

               sql += "'";
               sql += this.InviteCode;
               sql += "'";
               sql += ",";

               sql += this.from.ID;
               sql += ",";
               sql += this.to.ID;
               sql += ",";

               sql += "'";
               sql += this.Title;
               sql += "'";
               sql += ",";

               sql += "'";
               sql += this.InvitationText;
               sql += "'";
               sql += ",";

               sql += "'";
               sql += Convert.ToString(this.DateAdded);
               sql += "'";
               sql += ",";

               sql += "'";
               sql += this.GUID;
               sql += "'";
               sql += ",";

               sql += "'";
               sql += this.InviteEvent;
               sql += "'";
               sql += ")";

               

           }
           else
           {
               //item already existed
               //UPDATE
           }

           dbHelper.cmd.CommandText = sql;
           int numRows = dbHelper.cmd.ExecuteNonQuery();

           if (numRows != 1)
               throw new Exception("no new invite created for sql " + sql);


       }
    }
}
