using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraAddAlias : ICommand
    {


        public MacroEnvelope execute(Macro macro)
        {
            
            string alias64 = MacroUtils.getParameterString("alias64", macro);
            string alias = TheUtils.ute.decode64(alias64);

            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);

            int storyID = MacroUtils.getParameterInt("storyID", macro);


            DiceGameData dgd = null;
            dgd = new DiceGameData(config.conn);

            string msg = null;

            if (dgd.existsTxRecord(tx_id) == -1)
            {
                
                dgd.tx_id = tx_id;
                dgd.alias = alias64;
                dgd.Save();

                msg = string.Format("Alias {0} added successfully to datarow {1}", alias, dgd.id);
            }
            else
            {
                //exists
                dgd.loadFromDB(tx_id);
                dgd.ResetDiceGameData();
                
                dgd.Save();
                msg = string.Format("Welcome back, {0}, we have deleted your old game data.  Enjoy the game.", TheUtils.ute.decode64(dgd.alias));
             }

            
            
            msg = TheUtils.ute.encode64(msg);
            MacroEnvelope me = new MacroEnvelope();


          

            Macro proc = new Macro("RefreshStrategyTable", 1);
            proc.addParameter("by", macro.RunningMe.ID + "");
            me.addMacro(proc);

            MacroUtils.LogStoryTx(me, storyID, macro);          //broadcast


            Macro registerAlias = new Macro("RegisterAlias", 2);
            registerAlias.addParameter("alias64", alias64);
            //registerAlias.addParameter("id", dgd.id);
            registerAlias.addParameter("msg64", msg);
            me.addMacro(registerAlias);


            return me;

        }

       
    }
}
