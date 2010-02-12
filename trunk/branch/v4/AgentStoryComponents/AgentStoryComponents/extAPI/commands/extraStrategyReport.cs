using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraStrategyReport : ICommand
    {

        private Hashtable gTotals = new Hashtable();

        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);
            
            MacroEnvelope me = new MacroEnvelope();

            Macro proc = new Macro("DisplayDiv", 2);
            proc.addParameter("html64", TheUtils.ute.encode64( this.getHTML("SELECT * from DiceGameData")));
            proc.addParameter("targetDiv", targetDiv);

            me.addMacro(proc);
            return me;
        }

        public string getHTML(string sql)
        {
            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();
            System.Text.StringBuilder sbHTML = new StringBuilder();
            sbHTML.Append("<table class='clsDiceGameData' cellspacing='0'>");



            sbHTML.Append("<tr class='clsDiceGameHeader' >");
            sbHTML.Append("<td>");
            sbHTML.Append("id");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("alias");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("pearl");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("oyster");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("b&b");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("White Elephant");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("FundedSuccess");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("FundedPoints");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("UnFundedPoints");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("UnFundedSuccess");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("Best5Success");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("Best5Points");
            sbHTML.Append("</td>");

            sbHTML.Append("</tr>");



            while (dbHelper.reader.Read())
            {
                sbHTML.Append("<tr class='clsDiceGameRow1'>");
                
                sbHTML.Append("<td>");
                sbHTML.Append(Convert.ToInt32(dbHelper.reader["id"]));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(TheUtils.ute.decode64(Convert.ToString(dbHelper.reader["alias"])));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"pearl"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"oyster"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"breadAndButter"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"whiteElephant"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"Funded_Success"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"Funded_Points"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"UnFunded_Points"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"UnFunded_Success"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"Best5_Success"));
                sbHTML.Append("</td>");
                sbHTML.Append("<td>");
                sbHTML.Append(procNum(dbHelper.reader,"Best5_Points"));
                sbHTML.Append("</td>");
                
                sbHTML.Append("</tr>");
            }

            sbHTML.Append("<tr class='clsDiceGameTotals'>");

            sbHTML.Append("<td>");
            sbHTML.Append("totals");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("&nbsp;");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["pearl"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["oyster"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["breadAndButter"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["whiteElephant"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Funded_Success"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Funded_Points"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["UnFunded_Points"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["UnFunded_Success"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Best5_Success"]);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Best5_Points"]);
            sbHTML.Append("</td>");

            sbHTML.Append("</tr>");
            sbHTML.Append("</table>");
            dbHelper.cleanup();
            dbHelper = null;

            return sbHTML.ToString();

        }

        private string procNum(System.Data.OleDb.OleDbDataReader reader,string name)
        {

            int valInt = System.Convert.ToInt32( reader[name]);

            if (valInt < 0)
            {
                return "&nbsp;";
            }
            else
            {
                processTotals(name, valInt);
                return valInt + "";

            }





        }

        private void processTotals(string name, int valInt)
        {
            if (gTotals.ContainsKey(name))
            {
                int valtmp = (int) gTotals[name];
                valtmp = valtmp + valInt;
                gTotals[name] = valtmp;

            }
            else
            {
                gTotals.Add(name, valInt);
            }
        }


       
    }
}
