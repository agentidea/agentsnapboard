using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraDMreport : ICommand
    {
 
       private System.Collections.Generic.Dictionary<string, colSummary> gColSummaries = new Dictionary<string, colSummary>();



        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            int whoseReport = MacroUtils.getParameterInt("reportForUserID", macro);

            MacroEnvelope me = new MacroEnvelope();
            string sql = string.Format("SELECT * from {0} WHERE [owner_id] = {1}", "DMData", whoseReport);
            string html = this.getHTML(sql);

            Macro p = new Macro("DisplayDiv", 2);
            p.addParameter("html64", TheUtils.ute.encode64(html));
            p.addParameter("targetDiv", targetDiv);

            me.addMacro(p);
            return me;
        }

        public string getHTML(string sql)
        {
            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            System.Text.StringBuilder sbHTML = new StringBuilder();
            sbHTML.Append(@"<table id='tblRaiffaController' class='clsGrid'
                            cellspacing='0' cellpadding='3' border='1'>");
            sbHTML.Append("<TR class='clsGridHeadRow'>");

   
            sbHTML.Append("<td>");
            sbHTML.Append("time");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("sugar");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("carbs");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("insulin");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("base");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("comment");
            sbHTML.Append("</td>");

            sbHTML.Append("</TR>");



            int flip = 1;
            //report rows
            while (dbHelper.reader.Read())
            {
                if (flip == 1)
                    flip = 2;
                else
                    flip = 1;

                string comment = string.Empty;

                if (dbHelper.reader["comment"] is DBNull)
                {
                }
                else
                {
                    comment = TheUtils.ute.decode64((string)dbHelper.reader["comment"]);
                }

                sbHTML.AppendFormat("<tr class='clsGridRow{0}'>",flip);

                sbHTML.AppendFormat("<td nowrap>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td>",
                    prettyDate(Convert.ToDateTime(dbHelper.reader["when"]))
                    ,emptyIfNull(dbHelper.reader,"sugar")
                    ,emptyIfNull(dbHelper.reader,"carbs")
                    ,emptyIfNull(dbHelper.reader,"insulinA")
                    ,emptyIfNull(dbHelper.reader,"insulinB")
                    ,comment
                    );
                sbHTML.Append("</tr>");
            }
                
              
            return sbHTML.ToString();

        }

        private string emptyIfNull(System.Data.OleDb.OleDbDataReader dr, string key)
        {
            string ret = string.Empty;

            if (dr[key] is DBNull)
            {
                //
            }
            else
            {
                ret = Convert.ToString(dr[key]);

            }
            return ret;
        }

        private string prettyDate(DateTime dt)
        {

            return dt.ToString("f");

        }
    


    }

   


}
