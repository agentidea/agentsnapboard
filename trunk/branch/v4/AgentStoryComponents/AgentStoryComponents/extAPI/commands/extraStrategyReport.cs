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
        private int reveal = 1;

        private Dictionary<string, AverageTotaller> gTotals = new Dictionary<string, AverageTotaller>();
        private string gTX_ID = null;
        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);
            reveal = MacroUtils.getParameterInt("reveal", macro);

            this.gTX_ID = tx_id;
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
            sbHTML.Append("<table id='tblStrategy' class='clsDiceGameData' cellspacing='0' cellpadding='3' border='1'>");



            sbHTML.Append("<tr class='clsDiceGameHeader' >");
          
            sbHTML.Append("<td>");
            sbHTML.Append("Name");
            sbHTML.Append("</td>");
            if (reveal >= 2)
            {
                sbHTML.Append("<td colspan='4' align='center'>");
                sbHTML.Append("Strategy");
                sbHTML.Append("</td>");
            }
            if (reveal >= 3)
            {

                sbHTML.Append("<td nowrap='true' colspan='2'  align='center'>");
                sbHTML.Append("Unfunded");
                sbHTML.Append("</td>");
            }
            if (reveal >= 4)
            {
                sbHTML.Append("<td nowrap='true' colspan='2'  align='center'>");
                sbHTML.Append("Funded");
                sbHTML.Append("</td>");

            }
            if (reveal >= 5)
            {
                sbHTML.Append("<td nowrap='true' colspan='2'  align='center'>");
                sbHTML.Append("Best 5");
                sbHTML.Append("</td>");
            }
            sbHTML.Append("</tr>");

            sbHTML.Append("<tr class='clsDiceGameHeader' >");

            sbHTML.Append("<td>");
            sbHTML.Append("&nbsp;");
            sbHTML.Append("</td>");
            if (reveal >= 2)
            {
                sbHTML.Append("<td title='Pearl'>");
                sbHTML.Append("PE");
                sbHTML.Append("</td>");
                sbHTML.Append("<td title='Oyster'>");
                sbHTML.Append("OY");
                sbHTML.Append("</td>");
                sbHTML.Append("<td nowrap='true' title='Bread and Butter'>");
                sbHTML.Append("BB");
                sbHTML.Append("</td>");
                sbHTML.Append("<td nowrap='true' title='White Elephant'>");
                sbHTML.Append("WE");
                sbHTML.Append("</td>");
            }
            if (reveal >= 3)
            {

                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append("Points");
                sbHTML.Append("</td>");
                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append("Successes");
                sbHTML.Append("</td>");
            }
            if (reveal >= 4)
            {
                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append("Points");
                sbHTML.Append("</td>");
                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append("Successes");
                sbHTML.Append("</td>");

            }
            if (reveal >= 5)
            {
                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append("Points");
                sbHTML.Append("</td>");
                sbHTML.Append("<td nowrap='true'>");
                sbHTML.Append("Successes");
                sbHTML.Append("</td>");
            }
            sbHTML.Append("</tr>");


            

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

                if (reveal >= 2)
                {
                    sbHTML.Append("<td  align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "pearl"));
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "oyster"));
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "breadAndButter"));
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "whiteElephant"));
                    sbHTML.Append("</td>");
                }
                 if(reveal >=3)
                {
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "UnFunded_Points"));
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "UnFunded_Success"));
                    sbHTML.Append("</td>");
                }
                if(reveal >=4)
                {
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "Funded_Points")); 
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                    sbHTML.Append(procNum(dbHelper.reader, "Funded_Success"));
                    sbHTML.Append("</td>");

                    
                 }
                 if (reveal >= 5)
                 {
                     sbHTML.Append("<td align='right'>");
                     sbHTML.Append(procNum(dbHelper.reader, "Best5_Points"));
                     sbHTML.Append("</td>");
                     sbHTML.Append("<td align='right'>");
                     sbHTML.Append(procNum(dbHelper.reader, "Best5_Success"));
                     sbHTML.Append("</td>");
                 }
                
                sbHTML.Append("</tr>");
            }


            if (gTotals.Count > 0)
            {

                #region blank row
                sbHTML.Append("<tr class='clsDiceGameRow1'>");


                if (reveal >= 2)
                {
                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");
                }
                if (reveal >= 3)
                {

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");
                }
                if (reveal >= 4)
                {
                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");
                }
                if (reveal >= 5)
                {
                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td>");
                    sbHTML.Append("&nbsp;");
                    sbHTML.Append("</td>");
                }

                sbHTML.Append("</tr>");
                #endregion
                #region averages row
                sbHTML.Append("<tr class='clsDiceGameTotals'>");


                if (reveal >= 2)
                {
                    sbHTML.Append("<td>");
                    sbHTML.Append("AVERAGES");
                    sbHTML.Append("</td>");

                    sbHTML.Append("<td align='right'>");
                    try{  sbHTML.Append(gTotals["pearl"].Average); } catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                   try{ sbHTML.Append(gTotals["oyster"].Average);} catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                   try{ sbHTML.Append(gTotals["breadAndButter"].Average);} catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                  try{  sbHTML.Append(gTotals["whiteElephant"].Average);} catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                }
                if (reveal >= 3)
                {

                    sbHTML.Append("<td align='right'>");
                  try{  sbHTML.Append(gTotals["UnFunded_Points"].Average);} catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                 try{   sbHTML.Append(gTotals["UnFunded_Success"].Average);} catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                }
                if (reveal >= 4)
                {
                    sbHTML.Append("<td align='right'>");
                try{  sbHTML.Append(gTotals["Funded_Points"].Average);} catch (Exception epp) { sbHTML.Append(" * "); }
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                  
                  try { sbHTML.Append(gTotals["Funded_Success"].Average); } catch (Exception epp) { sbHTML.Append(" * "); }

                    sbHTML.Append("</td>");
                }
                if (reveal >= 5)
                {
                    sbHTML.Append("<td align='right'>");
                    try { sbHTML.Append(gTotals["Best5_Points"].Average); }                    catch (Exception epp) { sbHTML.Append(" * "); }                 
                    sbHTML.Append("</td>");
                    sbHTML.Append("<td align='right'>");
                    try { sbHTML.Append(gTotals["Best5_Success"].Average); }                    catch (Exception epp) { sbHTML.Append(" * "); }
                
                    sbHTML.Append("</td>");
                }

                sbHTML.Append("</tr>");
                #endregion
            }

            sbHTML.Append("</table>");
            dbHelper.cleanup();
            dbHelper = null;

            return sbHTML.ToString();

        }
        private string procNum(System.Data.OleDb.OleDbDataReader reader,string name)
        {

            string outputString = null;

            int valInt = System.Convert.ToInt32( reader[name]);

            if (valInt < 0)
            {
                outputString = "&nbsp;";
            }
            else
            {
                processAverages(name, valInt);
                outputString = valInt + "";
            }

            //var cellKey = string.Format("{0}_{1}", tx_id, name);
            return outputString;
            // return string.Format("<div id='{1}'>{0}</div>",outputString,cellKey);


        }
        

        private void processAverages(string name, int valInt)
        {
            if (gTotals.ContainsKey(name))
            {
                gTotals[name].add(valInt);
            }
            else
            {
                gTotals.Add(name, new AverageTotaller(valInt));
            }
        }
    }

    /// <summary>
    /// running list values, to get Average from
    /// </summary>
    public class AverageTotaller 
    {
        private List<double> _ints = new List<double>();
        public AverageTotaller(int initialValue)
        {
            this.add(initialValue);
        }
        public void add(int val)
        {
            _ints.Add( (double) val);
        }

        public double Average { 
            get {
                double sum = 0.0;
                double numItems = (double) _ints.Count;
                for (int i = 0; i < numItems; i++)
                    sum = sum + _ints[i];

                double result = sum / numItems;
                return Math.Round(result,1);
            }
        }

    }
}
