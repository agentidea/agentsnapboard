using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents;

namespace AgentStoryComponents.core
{
    public static class Logger
    {

        public static void log(string msg)
        {
            utils ute = new utils();
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;
        }


    }
}
