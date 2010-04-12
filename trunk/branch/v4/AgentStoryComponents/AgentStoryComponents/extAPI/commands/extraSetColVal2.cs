using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraSetColVal2 : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {

            string colName = MacroUtils.getParameterString("colName", macro);
            string gameCode = MacroUtils.getParameterString("gameCode", macro);
            string tableName = gameCode + "GameData";
            double colValue = MacroUtils.getParameterDouble("colDoubleValue", macro);
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);


            //validation
            //if(TheUtils.ute.tableExists(config.conn,tblName,false) == false)
            //    throw new Exception (string.Format("no table called {0} exists",tableName));

            string sql = string.Format("UPDATE {0} SET {1} = {2} WHERE tx_id = '{3}'",
                                       tableName, colName, colValue, tx_id);

            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);
            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            string msg = string.Format("updated {0} rows", numRows);
            msg = TheUtils.ute.encode64(msg);
            
            MacroEnvelope me = new MacroEnvelope();
            Macro proc = new Macro("RefreshController", 2);            
            proc.addParameter("gameCode", gameCode);
            proc.addParameter("by", macro.RunningMe.ID + "");
            me.addMacro(proc);

            MacroUtils.LogStoryTx(me, StoryID, macro);              //broadcast message ...

            return me;
        }
    }
}
