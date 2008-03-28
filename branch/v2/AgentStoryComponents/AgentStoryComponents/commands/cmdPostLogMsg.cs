using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdPostLogMessage : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            // update page information
            string msg = MacroUtils.getParameterString("msg", macro);


            //$TODO: LOG EASIER
            utils ute = new utils();
            //string msg = ute.decode64(msg);
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog64(msg);
            sl = null;

            MacroEnvelope me = new MacroEnvelope();



            Macro m = new Macro("ReloadLog", 1);
            m.addParameter("msg","logged " + msg);
           // m.addParameter("severity", "1");

            me.addMacro(m);

            return me;
        }
    }
}
