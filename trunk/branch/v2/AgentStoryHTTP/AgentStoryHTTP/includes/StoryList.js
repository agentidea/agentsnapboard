function StoryList(aStories,aAttachPoint)
{
    var _stories = aStories;
    this.stories = _stories;
    var _attachPoint = aAttachPoint;
    this.propegate = function()
    {
        alert("propegating " + this.id);
    
    }
    
    this.init = function ()
    {
    
        var outerGridValues = new Array();
    
        //hoz toolbar
        var toolbarVals = new Array();
       
        
        //context menu or spacer for now
        var spacer = document.createElement("DIV");
        //spacer.style.height = "500px";
        spacer.style.width = "300px";
         toolbarVals.push( spacer );
        
        
       
        
         var oToolBar = newGrid2("toolBar", 1 ,2,toolbarVals);
        oToolBar.init( oToolBar );
        
   
        // TOC
       
        
        var storyTocValues = new Array();
        var totalNumberStories = _stories.count;
        var i = 1;
        
        for ( ; i< totalNumberStories + 1; i++ )
        {
            var storyTocItem = document.createElement("DIV");
            storyTocItem.className="clsStoryTitle";
            eval(" var story = _stories.stories.story_" + i + ";");
            var storyTocItemText = document.createTextNode( TheUte().decode64( story.Title) );
            var anchorText = document.createElement("A");
            anchorText.href = "./StoryEditor4.aspx?StoryID=" + story.ID;
            storyTocItem.title = " by " + story.Author + " viewed " + story.UniqueHits + " times";
            anchorText.appendChild( storyTocItemText );
            storyTocItem.appendChild( anchorText );
            storyTocValues.push(storyTocItem);
            var cmdIndx = TheUte().getButton("propegate_" + story.ID," publish index","",oStoryList.propegate,"clsButtonAction");
            storyTocValues.push(cmdIndx);
        } 
        
        var oStoryTocGrid = newGrid2("storyTocGrid", totalNumberStories ,2,storyTocValues);
        oStoryTocGrid.init( oStoryTocGrid );

        outerGridValues.push( oStoryTocGrid.gridTable );
        outerGridValues.push( oToolBar.gridTable );
        
        var oOuterGrid = newGrid2("outerGrid", 1 ,2,outerGridValues);
        oOuterGrid.init( oOuterGrid );
        
        _attachPoint.appendChild( oOuterGrid.gridTable);    
        
        
        
    }
}