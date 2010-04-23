using System;
namespace AgentStoryComponents
{
    public static class config
    {
        //   LOCAL - DEV

        public static string db = "AgentStoryEvolution";
        public static string dbUser = "AgentStorydbo";
        public static string dbPwd = "jy1met2";
        public static string dbIP = "127.0.0.1";
        public static string protocol = "http";
        public static string host = "localhost:8181";
        public static string app = "";  //virtual dir ...
        public static int startStoryID = 6;
        public static int startStoryPage = 0;
        public static string aspNetEmailLicensePath = @"C:\Users\Admin2\Documents\Visual Studio 2008\Projects\GameStory\AgentStoryEvolution\AgentStoryHTTP\AgentStoryHTTP\bin\aspNetEmail.xml.lic";
        public static int helpStoryID = 69;
        
        //  PRODUCTION

        //public static string db = "AgentStoryEvolution";
        //public static string dbUser = "AgentStorydbo";
        //public static string dbPwd = "jy1met2";
        //public static string dbIP = "127.0.0.1";
        //public static string protocol = "http";
        //public static string host = "hosting.smartorg.com";
        //public static string app = "GameStory";
        //public static int startStoryID = 6;
        //public static int startStoryPage = 0;
        //public static string aspNetEmailLicensePath = @"C:\Inetpub\wwwroot\hosting.smartorg.com\GameStory\bin\aspNetEmail.xml.lic";
        //public static int helpStoryID = 69;

        public static string startEditor = "./screens/SlideNavigator.aspx";
        public static string extraClassID = "AgentStoryComponents.extAPI.commands.";        //plugin - extensibility class loader class ID

        public static string startStoryToolBR = "BASIC";
        public static string defaultStoryToolBR = "BASIC";              //platform
        public static string storyToolbarStartMode = "BASIC";           //NONE NAV MIN ALL NOCHAT
       

        public static string includeMasterURL = "./includes/YUI";

        public static bool editorsElementsExclusive = false;  // true - story editors can only edit their own elements OR false - any editor can edit any elements in story
        public static bool ownerOfStoryCanModerate = true;



        public static string logoText = "SmartOrg";
        public static string Orginization = "SmartOrg";
        public static string storyUnavailible = "story unavailable";

        public static string defaultLoginMessage = "Login ";

        public static bool bRequireVerificationForUserReg = false;
        public static bool bDebug = false;

       // public static bool allowMulipleWebmasterAliases = true;

        public static string auditMode = "FULL"; // FULL / NONE

        public static int numPEperUser = 15;     // MAX number page elements returned in library call / lib page
        public static int changed = 1;
        

       
        public static string YouTubeDeveloperID = "BQOPXj9u7UE";
        public static string defaultPassword = "pwd";
        
        public static string smtpServer = "mail.agentidea.com";
        public static string smtpUser = "mail-daemon@agentidea.com";
        public static string smtpPwd = "jy1met2";
        public static string allEmailFrom = "mail-daemon@agentidea.com";

        public static string HomePageTitle = "Welcome to " + logoText;
        public static string webMasterEmail = "g@agentidea.com";
        public static string welcomeTo = "welcome";
        public static string publicUserEmail = "joepublic@bukanator.com";
        public static int publicUserID = 4;

        /// <summary>
        ///  oledb connection string
        /// </summary>
        public static string conn
        {
            get
            {
                return string.Format("Provider=sqloledb;Data Source={3};Initial Catalog={0};User Id={1};Password={2};", db, dbUser, dbPwd, dbIP);
            }
        }

        /// <summary>
        /// sqlclient connection string
        /// </summary>
        public static string sqlConn
        {
            get
            {
                return string.Format("server={3};database={0};UID={1};PWD={2};", db, dbUser, dbPwd, dbIP);
            }
        }
        
        public static string anonEmailReplyTo = "anon-relay@agentidea.com";
        
        
        public static string AnonEmailBodyFooter= @"

The sender of this message has indicated that their email address be kept private.

";

        public static string HelpEmailBodyFooter = @"
Problems with this email?  Visit http://www.agentidea.com/EmailHelp";


        public static string GeneralEmailBodyFooter = @"

__________________________________________
Share Stories at http://www.agentidea.com 
";


        public static string defaultPEtext= @"<div style='width:400px;'>
<b>This is an example page element</b>
<br>
<br>

to EDIT:
<ul>
   
   <li>click the 'edit' button, which will allow you to edit the source code / HTML</li>
   <li>to add a new page element, double click anywhere on a page</li>
</ul>
</div>";


        public static string TOS = @" TOS removed!";


    }

}



