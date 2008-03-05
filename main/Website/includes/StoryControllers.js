/*

    AgentIdea - Story story controller
    Copyright AgentIdea 2007
    
    please note this is private and a copyrighted original work by Grant Steinfeld.
    
    
    
1. Registration Number:    TXu-959-906  
Title:    AgentIdea application studio for IE : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    text of computer program: Grant Steinfeld , 1967- & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
2. Registration Number:    TXu-960-165  
Title:    AgentIdea application studio for Java : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    program text: Grant Steinfeld , 1967-, & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
3. Registration Number:    TXu-961-904  
Title:    AgentIdea application studio for IIS : version 1.0 / authors, Grant Steinfeld, Oren Kredo. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    cAgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Special Codes:   1/C 

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

*/
function newStoryController(aStory,aCurrUser)
{
    var sc = new StoryController(aCurrUser,aStory);
    sc.setCurrentPageCursor(  0 );
    return sc;
}

function StoryController(aCurrUser,aStory)
{
    
    var _observers = new Array();
    this.observers = _observers;
    
    var _currStory = aStory;
    this.CurrentStory = _currStory;
    
    var _currUser = aCurrUser;
    this.CurrentUser = _currUser;
    
    var _currPageCursor = 0;
    
    this.getCurrentPageCursor = function ()
    {
        return _currPageCursor;
    }
    this.setCurrentPageCursor = function ( rhs )
    {
        _currPageCursor = rhs;
    }
    
    this.getPageNameArray = function ()
    {
    
       // alert("this story has " + _currStory.PageCount + " pages");
       
       var pageArray = new Array();
       
       var i = 0;
       for(;i< _currStory.PageCount;i++)
       {
            var s = "var oPage = _currStory.Pages.page_" + i + ";";
            eval(s);
            pageArray.push( TheUte().padNum(i+1,2) + " :: " + TheUte().decode64(oPage.Name));
       }
       
       
       return pageArray;
    
    }
    
    this.pagNavChanged = function(ev)
    {
        storyView.StoryController.gotoPage(this.selectedIndex);
    }
    
    this.IncrementPageCursor = function ()
    {
        _currPageCursor++;
    }
    
    this.DecrementPageCursor = function ()
    {
        _currPageCursor--;
    }
    
    this.CurrentPage = function()
    {
        var p = null;
        var s = "p = _currStory.Pages.page_" + _currPageCursor + ";";
        eval(s); 
        return p;
    }

    this.pageNext = function()
    {
        var totalStoryPages = _currStory.PageCount;
        if(_currPageCursor == totalStoryPages -1 )
          _currPageCursor = 0;
        else
          _currPageCursor++;

        this.notify("PageNav");
    }
    
    this.pagePrevious = function()
    {
        var totalStoryPages = _currStory.PageCount;
        if(_currPageCursor == 0 )
          _currPageCursor = totalStoryPages-1;
        else
          _currPageCursor--;

        this.notify("PageNav");
    }
    
    
    this.pageLast = function()
    {
        var totalStoryPages = _currStory.PageCount;
        _currPageCursor = totalStoryPages - 1;
        
        this.notify("PageNav");
    }
    
    this.gotoPage = function( pageIndex )
    {
        _currPageCursor = pageIndex;
        this.notify("PageNav");
        
    }
    
    this.pageFirst = function()
    {
        _currPageCursor = 0;
        this.notify("PageNav");
    }
    
    this.AddPage = StoryAddPage;
    this.GetPage = StoryGetPage;

    this.AddPageElement = AddPageElement;
    this.AddPageElementMap = AddPageElementMap;
    this.AddPageElementMap2 = AddPageElementMap2;
    this.FindPageElementMapByGUID = FindPageElementMapByGUID;
    
    this.FindPageElement = FindPageElement;
    this.FindPageElementMapByCoord = FindPageElementMapByCoord;   
    this.FindPageElementByCoord = FindPageElementByCoord; 
    
    this.addObserver = function (observer)
    {
        _observers.push( observer );
    }
    
    this.notify = function(change)
    {
        var lenObs = _observers.length;
        if( lenObs > 0)
        {
            var i = 0;
            
            for(;i<lenObs; i++)
            {
                _observers[i].update(change);
            }
        }
    }
    
    
    
}



function FindPageElement(story,page,key)
{
    //looks in pagelements for items that have a key in the format: 908 ...
    //alert(key);
    var pe = null;
    
    for(var i = 0;i<page.PageElementMapCount;i++)
    {
    
        var s = "pe = story.PageElements.pageElement_" + key + ";";
        eval(s);
        if(pe !=null)
        {
            if(pe.ID == key)
                return pe;
        }   
    }
    
    return null;
}

function FindPageElementMapByCoord(page,x,y)
{
    var s = "var pem = page.PageElementMaps.pageElementMap_" + x + "_" + y + ";";
    eval(s);
    return pem;
}


function FindPageElementMapByGUID(page,GUID)
{
      var numMaps = page.PageElementMapCount;
      var pem = null;

       var i = 0;
       for(;i<numMaps;i++)
       {
         var tmpMapKey = "pageElementMap_" + i;
         var s = "pem = page.PageElementMaps." + tmpMapKey + ";";
         eval(s);
         if(pem.GUID == GUID)
         {
            break;
         }
       }
       
       return pem;
}


function FindPageElementByCoord(story,page,x,y)
{
    var pem = this.FindPageElementMapByCoord(page,x,y);
    
    if(pem != null)
    {
        var key = pem.PageElementID;
        var pe = this.FindPageElement(story,page,key);
        
        return pe;
        
    }
    else
    {
        return null;
    }
    

}

function AddPageElement(story, pageElement)
{
    var s = "story.PageElements.pageElement_" + pageElement.ID + " = pageElement;";
    eval(s);
    story.PageElementCount++;

}

//for the TABLE LAYOUT
function AddPageElementMap(page, pageElementMap)
{
    var s = "page.PageElementMaps.pageElementMap_" + pageElementMap.X + "_" + pageElementMap.Y + " = pageElementMap;";
    eval(s);
    page.PageElementMapCount++;

}

// for the new LAYOUT XYZ
function AddPageElementMap2(page, pageElementMap)
{
    var pemIndex = page.PageElementMapCount;   //index is method used here, instead of XY ( to avoid conflict on the same XY point )
    var s = "page.PageElementMaps.pageElementMap_" + pemIndex + " = pageElementMap;";
    eval(s);
    page.PageElementMapCount++;
    

}

function StoryGetPage( index )
{
    //alert( "getting page index " + index );
    var s = "var oPage = this.CurrentStory.Pages.page_" + index + ";";
    eval(s);
    return oPage;
}
function StoryAddPage( page )
{
    var oNewPage = page;
    var s = "this.CurrentStory.Pages.page_" + this.CurrentStory.PageCount + " = oNewPage;";
    eval(s);
    this.CurrentStory.PageCount++;
    return oNewPage;

}


