using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;


namespace AgentStoryComponents
{

    /*
     
     *  use:  
     *        pageement.addProp("locked","true");
     *        pageement.addProp("private","false");
     * 
     *        story.addProp("hasTxHx","true");
     *        story.addProp("price","2.0";
     *        story.addProp("cost",3.45553);         // ???? numerics as string type?
     *        story.addProp("hasChat",true);
     * 
     * 
     *        page.addProp("background-color","yellow");
     *        page.addProp("style","background-color:#e0e0e0;color:green;");
     * 
     *        page.addProp("bloodsugarsTime","8:03 AM|14:00 PM|5:32 PM |5:32 PM|5:32 PM");
     *        page.addProp("bloodsugarsVals","139|239|300|102|85");
     *   
     * 
     */


    public class PropertyString
    {
        private List<Property> _properties = null;
        public List<Property> props
        {
            get
            {
                if (_properties == null)
                    _properties = new List<Property>();

                return _properties;
            }
        }

        public Property getPropVal(string key)
        {
            List<Property> matches = this.props.FindAll(delegate(Property p) { return p.Name == key; });
            if (matches != null && matches.Count == 1)
            {
                //found, do nothing
            }
            else
            {
                throw new ParameterNotFoundException("No property for key " + key);
            }

            return matches[0];

        }
        public string toJSON()
        {
            return "{}";
        }

        public Property addProp(string name, string val, string myType)
        {
            Property p = new Property();
            p.Name = name;
            p.Val = val;
            p.MyType = myType;
            this.addProp(p);
            return p;

        }
        public Property addProp(string name, string val)
        {
            return this.addProp(name, val, "string");
        }
        public void addProp(Property prop)
        {
            this.props.Add(prop);

        }
        public Property[] toPropArray()
        {
            int numProps = this.props.Count;
            Property[] pa = new Property[numProps];
            int i = 0;

            foreach (Property p in this.props)
            {
                pa[i] = p;
                i++;
            }
            return pa;
        }

        public PropertyString loadFromPropArray(Properties props)
        {
            PropertyString ps = new PropertyString();
            foreach (Property prop in props.propArray)
            {
                ps.addProp(prop);
            }

            return ps;

        }

        /// <summary>
        /// get the xml representation of the properties
        /// </summary>
        /// <returns></returns>
        public string getSerialized()
        {
            Properties paObj = new Properties();
            paObj.propArray = this.toPropArray();

            //serialize 
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringWriter sw = new StringWriter(sb);

            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(Properties));

                serializer.Serialize(sw, paObj);
                serializer = null;
                sw.Close();
                sw = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error serializing Property " + ex.Message);
            }

            return AgentStoryComponents.core.TheUtils.ute.encode64(sb.ToString());
        }

        public Properties deserializeProperties( string properties64 )
        {
            //de-serialize + load
            properties64 = AgentStoryComponents.core.TheUtils.ute.decode64( properties64 );

            Properties ps = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Properties));
                StringReader sr = new StringReader(properties64);
                ps = (Properties)serializer.Deserialize(sr);

            }
            catch (Exception ex)
            {
                throw new Exception("Errors deserializing Properties [ " + ex.Message + " ] raw XML :: " + properties64);
            }

            return ps;
        }
    }

    [Serializable()]
    public class Properties
    {
        public Property[] propArray;
        public Properties()
        {            
        }
    }

    [Serializable()]
    public class Property   
    {
        private string _name;
        private string _val;
        private string _myType;
        private int _state = 0;

        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
	

        public string MyType
        {
            get { return _myType; }
            set { _myType = value; }
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
