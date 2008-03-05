using System;
using System.Collections.Generic;
using System.Text;

using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryCMD
{
    public class cmdAddNewStory : ICommand
    {
        public Macro execute(Macro macro)
        {
            // add new story to db.

            Macro m = new Macro("DisplayAlert", 2);
            m.addParameter("msg", "new story added at " + System.DateTime.Now.ToString());
            m.addParameter("severity", "1");
            return m;
        }
    }
    
}
