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

        private Dictionary<string, AverageTotaller> gTotals = new Dictionary<string, AverageTotaller>();
        private string gTX_ID = null;
        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);
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
            sbHTML.Append("alias");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("pearl");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("oyster");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("bread and butter");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("white elephant");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("funded Success");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("funded Points");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("un-funded Points");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("un-funded Success");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("Best 5 Success");
            sbHTML.Append("</td>");
            sbHTML.Append("<td nowrap='true'>");
            sbHTML.Append("Best 5 Points");
            sbHTML.Append("</td>");

            sbHTML.Append("</tr>");


            

            while (dbHelper.reader.Read())
            {


                var _tx_id = (string) dbHelper.reader["tx_id"];

                if(gTX_ID == _tx_id)
                    sbHTML.Append("<tr class='clsDiceGameRowHighlighted'>");
                else
                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
            
                
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
            sbHTML.Append("Averages");
            sbHTML.Append("</td>");
 
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["pearl"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["oyster"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["breadAndButter"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["whiteElephant"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Funded_Success"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Funded_Points"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["UnFunded_Points"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["UnFunded_Success"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Best5_Success"].Average);
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append(gTotals["Best5_Points"].Average);
            sbHTML.Append("</td>");

            sbHTML.Append("</tr>");
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
        private List<int> _ints = new List<int>();
        public AverageTotaller(int initialValue)
        {
            this.add(initialValue);
        }
        public void add(int val)
        {
            _ints.Add(val);
        }

        public int Average { 
            get {
                var sum = 0;
                int numItems = _ints.Count;
                for (int i = 0; i < numItems; i++)
                    sum = sum + _ints[i];

                return sum / numItems;
            }
        }

    }
}
