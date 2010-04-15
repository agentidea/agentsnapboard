using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraStrategicFitSummary : ICommand
    {
     
       private string gTX_ID = null;
       private int gStoryID = -1;


        public MacroEnvelope execute(Macro macro)
        {
            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            string gameCode = MacroUtils.getParameterString("gameCode", macro);
            string tableName = gameCode + "GameData";
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);
            this.gTX_ID = tx_id;
            gStoryID = MacroUtils.getParameterInt("storyID", macro);

            MacroEnvelope me = new MacroEnvelope();
            string sql = string.Format("SELECT * from {0} WHERE tx_id ='{1}'",tableName,tx_id);
            string pipedSummary = this.getPipedSummary(sql);

            Macro p = new Macro("RenderStrategicFitSummary", 2);
            p.addParameter("pipedSummary", pipedSummary);
            p.addParameter("targetDiv", targetDiv);

            me.addMacro(p);
            return me;
        }

        public string getPipedSummary(string sql)
        {
            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;
            dbHelper.reader = dbHelper.cmd.ExecuteReader();

            System.Text.StringBuilder sbHTML = new StringBuilder();

            List<Tuple> tups = TupleElements.getStoryTuples(gStoryID, config.sqlConn);

            if (dbHelper.reader.HasRows)
            {
                dbHelper.reader.Read();

                foreach (Tuple t in tups)
                {

                    //check for incomplete data and breakout ...
                    if (dbHelper.reader[t.code] is DBNull)
                    {
                        sbHTML.AppendFormat("{0}|", -1);  //not chosen yet
                        continue;
                    }

                    decimal val = Convert.ToDecimal(dbHelper.reader[t.code]);
                    sbHTML.AppendFormat("{0}|", val);

                }
            }
           

           

            dbHelper.cleanup();
            dbHelper = null;
            return sbHTML.ToString();

        }
    }
}
