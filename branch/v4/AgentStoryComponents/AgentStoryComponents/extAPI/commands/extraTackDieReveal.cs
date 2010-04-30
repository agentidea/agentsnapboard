using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraTackDieReveal : ICommand
    {
       private int reveal = -1;
       private int storyID = -1;
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
            string sql = string.Format("SELECT * from {0} WHERE tx_id ='{1}'",tableName,tx_id);
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
                            cellspacing='0' cellpadding='4' border='0'>");


            int prize = TupleElements.getStoryTupleIntValue("prize", storyID, config.sqlConn);
            int prize0 = TupleElements.getStoryTupleIntValue("prize0", storyID, config.sqlConn);
            int prize50 = TupleElements.getStoryTupleIntValue("prize50", storyID, config.sqlConn);
            int prize100 = TupleElements.getStoryTupleIntValue("prize100", storyID, config.sqlConn);
            int costEntry = TupleElements.getStoryTupleIntValue("costEntry", storyID, config.sqlConn);
            int furtherInvestment = TupleElements.getStoryTupleIntValue("furtherInvestment", storyID, config.sqlConn);

            sbHTML.Append("<TR class='clsGridHeader'>");
            sbHTML.Append("<td>&nbsp;</td>");
            sbHTML.Append("<td>Pin Up</td>");
            sbHTML.Append("<td>Pin Down</td>");
            sbHTML.Append("</TR>");


            //report rows
            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();


                int k = 0;
                int ProbUp = (int)dbHelper.reader["FinalProbability"];
                int ProbDown = 100 - ProbUp;
                double pup = (ProbUp / 100.00);
                double pdown = (ProbDown / 100.00);

                string sOutcome = null;

                var max = System.Math.Max(ProbUp, ProbDown);
                var min = System.Math.Min(ProbUp, ProbDown);

                double dmax = max / 100.00;
                double dmin = min / 100.00;

                var EV = (prize * max) / 100;

                //$100 * 1/3 + $20 * 1/2 + $0 * 1/6 
                double alternative = ((prize100 / 3) + (prize50 / 2)) - furtherInvestment; // * (max)) / 100;    //=((100/3+50/2)-10)*E10

                double valBeforeTackCall = (alternative * max) / 100;

                sbHTML.Append("<tr class='clsDiceGameRow1'>");
                sbHTML.AppendFormat("<td>Your Probability</td><td>{0}%</td><td>{1}%</td>", ProbUp, ProbDown);
                sbHTML.Append("</tr>");

                sbHTML.Append("<tr class='clsDiceGameRow1'>");
                sbHTML.AppendFormat("<td>Chance to call correctly</td><td>&nbsp;</td><td>{0}%</td>", max);
                sbHTML.Append("</tr>");

                sbHTML.Append("<tr class='clsDiceGameRow1'>");
                sbHTML.AppendFormat("<td colspan='3'><pre><B>EV = ${0}</B> = ${1}*{2}  +  ${3}*{4} </pre></td>", EV, prize, dmax, prize0, dmin);
                sbHTML.Append("</tr>");

                //<h3>Probability of Pin Up</h3>

                double valueOfPerfectInformation = (double)prize - (double)EV;
                if (reveal > 1)
                {
                    //VALUE OF INFORMATION

                   

                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td><div class='clsSpaceman'></div>Value with perfect information</td><td  colspan='2'>${0}</td>", prize);
                    sbHTML.Append("</tr>");

                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>Value without information</td><td  colspan='2'><DIV style='border-bottom:single 1 black;'>${0}</DIV></td>", EV);
                    sbHTML.Append("</tr>");

                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>Value of perfect information</td><td colspan='2'>${0}</td>", valueOfPerfectInformation);
                    sbHTML.Append("</tr>");
                    

                }

                if (reveal > 2)
                {
                    //ALTERNATIVE

                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td><div class='clsSpaceman'></div>Value of new alternative, assuming you call tack correctly</td><td>${0}</td><td>&nbsp;</td>", alternative);
                    sbHTML.Append("</tr>");
                    
                    // Value of new alternative, assuming you call tack correctly $43 = $100 * 1/3 + $20 * 1/2 + $0 * 1/6 
                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td colspan='3'><pre> ${0} = ( ${1}*1/3 + ${2}*1/2 + ${3}*1/6 ) - {4} </pre></td>", alternative,prize100,prize50,prize0,furtherInvestment);
                    sbHTML.Append("</tr>");

                    //Value before the tack call $x = final probability * $43 + (1- final probability) * $0 

                    //var k = (prize100 / 3) + (prize50 / 2);
                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td colspan='3'>Value before the tack call <br/> <pre><b>${0}</b> = ${1}*{2} + ${3}*{4}</pre></td>", valBeforeTackCall,alternative,dmax, prize0, dmin); 
                    sbHTML.Append("</tr>");
                
                }

                int call = -1;

                if (reveal > 3)
                {
                    //your call

                    call = (int)dbHelper.reader["YourCall"];
                    var sCall = "";
                    if (call == 1) sCall = "Up";
                    if (call == 0) sCall = "Down";


                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td valign='bottom'><div class='clsSpaceman'></div>Your Call </td><td  valign='bottom'>{0}</td><td>&nbsp;</td>", sCall);
                    sbHTML.Append("</tr>");


                }

                if (reveal > 4)
                {
                    //post dice simulation

                    int orient = (int)dbHelper.reader["TackOrient"];
                    var sOrient = "";
                    if (orient == 1) sOrient = "Up";
                    if (orient == 0) sOrient = "Down";

                    
                    if (orient == call)
                        sOutcome = "correct";
                    else
                        sOutcome = "wrong";
                    

              
                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>The Tack is </td><td>{0}</td><td>&nbsp;</td>", sOrient);
                    sbHTML.Append("</tr>");


                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>Your call was </td><td>{0}</td><td>&nbsp;</td>", sOutcome);
                    sbHTML.Append("</tr>");

            


                }

                if (reveal > 7)
                {
                    //Commercial Outcome
                    int commercialResult = 0;
                    int net = 0;
                    int diver = 0;
                    var sDO = "";
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

                     if (sOutcome == "WRONG")
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
                                     sDO = "first";
                                 }
                                 if (dieOrient == 2)
                                 {
                                     commercialResult = prize50;
                                     sDO = "second";
                                 }
                                 if (dieOrient == 3)
                                 {
                                     commercialResult = prize0;
                                     sDO = "other";
                                 }

                                 if (dieOrient == -1)
                                 {
                                     commercialResult = 0;
                                     sDO = " not thrown yet ";
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

                    

                    var sWIF = "";
                    if (willInvestFurther == -1)
                    {
                        sWIF = "not an option";
                    }
                    if (willInvestFurther == 1)
                    {
                        sWIF = "YES";
                    }
                    if (willInvestFurther == 0)
                    {
                        sWIF = "NO";
                    }


                    
                   

                   

                   
                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td valign='bottom'><div class='clsSpaceman'></div>Do you wish to invest further?</td><td valign='bottom'>{0}</td><td>&nbsp;</td>", sWIF);
                    sbHTML.Append("</tr>");


                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>Market Position</td><td>{0}</td><td>&nbsp;</td>", sDO);
                    sbHTML.Append("</tr>");



                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>You have won</td><td>{0}</td><td>&nbsp;</td>", commercialResult);
                    sbHTML.Append("</tr>");


                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td>For a net winnings of</td><td>{0}</td><td>&nbsp;</td>", net);
                    sbHTML.Append("</tr>");


                    sbHTML.Append("<tr class='clsDiceGameRow1'>");
                    sbHTML.AppendFormat("<td valign='bottom'><div class='clsSpaceman'></div>The Commercial Result is</td><td valign='bottom' > <div class='clsGameTotal'>{0}</div></td><td>&nbsp;</td>", commercialResult);
                    sbHTML.Append("</tr>");

                }


            }
            else
            {
                sbHTML.Append("<tr><td colspan='3'>no data</td>");
                sbHTML.Append("</tr>");     
            }

            sbHTML.Append("<tr class='clsDiceGameRow1'>");
            sbHTML.Append("</tr>");           

            sbHTML.Append("</table>");

            dbHelper.cleanup();
            dbHelper = null;
            return sbHTML.ToString();

        }


    }
}
