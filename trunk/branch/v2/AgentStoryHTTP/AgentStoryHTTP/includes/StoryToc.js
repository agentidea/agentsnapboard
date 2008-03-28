function StoryToc()
{
    var _stories = null;
    this.stories = _stories;
    
    var _container = null;
    this.container = _container;
    
    var _operatorID = 0;
    this.operatorID = _operatorID;
    
    this.init = StoryTocInit;
    this.edit_story = edit_story;
    this.edit_story_new = edit_story_new;
    this.delete_story = delete_story;
    this.prop_story = prop_story;
    this.notify_story = notify_story;
    this.getStoryActionButtons = getStoryActionButtons;
}

function StoryTocInit(storiesJSON,operatorID)
{
    //alert(storiesJSON);
    this.operatorID = operatorID;
    eval(" this.stories = " + storiesJSON + ";");
    //var canEdit = this.stories.canEdit *1;
    var totalNumberOfStories = this.stories.count;
    var values = new Array();
    for(var i = 1;i<totalNumberOfStories+1;i++)
    {
       var sToEval = "var lStory = this.stories.stories.story_" + i + ";";
       //alert("about to eval " + sToEval);
       eval(sToEval);
         
       
       values[i] = GetStoryTocLine( lStory);
        
    }
    

    var oGrid2 = newGrid2("storiesMainGrid",totalNumberOfStories+1,1,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
    
    
}


function GetStoryTocLine(story)
{
    var sName = TheUte().decode64( story.Title );
    
    var typeStory = story.TypeStory;
    
    
    
    var storyTitleDiv = document.createElement("div");
    storyTitleDiv.className = "clsStoryTitlePlatform";
    var storyStatsDiv = document.createElement("div");
    storyStatsDiv.className = "clsStoryStatsPlatform";
    
    var url = document.createElement("A");
    if(typeStory == 0)
    {
        url.href = "story.aspx?StoryID=" + story.ID;
    }
    else
    {
       url.href = "StoryEditor4.aspx?StoryID=" + story.ID;
    }
    
    var urlText = document.createTextNode( sName );
    
    url.appendChild(urlText);
    
    var storyAuthorDiv = document.createElement("div");
    //storyAuthorDiv.className = "clsAuthorLabel";
    storyAuthorDiv.title = "send Story Owner " + story.Author + " a message";
    
    var storyByText = document.createTextNode( "added by " + story.Author + "" );
    
    var linkAuthor = document.createElement("A");
    linkAuthor.appendChild( storyByText );
    linkAuthor.href = "SendEmail.aspx?idTo=" + story.AuthorID;
    
    var storyAddedText = document.createTextNode( " " + story.Added + "" );  
    var hits = story.UniqueHits;
    if( hits == -1 ) 
    {
        hits = 0;
    }
    var storyViewedUniquelyTimes = document.createTextNode( " viewed " + hits + " times "  );
    var spanSpacer = document.createElement("div");
    spanSpacer.style.width = "6px";
    
    storyAuthorDiv.appendChild( linkAuthor );
    storyAuthorDiv.appendChild( storyAddedText );
    storyAuthorDiv.appendChild( spanSpacer );
    storyAuthorDiv.appendChild( storyViewedUniquelyTimes );
    if( story.LastEditedBy != null)
    {
        var storyLastEditedDiv = document.createElement("DIV");
        //storyLastEditedDiv.className = "clsAuthorLabel";
        
        var storyLastEdited = document.createTextNode( "Last edit by " + story.LastEditedBy + " - " + story.LastEditedWhen + "");
        
        storyLastEditedDiv.appendChild( storyLastEdited );
        storyAuthorDiv.appendChild( storyLastEditedDiv );
    }
    
    
    storyTitleDiv.appendChild( url );
    storyStatsDiv.appendChild( storyAuthorDiv );
    
    var values = new Array();
    var values2 = new Array();
    
    if(story.CanView == 1)
    {
        values2[0] = storyTitleDiv;
        values2[1] = storyStatsDiv;
        
        
         var grd = newGrid2("jjj",2,1,values2);
         grd.init( grd );


        
        values.push( grd.gridTable); 
    }
    
    
    if(story.CanEdit == 1)
    {
        values.push( this.getStoryActionButtons(story) );
    }
    
    var oGrid2 = newGrid2("storiesMainGrid",1,2,values,1);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;
}

function getStoryActionButtons(story)
{
    var values = new Array();
   
   var editButton = TheUte().getButton("cmdEditStory_"+story.ID ,"edit ","edit story ( grid layout )" + story.ID ,this.edit_story,"clsButtonAction");
    var propButton = TheUte().getButton("cmdProperties_"+story.ID ,"sharing ","edit access control for story id:" + story.ID ,this.prop_story,"clsButtonAction");
    var notifyButton = TheUte().getButton("cmdNotify_"+story.ID ,"notify","Alert story viewers that this story has been added or changed." ,this.notify_story,"clsButtonAction");
    var delButton = TheUte().getButton("cmdDeleteStory_"+story.ID ,"delete","mark story (" + story.ID + ") as deleted.  Recoverable via sys admin only",this.delete_story,"clsButtonAction");
    if(story.TypeStory == 0)
        values.push(editButton);
        

    values.push(propButton);
    values.push(notifyButton);
    values.push(delButton);
    
    var oGrid2 = newGrid2("storyActionbuttons",1,5,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;
    

}


function delete_story()
{
    
    var bits = this.id.split('_');
    var id =  bits[1];
    var res = confirm("are you sure you want to remove this story (" + id + ")" );
    
    if(res == true)
    {
       //mark story as deleted
       var macroCreateNewPage = newMacro("MarkStoryState");
       addParam( macroCreateNewPage,"StoryID",id);
       addParam( macroCreateNewPage,"StoryState",5);
       addParam( macroCreateNewPage,"OperatorID",oStoryToc.operatorID);
       
      //alert( serializeMacroForRequest( macroCreateNewPage) );
       
       processRequest( macroCreateNewPage );
    }
    else
    {
        alert("story " + id + " was not deleted");
    }
    
}
function edit_story()
{
    
    var bits = this.id.split('_');
    var url = "./StoryEditor.aspx?StoryID=" + bits[1];
    window.location.href = url;
    
}
function edit_story_new()
{
    
    var bits = this.id.split('_');
    var url = "./StoryEditor4.aspx?StoryID=" + bits[1];
    
    window.location.href = url;
    
}
function prop_story()
{
    
    var bits = this.id.split('_');
    var url = "./ShareStory.aspx?StoryID=" + bits[1];
    window.location.href = url;
    
}

function notify_story()
{

    alert("coming soon!! \r\n\r\n Story Notifications, will be sent to the viewers of this story.  \r\n Notifications will be sent as per user profile preference settings. \r\n\r\n  None Immediately Daily Weekly \r\n\r\n  \r\n Notifications can include \r\n\r\n an HTML or Text view of the whole story, or just a page or just a page element\r\n\r\n How the notification looks \r\n\ that is up to the notifier");

}
