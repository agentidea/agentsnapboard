using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraRaiffaController : ICommand
    {
        private int reveal = 1;

       private Dictionary<string, AverageTotaller> gTotals = new Dictionary<string, AverageTotaller>();
        private string gTX_ID = null;
        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string colHeaderList64 = MacroUtils.getParameterString("colHeaderList64", macro);
            string colActualsList64 = MacroUtils.getParameterString("colActualsList64", macro);
            string colHeaderList = TheUtils.ute.decode64(colHeaderList64);
            string[] colHeaders = colHeaderList.Split('|');

            string[] colActuals = TheUtils.ute.decode64(colActualsList64).Split('|');
            double[] colActualsF = new double[colActuals.Length];
            for (int i = 0; i < colActuals.Length; i++)
            {
                colActualsF[i] = Convert.ToDouble(colActuals[i]);
            }

            string tx_id = TheUtils.ute.decode64(tx_id64);
            reveal = MacroUtils.getParameterInt("reveal", macro);

            this.gTX_ID = tx_id;
            MacroEnvelope me = new MacroEnvelope();

            Macro proc = new Macro("DisplayDiv", 2);
            proc.addParameter("html64", TheUtils.ute.encode64(this.getHTML("SELECT * from RaiffaGameData", colHeaders)));
            proc.addParameter("targetDiv", targetDiv);

            me.addMacro(proc);
            return me;
        }

        public string getHTML(string sql ,string[] colHeaders)
        {
            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();
            System.Text.StringBuilder sbHTML = new StringBuilder();
            sbHTML.Append("<table id='tblRaiffaController' class='clsGrid' cellspacing='0' cellpadding='3' border='1'>");

            sbHTML.Append("<TR class='clsGridHeader'>");
            //header
            foreach (string colHead in colHeaders)
            {
                sbHTML.AppendFormat("<TD>{0}</TD>", colHead);
            }
            sbHTML.Append("</TR>");

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


                foreach (string colHead in colHeaders)
                {
                    var lowVal = dbHelper.reader[colHead + "_LOW"];
                    var highVal = dbHelper.reader[colHead + "_HIGH"];
                    
                    //how to pass value here - represent issue
                    //JSON
                    //LOOKUP by name
                    /*
                    
                     
                     */
                    
                    var actualVal = 435;

                   // sbHTML.AppendFormat("<TD>{0}</TD><TD>{1}</TD><TD>{2}</TD>", lowVal, highVal, InsideOrOut(lowVal, highVal, actualVal));


                }
                sbHTML.Append("</tr>");
            }

            sbHTML.Append("</table>");
            dbHelper.cleanup();
            dbHelper = null;

            return sbHTML.ToString();

        }

        private object InsideOrOut(object lowVal, object highVal)
        {
            throw new NotImplementedException();
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
                //processAverages(name, valInt);
                outputString = valInt + "";
            }

            //var cellKey = string.Format("{0}_{1}", tx_id, name);
            return outputString;
            // return string.Format("<div id='{1}'>{0}</div>",outputString,cellKey);


        }
        

       
    }

}
