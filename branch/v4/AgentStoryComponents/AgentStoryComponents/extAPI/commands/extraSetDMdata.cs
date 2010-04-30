using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents.extAPI;

namespace AgentStoryComponents.extAPI.commands
{
    public class extraSetDMdata : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {


            int sugar = MacroUtils.getParameterInt("sugar", macro);
            int insulinA = MacroUtils.getParameterInt("insulinA", macro);
            int insulinB = MacroUtils.getParameterInt("insulinB", macro);
            int carbs = MacroUtils.getParameterInt("carbs", macro);
            string localTime64 = MacroUtils.getParameterString("localtime64", macro);
            string comment64 = MacroUtils.getParameterString("comment64", macro);
            string comment = TheUtils.ute.decode64(comment64);

            DateTime localTime;
            try
            {
                localTime = Convert.ToDateTime(TheUtils.ute.decode64(localTime64));
            }
            catch (Exception timeExp)
            {

                throw new Exception("time format not recognized " + timeExp.Message);
            }

          
            int meId = macro.RunningMe.ID;


            DMdata dmd = new DMdata(config.conn);
            dmd.OwnerUserId = meId;
            dmd.sugar = sugar;
            dmd.insulinA = insulinA;
            dmd.insulinB = insulinB;
            dmd.comment = comment64;
            dmd.timestamp = localTime;
            dmd.Save();


            int numRows = 0;
            string msg = string.Format("saved {0}", dmd.id);
            msg = TheUtils.ute.encode64(msg);
            
            MacroEnvelope me = new MacroEnvelope();
            Macro proc = new Macro("DisplayAlert", 2);            
            proc.addParameter("msg", msg);
            proc.addParameter("severity", "1");
            me.addMacro(proc);

          
            return me;
        }
    }
}
