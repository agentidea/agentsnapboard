
//object representing simple (iPhone inspired)
//page navigation panel

function simplePageNavigatorPanel( aoStoryController )
{

    var _StoryController = aoStoryController;
   
    var _PageNavPanel = document.createElement("DIV");
    _PageNavPanel.className = "clsPageNameLabelLGE";
    this.container = _PageNavPanel;
    
    var _divPageEditControls = document.createElement("DIV");
    _divPageEditControls.id = "dvPageEditControls";
    _divPageEditControls.style.display = "block";
    this.divPageEditControls = _divPageEditControls;
    
    var _lePageName = null;
    this.lePageName = _lePageName;
    
    this.goToLogin = function ()
    {
        location.href = "./Login.aspx";
    }

    this.home = function ()
    {
        location.href = "./Platform2.aspx";
    }

    this.init = function ( oPage, bReadOnly )
    {
        TheUte().removeChildren( _divPageEditControls );

        var PageNavValues = new Array();
        var grdPageEditControlsVals = new Array();
        var cmdAddNewButton = TheUte().getButton("cmdAddElem","+Note","add new note",null,"clsButtonAction2LGE");
        
        cmdAddNewButton.onclick = function()
        {
            if(storyView.getBusy() == 1) return;
 
            storyView.dropElemIn("", storyView.getOffsetCoord(),storyView.getTmpGUID(),"UNSAVED new element");

            //allow only one to be added at any one time?
            storyView.setBusy(1);
        }
        
        cmdAddNewButton.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
        }
        
        grdPageEditControlsVals.push( cmdAddNewButton );
        
         
         
        var aPageNames = storyView.StoryController.getPageNameArray();
        // add names to pulldown - set sel index for curr page.
        PageNavValues.push( TheUte().getSpacer(15,1) );
        PageNavValues.push( TheUte().getButton("cmdHome","toc","table of contents",this.home,"clsButtonAction2LGE"));
        
        PageNavValues.push( TheUte().getSpacer(5,1) );
        var selPulldown = TheUte().getSelect3("selPageNavList",aPageNames,storyView.StoryController.pagNavChanged,storyView.StoryController.getCurrentPageCursor() ,"clsPageNavPulldownLGE" );
        
        PageNavValues.push( selPulldown );



        
        
         PageNavValues.push( TheUte().getSpacer(3,1) );
         PageNavValues.push( TheUte().getButton("cmdPreviousPage","^","go to previous page",this.page_previous,"clsButtonLGE"));
         PageNavValues.push( TheUte().getButton("cmdNextPage","v","go to next page",this.page_next,"clsButtonLGE"));
         PageNavValues.push( TheUte().getSpacer(20,1) );
       

        
      
        var grdPageEditControls = newGrid2("grdPageEditControls",1,5,grdPageEditControlsVals);
        grdPageEditControls.init( grdPageEditControls );
        this.divPageEditControls.appendChild( grdPageEditControls.gridTable );
        
        if(bReadOnly)
            this.divPageEditControls.style.display = "none";
        
        
        
        
        PageNavValues.push( TheUte().getSpacer(10,1) );
        PageNavValues.push( this.divPageEditControls );
        PageNavValues.push( TheUte().getSpacer(5,1) );
        
        PageNavValues.push( TheUte().getButton("cmdLogin","usr","login to a personal account on this system",this.goToLogin ,"clsButtonLGE"));

        var cmdShowAdvancedOptions = TheUte().getButton("cmdAdvancedOptions","a","show advanced options",null,"clsButtonLGE");
        cmdShowAdvancedOptions.onclick = function ()
        {
            location.href = "./StoryEditor4.aspx?StoryID=" + storyView.StoryController.CurrentStory.ID + "&toolBR=ALL";
            
        }
        
        cmdShowAdvancedOptions.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
        }
        
        if(bReadOnly == false)
            PageNavValues.push( cmdShowAdvancedOptions );
        

        var PageNavGrid  = newGrid2("PageNavGrid",1,PageNavValues.length,PageNavValues,0);
        PageNavGrid.init( PageNavGrid );

        TheUte().removeChildren( _PageNavPanel );

        _PageNavPanel.appendChild( PageNavGrid.gridTable );
        
    }

    this.page_previous = function ()
    {
        _StoryController.pagePrevious();
    }
    
    this.page_next = function ()
    {
        _StoryController.pageNext();
    }   
}
















