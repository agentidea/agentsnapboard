/*

    AgentIdea - Story toc login page
 */


function StoryToc(aStories,aAttachPoint,aKlubName,loginCallBack)
{
    var _stories = aStories;
    this.stories = _stories;
    var _attachPoint = aAttachPoint;
    var _klubName = aKlubName;
    var _loginCallBack = loginCallBack;

    this.init = function ()
    {
        if(! _stories.count)
        {
              //  alert("no stories yet for this user");
                return;
        }
        
      
            
        var outerGridValues = new Array();
    
        //hoz toolbar
        var toolbarVals = new Array();
       
        
        //context menu or spacer for now
        var spacer = document.createElement("DIV");
        //spacer.style.height = "500px";
        spacer.style.width = "300px";
         toolbarVals.push( spacer );
        
        
        //login or user information
        var _lw = new LoginWidget(_loginCallBack);
      //  toolbarVals.push( _lw.init( "login" ) );
       
        
         var oToolBar = newGrid2("toolBar", 1 ,2,toolbarVals);
        oToolBar.init( oToolBar );
        
   
        // TOC
  
        var storyTocValues = new Array();
        var totalNumberStories = _stories.count;
        var i = 1;
        
        for ( ; i< totalNumberStories + 1; i++ )
        {
            var storyTocItem = document.createElement("SPAN");
            storyTocItem.className="clsStoryTitle";
            eval(" var story = _stories.stories.story_" + i + ";");
            var storyTocItemText = document.createTextNode( TheUte().decode64( story.Title) );
            var anchorText = document.createElement("A");
            anchorText.href = "./SlideNavigator.aspx?StoryID=" + story.ID;
            storyTocItem.title = " by " + story.Author + " viewed " + story.UniqueHits + " times";
            anchorText.appendChild( storyTocItemText );
            storyTocItem.appendChild( anchorText );
            
             var anchorViewItem = document.createElement("SPAN");
             anchorViewItem.className="clsStoryTitle";
            var anchorViewText2 = document.createElement("A");
            anchorViewText2.href = "./StoryEditor4.aspx?StoryID=" + story.ID + "&toolBR=" + defaultToolBR;
            
             var viewReplayTXT = document.createTextNode(" .");
             anchorViewText2.appendChild(viewReplayTXT);
             
            anchorViewItem.appendChild( anchorViewText2 );
            
            storyTocItem.appendChild( anchorViewItem );
            
            storyTocValues.push(storyTocItem);
             
        } 
        
        var oStoryTocGrid = newGrid2("storyTocGrid", storyTocValues.length ,1,storyTocValues,0);
        oStoryTocGrid.init( oStoryTocGrid );

        outerGridValues.push( oStoryTocGrid.gridTable );
        outerGridValues.push( oToolBar.gridTable );
        
        var oOuterGrid = newGrid2("outerGrid", 1 ,2,outerGridValues);
        oOuterGrid.init( oOuterGrid );
        
        _attachPoint.appendChild( oOuterGrid.gridTable);    
        
        
        
    }
}