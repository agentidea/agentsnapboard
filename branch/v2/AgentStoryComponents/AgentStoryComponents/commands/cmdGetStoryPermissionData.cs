using System;
using System.Collections.Generic;
using System.Text;
using AgentStoryComponents.core;
using AgentStoryComponents;

namespace AgentStoryComponents.commands
{
    public class cmdGetStoryPermissionData : ICommand
    {
        public MacroEnvelope execute(Macro macro)
        {
            string userOrGroup = MacroUtils.getParameterString("userOrGroup", macro);
            string editorOrViewer = MacroUtils.getParameterString("editorOrViewer", macro);
            int StoryID = MacroUtils.getParameterInt("StoryID", macro);

            Story s = new Story(config.conn, StoryID, macro.RunningMe);

            
            int count = 0;
            selWidget sw = null;

            if (userOrGroup.ToLower() == "users" && 
                    editorOrViewer.ToLower() == "editors")
            {
                sw = s.getUserEditors();
                count = s.StoryEditorUsers.Count;
            }
            else
            if (userOrGroup.ToLower() == "users" &&
                    editorOrViewer.ToLower() == "viewers")
            {
                sw = s.getUserViewers();
                count = s.StoryViewerUsers.Count;
            }
            else
            if (userOrGroup.ToLower() == "groups" &&
                    editorOrViewer.ToLower() == "editors")
            {
                sw = s.getGroupEditors();
                count = s.StoryEditorGroups.Count;
            }
            else
            if (userOrGroup.ToLower() == "groups" &&
                    editorOrViewer.ToLower() == "viewers")
            {
                sw = s.getGroupViewers();
                count = s.StoryViewerGroups.Count;
            }



            MacroEnvelope me = new MacroEnvelope();

            Macro refreshView = new Macro("RefreshView", 5);
            refreshView.addParameter("ViewID", "" + userOrGroup + "_" + editorOrViewer);
            refreshView.addParameter("userOrGroup", userOrGroup );

            refreshView.addParameter("ids", sw.ids);
            refreshView.addParameter("names", sw.names);
            refreshView.addParameter("count", "" + count);

            me.addMacro(refreshView);

            return me;
        }
    }
}
