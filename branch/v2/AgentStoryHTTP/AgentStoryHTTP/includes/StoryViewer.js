function StoryViewer()
{

    var _storyController = null;
    this.StoryController = _storyController;
    
    var _container = null;
    this.container = _container;
    
    this.page_next = page_next;
    this.page_previous = page_previous;
    
    var _divPageAttach = null;
    this.divPageAttach = _divPageAttach;
    
    var _divPageInfoAttach = null;
    this.divPageInforAttach = _divPageInfoAttach;
    
    this.init = initCurrentStory;
    this.getSimplePageNavigator = getSimplePageNavigator;
    this.getPageGrid = getPageGrid;
    this.getStoryHeader = getStoryHeader;
    
    this.displayPageChange = displayPageChange;
    this.displayPageInfo = displayPageInfo;
    
    


}


function initCurrentStory(storyController,canEdit)
{
    this.StoryController = storyController;
    
    if( storyController.CurrentStory.TypeStory != 0 )
    {
        //alert("incorrect story type for this editor");
        location.href = "./StoryEditor4.aspx?StoryID=" + storyController.CurrentStory.ID;
        return;
    }
    
    var sTitle = TheUte().decode64( this.StoryController.CurrentStory.Title );
    var sDesc = TheUte().decode64( this.StoryController.CurrentStory.Description );
    // alert("You are about to view : " + ) );
     
    var values = new Array();
    
    values[0] = this.getStoryHeader(sTitle,sDesc,canEdit);
    
    var currPage = oStoryViewer.StoryController.GetPage( oStoryViewer.StoryController.getCurrentPageCursor() );
    if(currPage!=null)
    {
        values[1] = this.getSimplePageNavigator();
        
        this.divPageAttach = document.createElement("div");
        this.divPageAttach.id = "divPageAttach";
        this.divPageAttach.className = "clsPageView";
        
        this.displayPageChange();
        
        values[2] = this.divPageAttach;
        
    }
    else
    {
        values[1] = document.createTextNode(" there are no pages yet for this story ");
    }
    
    var oGrid2 = newGrid2("storyViewerMainContainer",3,1,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
 
}


function displayPageChange()
{
    TheUte().removeChildren( oStoryViewer.divPageAttach );
    oStoryViewer.divPageAttach.appendChild(  oStoryViewer.getPageGrid() );
}

function getStoryHeader(sTitle,sDesc)
{
    var divTitle = document.createElement("div");
    divTitle.className = "clsStoryTitle";
    divTitle.appendChild( document.createTextNode(sTitle ) );
    var values = new Array();
    values[0] =  divTitle;
    if(sDesc != null && sDesc != "")
    {
        var divDesc = document.createElement("div");
        divDesc.className = "clsStoryDescription";
        divDesc.appendChild( document.createTextNode( sDesc ) );
        values[1] =  divDesc;
    }
    
    //alert( this.StoryController.CurrentStory.CanEdit );
    if(this.StoryController.CurrentStory.CanEdit == 1)
    {
        var editLinkDiv = document.createElement("div");
        editLinkDiv.className = "clsSmallLink";
        var editLink = document.createElement("a");
        editLink.href = "StoryEditor.aspx?StoryID=" + this.StoryController.CurrentStory.ID;
        var editLinkText = document.createTextNode("( edit )");
        editLink.appendChild(editLinkText);
        editLinkDiv.appendChild(editLink);
        values[2] = editLinkDiv;
    }
    
     var oGrid2 = newGrid2("storyHeader",1,3,values);
     oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}

function getPageGrid()
{
    
    var currPage = this.StoryController.GetPage(  oStoryViewer.StoryController.getCurrentPageCursor() );
    var values = new Array();
    
    if(currPage != null)
    {
        var rows = currPage.gridRows *1;
        var cols = currPage.gridCols *1;
        
        var totalNumCells = rows * cols;
        
        var rowCursor = 1;
        var colCursor = 0;
        
        
        for(var i = 0;i<totalNumCells;i++)
        {
            colCursor++;
            
            if(colCursor == cols+1)
            {
                colCursor = 1;
                rowCursor++;
            }
            
            
                
            //TheUte().decode64( currPage.Name )
                
            //values[i] =  document.createTextNode( colCursor + "." + rowCursor );
            
           var pe = oStoryViewer.StoryController.FindPageElementByCoord(oStoryViewer.StoryController.CurrentStory,currPage,colCursor,rowCursor);
   
            if(pe != null)
            {   
                var div = document.createElement("div");
                div.innerHTML = TheUte().decode64( pe.Value );
                
                values[i] =  div;
            }
        }
    }
    else
    {
        values[0] =  document.createTextNode( "no pages yet for this story!" );
    }
    
    var oGrid2 = newGrid2("storyViewerMainGrid",rows,cols,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}

function page_next()
{
    if( oStoryViewer.StoryController.getCurrentPageCursor() == oStoryViewer.StoryController.CurrentStory.PageCount - 1)
    {   //reset
        oStoryViewer.StoryController.setCurrentPageCursor(0);
    }
    else
    {
        oStoryViewer.StoryController.IncrementPageCursor();
    }

   oStoryViewer.displayPageChange();
   oStoryViewer.displayPageInfo();
}

function page_previous()
{
    if( oStoryViewer.StoryController.getCurrentPageCursor() == 0)
    {   //reset
        oStoryViewer.StoryController.setCurrentPageCursor( oStoryViewer.StoryController.CurrentStory.PageCount - 1 );
    }
    else
    {
        oStoryViewer.StoryController.DecrementPageCursor();
    }

   oStoryViewer.displayPageChange();
   oStoryViewer.displayPageInfo();
}


function displayPageInfo()
{
    var infoDiv = document.createElement("div");
    
    var pageIdentifier = "";
    
    var currPage = oStoryViewer.StoryController.GetPage( oStoryViewer.StoryController.getCurrentPageCursor() );
    
    var divPageLabel = document.createElement("div");
    divPageLabel.className = "clsPageLabel";
    
    var pageName = TheUte().decode64( currPage.Name );
    
    divPageLabel.appendChild( document.createTextNode( pageName ) );
    divPageLabel.appendChild( document.createTextNode( " page " + ( oStoryViewer.StoryController.getCurrentPageCursor() + 1) ));
    divPageLabel.appendChild( document.createTextNode( " of " +  oStoryViewer.StoryController.CurrentStory.PageCount  ));
   
    
   // var info = document.createTextNode( "" + pageIdentifier + "  of " + this.StoryController.CurrentStory.PageCount );
    
    //infoDiv.appendChild(info);
    
    TheUte().removeChildren( oStoryViewer.divPageInforAttach );
    oStoryViewer.divPageInforAttach.appendChild(  divPageLabel );
}

function getSimplePageNavigator()
{
    var values = new Array();
    values[0] =  TheUte().getButton("cmdPreviousPage","< previous page","go to previous page",this.page_previous,"clsButton");
    
    this.divPageInforAttach = document.createElement("div");
    
    this.displayPageInfo();
    
    values[1] =  this.divPageInforAttach;
    values[2] =  TheUte().getButton("cmdNextPage"," next page >","go to next page",this.page_next,"clsButton");
    
    var oGrid2 = newGrid2("storyViewerPageNavigator",1,3,values,0);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}




