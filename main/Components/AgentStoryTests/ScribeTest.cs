﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using AgentStoryComponents;
namespace AgentStoryTests
{
    /// <summary>
    ///This is a test class for AgentStoryComponents.Scribe and is intended
    ///to contain all AgentStoryComponents.Scribe Unit Tests
    ///</summary>
    [TestClass()]
    public class ScribeTest
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
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CopyStory ()
        ///</summary>
        [TestMethod()]
        public void CopyStoryTest()
        {
            User scribe = new User(config.conn,1);
            Story story = new Story(config.conn, 109,new User(config.conn, "buka@bukanator.com"));

            Scribe target = new Scribe(scribe, story);

            Story expected = null;
            Story actual;

            actual = target.CopyStory();


            Assert.AreEqual(story.Title, actual.Title);
            Assert.AreEqual(story.PageElements.Length, actual.PageElements.Length);
            Assert.AreEqual(story.Pages.Length, actual.Pages.Length);


        }

    }


}
