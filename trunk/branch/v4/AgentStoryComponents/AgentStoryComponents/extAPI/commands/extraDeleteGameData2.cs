using System;
using System.Collections.Generic;
using System.Collections;

using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraDeleteGameData2 : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
          
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);
            string gameCode = MacroUtils.getParameterString("gameCode", macro);
            string tableName = gameCode + "GameData";
           
            MacroEnvelope me = new MacroEnvelope();
            var msg = string.Empty;
            

            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);

            var sql = "delete from " + tableName;

            dbHelper.cmd.CommandText = sql;
            int numRows = dbHelper.cmd.ExecuteNonQuery();

            dbHelper.cleanup();
            dbHelper = null;

            msg = string.Format("deleted {0} rows from table {1}", numRows, tableName);

            Macro proc = new Macro("DisplayAlert", 2);
            proc.addParameter("msg", TheUtils.ute.encode64( msg));
            proc.addParameter("severity", "1");

            me.addMacro(proc);
            return me;
        }

    }
}
