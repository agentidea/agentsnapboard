
//object representing simple (iPhone inspired)
//page navigation panel

function simplePageNavigatorPanel( aoStoryController ,view)
{

    var _StoryController = aoStoryController;
    var _view = view;
    var _readOnly = true;
    
    var selToc = null;

    var _nextButton = null;
    var _prevButton = null;

    var _nextFxnPointer = null;
   
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

        location.href = "./SlideNavigator.aspx?StoryID=" + selToc.value;

    }

    this.hideNext = function() {
        var nxt = document.getElementById("cmdNextPage");
        nxt.style.display = "none";

    }
    this.showNext = function() {
        var nxt = document.getElementById("cmdNextPage");
        nxt.style.display = "block";

    }
    this.addClickNext = function(cb) {

        
        var nxt = document.getElementById("cmdNextPage");
        nxt.onclick = cb;
    }

    this.restoreNext = function(cb) {

        var nxt = document.getElementById("cmdNextPage");
        nxt.onclick = _nextFxnPointer;
    }

    this.init = function(oPage, bReadOnly) {

        TheUte().removeChildren(_divPageEditControls);

        _readOnly = bReadOnly;

        var PageNavValues = new Array();
        var grdPageEditControlsVals = new Array();
        var cmdAddNewButton = TheUte().getButton("cmdAddElem", "+ Slide", "add slide", null, "clsButtonAction2LGE");
        var cmdAddNewPage = TheUte().getButton("cmdAddPage", "+ Group", "add new group", this.page_new, "clsButtonAction2LGE");

        cmdAddNewButton.onclick = function() {
            if (storyView.getBusy() == 1) return;
            storyView.dropElemIn("", storyView.getOffsetCoord(), storyView.getTmpGUID(), "UNSAVED new element");

            //allow only one to be added at any one time?
            storyView.setBusy(1);
        }



        grdPageEditControlsVals.push(cmdAddNewButton);
        grdPageEditControlsVals.push(cmdAddNewPage);



        //add story name.
        var storyTitle = storyView.StoryController.CurrentStory.Title;
        var storyDesc = storyView.StoryController.CurrentStory.Description;
        var txtStoryTitle = document.createTextNode(TheUte().decode64(storyTitle));
        var dvStoryTitle = document.createElement("DIV");
        dvStoryTitle.className = "clsStoryTitle2";
        dvStoryTitle.appendChild(txtStoryTitle);
        PageNavValues.push(dvStoryTitle);


        if (bReadOnly) {
            //show page name only
            var currPageCursor = storyView.StoryController.getCurrentPageCursor();
            var ss = "var oPage = _StoryController.CurrentStory.Pages.page_" + currPageCursor;
            eval(ss);
            var txtPageTitle = TheUte().decode64(oPage.Name);

            //var txtPageTitle = document.createTextNode(TheUte().decode64(oPage.Name));
            //            var dvPageTitle = document.createElement("DIV");
            //            dvPageTitle.className = "clsPageTitle";
            //            dvPageTitle.appendChild(txtPageTitle);
            //            PageNavValues.push(dvPageTitle);
            dvStoryTitle.title = txtPageTitle;
            //dvStoryTitle.style = "cursor:crosshair;";


        }
        else {
            //show page pulldown
            var aPageNames = storyView.StoryController.getPageNameArray();
            var selPulldown = TheUte().getSelect3("selPageNavList", aPageNames, storyView.StoryController.pagNavChanged, storyView.StoryController.getCurrentPageCursor(), "clsPageNavPulldownLGE");
            PageNavValues.push(selPulldown);
        }

        PageNavValues.push(TheUte().getSpacer(3, 1));

        _nextFxnPointer = this.page_next;
        if (bReadOnly == false) {
            this._prevButton = TheUte().getButton("cmdPreviousPage", "< previous", "previous", this.page_previous, "clsButtonLGE");
            PageNavValues.push(this._prevButton);
        }
        this._nextButton = TheUte().getButton("cmdNextPage", "next >", "next", this.page_next, "clsButtonLGE");
        
        PageNavValues.push(this._nextButton);

        var grdPageEditControls = newGrid2("grdPageEditControls", 1, 5, grdPageEditControlsVals);
        grdPageEditControls.init(grdPageEditControls);
        this.divPageEditControls.appendChild(grdPageEditControls.gridTable);

        if (bReadOnly)
            this.divPageEditControls.style.display = "none";




        PageNavValues.push(TheUte().getSpacer(15, 1));
        PageNavValues.push(this.divPageEditControls);
        //PageNavValues.push(TheUte().getSpacer(5, 1));

        // PageNavValues.push(TheUte().getButton("cmdLogin", "usr", "login to a personal account on this system", this.goToLogin, "clsButtonLGE"));

        var cmdShowAdvancedOptions = TheUte().getButton("cmdAdvancedOptions", "a", "show advanced options", null, "clsButtonAction2LGE");
        cmdShowAdvancedOptions.onclick = function() {
            location.href = "./StoryEditor4.aspx?StoryID=" + storyView.StoryController.CurrentStory.ID + "&toolBR=ALL";
        }
        var cmdShare = TheUte().getButton("cmdShare", "share", "sharing options", null, "clsButtonAction2LGE");
        cmdShare.onclick = function() {
            location.href = "./ShareStory.aspx?StoryID=" + storyView.StoryController.CurrentStory.ID;

        }

        if (bReadOnly == false) {
            PageNavValues.push(cmdShare);
            //PageNavValues.push(cmdShowAdvancedOptions);
            // PageNavValues.push(TheUte().getSpacer(5, 1));
            PageNavValues.push(TheUte().getButton("cmdHome", "toc", "table of contents", this.home, "clsButtonAction2LGE"));
        }



        var PageNavGrid = newGrid2("PageNavGrid", 1, PageNavValues.length, PageNavValues, 0);
        PageNavGrid.init(PageNavGrid);

        TheUte().removeChildren(_PageNavPanel);

        _PageNavPanel.appendChild(PageNavGrid.gridTable);

    }



   
    //these cursors are circulare through PE and Views

    this.page_previous = function() {
    _view.clearLog();
        // only go back one slide at a time, never back to a page(unless editor)

        var currElementCursor = _StoryController.getCurrentPECursor();
        currElementCursor = currElementCursor - 1;
        var currPageCursor = _StoryController.getCurrentPageCursor();
        var s = "var currPageElementMapCount =_StoryController.CurrentStory.Pages.page_" + currPageCursor + ".PageElementMapCount";
        eval(s);
        currPageElementMapCount = currPageElementMapCount * 1;

        if (currElementCursor >= 0) {
            //change element
            s = "var oPage = _StoryController.CurrentStory.Pages.page_" + currPageCursor;
            eval(s);
            loadPageElement(_StoryController, oPage, currElementCursor);

        }
        else {
            if (_readOnly) {
                //halt
                return;
            }
            else {
                _StoryController.pagePrevious();
            }

        }


    }

    this.page_next = function() {

    _view.clearLog();
        
        var currElementCursor = _StoryController.getCurrentPECursor();
        currElementCursor = currElementCursor + 1;
        var currPageCursor = _StoryController.getCurrentPageCursor();
        var s = "var currPageElementMapCount =_StoryController.CurrentStory.Pages.page_" + currPageCursor + ".PageElementMapCount";
        eval(s);
        currPageElementMapCount = currPageElementMapCount * 1;

        if (currElementCursor < currPageElementMapCount) {
            //change element
            s = "var oPage = _StoryController.CurrentStory.Pages.page_" + currPageCursor;
            eval(s);
            loadPageElement(_StoryController, oPage, currElementCursor);

        }
        else {


            var stateCursor = _StoryController.CurrentStory.StateCursor;

            if (stateCursor > currPageCursor) {
                _StoryController.pageNext();
            }
            else {
                if (_readOnly) {

                    _view.log("Please wait for the Game Controller to advance the game");

                }
                else {
                    _StoryController.pageNext();
                }
            }
        }



    }

   
    
    this.page_new = function ()
    {
        var pageName = window.prompt("What is the new Group Name","");
        
    
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
        
        var xy = "800 600"; 
        
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
















