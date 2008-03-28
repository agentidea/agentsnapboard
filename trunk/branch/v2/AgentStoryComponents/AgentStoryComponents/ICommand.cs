using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;

namespace AgentStoryComponents
{
    public interface ICommand
    {
         MacroEnvelope execute(Macro macro); 
    }
}
