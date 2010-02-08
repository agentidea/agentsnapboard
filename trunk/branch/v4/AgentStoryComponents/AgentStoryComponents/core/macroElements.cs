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

        public static void LogStoryEvent(string eventMessage64, Story story, Macro m, int priority)
        {
            //log to StoryChangeLog table 
            
            StoryChangeEvent sce = new StoryChangeEvent(config.conn, m.RunningMe, eventMessage64, story, priority, true);
        }

        public static void LogStoryTx(MacroEnvelope me, int StoryID, Macro m)
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


            string lsClassID = "AgentStoryComponents.commands.cmd" + m.Name;
            ICommand command = null;

            try
            {
                Type type = Type.GetType(lsClassID, true);
                object newInstance = Activator.CreateInstance(type);
                command = (ICommand)newInstance;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load class [" + lsClassID + "] " + ex.Message);
            }


            m.addUserRef(sessionUser);
         


            MacroEnvelope me = null;
            try
            {
                me = command.execute(m);

                if (m.Name.ToUpper() == "HEARTBEAT")
                {
                    //don't log heartbeats
                }
                else
                {
                    Logger.log("successfully ran command [" + m.Name + "] by user [ " + m.RunningMe.UserName + " ] --->  [" + m.RunningMe.ID + "][" + m.RunningMe.UserGUID + "][" + m.UserCurrentTxID + "]");
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
                Logger.log("CommandExectueError: " + msg + " :stack trace: " + ex.StackTrace);

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

            string sInt = getParameterString(key, macro);
            int ret = -1;
            try
            {
                ret = Convert.ToInt32(sInt);
            }
            catch (Exception ex)
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

        private string _UserCurrentTxID = "none";
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
        public Macro(string name, int numberParams)
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
