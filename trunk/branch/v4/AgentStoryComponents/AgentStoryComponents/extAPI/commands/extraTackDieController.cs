using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraTackDieController : ICommand
    {
       private int reveal = -1;
       private int storyID = -1;

       private System.Collections.Generic.Dictionary<string, colSummary> gColSummaries = new Dictionary<string, colSummary>();

       private string gTX_ID = null;


        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            string gameCode = MacroUtils.getParameterString("gameCode", macro);
            string tableName = gameCode + "GameData";
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            storyID = MacroUtils.getParameterInt("storyID", macro);
            reveal = MacroUtils.getParameterInt("reveal", macro);

            string tx_id = TheUtils.ute.decode64(tx_id64);
           

            this.gTX_ID = tx_id;
            MacroEnvelope me = new MacroEnvelope();
            string sql = string.Format("SELECT * from {0}",tableName);
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
            sbHTML.Append(@"<table id='tblController' class='clsGrid'
                            cellspacing='0' cellpadding='3' border='1'>");
            


            List<Tuple> tups = TupleElements.getStoryTuples(storyID, config.sqlConn);
            
            //compound header row 
            sbHTML.Append("<TR class='clsGridHeader'>");
            sbHTML.Append("<td>Name</td>");

            if (reveal > 1)
            {
               
              
            }

            //report rows
            while (dbHelper.reader.Read())
            {
                var _tx_id = (string) dbHelper.reader["tx_id"];

                if(gTX_ID == _tx_id)
                    sbHTML.Append("<tr class='clsDiceGameRowHighlighted'>");
                else
                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
            
                
                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append(TheUtils.ute.decode64(Convert.ToString(dbHelper.reader["alias"])));
                sbHTML.Append("</td>");

                if (reveal > 1)
                {
                   
                }

                sbHTML.Append("</tr>");

            }

            if (reveal > 1)
            {
                //add totals
                
            }

           

            sbHTML.Append("</table>");

            dbHelper.cleanup();
            dbHelper = null;
            return sbHTML.ToString();

        }
    }
}
