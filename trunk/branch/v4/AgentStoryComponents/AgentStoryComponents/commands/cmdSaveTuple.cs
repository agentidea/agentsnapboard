using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdSaveTuple : ICommand 
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();

            int storyID = MacroUtils.getParameterInt("storyID", macro);
            int id = MacroUtils.getParameterInt("id", macro);
            string name = MacroUtils.getParameterString("name", macro);
            string code = MacroUtils.getParameterString("code", macro);
            string units = MacroUtils.getParameterString("units", macro);
            string description = MacroUtils.getParameterString("description64", macro);
            string txtValue = MacroUtils.getParameterString("value64", macro);
            string numValue = MacroUtils.getParameterString("numValue", macro);



            if (name.Trim().Length == 0)
                throw new Exception("Invalid tuple name");
            if (code.Trim().Length == 0)
                throw new Exception("Invalid tuple code");


            Tuple t = null;
            bool newTuple = false;

            if (id == -1)
            {
                //new
                t = new Tuple(config.sqlConn);
                t.id = 0; //new
                newTuple = true;

            }
            else
            {
                //existing
                t = new Tuple(config.sqlConn, id);
            }

            t.name = name;
            t.code = code;
            t.units = units;


            if (description == "AA==")
                t.description = null;
            else
                t.description = description;


            if (txtValue == "AA==")
                t.val = null;
            else
                t.val = txtValue;
          
            if(numValue.Trim().Length != 0)
                t.valNum = Convert.ToDecimal(numValue);

            t.save();

            if (newTuple)
            {
                //associate NEW tuple with story
                Story.associateTuple(config.conn, t.id, storyID);
            }


            string msg = string.Format("saved tuple {0}",t.id);



            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("AlertAndRefresh", 2);
            m.addParameter("msg", ute.encode64(msg));
            m.addParameter("severity", "1");

            me.addMacro(m);
          

          

            return me;
        }
    }
}
