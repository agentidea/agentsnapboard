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
            sbHTML.Append(@"<table id='tblRaiffaController' class='clsGrid'
                            cellspacing='0' cellpadding='3' border='1'>");
            sbHTML.Append("<TR class='clsGridHeader'>");


            List<Tuple> tups = TupleElements.getStoryTuples(storyID, config.sqlConn);
            
            //compound header row 
            sbHTML.Append("<TR>");
            sbHTML.Append("<td>Name</td>");

            if (reveal > 1)
            {
                foreach (Tuple t in tups)
                {
                    sbHTML.AppendFormat("<td colspan='3' align='center'>{0}<br/>{1}</td>", t.name, Math.Round( t.valNum,1));
                }
                sbHTML.Append("</TR>");
                sbHTML.Append("<TR>");
                sbHTML.Append("<td>&nbsp;</td>");
                foreach (Tuple t in tups)
                {
                    sbHTML.AppendFormat("<td align='center'>low</td><td align='center'>high</td><td align='center'>accuracy</td>");
                }
                sbHTML.Append("</TR>");
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
                    foreach (Tuple t in tups)
                    {

                        //check for incomplete data and breakout ...
                        if (dbHelper.reader[t.code + "_LOW"] is DBNull || dbHelper.reader[t.code + "_HIGH"] is DBNull)
                        {
                            sbHTML.AppendFormat("<TD>{0}</TD><TD>{1}</TD><TD>{2}</TD>", "*", "*", "*");
                            continue;
                        }

                        decimal lowVal = Convert.ToDecimal(dbHelper.reader[t.code + "_LOW"]);
                        decimal highVal = Convert.ToDecimal(dbHelper.reader[t.code + "_HIGH"]);
                        decimal actualVal = t.valNum;

                        sbHTML.AppendFormat("<TD>{0}</TD><TD>{1}</TD><TD>{2}</TD>", lowVal, highVal, InsideOrOut(lowVal, highVal, actualVal, t.code));

                    }
                }

                sbHTML.Append("</tr>");

            }

            if (reveal > 1)
            {
                sbHTML.Append("<TR class='clsDiceGameRow1'>");
                sbHTML.Append("<td nowrap>inside<br/>outside<br/>%inside</td>");
                //footer row
                foreach (Tuple t in tups)
                {
                    colSummary cs = gColSummaries[t.code];
                    sbHTML.AppendFormat("<td colspan='3' align='right'>{0}<br/>{1}<br/>{2}", cs.totalIn, cs.totalOut, Math.Round( cs.percentIn,2));
                }
                sbHTML.Append("</TR>");
            }

            sbHTML.Append("</table>");



            if (reveal > 1)
            {
                //summary table is included
                sbHTML.Append(@"<table id='tblRaiffaControllerSummary' class='clsGrid'
                            cellspacing='0' cellpadding='3' border='1'>");

                sbHTML.Append("<tr  class='clsGridHeader'>");
                sbHTML.Append("<td>&nbsp;</td>");
              
                sbHTML.Append("<td colspan='2' align='center'>");
                sbHTML.Append("Actual");
                sbHTML.Append("</td>");

                sbHTML.Append("<td colspan='2' align='center'>");
                sbHTML.Append("Expected");
                sbHTML.Append("</td>");

                sbHTML.Append("</tr>");

                sbHTML.Append("<tr>");
                sbHTML.Append("<td>&nbsp;</td>");
                sbHTML.Append("<td  align='center'>in</td>");
                sbHTML.Append("<td  align='center'>out</td>");
                sbHTML.Append("<td  align='center'>in</td>");
                sbHTML.Append("<td  align='center'>out</td>");
                sbHTML.Append("</tr>");

                sbHTML.Append("<tr  class='clsDiceGameRow1'>");
                sbHTML.Append("<td>Total</td>");


                int totalIn = getTotalIn();
                int totalOut = getTotalOut();
                double totalSum = ((double)(totalIn + totalOut));

                double totalInP = ((double)totalIn / totalSum) * 100;
                double totalOutP = ((double)totalOut / totalSum) * 100;

                double totalExpectedIn = totalSum * 0.8;
                double totalExpectedOut = totalSum * 0.2;


                sbHTML.AppendFormat("<td  align='center'>{0}</td>", totalIn );
                sbHTML.AppendFormat("<td  align='center'>{0}</td>", totalOut);
                sbHTML.AppendFormat("<td  align='center'>{0}</td>", totalExpectedIn );
                sbHTML.AppendFormat("<td  align='center'>{0}</td>", totalExpectedOut);
                sbHTML.Append("</tr>");

                sbHTML.Append("<tr  class='clsDiceGameRow1'>");
                sbHTML.Append("<td>%</td>");
                sbHTML.AppendFormat("<td  align='center'>{0}</td>", totalInP);
                sbHTML.AppendFormat("<td  align='center'>{0}</td>", totalOutP);
                sbHTML.Append("<td  align='center'>80</td>");
                sbHTML.Append("<td  align='center'>20</td>");
                sbHTML.Append("</tr>");

                sbHTML.Append("</table>");


            }



            dbHelper.cleanup();
            dbHelper = null;
            return sbHTML.ToString();

        }


        private int getTotalIn()
        {
            int t = 0;
            foreach (KeyValuePair<string,colSummary> kvp in gColSummaries)
            {
                t = t + kvp.Value.totalIn;
            }
            return t;
        }

        private int getTotalOut()
        {
            int t = 0;
            foreach (KeyValuePair<string, colSummary> kvp in gColSummaries)
            {
                t = t + kvp.Value.totalOut;
            }
            return t;
        }


        private string InsideOrOut(decimal lowVal, decimal highVal, decimal actualVal, string key)
        {
            string ret = string.Empty;


            if (gColSummaries.ContainsKey(key) == false) 
                gColSummaries.Add(key, new colSummary());

            if (actualVal < lowVal || actualVal > highVal)
            {
                ret = "outside";
                gColSummaries[key].totalOut = gColSummaries[key].totalOut + 1;
            }
            else
            {
                ret = "inside";
                gColSummaries[key].totalIn = gColSummaries[key].totalIn + 1;
            }

            return ret;
        }
         

       
    }

    public class colSummary
    {
        public string key { get; set; }
        public int totalIn { get; set; }
        public int totalOut { get; set; }
        public double percentIn
        {
            get
            {
                double percentIn = 0.0;
                try
                {
                    percentIn = totalIn / ( Convert.ToDouble(totalIn) + Convert.ToDouble(totalOut) );

                }
                catch (Exception exp)
                {
                }
                return percentIn;

            }

        }

    }

}
