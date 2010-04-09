using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraSetColVal : ICommand
    {


        public MacroEnvelope execute(Macro macro)
        {

            string colName = MacroUtils.getParameterString("colName", macro);
            int colValue = MacroUtils.getParameterInt("colIntVal", macro);

            string tx_id64 = MacroUtils.getParameterString("tx_id64", macro);
            string tx_id = TheUtils.ute.decode64(tx_id64);

            int StoryID = MacroUtils.getParameterInt("StoryID", macro);


            DiceGameData dgd = null;
            dgd = new DiceGameData(config.conn);

            string msg = null;

            if (dgd.existsTxRecord(tx_id) == -1)
            {
                msg ="no record found for tx id ... weird ... ???";
            }
            else
            {
                //exists, so get current datum
                dgd.loadFromDB(tx_id);

                //set single value accordingly
                switch (colName.ToUpper())
                {
                    case "PEARL":
                        dgd.pearl = colValue;
                        break;
                    case "OYSTER":
                        dgd.oyster = colValue;
                        break;
                    case "WHITEELEPHANT":
                        dgd.whiteElephant = colValue;
                        break;
                    case "BREADANDBUTTER":
                        dgd.breadAndButter = colValue;
                        break;
                    case "FUNDED_SUCCESS":
                        dgd.funded_success = colValue;
                        break;
                    case "FUNDED_POINTS":
                        dgd.funded_points = colValue;
                        break;
                    case "UNFUNDED_SUCCESS":
                        dgd.unfunded_success = colValue;
                        break;
                    case "UNFUNDED_POINTS":
                        dgd.unfunded_points = colValue;
                        break;
                    case "BEST5_SUCCESS":
                        dgd.best5_success = colValue;
                        break;
                    case "BEST5_POINTS":
                        dgd.best5_points = colValue;
                        break;

                    default:
                        break;
                }
                
                dgd.Save();
                msg = "saved";
             }

            
            
            msg = TheUtils.ute.encode64(msg);
            MacroEnvelope me = new MacroEnvelope();


            Macro proc = new Macro("RefreshStrategyTable", 1);
            proc.addParameter("by", macro.RunningMe.ID + "");

            me.addMacro(proc);

            MacroUtils.LogStoryTx(me, StoryID, macro);

            return me;
        }

       
    }
}
