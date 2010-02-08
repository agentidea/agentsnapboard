using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;
using AgentStoryComponents.extAPI;


namespace AgentStoryComponents.commands
{
    public class cmdLoadMediaItems : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
           
            string UserList = MacroUtils.getParameterString("UserList", macro);
            string ContentProviderName = MacroUtils.getParameterString("ContentProviderName", macro);
            string viewPortID = MacroUtils.getParameterString("viewPortID", macro);
            int width = 300;
            int height = 150;

            IContentProvider provider = null;
            StringBuilder output = new StringBuilder();

            if (ContentProviderName.ToLower() == "youtube")
            {
                provider = new YouTube();
                provider.setDeveloperID(config.YouTubeDeveloperID);

            }
            else
            if (ContentProviderName.ToLower() == "myelements")
            {
                provider = new MyElements();
            }
            else
            if (ContentProviderName.ToLower() == "flickr")
            {
                provider = new MyFlikr();
            }
            else
            {
                throw new Exception(" no provider found for " + ContentProviderName.ToLower());
            }


  


        string[] users = UserList.Split(' ');

        output.Append("{'envelope':[");

        foreach (string usr in users)
        {
            output.Append(provider.getMediaMetaJSON(usr, true, width, height));
            output.Append(",");
        }
        if (users.Length > 0)
            output.Remove(output.Length - 1, 1);
        output.Append("]}");
    


            string finalOutput = TheUtils.ute.encode64(output.ToString() );

            MacroEnvelope me = new MacroEnvelope();

            Macro m = new Macro("LoadMediaItems", 2);
            m.addParameter("mediaItemsJSON64", finalOutput);
            m.addParameter("viewPortID", viewPortID);
            me.addMacro(m);

            return me;
        }
    }
}
