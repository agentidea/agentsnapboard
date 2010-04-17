using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdDeleteStoryTuple : ICommand 
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();

            int storyID = MacroUtils.getParameterInt("storyID", macro);
            int id = MacroUtils.getParameterInt("id", macro);
            
            Tuple t = new Tuple(config.sqlConn, id);
            Story.dissociateStoryTuple(config.conn, t.id, storyID);
            t.delete();

            string msg = string.Format("removed Story Tuple {0}",t.id);

            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("AlertAndRefresh", 2);
            m.addParameter("msg", ute.encode64(msg));
            m.addParameter("severity", "1");

            me.addMacro(m);
          

          

            return me;
        }
    }
}
