    /*

    AgentStory Core
    Copyright AgentIdea 2007
    
    please note this is private and a copyrighted original work by Grant Steinfeld.
    
    
    
1. Registration Number:    TXu-959-906  
Title:    AgentIdea application studio for IE : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    text of computer program: Grant Steinfeld , 1967- & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
2. Registration Number:    TXu-960-165  
Title:    AgentIdea application studio for Java : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    program text: Grant Steinfeld , 1967-, & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
3. Registration Number:    TXu-961-904  
Title:    AgentIdea application studio for IIS : version 1.0 / authors, Grant Steinfeld, Oren Kredo. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    cAgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Special Codes:   1/C 

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

*/
    
    
    
    
    
    
    
    

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Web;

using AgentStoryComponents.commands;
using System.Reflection;

namespace AgentStoryComponents.core
{
    public static class MacroUtils
    {

        public static void LogStoryEvent(string eventMessage64, Story story, Macro m, int priority )
        {
            //log to StoryChangeLog table 
            StoryChangeEvent sce = new StoryChangeEvent(config.conn, m.RunningMe, eventMessage64, story, priority,true);
        }

        public static void LogStoryTx(MacroEnvelope me, int StoryID,Macro m )
        {
            User by = m.RunningMe;
            string _UserCurrentTxID = m.UserCurrentTxID;

            foreach (Macro tmpM in me.Macros)
            {
                tmpM.UserCurrentTxID = m.UserCurrentTxID;
            }

            if (me.macroCount > 0)
            {
                StoryTxLog stl = new StoryTxLog(config.conn, by, me, StoryID);
                stl.Save();
            }
        }

        public static MacroEnvelope processRequest(AgentStoryComponents.User sessionUser, Macro m)
        {
            /*
           // AssemblyName an = new AssemblyName("AgentStoryCMD.dll");
           // an.CodeBase = @"C:\Documents and Settings\Administrator\My Documents\Visual Studio 2005\Projects\AgentStory\AgentStoryCMD\bin\Debug\";

            //Assembly a = Assembly.Load("AgentStoryCMD.dll");
            Assembly a =
                Assembly.GetAssembly(typeof(AgentStoryComponents.core.Macro));

            Type mm = a.GetType("cmdAddNewStory");
            object o = Activator.CreateInstance(mm);
            object[] par = new object[] { "kunal" };

            Macro returnMacro = (Macro)mm.InvokeMember("execute", BindingFlags.Default |
BindingFlags.InvokeMethod, null, o, par); 
            */

            ICommand cmd = null;
            if (m.Name == "HeartBeat")
            {
                cmd = new cmdHeartBeat();
            }
            else
            if (m.Name == "AddStrategy")
            {
                cmd = new cmdAddStrategy();
            }
            else
                if (m.Name == "OrderPages")
                {
                    cmd = new cmdOrderPages();
                }
            else
            if (m.Name == "UpdatePageElementXYZ")
            {
                cmd = new cmdUpdatePageElementXYZ();
            }

            else
            if (m.Name == "LoadMediaItems")
            {
                cmd = new cmdLoadMediaItems();
            }
            else
            if (m.Name == "StoryPostMsg")
            {
                cmd = new cmdStoryPostMsg();
            }

            else
            if (m.Name == "AddNewStory")
            {
                cmd = new cmdAddNewStory();
            }
            else
                if (m.Name == "CreateMail")
            {
                cmd = new cmdCreateMailDraft();
            }
            else
                if (m.Name == "DeleteMessages")
                {
                    cmd = new cmdDeleteEmailMessages();
                }
                else
            if (m.Name == "UserGroupsStoryMx")
            {
                cmd = new cmdUserGroupsStoryMx();
            }
            else
                if (m.Name == "TerminateUser")
            {
                cmd = new cmdTerminateProfile();
            }
            else
                if (m.Name == "ChangeUserState")
                {
                    cmd = new cmdChangeUserState();
                }
                else 
                if (m.Name == "UpdateStoryMeta")
            {
                cmd = new cmdUpdateStoryMeta();
            }

            else
                if (m.Name == "CreateInvitation")
                {
                    cmd = new cmdCreateInvitation();
                }

                else
                if (m.Name == "getStoryPermissionData")
            {
                cmd = new cmdGetStoryPermissionData();
            }
            else
            if (m.Name == "CreateNewPage")  //need for indirection!
            {
                cmd = new cmdAddNewPage();
            }
            else
                if (m.Name == "AddNewGroup")  
            {
                cmd = new cmdAddNewGroup();
            }
            else
                if (m.Name == "GetGroupUsers")  
            {
                cmd = new cmdGetGroupUsers();
            }
            else
                if (m.Name == "AddUserToGroup")  
            {
                cmd = new cmdAddUsersToGroup();
            }
            else
                if (m.Name == "SaveUser")  
            {
                cmd = new cmdSaveUser();
            }
            else
                if (m.Name == "CreateMailAndSend")  
            {
                cmd = new cmdCreateMailAndSend();
            }
            else
                if (m.Name == "SaveEmailDraft")
                {
                    cmd = new cmdSaveEmailDraft();
                }
                else
                if (m.Name == "SaveEmailAndSend")  
            {
                cmd = new cmdSaveEmailAndSend();
            }
             else
                if (m.Name == "SendMessages")  
            {
                cmd = new cmdSendMessages();
            }
            else
                if (m.Name == "RemoveUsersFromGroup")  
            {
                cmd = new cmdRemoveUsersFromGroup();
            }
            else
                if (m.Name == "CloneStory")
                {
                    cmd = new cmdCloneStory();
                }

            else
            if (m.Name == "UpdatePageElement")
            {
                cmd = new cmdUpdatePageElement();
            }
            else
            if (m.Name == "MarkStoryState")
            {
                cmd = new cmdMarkStoryState();
            }
            else
            if (m.Name == "UpdatePage")
            {
                cmd = new cmdUpdatePage();
            }
            else
            if (m.Name == "RemovePage")
            {
                cmd = new cmdRemovePage();
            }
            else
            if (m.Name == "RemovePageElement")
            {
                cmd = new cmdRemovePageElement();
            }
            else
            if (m.Name == "PostLogMessage")
            {
                cmd = new cmdPostLogMessage();
            }

            else
            if (m.Name == "CreateNewPageElementAndMap")
            {
                cmd = new cmdAddNewPageElementAndMap();
            }
            else
            {
                //$TODO: LOG EASIER
                string msg = "no COMMAND found for " + m.Name;
                StoryLog sl = new StoryLog(config.conn);
                sl.AddToLog("CommandDoesNotExistException: " + msg);
                sl = null;

                throw new CommandDoesNotExistException(msg);
            }

            m.addUserRef(sessionUser);
           //m.addTxRef(UserCurrentTxID);
            

            MacroEnvelope me = null;
            try
            {
                me = cmd.execute(m);

                if (m.Name.ToUpper() == "HEARTBEAT")
                {
                    //don't log heartbeats
                }
                else
                {
                    Logger.log("successfully ran command [" + m.Name + "] by user [ "+m.RunningMe.UserName +" ] --->  ["+m.RunningMe.ID+"]["+m.RunningMe.UserGUID+"][" + m.UserCurrentTxID + "]");
                }
            }
            catch (PossibleHackException phe)
            {
                //$TODO: LOG EASIER
                string msg = "Hack? : " + phe.Message;
                Logger.log(msg);
                throw new PossibleHackException(phe.Message);

            }
            catch (Exception ex)
            {
                
                string msg = ex.Message;
                Logger.log( "CommandExectueError: " + msg + " :stack trace: " + ex.StackTrace );

                //throw ex;
                //throw new CommandExecuteException(ex.Message);
                Macro errorMacro = new Macro("DisplayAlert", 1);
                errorMacro.addParameter("msg", TheUtils.ute.encode64("CommandExectueError: " + msg));
                if (me == null) me = new MacroEnvelope();
                me.addMacro(errorMacro);
            }


            return me;
            
        }

        public static Macro deserializeMacro(string asXML)
        {
            Macro m = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Macro));
               
                StringReader sr = new StringReader(asXML);
                m = (Macro)serializer.Deserialize(sr);

            }
            catch (Exception ex)
            {
                throw new Exception("Errors deserializing Macro [ " + ex.Message + " ] raw XML :: " + asXML);
            }

            return m;

        }
            
        public static string serializeMacro(Macro m)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(Macro));

                serializer.Serialize(sw, m);
                serializer = null;
                sw.Close();
                sw = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error serializing Macro " + ex.Message);
            }


            return sb.ToString();
        }

        public static string serializeMacroJSON(Macro m)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.Append("'name':'");
            json.Append(m.Name);
            json.Append("'");
            json.Append(",");
            json.Append("'UserCurrentTxID':");
            json.Append("'");
            json.Append(TheUtils.ute.encode64(m.UserCurrentTxID));
            json.Append("'");
            json.Append(",");

            json.Append("'paramCount':");
            json.Append(m.parameterCount);
            json.Append(",");
            json.Append("'parameters':[");
            foreach (Parameter p in m.Parameters)
            {
                json.Append("{");
                json.Append("'name':");
                json.Append("'");
                json.Append(p.Name);
                json.Append("'");
                json.Append(",");
                json.Append("'value':");
                json.Append("'");
                json.Append(p.Val);
                json.Append("'");
                json.Append("}");
                json.Append(",");
            }
            json.Remove(json.Length - 1, 1); //remove trailing ,
            json.Append("]");
            json.Append("}");


            return json.ToString();
        }

        public static string serializeMacroEnvelopeJSON(MacroEnvelope response)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.Append("'macros':[");
            foreach (Macro m in response.Macros)
            {
                json.Append(serializeMacroJSON(m));
                json.Append(",");
            }
            json.Remove(json.Length - 1, 1); //remove trailing ,
            json.Append("]");
            json.Append("}");
            return json.ToString();
           
        }

        public static string getParameterString(string key, Macro macro)
        {
            string val = null;
            foreach (Parameter p in macro.Parameters)
            {
                if (p.Name == key)
                {
                    val = p.Val;
                    break;
                }
            }
            if (val == null) throw new ParameterNotFoundException("No param found for key " + key);

            return val;
        }

        public static int getParameterInt(string key, Macro macro)
        {

            string sInt = getParameterString(key,macro);
            int ret = -1;
            try
            {
                ret = Convert.ToInt32( sInt );
            }
            catch(Exception ex)
            {
                throw new ParameterNotNumberException(" parameter for key [" + key + "] was not a number, it was [" + sInt + "] ex " + ex.Message);
            }
            return ret;
        }
    }

    [Serializable()]
    public class MacroEnvelope
    {
        public MacroEnvelope() { }
        public Macro[] Macros = null;
        public int macroCount = 0;

       
	

        public void addMacro(Macro m)
        {
            if (Macros == null)
            {
                Macros = new Macro[1];
                Macros[0] = m;
                macroCount++;
            }
            else
            {
                Macro[] tmpMacros = new Macro[macroCount + 1];
                int i = 0;
                for (i = 0; i < macroCount; i++)
                {
                    tmpMacros[i] = Macros[i];
                }
                tmpMacros[i] = m;
                macroCount++;
                Macros = null;
                Macros = tmpMacros;
            }
        }

    }

    [Serializable()]
    public class Macro
    {
        private string _name;
        private User _runningMe;

        private string _UserCurrentTxID ="none";
        public string UserCurrentTxID
        {
            get
            {
                return _UserCurrentTxID;
            }
            set
            {
                _UserCurrentTxID = value;
            }
        }


	

        public Parameter[] Parameters = null;

        [XmlAttribute]
        public int parameterCount = 0;


        public Macro() { }
        public Macro(string name,int numberParams)
        {
            this.Name = name;
            this.Parameters = new Parameter[numberParams];

        }

   

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public User RunningMe
        {
            get { return _runningMe; }
        }

        public void addParameter(string name, string val)
        {
            Parameters[parameterCount] = new Parameter(name, val);
            parameterCount++;
        }


        internal void addUserRef(User sessionUser)
        {
            _runningMe = sessionUser;
        }



        //internal void addTxRef(string aUserCurrentTxID)
        //{
        //    _UserCurrentTxID = aUserCurrentTxID;
        //}
    }

    [Serializable()]
    public class Parameter
    {

        private string _name;
        private string _val;

        public Parameter() { }

        public Parameter(string name, string val)
        {
            this.Name = name;
            this.Val = val;
        }

        public string Val
        {
            get { return _val; }
            set { _val = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }

}
