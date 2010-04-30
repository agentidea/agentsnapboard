
//object representing simple (iPhone inspired)
//page navigation panel

function simplePageNavigatorPanel( aoStoryController )
{

    var _StoryController = aoStoryController;

    var selToc = null;
   
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

    this.changeStory = function(ev) {
       
        location.href = "./StoryEditor4.aspx?StoryID=" + selToc.value + "&toolBR=BASIC";

    }

    this.init = function(oPage, bReadOnly) {
        TheUte().removeChildren(_divPageEditControls);

        var PageNavValues = new Array();
        var grdPageEditControlsVals = new Array();
        var cmdAddNewButton = TheUte().getButton("cmdAddElem", "Note", "add new note", null, "clsButtonAction2LGE");
        var cmdAddNewPage = TheUte().getButton("cmdAddElem", "Page", "add new page", null, "clsButtonAction2LGE");

        cmdAddNewButton.onclick = function() {
            if (storyView.getBusy() == 1) return;

            storyView.dropElemIn("", storyView.getOffsetCoord(), storyView.getTmpGUID(), "UNSAVED new element");

            //allow only one to be added at any one time?
            storyView.setBusy(1);
        }

        cmdAddNewButton.ondblclick = function(ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;
        }

        grdPageEditControlsVals.push(cmdAddNewButton);

        cmdAddNewPage.onclick = function() {
            storyView.pageNavPanel.page_new();
        }

        cmdAddNewPage.ondblclick = function(ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;
        }

        grdPageEditControlsVals.push(cmdAddNewPage);

        selToc = storyView.StoryController.getTocItems("selToc", this.changeStory, storyView.StoryController.CurrentStory.ID, "clsPageNavPulldownLGE");
        PageNavValues.push(selToc);


        PageNavValues.push(TheUte().getSpacer(5, 1));

        // add names to pulldown - set sel index for curr page.
        var aPageNames = storyView.StoryController.getPageNameArray();
        var selPulldown = TheUte().getSelect3("selPageNavList", aPageNames, storyView.StoryController.pagNavChanged, storyView.StoryController.getCurrentPageCursor(), "clsPageNavPulldownLGE");
        PageNavValues.push(selPulldown);

        PageNavValues.push(TheUte().getSpacer(3, 1));
        PageNavValues.push(TheUte().getButton("cmdPreviousPage", "^", "go to previous page", this.page_previous, "clsButtonLGE"));
        PageNavValues.push(TheUte().getButton("cmdNextPage", "v", "go to next page", this.page_next, "clsButtonLGE"));




        var grdPageEditControls = newGrid2("grdPageEditControls", 1, 5, grdPageEditControlsVals);
        grdPageEditControls.init(grdPageEditControls);
        this.divPageEditControls.appendChild(grdPageEditControls.gridTable);

        if (bReadOnly)
            this.divPageEditControls.style.display = "none";




        PageNavValues.push(TheUte().getSpacer(10, 1));
        PageNavValues.push(this.divPageEditControls);
        PageNavValues.push(TheUte().getSpacer(5, 1));

        PageNavValues.push(TheUte().getButton("cmdLogin", "usr", "login to a personal account on this system", this.goToLogin, "clsButtonLGE"));

        var cmdShowAdvancedOptions = TheUte().getButton("cmdAdvancedOptions", "a", "show advanced options", null, "clsButtonLGE");
        cmdShowAdvancedOptions.onclick = function() {
            location.href = "./StoryEditor4.aspx?StoryID=" + storyView.StoryController.CurrentStory.ID + "&toolBR=ALL";
        }



        cmdShowAdvancedOptions.ondblclick = function(ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;
        }


        var cmdTupleMx = TheUte().getButton("cmdAdvancedOptions", "t", "manage story tuples", null, "clsButtonAction2LGE");
        cmdTupleMx.onclick = function() {
            location.href = "./storyTupleManagement.aspx?StoryID=" + storyView.StoryController.CurrentStory.ID;
        }

        if (bReadOnly == false) {
            PageNavValues.push(cmdTupleMx);
            PageNavValues.push(cmdShowAdvancedOptions);
        }

        PageNavValues.push(TheUte().getSpacer(20, 1));
        PageNavValues.push(TheUte().getButton("cmdHome", "toc", "table of contents", this.home, "clsButtonAction2LGE"));


        var PageNavGrid = newGrid2("PageNavGrid", 1, PageNavValues.length, PageNavValues, 0);
        PageNavGrid.init(PageNavGrid);

        TheUte().removeChildren(_PageNavPanel);

        _PageNavPanel.appendChild(PageNavGrid.gridTable);

    }

    this.page_previous = function ()
    {
        _StoryController.pagePrevious();
    }
    
    this.page_next = function ()
    {
        _StoryController.pageNext();
    }  
    
     this.page_new = function ()
    {
        var pageName = window.prompt("What is the new Page Name","");
        
    
        if(pageName == null)
        {
            return;
        }
        else
        if(pageName.trim() == "" )
        {
            alert("Please provide a page name");
            return;
        }      
        
        var xy = "800 600"; //window.prompt("enter page pixel dimensions eg 800x600 ","800 600");
       // if(xy ==null)
       //     xy = "800 600";
       // stage no longer used, could be useful for portal mask though.
        
        var dimsArray = xy.split(' ');
        
        var x = dimsArray[0]*1;
        var y = dimsArray[1]*1;
        
        pageName = TheUte().encode64( TheUte().filterText( pageName ) );

        try
        {
            //upadate server ( callback will update model  + view )
            var macroCreateNewPage = newMacro("AddNewPage");
            addParam( macroCreateNewPage,"pageName",pageName);
            addParam( macroCreateNewPage,"gridCols",y);
            addParam( macroCreateNewPage,"gridRows",x);
            addParam( macroCreateNewPage,"gridLayers",0);
            addParam( macroCreateNewPage,"StoryID",_StoryController.CurrentStory.ID );
            addParam( macroCreateNewPage,"StoryOpenedBy",_StoryController.CurrentStory.StoryOpenedBy );
            processRequest( macroCreateNewPage ); 
       }
       catch(e)
       {
            alert("page add error " + e.description);
       }
    }
  
   
}
















