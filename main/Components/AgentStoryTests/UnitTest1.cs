using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentStoryComponents;
using AgentStoryComponents.core;

namespace AgentStoryTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        public string conn = "Provider=sqloledb;Data Source=127.0.0.1;Initial Catalog=AgentStory;User Id=sa;Password=smartorg;";

        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        
        [TestMethod]
        public void serializeMacro()
        {
            Macro m = new Macro("commandName",1);
            
            m.addParameter("msg","hello world");

            string xml = MacroUtils.serializeMacro(m);

        }

        [TestMethod]
        public void AddMacroToMacros()
        {
            Macro a = new Macro("a", 1);
            a.addParameter("age", "12");

            Macro b = new Macro("b", 1);
            b.addParameter("age", "21");

            Macro c = new Macro("c", 1);
            c.addParameter("age", "59");

            MacroEnvelope me = new MacroEnvelope();
            me.addMacro(a);
            me.addMacro(b);
            me.addMacro(c);

            Assert.AreEqual(3, me.macroCount);

        }

        [TestMethod]
        public void SentInvitation()
        {

            string emailAddress = "buka@bukanator.com";
            User u = new User(conn, emailAddress);

            string origInviteCode = "x0xFFAE";

            if (origInviteCode == u.OrigInviteCode)
            {
                //user state changed
                u.State = (int)UserStates.signed_in;

                //change pwd
                u.Password = "newGoodPwd";

                //change nick
                u.Nick = "nick the knife";

                u.Save();
            }


        }




        [TestMethod]
        public void UserExists()
        {
            string emailAddress = "buka@bukanator.com";

            try
            {
                User u = new User(conn, emailAddress);
            }
            catch (UserDoesNotExistException udnee)
            {
                throw new Exception(udnee.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void UserDoesNotExist()
        {
            string emailAddress = "nosuchuser@bukanator.com";
            User u = new User(conn, emailAddress);
            
        }

        [TestMethod]
        public void UpdateUser()
        {
            string emailAddress = "buka@bukanator.com";
            User u = new User(conn, emailAddress);

            u.Password = "new";
            u.State = (int)UserStates.suspended;

            u.Save();

        }

        [TestMethod]
        public void CreateUser()
        {

            User u = new User(conn);

            u.Name = "Aisha";
            u.UserName = "Aishapup";
            u.Password = "purpleMonkey";
            u.Email = "Aisha22@Bukanator.com";
            u.State = (int) UserStates.added;
            u.OrigInviteCode = "x0xFFAErrrufff";

            int uid = u.Save();

            u.Delete();

            
        }

        [TestMethod]
        public void AddNewEmailMessage()
        {
            User by = new User(conn, 25);


            for (int i = 0; i < 12; i++)
            {


                EmailMsg msg = new EmailMsg(conn, by);

                msg.subject = "test subject " + i;
                msg.to = "g@agentidea.com";
                msg.from = "wonderland@agentidea.com";
                msg.body = "chass must be from a real email address!";

                int msgID = msg.Save();

            }


            PostMan pm = new PostMan(conn);
            pm.SendMessages(EmailStates.draft);

            
            //msg.Delete();




        }

        [TestMethod]
        public void RunMadPostman()
        {
            PostMan pm = new PostMan(conn);
            pm.SendMessages(EmailStates.draft);

        }

        [TestMethod]
        public void RunPostMan()
        {

            User usr = new User(conn,"aishapup@bukanator.com");

            utils ute = new utils();

            EmailMsg msg = new EmailMsg(conn,usr);
            msg.subject = ute.encode64("TestSubject");
            msg.body = ute.encode64("bo d eeeeeeeeeeeeeeeeee");
            msg.ReplyToAddress = "wonderland@agentidea.com";
            msg.from = config.allEmailFrom;
            msg.to = "g@agentidea.com";
            int msgID = msg.Save();


            PostMan pm = new PostMan(conn);
            pm.SendMessage(msgID);


           

        }


        [TestMethod]
        public void AddUserToGroup()
        {
            User by1 = new User(conn,25);
            User by2 = new User(conn, 26);
            User by3 = new User(conn, 30);
            Group g = new Group(conn, by1);
            g.Name = "boraters";
            g.Description = "eagels";
            g.Save();

            g.AddUser(by1);
            g.AddUser(by2);
            g.AddUser(by3);

            List<User> usersInGroup = g.GroupUsers;

            g.RemoveUser(by2);

            List<User> usersInGroup2 = g.GroupUsers;



            g.Delete();

        }

        [TestMethod]
        public void CreateGroup()
        {
            User by = new User(conn, 25);
            Group g = new Group(conn, by);
            g.Name = "testGroup";
            g.Description = "testGroupDesc";
            g.Save();

            int groupID = g.ID;

            Group gLookup = new Group(conn, new System.Guid( g.GUID) );


            Groups groups = new Groups(conn);
            int num = groups.NumberGroups;
            List<Group> gList = groups.GroupList;



            gLookup.Delete();


        }


        [TestMethod]
        public void OpenValidInvitationFromGUID()
        {

            User from = new User(conn, "aishapup@bukanator.com");
            User to = new User(conn, 30);

            Invitation i = new Invitation(conn, from, to);
            i.Title = "welcome to Klub Buka 99";
            i.InvitationText = "hi " + to.UserName + ", please come tell your story down at the Klub";
            i.InviteCode = to.OrigInviteCode;
            i.Save();
            string uuid = i.GUID;
            Invitation iToOpen = new Invitation(conn, uuid);
            string tit = i.Title;

            Assert.IsNotNull(tit);
            Assert.AreEqual(i.Title, iToOpen.Title);


        }

        [TestMethod]
        [ExpectedException(typeof(InvitationDoesNotExistException))]
        public void OpenInValidInvitationFromGUID()
        {
            string uuid = "9cf1c24f-d065-4471-960c-ba08efc0da30";
            Invitation i = new Invitation(conn, uuid);
            

        }

        [TestMethod]
        public void ReadUsers()
        {
            Users us = new Users(conn);
            List<User> users = us.UserList;
        }

        [TestMethod]
        public void CreateGuid()
        {
            AgentStoryComponents.core.utils ute = new AgentStoryComponents.core.utils();
            string lsGuid = ute.getGUID().ToString();
        }

        [TestMethod]
        public void CreateInvitation()
        {
            User from = new User(conn, "aishapup@bukanator.com");
            User to = new User(conn, 30);

            Invitation i = new Invitation(conn,from,to);
            i.Title = "welcome to Klub Buka 99";
            i.InvitationText = "hi "+ to.UserName +", please come tell your story down at the Klub";
            i.InviteCode = to.OrigInviteCode;
            i.Save();

        }


        [TestMethod]
        public void AddPage()
        {
            User by = new User(conn,"buka@bukanator.com");
           
            Story s = new Story(conn, by);
            s.Title = "Bukanator is dead?";
            int storyID = s.Save();

            Page p = new Page(conn,by);
            p.Name = "apples";
            p.Code = "app";
            p.Seq = 1;
            p.GridX = 5;
            p.GridY = 4;

            p.Save();

            int pageID = p.ID;

            Page p2 = new Page(conn, by);
            p2.Name = "apples2";
            p2.Code = "app2";
            p2.Seq = 2;
            p2.GridX = 52;
            p2.GridY = 42;

            p2.Save();

            int pageID2 = p2.ID;

            s.AddPage(p);
            s.AddPage(p2);

            s.Delete();

        }

        [TestMethod]
        public void CreateNewPageElement()
        {
            User by = new User(conn, "buka@bukanator.com");

            Story s = new Story(conn, by);
            s.Title = "Bukanator is dead ver 2?";
            int storyID = s.Save();

            PageElement pe = new PageElement(conn, by);
            pe.Value = "<b> lonely girl </b>";
            pe.TypeID = 1;
            pe.Tags = " oat meal fish cat rat poison";
            pe.Code = "cde";

            pe.Save();

            PageElement pe2 = new PageElement(conn, by);
            pe2.Value = "<b> lonely girl 2</b>";
            pe2.TypeID = 2;
            pe2.Tags = " oat2 meal2 fish2 cat2 rat2 poison2";
            pe2.Code = "cde2";

            pe2.Save();
            s.AddPageElement(pe);
            s.AddPageElement(pe2);

            s.Delete();
        }

        [TestMethod]
        public void CreateNewPageElementAndMap()
        {
            User by = new User(conn, "buka@bukanator.com");

            Story s = new Story(conn, by);
            s.Title = "Bukanator is dead ver 2?";
            int storyID = s.Save();

            Page p = new Page(conn, by);
            p.Name = "apples";
            p.Code = "app";
            p.Seq = 1;
            p.GridX = 5;
            p.GridY = 4;

            p.Save();

            s.AddPage(p);

            PageElement pe = new PageElement(conn, by);
            pe.Value = "<b> lonely girl </b>";
            pe.TypeID = 1;
            pe.Tags = " oat meal fish cat rat poison";
            pe.Code = "cde";

            pe.Save();

            PageElement pe2 = new PageElement(conn, by);
            pe2.Value = "<b> lonely girl 2</b>";
            pe2.TypeID = 2;
            pe2.Tags = " oat2 meal2 fish2 cat2 rat2 poison2";
            pe2.Code = "cde2";

            pe2.Save();
            s.AddPageElement(pe);
            s.AddPageElement(pe2);


            //add map
            PageElementMap pem = new PageElementMap(conn);
            pem.PageElementID = pe.ID;
            pem.GridX = 2;
            pem.GridY = 3;
            pem.Save();

            PageElementMap pem2 = new PageElementMap(conn);
            pem2.PageElementID = pe2.ID;
            pem2.GridX = 1;
            pem2.GridY = 2;
            pem2.Save();

            p.AddPageElementMap(pem,true);
            p.AddPageElementMap(pem2,true);

            s.Delete();
        }

        [TestMethod]
        public void CreateStory()
        {
            User by = new User(conn, "buka@bukanator.com");
            Story s = new Story(conn, by);
            s.Title = "Bukanator is dead?";
            int storyID = s.Save();

            s.Delete();

        }

        [TestMethod]
        public void LoadStory()
        {
            User by = new User(conn, "buka@bukanator.com");
            Story s = new Story(conn, by);
            s.Title = "Borys Bukanator is dead?";
            int storyID = s.Save();

            Story loadS = new Story(conn, storyID,by);

            Assert.AreEqual(s.Title, loadS.Title);

            s.Delete();

        }

        [TestMethod]
        public void LoadExistingStory()
        {

            Story s = new Story(conn, 83, new User(conn, "buka@bukanator.com"));
          //  string storyJSONexpected = "{}";
            string storyJSONactual = s.GetStoryJSON(s);
            
        }

        [TestMethod]
        public void TestLogger()
        {
            StoryLog sl = new StoryLog(conn);
            sl.TruncateLog();
            sl.AddToLog(" moi message is this ");
            sl.AddToLog(" moi second message is this ");

            List<StoryLogMessage> slm = sl.StoryLogList;

            int numLogMessagesActual = slm.Count;
            int numLogMessagesExpected = 2;

            Assert.AreEqual(numLogMessagesExpected, numLogMessagesActual);

            sl.TruncateLog();



            sl = null;


        }

        [TestMethod]
        public void GetStories()
        {
            Stories stories = new Stories(conn);

            Assert.IsNotNull(stories);

        }

        [TestMethod]
        public void CreateStoryAndElements()
        {

            User by = new User(conn, "buka@bukanator.com");
            Story s = new Story(conn, by);
            s.Title = "Bukanator is dead 2";

           /* 
            
            * 
            StoryElement page = new StoryElement(StoryType.page);
            StoryElement paragraph = new StoryElement(StoryType.paragraph);

            paragraph.text = "the brown fox jumped over the sleeping black cat";
            page.AddElement(1,1,paragraph);  // page is made up of a grid, so this places the paragraph in position A1 ( ala Excel )
            s.AddElement(1, 1, page); //story is made up of chapters?  page 1 chapter 1?
            
            
            */

            int storyID = s.Save();  //should save story elements?  OR page.Save();  and paragraph.Save();

            s.Delete();

        }


        [TestMethod]
        public void removeAddUserW()
        {
            User u = new User(conn,25);
            Story s = new Story(conn,u);
            s.Title = "blah";
            s.Save();
            s.AddUserViewer(u);
            s.AddUserEditor(u);
            s.RemoveUserEditor(u);
            s.RemoveUserViewer(u);
            s.Delete();
        }
        [TestMethod]
        public void removeAddGroupW()
        {
            User u = new User(conn, 25);
            Story s = new Story(conn, u);
            Group g = new Group(conn, u);
            g.Name = "fluffers";
            g.Save();
            g.AddUser(u);

            s.Title = "blah";
            s.Save();

            s.AddGroupEditor(g);
            s.AddGroupViewer(g);

            s.RemoveGroupEditor(g);
            s.RemoveGroupViewer(g);



            s.Delete();
        }



    }
}
