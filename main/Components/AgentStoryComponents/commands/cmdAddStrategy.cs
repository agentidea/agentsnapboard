using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;
using Strategy;

namespace AgentStoryComponents.commands
{
    public class cmdAddStrategy : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();
            string name64 = MacroUtils.getParameterString("name64", macro);
            string sevGUID = MacroUtils.getParameterString("sevGUID", macro);

            Strategy.code.strategyTableManager stm = new Strategy.code.strategyTableManager(config.conn);
            int newStrategyTableID = stm.CreateNewStrategy(name64, macro.RunningMe.ID);

            Strategy.code.strategyTable st = stm.GetStrategy(newStrategyTableID);

            //$TODO: LOG EASIER
             string msg = macro.RunningMe.UserName;
            msg += " added a new strategy table [";
            msg += ute.decode64(name64);
            msg += "] of ID [";
            msg += newStrategyTableID;
            msg += "]";
            msg += " of GUID " + st.GUID;
         
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog64(msg);
            sl = null;

            MacroEnvelope me = new MacroEnvelope();
            Macro m = new Macro("StrategyAdded", 3 );
            m.addParameter("stratID", newStrategyTableID + "");
            m.addParameter("stratGUID", st.GUID );
            m.addParameter("sevGUID", sevGUID);


            me.addMacro(m);

            return me;
        }
    }
}
