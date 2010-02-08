using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;


using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryHTTP.webservices
{
    /// <summary>
    /// Summary description for daemon
    /// </summary>
    [WebService(Namespace = "http://www.AgentIdea.com/AgentStory/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, 
    //  using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    
    public class daemon : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string ProcessMacro(string serializedMacro)
        {

            //due to the IIS we can access the user whose session this is
            AgentStoryComponents.User sessionUser = Session["user"] as AgentStoryComponents.User;

            if (sessionUser == null) throw new ServiceTimeoutException("session timed out, please reload page :) ");

            Macro req = MacroUtils.deserializeMacro(serializedMacro);
            MacroEnvelope response = MacroUtils.processRequest(sessionUser, req);

            //pass transaction context back to browser.
            foreach (Macro tmpM in response.Macros)
                tmpM.UserCurrentTxID = req.UserCurrentTxID;


            return MacroUtils.serializeMacroEnvelopeJSON(response);
        }
    }
}
