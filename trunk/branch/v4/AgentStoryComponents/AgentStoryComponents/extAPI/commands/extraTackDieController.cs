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

       private Dictionary<string, AverageTotaller> gTotals = new Dictionary<string, AverageTotaller>();

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
            



            int prize = TupleElements.getStoryTupleIntValue("prize",storyID, config.sqlConn);
            int prize0 = TupleElements.getStoryTupleIntValue("prize0", storyID, config.sqlConn);
            int prize50 = TupleElements.getStoryTupleIntValue("prize50",storyID, config.sqlConn);
            int prize100 = TupleElements.getStoryTupleIntValue("prize100",storyID, config.sqlConn);
            int furtherInvestment = TupleElements.getStoryTupleIntValue("furtherInvestment",storyID, config.sqlConn);
            int costEntry = TupleElements.getStoryTupleIntValue("costEntry", storyID, config.sqlConn);

            //compound header row 

            //sbHTML.Append("<TR class='clsGridHeader'>");
            //sbHTML.Append("<td>&nbsp;</td>");
            //if (reveal > 1)
            //{
                
            //    sbHTML.Append("<td>&nbsp;</td>");
            //    sbHTML.AppendFormat("<td colspan='{0}'>ASSESSMENT</td>",2);
            //}
            //if (reveal > 3)
            //{
            //    sbHTML.Append("<td>&nbsp;</td>");
            //    sbHTML.AppendFormat("<td colspan='{0}'>VALUE IN USE</td>", 3);
            //}
            //sbHTML.Append("</TR>");


            sbHTML.Append("<TR class='clsGridHeader'>");
            sbHTML.Append("<td>Name</td>");

            if (reveal > 1)
            {
                //2
                sbHTML.Append("<td>Initial UP Probability</td>");
              
            }
            if (reveal > 2)
            {
                //3
                sbHTML.Append("<td>Final UP Probability</td>");

            }

            if (reveal > 3)
            {
                //4
                sbHTML.Append("<td>Expected Value</td>");

            }

            if (reveal > 4)
            {
                //5
                sbHTML.Append("<td>VOI</td>");

            }

            if (reveal > 5)
            {
                //6
                sbHTML.Append("<td>With New Alternative</td>");

            }

            if (reveal > 6)
            {
                
                sbHTML.Append("<td>Your Call</td>");
                sbHTML.Append("<td>Your Tack</td>");
                sbHTML.Append("<td>Correct</td>");

            }
            if (reveal > 7)
            {

                sbHTML.Append("<td>Invest Further</td>");

            }
            if (reveal > 8)
            {
                sbHTML.Append("<td>Commercial result</td>");
                sbHTML.Append("<td>Net</td>");
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


                var initialProbability = procNum(dbHelper.reader, "InitialProbability");
                var finalProbability = procNum(dbHelper.reader, "FinalProbability");

                int ProbUp = finalProbability;
                int ProbDown = 100 - ProbUp;
                var max = System.Math.Max(ProbUp, ProbDown);
                var min = System.Math.Min(ProbUp, ProbDown);

                double dmax = max / 100.00;
                double dmin = min / 100.00;

                var EV = (prize * max) / 100;
                processAverages("ExpectedValue", EV);
                var VOI = prize - EV;
                processAverages("VOI", VOI);

                double alternative = ((prize100 / 3) + (prize50 / 2)) - furtherInvestment; // * (max)) / 100;    //=((100/3+50/2)-10)*E10

                double valBeforeTackCall = (alternative * max) / 100;

               processAverages("ALT", Convert.ToInt32(alternative));
               processAverages("VBTC", Convert.ToInt32(valBeforeTackCall));

                if (reveal > 1)
                {
                    //2
                    sbHTML.AppendFormat("<td align='right'>{0}</td>", initialProbability);

                }
                if (reveal > 2)
                {
                    //3
                    sbHTML.AppendFormat("<td align='right'>{0}</td>", finalProbability);

                }

                if (reveal > 3)
                {
                    //4
                    sbHTML.AppendFormat("<td align='right'>{0}</td>", EV);

                }

                if (reveal > 4)
                {
                    //5
                    sbHTML.AppendFormat("<td align='right'>{0}</td>", VOI);

                }

                if (reveal > 5)
                {
                    //6
                    sbHTML.AppendFormat("<td align='right'>{0}</td>", valBeforeTackCall);


                }
                string outcome = string.Empty;

                if (reveal > 6)
                {

                    if (dbHelper.reader["TackOrient"] is System.DBNull)
                    {
                        sbHTML.AppendFormat("<td align='right'>{0}</td><td align='right'>{1}</td><td align='right'>{2}</td>",
                            "*", "*", "*");
                    }
                    else
                    {
                        var YourCall = (int)dbHelper.reader["YourCall"];
                        var TackOrient = (int)dbHelper.reader["TackOrient"];


                        processAverages("YourCall", YourCall);
                        processAverages("TackOrient", TackOrient);

                       outcome = "wrong";
                        if (YourCall == TackOrient) outcome = "correct";

                        var sYourCall = "up";
                        if(YourCall == 0) sYourCall = "down";

                        var sTackOrient = "up";
                        if(TackOrient == 0)sTackOrient = "down";

                        sbHTML.AppendFormat("<td align='right'>{0}</td><td align='right'>{1}</td><td align='right'>{2}</td>", sYourCall, sTackOrient, outcome);
                    }

                }

                if (reveal > 7)
                {

                    //
                    int commercialResult = 0;
                    int net = 0;
                    int diver = 0;
                    int willInvestFurther = -1;
                    int dieOrient = -1;
                    if (dbHelper.reader["WillInvestFurther"] is DBNull)
                    {

                    }
                    else
                    {
                        willInvestFurther = (int)dbHelper.reader["WillInvestFurther"];
                    }

                    if (dbHelper.reader["DieOrient"] is DBNull)
                    {

                    }
                    else
                    {
                        dieOrient = (int)dbHelper.reader["DieOrient"];
                    }

                    if (outcome.ToUpper() == "WRONG")
                    {
                        commercialResult = 0;
                        diver = 0;

                    }
                    else
                    {
                        if (willInvestFurther == -1)
                        {
                            // throw new Exception("willInvestFurtherStageNotReachedYet");
                        }
                        else
                        {
                            if (willInvestFurther == 1)
                            {
                                diver = furtherInvestment;

                                if (dieOrient == 1)
                                {
                                    commercialResult = prize100;
                                }
                                if (dieOrient == 2)
                                {
                                    commercialResult = prize50;
                                    
                                }
                                if (dieOrient == 3)
                                {
                                    commercialResult = prize0;
                                    
                                }

                                if (dieOrient == -1)
                                {
                                    commercialResult = 0;
                                    
                                }


                            }
                            else
                            {
                                commercialResult = prize;
                                diver = 0;

                            }
                        }

                    }

                    net = commercialResult - costEntry - diver;



                    if (dbHelper.reader["WillInvestFurther"] is System.DBNull)
                    {
                        sbHTML.AppendFormat("<td align='right'>{0}</td>",
                            "*");
                    }
                    else
                    {
                        var sWillInvest = "yes";
                        if (willInvestFurther == 0) sWillInvest = "no";
                        if (willInvestFurther == -1) sWillInvest = "*";

                      
                        sbHTML.AppendFormat("<td align='right'>{0}</td>", sWillInvest);
                    }

               
                   
                    sbHTML.AppendFormat("<td align='right' nowrap>{0}</td><td align='right' nowrap>{1}</td>", commercialResult,net);
                    

                }
                sbHTML.Append("</tr>");

            }

            sbHTML.Append("<tr class='clsDiceGameHeader'>");
            //totals

            //yes

            //correct

            //averages
            if (reveal > 1)
            {
                sbHTML.Append("<td>");
                sbHTML.Append("Average");
                sbHTML.Append("</td>");

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["InitialProbability"].Average); } catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 2)
            {
                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["FinalProbability"].Average); } catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 3)
            {
                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["ExpectedValue"].Average); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }
            if (reveal > 4)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["VOI"].Average); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }
            if (reveal > 5)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["VBTC"].Average); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            sbHTML.Append("</tr>");

            //min
            sbHTML.Append("<tr class='clsDiceGameRow1'>");
            if (reveal > 1)
            {
                sbHTML.Append("<td>");
                sbHTML.Append("Min");
                sbHTML.Append("</td>");

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["InitialProbability"].Min); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 2)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["FinalProbability"].Min); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 3)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["ExpectedValue"].Min); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");

            }

            if (reveal > 4)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["VOI"].Min); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 5)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["VBTC"].Min); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }
            
            sbHTML.Append("</tr>");

            //max
            sbHTML.Append("<tr class='clsDiceGameRow1'>");
            if (reveal > 1)
            {
                sbHTML.Append("<td>");
                sbHTML.Append("Max");
                sbHTML.Append("</td>");

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["InitialProbability"].Max); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 2)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["FinalProbability"].Max); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 3)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["ExpectedValue"].Max); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 4)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["VOI"].Max); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }

            if (reveal > 5)
            {

                sbHTML.Append("<td align='right'>");

                try { sbHTML.Append(gTotals["VBTC"].Max); }
                catch (Exception epp) { sbHTML.Append(" * "); }

                sbHTML.Append("</td>");
            }


            sbHTML.Append("</tr>");           

            sbHTML.Append("</table>");

            dbHelper.cleanup();
            dbHelper = null;
            return sbHTML.ToString();

        }



        private int procNum(System.Data.OleDb.OleDbDataReader reader, string name)
        {

            
            if (reader[name] is DBNull) return 0;

            int valInt = System.Convert.ToInt32(reader[name]);

            if (valInt < 0)
            {
                //outputString = "&nbsp;";
            }
            else
            {
                processAverages(name, valInt);
            }

            return valInt;


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
}
