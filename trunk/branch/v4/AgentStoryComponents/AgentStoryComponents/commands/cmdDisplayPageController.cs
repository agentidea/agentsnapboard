using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgentStoryComponents.core;

namespace AgentStoryComponents.commands
{
    public class cmdDisplayPageController : ICommand
    {


        public MacroEnvelope execute(Macro macro)
        {

            string targetDiv = MacroUtils.getParameterString("targetDiv", macro);
            int storyID = MacroUtils.getParameterInt("storyID", macro);
            Dictionary<string,int> listGates = new Dictionary<string,int>();
            Story currStory = new Story(config.conn, storyID, macro.RunningMe);
            

            int i = 0;
            foreach (Page pg in currStory.Pages)
            {
                listGates.Add(pg.Name, i);
                i++;
            }
            string html = this.getHTML(listGates);

            MacroEnvelope me = new MacroEnvelope();
            Macro proc = new Macro("DisplayDiv", 2);
            proc.addParameter("html64", TheUtils.ute.encode64(html));
            proc.addParameter("targetDiv", targetDiv);

            me.addMacro(proc);
            return me;

        }

        private string getHTML(Dictionary<string, int> listGates)
        {
            System.Text.StringBuilder sbHTML = new StringBuilder();

            sbHTML.Append("<select class='clsGameController' ondblclick='setTakeToPage(this.options[this.selectedIndex].value);' onchange='setTakeToPage(this.options[this.selectedIndex].value);' title='double click to re-send page take to command'>");

           sbHTML.Append("<option value='-1'> -- select page to send users to --</option>");

            string selstring = string.Empty;

            int i = 0;
            foreach (KeyValuePair<string, int> kvp in listGates)
            {
                sbHTML.AppendFormat("<option value='{1}' {2}>{0}</option>", TheUtils.ute.decode64(kvp.Key), kvp.Value,selstring);
                i++;
            }
            sbHTML.Append("</select>");


            return sbHTML.ToString();
        }

       
    }
}
