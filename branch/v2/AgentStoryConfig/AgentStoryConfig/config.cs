using System;
namespace AgentStoryComponents
{
    public static class config
    {

        //
        //   LOCAL - DEV
        //

        //public static string db = "AgentStoryOLDDEV";
        //public static int startStoryID = 83;
        //public static int startStoryPage = 0;
        //public static string startStoryToolBR = "ALL";
        //public static string defaultStoryToolBR = "BASIC";              //platform
        //public static string storyToolbarStartMode = "BASIC";           //NONE NAV MIN ALL NOCHAT
        //public static string conn = "Provider=sqloledb;Data Source=127.0.0.1;Initial Catalog=" + db + ";User Id=AgentStoryUser;Password=agentidea;";
        //public static string aspNetEmailLicensePath = @"C:\data\dev\AgentStoryHTTP\AgentStoryHTTP\bin\aspNetEmail.xml.lic";
        //public static string host = "localhost:1585";
        //public static string app = "";
        //public static int helpStoryID = 69;

        

        //
        //   WEBHOSTING 4 LIFE - PRODUCTION
        //

        public static string db = "AgentStory";
        public static int startStoryID = 76;
        public static int startStoryPage = 0;
        public static string startStoryToolBR = "NONE";
        public static string defaultStoryToolBR = "BASIC";
        public static string storyToolbarStartMode = "BASIC";    //platform
        public static string aspNetEmailLicensePath = @"c:\hosting\webhost4life\member\agentidea\apps\agentidea\bin\aspnetemail.xml.lic";
        public static string conn = "provider=sqloledb;data source=sql349.mysite4now.com;initial catalog=" + db + ";user id=agentstorydbo;password=jy1met2;";
        public static string host = "www.agentidea.com";
        public static string app = "";
        public static int helpStoryID = 37;

        
        
        public static string includeMasterURL = "./includes/YUI";
        public static string protocol = "http";



        public static string logoText = "AI";
        public static string Orginization = "AgentIdea";
        public static string storyUnavailible = "story unavailable";

        public static string defaultLoginMessage = "To login please enter the following: ";

        public static bool bRequireVerificationForUserReg = true;
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


        public static string TOS = @"
1) Confidentiality during beta testing
AgentIdea is still severly under construction, yet is nevertheless, rich 
in functionality and concept.  These ideas are new and proprietary till
stated otherwise, please note that by accepting these terms, you accept to be
under 'NDA' so please don't steal any IP ( we are watching you! :)

2)  You retain copyright and other intellectual property rights with respect to Content
you create in AgentIdea, to the extent that you have such rights under applicable law. 
However, you must make certain representations and warranties, and provide certain 
license rights, forbearances and indemnification, to AgentIdea and to other 
users of this software.

3) Service Description
AgentIdea provides users with access to a rich collection of resources, 
including various communications tools, forums, shopping services, 
search services, personalized content and branded programming through 
its network of properties which may be accessed through any various medium 
or device now known or hereafter developed (the 'Service'). You also understand
        and agree that the Service may include advertisements and that these
            advertisements are necessary for AgentIdea to provide the Service.
        You also understand and agree that the Service may include certain 
            communications from AgentIdea, such as service announcements, 
                administrative messages and the AgentIdea Newsletter, 
        and that these communications are considered part of AgentIdea 
        membership and you will not be able to opt out of receiving them. 
            Unless explicitly stated otherwise, any new features that augment or
                enhance the current Service, including the release of new AgentIdea 
        properties, shall be subject to the TOS. You understand and agree that the 
            Service is provided 'AS-IS' and that AgentIdea assumes no responsibility 
                for the timeliness, deletion, mis-delivery or failure to store any user

                    communications or personalization settings. You are responsible for 
obtaining access to the Service, and that access may involve third-party fees 
(such as Internet service provider or airtime charges). 
You are responsible for those fees, including those fees associated with the display or
delivery of advertisements. In addition, you must provide and are responsible for all
equipment necessary to access the Service.

Please be aware that AgentIdea has created certain areas on the 
Service that contain adult or mature content. 
You must be at least 18 years of age to access and view such areas.





";


    }

}



