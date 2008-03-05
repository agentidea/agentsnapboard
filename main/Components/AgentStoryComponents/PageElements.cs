using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public class PageElements
    {
        private string _conn;
        private utils ute = new utils();

        public string conn
        {
            get { return _conn; }
        }

        public PageElements(string asConnectionString)
        {
            this._conn = asConnectionString;
        }

        public List<PageElement> getPageElements(string UserName,int num)
        {

            List<PageElement> pes = new List<PageElement>();
            UserName = TheUtils.ute.encode64(UserName.Trim());

            string sql = "";
            sql += "SELECT TOP "+ num +" * FROM vPageElemByUser ";
            sql += " WHERE username ='" + UserName + "'";
            sql += " ORDER BY id DESC";

            OleDbHelper dbHelper = ute.getDBcmd(this._conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    int id = Convert.ToInt32(dbHelper.reader["ID"]);

                    PageElement pe = new PageElement(this._conn, id);

                    pes.Add(pe);

                }
            }

            dbHelper.cleanup();
            return pes;
        }


        public List<PageElement> getPageElements(int userID)
        {

            List<PageElement> pes = new List<PageElement>();

            string sql = "";
            sql += "SELECT TOP 150 * FROM PageElement ";
            if(userID != 1)
                sql += " WHERE user_id_originator=" + userID;
            sql += "ORDER BY id DESC";

            OleDbHelper dbHelper = ute.getDBcmd(this._conn);
            dbHelper.cmd.CommandText = sql;

            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            if (dbHelper.reader.HasRows)
            {
                while (dbHelper.reader.Read())
                {
                    int id = Convert.ToInt32(dbHelper.reader["ID"]);

                    PageElement pe = new PageElement(this._conn, id);

                    pes.Add(pe);

                }
            }

            dbHelper.cleanup();
            return pes;
        }


        public string getPageElementsJSON(int userID)
        {
            List<PageElement> lstPageElems = this.getPageElements(userID);

            StringBuilder json = new StringBuilder();

            json.Append("{'PageElements':");

            json.Append("[");

            foreach (PageElement pe in lstPageElems)
            {
                json.Append("{");
                json.Append("'id':");
                json.Append(pe.ID);
                json.Append(",");

                json.Append("'value64':");
                json.Append("'");
                json.Append(pe.Value);
                json.Append("'");
                json.Append(",");

                json.Append("'addedByUsername':");
                json.Append("'");
                json.Append(pe.by.UserName);
                json.Append("'");
                json.Append(",");

                json.Append("'DateAdded':");
                json.Append("'");
                json.Append(pe.DateAdded);
                json.Append("'");
                json.Append(",");

                json.Append("'code':");
                json.Append("'"); 
                json.Append(pe.Code);
                json.Append("'");
                json.Append("}");
                json.Append(",");
                
            }
            if(lstPageElems.Count > 0)
                json.Remove(json.Length - 1, 1);
            json.Append("]");
            json.Append("}");

            return json.ToString();

        }
    
    }
}
