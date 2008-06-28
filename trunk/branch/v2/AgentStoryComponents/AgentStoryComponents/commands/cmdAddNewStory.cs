using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdAddNewStory : ICommand 
    {
        public MacroEnvelope execute(Macro macro)
        {
            utils ute = new utils();
            // add new story to db.
            string storyTitle = MacroUtils.getParameterString("txtStoryName", macro);
            string storyDescription = MacroUtils.getParameterString("txtStoryDescription", macro);
            // string chatEnabled = MacroUtils.getParameterString("chatEnabled", macro);

            string decodedTitle = ute.decode64(storyTitle);

            if (decodedTitle.Trim().Length == 0)
                throw new Exception("Invalid story title");

            
            int userID = MacroUtils.getParameterInt("UserID", macro);

           

            int layoutToUse = MacroUtils.getParameterInt("nLayoutToUse", macro);
            

            

            User by = new User(config.conn,userID);
            Story s = new Story(config.conn,by);
            s.Title = storyTitle;
           
            if (layoutToUse == 0)
                s.TypeStory = 1;

            s.Description = storyDescription;
            s.Save();


            //ADD THE STORY CREATOR as a viewer and editor of his/her own story.
            s.AddUserEditor(by);
            s.AddUserViewer(by);
            

            //$TODO: LOG EASIER
            
            string storyNameH = ute.decode64( macro.Parameters[0].Val );
            string msg = "new story ( " + storyNameH.ToString() + " ) added by " + by.UserName;
            StoryLog sl = new StoryLog(config.conn);
            sl.AddToLog(msg);
            sl = null;



            MacroEnvelope me = new MacroEnvelope();

            //Macro m = new Macro("DisplayAlert", 2);
            //m.addParameter("msg", ute.encode64( msg ));
            //m.addParameter("severity", "1");

            //me.addMacro(m);

          

            Macro relocateToStoryPage = new Macro("Redirect", 1);

            if (layoutToUse == 0)
            {
                //cartesian style
                int gridZ = 1;

                


                //it was decided that there would be a default page added for cartesian style, page named 'default'
                //800 x 600 px stage ( first page )
                Page defaultPage = new Page(config.conn, by);
                defaultPage.GridX = 800;
                defaultPage.GridY = 800;
                defaultPage.GridZ = gridZ;
                defaultPage.Name = TheUtils.ute.encode64( "( default )");
                defaultPage.Save();

                
                //defaultPage.AddUserEditor(by);
                //defaultPage.AddUserViewer(by);


                //to a defualt page it has been decided that there will be a default TEXT page element representing the page name.
                PageElement pe = new PageElement(config.conn, by);
                pe.TypeID = 1; //text

                
                //pe.AddUserViewer(by);
                //pe.AddUserEditor(by);   



                string defPageTxt = config.defaultPEtext;


                pe.Value = TheUtils.ute.encode64(defPageTxt);
                pe.Save();

                s.AddPageElement(pe);

                PageElementMap pem = new PageElementMap(config.conn);
                pem.PageElementID = pe.ID;
                pem.GridX = 280; //ouch!!!
                pem.GridY = 230;
                pem.GridZ = gridZ;
                pem.Save();


                defaultPage.AddPageElementMap(pem, true);

                s.AddPage(defaultPage);
               // s.Save();

                relocateToStoryPage.addParameter("url", "storyEditor4.aspx?StoryID=" + s.ID);








            }
            else
            {
                //must be old way - tables
                relocateToStoryPage.addParameter("url", "StoryEditor.aspx?StoryID=" + s.ID);

            }

            me.addMacro(relocateToStoryPage);

            return me;
        }
    }
}
