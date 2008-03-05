using System;
using System.Collections.Generic;
using System.Text;


namespace AgentStoryComponents
{
    /// <summary>
    /// copies story
    /// </summary>
    public class Scribe : User
    {

        private Story _currentStory;

        public Story CurrentStory
        {
            get { return _currentStory; }
            
        }

        public Scribe(User scribe, Story story):base(config.conn,scribe.ID)
        {
            _currentStory = story;
        }

        public Story CopyStory()
        {

#region permissions check.

            User me = this;
            bool allowClone = false;

            foreach (User storyEditorUser in _currentStory.StoryEditorUsers)
            {
                if (storyEditorUser.ID == me.ID)
                {
                    allowClone = true;
                    break;
                }
            }

            if (allowClone == false)
            {
                foreach (Group group in _currentStory.StoryEditorGroups)
                {
                    foreach (Group userGroup in me.MyGroups)
                    {
                        if (group.ID == userGroup.ID)
                        {
                            allowClone = true;
                            break;
                        }
                    }
                }

            }
#endregion


            if (allowClone == false) throw new UserMayNotClone("you need EDITOR rights on a story to clone it.");

            Story newCopy = new Story(config.conn, (User) this  );
            newCopy.Title = _currentStory.Title;
            newCopy.Description = _currentStory.Description;
            newCopy.TypeStory = _currentStory.TypeStory;

            newCopy.Save();

            //ADD THE SCRIBE as a viewer and editor of his/her CLONED story.
            newCopy.AddUserEditor( this );
            newCopy.AddUserViewer( this );

      

            //copy pages
            foreach (Page page in _currentStory.Pages)
            {
                Page newPage = page.Clone(( User)this ,newCopy ,true);
                newCopy.AddPage(newPage);
            }

            

           

            return newCopy;
        }
    }
}
