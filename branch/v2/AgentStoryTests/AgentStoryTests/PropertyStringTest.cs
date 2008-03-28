using AgentStoryComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AgentStoryTests
{
    
    
    /// <summary>
    ///This is a test class for PropertyStringTest and is intended
    ///to contain all PropertyStringTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PropertyStringTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void SerializeTest()
        {
            Property p = new Property();
            PropertyString ps = new PropertyString();
            ps.addProp("name", "grant");
            string actual = ps.getSerialized();
            string expected = @"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTE2Ij8+DQo8UHJvcGVydGllcyB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiB4bWxuczp4c2Q9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hIj4NCiAgPHByb3BBcnJheT4NCiAgICA8UHJvcGVydHk+DQogICAgICA8U3RhdGU+MDwvU3RhdGU+DQogICAgICA8TXlUeXBlPnN0cmluZzwvTXlUeXBlPg0KICAgICAgPFZhbD5ncmFudDwvVmFsPg0KICAgICAgPE5hbWU+bmFtZTwvTmFtZT4NCiAgICA8L1Byb3BlcnR5Pg0KICA8L3Byb3BBcnJheT4NCjwvUHJvcGVydGllcz4=";
            Assert.AreEqual(expected, actual);

            PropertyString ps2 = new PropertyString();
            Properties actual2 = ps2.deserializeProperties(expected);
            PropertyString loadedPS = ps2.loadFromPropArray(actual2);
            Property res = loadedPS.getPropVal("name");
            Assert.AreEqual("grant", res.Val);
        }

        ///</summary>
        [TestMethod()]
        public void addPropTest()
        {
            PropertyString target = new PropertyString(); 
            string name = "Age";
            string val = "Ancient";
            Property expected = new Property();
            expected.Name = "Age";
            expected.Val = "Ancient";

            Property actual;
            
            target.addProp(name, val);

            actual = target.getPropVal("Age");
            
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Val, actual.Val);




        }
    }
}
