using AgentStoryComponents.core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgentStoryComponents;

namespace AgentStoryTests
{
    
    
    /// <summary>
    ///This is a test class for TupleTest and is intended
    ///to contain all TupleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TupleTest
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
        public void saveTestDECIMAL()
        {
            string aSqlConn = config.sqlConn;

            Tuple target = new Tuple(aSqlConn); // TODO: Initialize to an appropriate value

            target.name = "test";
            target.description = "test description";
            target.code = "TST";
            target.units = "kilos";
            target.valNum = System.Convert.ToDecimal(45.67);





            target.save();
         
        }

        [TestMethod()]
        public void saveTestSTRING()
        {
            string aSqlConn = config.sqlConn;

            Tuple target = new Tuple(aSqlConn); // TODO: Initialize to an appropriate value

            target.name = "favorite color";
            target.description = null;
            target.code = "FC";
            target.units = "rgb";
            target.val = "#ff99ff";





            target.save();

        }
    }
}
