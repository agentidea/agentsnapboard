using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdUserGroupsStoryMx : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            //manage the users and groups relevant to story sharing
            string userOrGroup = MacroUtils.getParameterString("userOrGroup", macro);
            string passedDelims = MacroUtils.getParameterString("delims", macro);
            string passedDelimsOther = MacroUtils.getParameterString("delimsOther", macro);
            string editorOrViewer = MacroUtils.getParameterString("editorOrViewer", macro);
            string direction = MacroUtils.getParameterString("direction", macro);
            
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);

            string operation = null; //remove | add
            string delims = null;

            utils ute = new utils();


            User operatorUser = macro.RunningMe;


            string msg = null;

            //this is a bit slippery when wet
            //geography of view is dicating function!
            if (editorOrViewer.ToLower() == "viewers")
            {
                if (direction.ToLower() == "west")
                {
                    operation = "remove";
                    delims = passedDelimsOther;
                }
                else
                {
                    operation = "add";
                    delims = passedDelims;
                }
            }
            else
            {
                if (direction.ToLower() == "west")
                {
                    operation = "add";
                    delims = passedDelims;
                }
                else
                {
                    operation = "remove";
                    delims = passedDelimsOther;
                }
            }


            //lets get the story
            Story s = new Story(config.conn, StoryID,macro.RunningMe);


            //story access permissions
            if (operatorUser.isRoot())
            {
                Logger.log("root exercising story permission override");
            }
            else
            {

                if (operatorUser.ID != s.by.ID)
                {
                    //not the story owner
                    //allow removal of operator only!

                    //todo: eeek hard to do, need to rething this module todo:

                    Logger.log(" non story owner trying to change story access permissions ");
                    throw new InsufficientPermissionsException("Sorry " + operatorUser.UserName + ", only Story Owner " + s.by.UserName + " can manage the editors and viewers for this story. \r\n\r\n You could send them a message via the members page asking them to add or remove you. ");
                
                }
            }

            if (userOrGroup.ToLower() == "users")
            {
                //get a string of users off the delim
                string[] userIDs = delims.Split('|');
                foreach (string userID in userIDs)
                {
                    if (userID.Trim().Length > 0)
                    {
                        int iUserId = Convert.ToInt32(userID);
                        User u = new User(config.conn, iUserId);

                        if (operation == "add")
                        {
                            msg = "In Story entitled '" + ute.decode64(s.Title) + "' " + editorOrViewer.ToUpper() + " role added to user '" + u.UserName + "'";
                           
                            if (editorOrViewer.ToLower() == "editors")
                                s.AddUserEditor(u);
                            else
                                s.AddUserViewer(u);
                        }
                        else
                        {
                            msg = "In Story entitled '" + ute.decode64(s.Title) + "' " + editorOrViewer.ToUpper() + " role removed from user '" + u.UserName + "'";

                            if (editorOrViewer.ToLower() == "editors")
                                s.RemoveUserEditor(u);
                            else
                                s.RemoveUserViewer(u);
                        }
                    }
                }

            }
            else
            {
                //get a string of groups off the delim
                string[] groupIDs = delims.Split('|');
                foreach (string groupID in groupIDs)
                {
                    if (groupID.Trim().Length > 0)
                    {
                        int iGroupId = Convert.ToInt32(groupID);
                        Group g = new Group(config.conn, iGroupId);

                        if (operation == "add")
                        {
                            msg = "In Story entitled '" + ute.decode64(s.Title) + "' " + editorOrViewer.ToUpper() + " role added to GROUP '" + g.Name + "'";

                            if (editorOrViewer.ToLower() == "editors")
                                s.AddGroupEditor(g);
                            else
                                s.AddGroupViewer(g);
                        }
                        else
                        {
                            msg = "In Story entitled '" + ute.decode64(s.Title) + "' " + editorOrViewer.ToUpper() + " role removed from GROUP '" + g.Name + "'";

                            if (editorOrViewer.ToLower() == "editors")
                                s.RemoveGroupEditor(g);
                            else
                                s.RemoveGroupViewer(g);
                        }
                    }

                }
            }


            MacroEnvelope me = new MacroEnvelope();

            Macro proc = new Macro("RefreshPermissionsView", 3);
            proc.addParameter("ViewID", userOrGroup + "_" + editorOrViewer );
            proc.addParameter("userOrGroup", userOrGroup );
            proc.addParameter("editorOrViewer", editorOrViewer);

            Logger.log(msg);
            MacroUtils.LogStoryEvent(msg, s, macro, 6);
            

            me.addMacro(proc);

            return me;
        }
    }
}
