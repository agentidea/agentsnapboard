using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraRaiffaReveal : ICommand
    {
       private int reveal = -1;
       private int storyID = -1;

       private System.Collections.Generic.Dictionary<string, colSummary> gColSummaries = new Dictionary<string, colSummary>();
       private System.Collections.Generic.Dictionary<string, row> gRows = new Dictionary<string, row>();

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

           


            List<Tuple> tups = TupleElements.getStoryTuples(storyID, config.sqlConn);

            //report rows
            while (dbHelper.reader.Read())
            {
                var _tx_id = (string) dbHelper.reader["tx_id"];

                int _rev = 0;
                    foreach (Tuple t in tups)
                    {
                        _rev++;

                        //check for incomplete data and breakout ...
                        if (dbHelper.reader[t.code + "_LOW"] is DBNull || dbHelper.reader[t.code + "_HIGH"] is DBNull)
                        {
                            continue;
                        }

                        decimal lowVal = Convert.ToDecimal(dbHelper.reader[t.code + "_LOW"]);
                        decimal highVal = Convert.ToDecimal(dbHelper.reader[t.code + "_HIGH"]);
                        decimal actualVal = Math.Round( t.valNum,1);
                        string result = InsideOrOut(lowVal, highVal, actualVal, t.code);

                        if (gTX_ID == _tx_id)
                        {
                            //build user specific row summary
                            row r = new row();
                            r.showReveal = false;
                            r.desc = t.name + " ( " + t.units + " ) ";
                            r.low = lowVal;
                            r.high = highVal;
                            if (_rev <= reveal)
                            {
                                r.answer = actualVal;
                                r.result = result;
                                r.showReveal = true;
                            }

                            gRows.Add(t.code, r);
                        }
                    }
            }

            //associate row totals by reveal
            int _rev2 = 0;
            foreach (Tuple t in tups)
            {
                _rev2++;
                if (_rev2 <= reveal)
                {

                    colSummary cs = gColSummaries[t.code];

                    gRows[t.code].totalIn = cs.totalIn;
                    gRows[t.code].totalOut = cs.totalOut;
                }
            }

            int totalIn = getTotalIn();
            int totalOut = getTotalOut();
            double totalSum = ((double)(totalIn + totalOut));


           double totalInP = Math.Round( ((double)totalIn / totalSum) * 100,2);
           double totalOutP = Math.Round( ((double)totalOut / totalSum) * 100,2);

           double totalExpectedIn = totalSum * 0.8;
           double totalExpectedOut = totalSum * 0.2;




            dbHelper.cleanup();
            dbHelper = null;


            System.Text.StringBuilder sbHTML = new StringBuilder();

            sbHTML.Append("<div class='clsPageNote'>");

            sbHTML.Append(@"<b>How good are your assessments?</b>
                            
                            <br>
                    For each uncertain quantity, the actual answer is given and your range is <br>
                    judged based on whether the actual answer is inside or outside of your range. <br> 
                    In the whole group,the number of Inside and Outside answers are also shown. <br>
                    Since the range is set at the 80% level, we would expect about 80% of the <br>
                    group answers to fall Inside the range.<br> ");


            sbHTML.Append("</div><br><br>");



            sbHTML.Append(@"<table id='tblRaiffaController' class='clsGrid'
                            cellspacing='0' cellpadding='3' border='1'>");
            //header row
            sbHTML.Append("<TR class='clsDiceGameHeader'>");
            sbHTML.Append("<TD align='center'>&nbsp;</TD>");
            sbHTML.Append("<TD align='center' colspan='2' nowrap>YOUR ASSESSMENT</TD>");
            sbHTML.Append("<TD align='center' colspan='2' nowrap>YOUR RESULT</TD>");
            sbHTML.Append("<TD align='center' colspan='2' nowrap>GROUP RESULT</TD>");
            sbHTML.Append("</TR>");

            sbHTML.Append("<TR class='clsDiceGameHeader'>");
            sbHTML.Append("<TD align='center'>Uncertain Quantity</TD>");
            sbHTML.Append("<TD align='center'>Low</TD>");
            sbHTML.Append("<TD align='center'>High</TD>");
            sbHTML.Append("<TD align='center'>Answer</TD>");
            sbHTML.Append("<TD align='center'>Result</TD>");
            sbHTML.Append("<TD align='center'>Inside</TD>");
            sbHTML.Append("<TD align='center'>Outside</TD>");
            sbHTML.Append("</TR>");

            sbHTML.Append("<TR class='clsDiceGameRow2'>");
            sbHTML.Append("<TD align='center'>&nbsp;</TD>");
            sbHTML.Append("<TD align='center' nowrap>10% chance that <br>the actual is <br>below this value</TD>");
            sbHTML.Append("<TD align='center' nowrap>10% chance that <br>the actual is <br>above this value</TD>");
            sbHTML.Append("<TD align='center'>&nbsp;</TD>");
            sbHTML.Append("<TD align='center'>&nbsp;</TD>");
            sbHTML.Append("<TD align='center'>total in group</TD>");
            sbHTML.Append("<TD align='center'>total in group</TD>");
            sbHTML.Append("</TR>");

            //questions row
            foreach (KeyValuePair<string, row> kvp in gRows)
            {
                row r  = kvp.Value;

                sbHTML.Append("<TR class='clsDiceGameRow1'>");
                sbHTML.AppendFormat("<TD nowrap>{0}</TD>", r.desc);
                sbHTML.AppendFormat("<TD>{0}</TD>", r.low);
                sbHTML.AppendFormat("<TD>{0}</TD>", r.high);
                if (r.showReveal)
                {
                    sbHTML.AppendFormat("<TD>{0}</TD>", r.answer);
                    sbHTML.AppendFormat("<TD>{0}</TD>", r.result);
                    sbHTML.AppendFormat("<TD>{0}</TD>", r.totalIn);
                    sbHTML.AppendFormat("<TD>{0}</TD>", r.totalOut);
                }
                else
                {
                    sbHTML.Append("<TD align='center'>&nbsp;</TD>");
                    sbHTML.Append("<TD align='center'>&nbsp;</TD>");
                    sbHTML.Append("<TD align='center'>&nbsp;</TD>");
                    sbHTML.Append("<TD align='center'>&nbsp;</TD>");

                }
                sbHTML.Append("</TR>");
            }

            //footer summary rowS

            sbHTML.Append("<TR class='clsDiceGameRow1'>");
            sbHTML.Append("<TD colspan='5' align='right'>total</TD>");
            sbHTML.AppendFormat("<TD>{0}</TD>", totalIn);
            sbHTML.AppendFormat("<TD>{0}</TD>", totalOut);
            sbHTML.Append("</TR>");

            sbHTML.Append("<TR class='clsDiceGameRow1'>");
            sbHTML.Append("<TD colspan='5' align='right'>%</TD>");
            sbHTML.AppendFormat("<TD>{0}</TD>", totalInP);
            sbHTML.AppendFormat("<TD>{0}</TD>", totalOutP);
            sbHTML.Append("</TR>");

            sbHTML.Append("<TR class='clsDiceGameRow1'>");
            sbHTML.Append("<TD colspan='5' align='right'>expected</TD>");
            sbHTML.AppendFormat("<TD>{0}</TD>", totalExpectedIn);
            sbHTML.AppendFormat("<TD>{0}</TD>", totalExpectedOut);
            sbHTML.Append("</TR>");

            sbHTML.Append("<TR class='clsDiceGameRow1'>");
            sbHTML.Append("<TD colspan='5' align='right'>%</TD>");
            sbHTML.AppendFormat("<TD>{0}</TD>", 80);
            sbHTML.AppendFormat("<TD>{0}</TD>", 20);
            sbHTML.Append("</TR>");


            sbHTML.Append("</table>");


            return sbHTML.ToString();

        }


        private int getTotalIn()
        {
            int t = 0;
            foreach (KeyValuePair<string,row> kvp in gRows)
            {
                t = t + kvp.Value.totalIn;
            }
            return t;
        }

        private int getTotalOut()
        {
            int t = 0;
            foreach (KeyValuePair<string, row> kvp in gRows)
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


    public class row
    {
        public string desc { get; set; }
        public decimal low { get; set; }
        public decimal high { get; set; }
        public decimal answer { get; set; }
        public string result { get; set; }
        public int totalIn { get; set; }
        public int totalOut { get; set; }
        public bool showReveal { get; set; }
    }
}
