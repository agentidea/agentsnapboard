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
            sbHTML.Append("<TR class='clsGridHeader'>");


           

            //report rows
            while (dbHelper.reader.Read())
            {

                sbHTML.Append("<tr class='clsDiceGameRow1'>");
                sbHTML.AppendFormat("<td>{0}</td>", (int) dbHelper.reader["sugar"]);
                sbHTML.Append("</tr>");
            }
                
              
            return sbHTML.ToString();

        }


       
    }



}
