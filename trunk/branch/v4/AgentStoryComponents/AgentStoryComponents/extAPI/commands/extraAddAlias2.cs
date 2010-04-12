using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraAddAlias2 : ICommand
    {

        public MacroEnvelope execute(Macro macro)
        {
            
            string alias64 = MacroUtils.getParameterString("alias64", macro);
            string alias = TheUtils.ute.decode64(alias64);

            string gameCode = MacroUtils.getParameterString("gameCode", macro);
            string tableName = gameCode + "GameData";
            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);

            int storyID = MacroUtils.getParameterInt("storyID", macro);


            //validation
            //if (TheUtils.ute.tableExists(config.conn, dataTable, false) == false)
            //    throw new Exception(string.Format("no table called {0} exists", dataTable));
            var now = DateTime.Now;
            var day = now.Day;
            var month = now.Month;
            var year = now.Year;
            string dateStamp = now.ToString();

            
            string sql = string.Format("INSERT INTO {0} ( tx_id,alias  ,[lastEditedDay],[lastEditedMonth],[lastEditedYear],[lastEditedWhen]) values ('{1}','{2}',{3},{4},{5},'{6}')",
                                       tableName, tx_id, alias64, day, month, year, dateStamp);

            OleDbHelper dbHelper = TheUtils.ute.getDBcmd(config.conn);

            string msg = string.Empty;

            try
            {
                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();
                 msg = string.Format("welcome {0} ", alias);
            }
            catch (Exception insertError)
            {

                sql = string.Format("UPDATE {0} SET alias = '{1}' WHERE tx_id = '{2}'", tableName,alias64, tx_id);
                dbHelper.cmd.CommandText = sql;
                int numRows = dbHelper.cmd.ExecuteNonQuery();
                if (numRows != 1) throw new Exception(string.Format("Error updating {0}", sql));
                msg = string.Format("welcome back {0} ", alias);
            }

          
            msg = TheUtils.ute.encode64(msg);

            MacroEnvelope me = new MacroEnvelope();

            Macro proc = new Macro("RefreshController", 2);              //refresh data table broadcast
            // update model ...
            proc.addParameter("gameCode", gameCode);
            proc.addParameter("by", macro.RunningMe.ID + "");

            me.addMacro(proc);

            MacroUtils.LogStoryTx(me, storyID, macro);              //broadcast message ...



            Macro registerAlias = new Macro("RegisterAlias", 2);    //unicast alias registration ...
            registerAlias.addParameter("alias64", alias64);
            //registerAlias.addParameter("id", dgd.id);
            registerAlias.addParameter("msg64", msg);
            me.addMacro(registerAlias);

            return me;

        }

       
    }
}
