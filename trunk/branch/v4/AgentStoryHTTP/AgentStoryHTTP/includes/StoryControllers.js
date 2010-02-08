/*

    AgentIdea - Story story controller
   

*/
function newStoryController(aStory,aCurrUser,aToc)
{
    var sc = new StoryController(aCurrUser, aStory, aToc);
    sc.setCurrentPageCursor(  0 );
    return sc;
}

function StoryController(aCurrUser,aStory,aToc)
{
    
    var _observers = new Array();
    this.observers = _observers;
    
    var _currStory = aStory;
    this.CurrentStory = _currStory;
    
    var _currUser = aCurrUser;
    this.CurrentUser = _currUser;

    var _storyToc = aToc;
    this.StoryToc = _storyToc;
    
    var _currPageCursor = 0;
    this.getCurrentPageCursor = function ()
    {
        return _currPageCursor;
    }
    this.setCurrentPageCursor = function ( rhs )
    {
        _currPageCursor = rhs;
    }

    //added for slide navigation
    var _currPageElementCursor = 0;
    this.getCurrentPECursor = function() { return _currPageElementCursor; }
    this.setCurrentPECursor = function(rhs) { _currPageElementCursor = rhs; }

    var _currPageElementCursorID = -1;
    this.getCurrentPECursorID = function() { return _currPageElementCursorID; }
    this.setCurrentPECursorID = function(rhs) { _currPageElementCursorID = rhs; }


    
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

    //return toc select box
   this.getTocItems = function(id, callback, selSelStory, aClassName) {

       //  alert(_storyToc.count);


       var totalNumberStories = _storyToc.count;
       var i = 1;
       var sel = document.createElement("SELECT");
       sel.id = id;
       sel.className = aClassName;

       if (callback != null) {
           sel.onchange = callback;
          // alert("set callback");
       }

       for (; i < totalNumberStories + 1; i++) {

           eval(" var story = _storyToc.stories.story_" + i + ";");
           var storyTocItemText = TheUte().decode64(story.Title);
           var storyID = story.ID;
           // storyTocItem.title = " by " + story.Author + " viewed " + story.UniqueHits + " times";



           if (document.all) {
               //ie
               var ooption = document.createElement("OPTION");
               sel.options.add(ooption);
               ooption.innerText = storyTocItemText;
               ooption.value = storyID;
               if (selSelStory == storyID) {
                   ooption.selected = true;
               }
           }
           else {
               //netscape
               sel.options[i] = new Option(storyTocItemText, storyID + "");


               if (selSelStory == storyID) {

                   sel.options[i].selected = true;
               }
           }
       }

       return sel;



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

    this.pageNext = function() {
        var totalStoryPages = _currStory.PageCount;
        if (_currPageCursor == totalStoryPages - 1)
        {
        //removed roll around
        //_currPageCursor = 0;
            storyView.log("You have reached the last screen");
            return;
        }
        else
        {
            _currPageCursor++;
        }

        this.notify("PageNav");
    }

    this.pagePrevious = function() {
        var totalStoryPages = _currStory.PageCount;
        if (_currPageCursor == 0) {
            //removed roll around
            // _currPageCursor = totalStoryPages-1;
            storyView.log("You are at the beginning");
            return;
        }
        else {
            _currPageCursor--;
        }

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


