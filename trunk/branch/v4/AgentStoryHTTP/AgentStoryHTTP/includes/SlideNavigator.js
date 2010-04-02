
//    AgentIdea - Slide Navigator viewer/editor


function OffsetCoord(aX,aY)
{
    this.x = aX;
    this.y = aY;
}

function storyView2(aoController,aoBod,seqLastChange,astoryID)
{ 
    aoController.addObserver( this ); //make this view an observer
    var _StoryController = aoController;
    this.StoryController = _StoryController;

    var _alertArea = null;


    this.log = function(message) {


        if (_alertArea == null) {
            _alertArea = new AlertArea();
        }
        _alertArea.log(message);
    }

    this.clearLog = function() {

    if (_alertArea == null) {
        _alertArea = new AlertArea();
    }
    _alertArea.log("");
    }


    var _pageNavPanel = null;
    _pageNavPanel = new simplePageNavigatorPanel(_StoryController, this);


    this.pageNavPanel = _pageNavPanel;

    this.hideNext = function() {
        _pageNavPanel.hideNext();
    }
    this.showNext = function() {
        _pageNavPanel.showNext();
    }

    
    
    
    var _refFrontHook = null;
    this.refFrontHookSet = function(rhs) { _refFrontHook = rhs; }
    this.refFrontHookGet = function() { return _refFrontHook; }
    
    this.hazeScreen = function(guid,srcText)
    {
        _refFrontHook.style.width = "1000%";
        _refFrontHook.style.height = "1000%";
        _refFrontHook.style.textAlign = "left";
        _refFrontHook.style.paddingTop = 88;
        
        var _StoryElementEditor = new StoryElementEditor(44,guid,srcText);
        _StoryElementEditor.init( _refFrontHook );
        _StoryElementEditor.focus();
        
    }
    
    this.unHaze = function()
    {
        TheUte().removeChildren( _refFrontHook );
        _refFrontHook.style.width = "1";
        _refFrontHook.style.height = "1";
    }

    this.isGateJustAheadOfMe = function() {
        var ret = false;

        var gateCursor = storyView.StoryController.CurrentStory.StateCursor;


        var PageCursor = storyView.StoryController.getCurrentPageCursor();

        if (gateCursor == (PageCursor + 1)) {
            ret = true;
        }

        //alert(gateCursor + "::" + PageCursor);

        return ret;
    }
    
    this.saveCurrentElement = function()
    {
        
       // alert("SAVING: " + storyView.getCurrElement().GUID );
       
       try
       {
        var currElem = storyView.getCurrElement();
        if(currElem != null)
        {
            currElem.save();
            currElem.updateViewHTML(currElem.id);
            currElem.hideProps();
        }
       }
       catch(Erro)
       {
        alert("Error saving current element " + Erro.description);
       }
        
    }

    
    var _Bod = aoBod;
    var _name = null;
    this.name = _name;
    
    this.LEFT_ADJUST_PANEL_OPEN = 385;
    this.LEFT_ADJUST_PANEL_CLOSED = 5;
    
    this.TOP_ADJUST_PANEL_OPEN = 10;
    this.TOP_ADJUST_PANEL_CLOSED = 200;
    
    var _offsetX = 5;
    var _offsetY = 0;
    this.getOffSetX = function() { return _offsetX; }
    this.getOffSetY = function() { return _offsetY; }
    this.setOffSetX = function(rhs) { _offsetX=rhs; }
    this.setOffSetY = function(rhs) { _offsetY=rhs; }
    
    var _storyID = astoryID;
    
    var _busyWithEditor = 0;
    this.getBusy = function() { return _busyWithEditor; }
    this.setBusy = function(rhs) { _busyWithEditor = rhs; }
    
    var _currElement = null;
    this.getCurrElement = function() { return _currElement;}
    this.setCurrElement = function(rhs) { _currElement = rhs; }
    
    var _seq = 0;
    this.getSeq = function(){ return _seq; }
    this.setSeq = function(rhs){ _seq=rhs; }
    
    var _storySeq = seqLastChange;   //LOADS initially with story last tx log seq number.
    this.getStorySeq = function(){ return _storySeq; }
    this.setStorySeq = function(rhs){ _storySeq=rhs; }
    
    
    var _noGoX = 10;
    var _noGoY = 10;
    this.noGoX = _noGoX;
    this.noGoY = _noGoY;
    
    var _tmpGUIDcounter = 0;
    this.getTmpGUID = function()
    {
        //based off session id so should be unique x sessions
        _tmpGUIDcounter++;
        return _tmpGUIDcounter + "_" + gUserCurrentTxID; 
    }
    
    var _tmpOffSetLibPlaceMentX = 150;
    var _tmpOffSetLibPlaceMentY = 150;
    var _tmpOffSetLibPlaceMentCount = 0;
    var _tmpOffSetLibPlaceMentStagger = 50;
    
    this.getOffsetCoord = function()
    {
        _tmpOffSetLibPlaceMentCount = _tmpOffSetLibPlaceMentCount + 1;
        
        var factor =  _tmpOffSetLibPlaceMentCount * _tmpOffSetLibPlaceMentStagger;
        
        var coord = new OffsetCoord( (_tmpOffSetLibPlaceMentX + factor),( _tmpOffSetLibPlaceMentY + (factor/2) ) )
        
        return coord;
    }


     this.dropElemIn = function(content,coord,guid,handleTitle )
     {
        // create a new page element
        var newWidget = addNewWidget( coord.x,coord.y,9,-1,guid,handleTitle,false,null,1 );
        newWidget.updateSrcText( content );
        newWidget.updateViewHTML(newWidget.id);  
        newWidget.showProps();
        return newWidget;
     
     }
     
      this.dropElemIn64 = function(content64,coord,guid,handleTitle )
     {
        if(handleTitle == null) handleTitle = "UNSAVED library element";
        var src = TheUte().decode64(content64);
        // create a new page element
        var newWidget = addNewWidget( coord.x,coord.y,9,-1,guid,handleTitle,false,null,1 );
        newWidget.updateSrcText( src );
        newWidget.updateViewHTML(newWidget.id);  
        newWidget.showProps();
        return newWidget;
     
     }
    

    var _viewLocked = false;
    this.viewLocked = _viewLocked;
    this.setViewLocked = function(b)
    {
        _viewLocked = b;
    }
    
    this.getViewLocked = function ()
    {
        return _viewLocked;
    }
    
    var _container = null;
    this.container = _container;
    
    
   this.binderOpen = function()
   {
            storyView.adjustHook(0,storyView.TOP_ADJUST_PANEL_OPEN);
            storyView.setOffSetY(storyView.TOP_ADJUST_PANEL_OPEN);
   }
   this.binderClose = function()
   {
            storyView.adjustHook(0,storyView.TOP_ADJUST_PANEL_CLOSED);
            storyView.setOffSetY(storyView.TOP_ADJUST_PANEL_CLOSED);
   }
    
    //
    // library panel
    //
    
//    var _libPanel = document.createElement("DIV");
//    this.libPanel = _libPanel;
//    _libPanel.className = "clsLibPanel";  
//    _libPanel.style.display = "none";
//    
//    var _libViewer = new LibraryViewer();
//    _libViewer.init();
//    _libPanel.appendChild( _libViewer.container );
//    
//    this.getLibViewer = function()
//    {
//        return _libViewer;
//    }

   this.adjustHook = function( x,y )
   {
        var theHook = document.getElementById("TheHook");
        theHook.style.top = y;
        theHook.style.left = x;
   }
    
    var _storyElemViews = new Array();
    this.registerSEV = function ( sev )
    {
        _storyElemViews.push( sev );
    }
    
    this.lenSEV = function () { return _storyElemViews.length; }
    
    this.sevByGuid = function( aGUID )
    {
        var sev = null;
        var lenSEV = _storyElemViews.length;
        var i = 0;
        for(;i<lenSEV;i++)
        {
            var tmpSEV = _storyElemViews[i];
            
            if(tmpSEV != null)
            {
                if(tmpSEV.getGUID() == aGUID)
                {
                   sev = tmpSEV;
                   break;
                }
            }
        }
        
        return sev;
    }
    
  
    
    this.showAllSEV = function()
    {
        var sev = null;
        var lenSEV = _storyElemViews.length;
        var i = 0;
        for(;i<lenSEV;i++)
        {
            var tmpSEV = _storyElemViews[i];
            
            if(tmpSEV != null)
            {
                tmpSEV.show();
            }
        }
    
    }
    
    
  
   
    
    this.getStoryElemView = function(dbID)
    {
        var sev = null;
        var lenSEV = _storyElemViews.length;
        var i = 0;
        for(;i<lenSEV;i++)
        {
            var tmpSEV = _storyElemViews[i];
            
            if(tmpSEV != null)
            {
                if(tmpSEV.getDBID() == dbID)
                {
                   sev = tmpSEV;
                   break;
                }
            }
        }
        
        return sev;    
    }
    

    
    var _widgetCounter = 0;
    this.widgetCounter = _widgetCounter;

    var _inputArea = TheUte().getInputBox("","TheInputArea",null,null,"clsTheInput","type commands here");
    var _outputArea = new bufferedDisplay(gBufferDisplay);
     
    var _sentAnnounce = false;
   
   this.heartbeat = function(s)
   {
       var macroHeartBeat = newMacro("HeartBeat");
       addParam( macroHeartBeat,"seq",_seq);
       addParam( macroHeartBeat,"storyID",_storyID);
       addParam ( macroHeartBeat,"storySeq",_storySeq);
       processRequest( macroHeartBeat );  	
       
       if(_sentAnnounce == false)
       {
            //call back to server to announce user
            var userOpening = _StoryController.CurrentUser.UserName;
            
            var postMsg = newMacro("StoryPostMsg");
            addParam( postMsg,"msg64",TheUte().encode64(userOpening + " joined this story" ));
            addParam( postMsg,"StoryID",_storyID );
            addParam( postMsg,"userName", userOpening);
            processRequest( postMsg );   
            _sentAnnounce = true;
    
    } 

   }
   
   
   this.postMsg = function(msg)
   {
        var userOpening = _StoryController.CurrentUser.UserName;
            
            var postMsg = newMacro("StoryPostMsg");
            addParam( postMsg,"msg64",TheUte().encode64(msg ));
            addParam( postMsg,"StoryID",_storyID );
            addParam( postMsg,"userName", userOpening);
            processRequest( postMsg ); 
   }
   
      this.postExitMsg = function()
   {
        var userOpening = _StoryController.CurrentUser.UserName;
            
            var postMsg = newMacro("StoryPostMsg");
            addParam( postMsg,"msg64",TheUte().encode64(userOpening + " has left this story" ));
            addParam( postMsg,"StoryID",_storyID );
            addParam( postMsg,"userName", userOpening);
            processRequest( postMsg ); 
   }
   
   
   
   this.displayMessage = function(msg,sev)
   {
       
        _outputArea.addMessage(msg,sev);
   }
   
  
   
    var _northPanel = new panel( _inputArea,_outputArea.container );
    this.northPanel = _northPanel;


    this.storyViewDoublClick = function() {
        alert("this feature is disabled in this version");
    }
    

   


    this.init = storyView2init;
    
    //marks this object as an observer
    this.update = function(change)
    {
        //this is called on every notify that the model has changed.
        //alert(" observer updated! :: " + change + " " + _StoryController.getCurrentPageCursor() );

        //get current page
        var currPage = _StoryController.GetPage( _StoryController.getCurrentPageCursor() );
        
        var bReadOnly = false;
        if(_StoryController.CurrentStory.CanEdit == 0)
        {
            bReadOnly = true;
        }
            
        //pass page nav information
        //pass page to page nav panel
        _pageNavPanel.init( currPage, bReadOnly );

        //CLEAR current page
        this.resetStoryView();
        
   
        
        //LOAD page elements for this page
        this.loadPageElements(_StoryController,currPage);
    }
    

    
    var _targetStage = null;
    this.targetStage = _targetStage;
    
    var _dohm = null;
    this.dohm = _dohm;
    
    
    this.getStoryMetaPanel = getStoryMetaPanel;
    
    this.getNewDraggableDiv = getNewDraggableDiv;
    this.addNewWidget = addNewWidget;
    
    
    this.resetStoryView = function ()
    {
        //remove from object ...
       storyView.storyElemViews = new Object();
        //remove elements from their targets
        //check
 
        var i = 1;
        for(;i<storyView.widgetCounter + 1;i++)
        {
            
            var _attachDiv = document.getElementById("divAttacher_" + i);
            if( _attachDiv != null )
            {
               //remove the children from the attach
               TheUte().removeChildren( _attachDiv );
            }
        }
        
       storyView.widgetCounter = 0;
       _widgetCounter = 0;
       
        //$TODO: correct way to break state of editor open? 1
        //set busy false
        storyView.setBusy(0);

        
  
    }

    this.loadPageElements = loadPageElements;

}
    

function storyView2init(attachPoint,aFrontHook)
{
    this.refFrontHookSet(aFrontHook);
    //prepare the view.
    var StoryViewGridItems = new Array();
    attachPoint.appendChild (  this.northPanel.collapseExpandPanel );

    var oStoryViewGrid  = newGrid2("storyViewerGrid",3,1,StoryViewGridItems,0);
    oStoryViewGrid.init( oStoryViewGrid );

    attachPoint.appendChild(this.pageNavPanel.container);

    if (this._alertArea == null) {
        this._alertArea = new AlertArea();
    }
    attachPoint.appendChild(this._alertArea.container);
//    attachPoint.appendChild( this.libPanel );

    this.container = this.pageNavPanel.container;

    // SHOW STORY META
    var _storyTitle = TheUte().decode64( this.StoryController.CurrentStory.Title           );
    var _storyDesc  = TheUte().decode64( this.StoryController.CurrentStory.Description     );
    var _author     = TheUte().decode64( this.StoryController.CurrentStory.ByUserName      );
    var _canEdit    = this.StoryController.CurrentStory.CanEdit;
    
    //pass data to north panel
    this.northPanel.init( _storyTitle, _author , _storyDesc, _canEdit );
    
   
   
   //set the toolbars
    gToolBarVisible = gToolBarVisible.toUpperCase();
   
    if( gToolBarVisible == "ALL" )
    {
        //do nothing.
    }
    else
    if( gToolBarVisible == "MIN" )
    {
        this.northPanel.minimize();
    }
    else
    if( gToolBarVisible == "NOCHAT" )
    {
        this.northPanel.hideChat();
    }
    else
    if( gToolBarVisible == "NONE" )
    {

        this.northPanel.hide();
    }
    else
    if( gToolBarVisible == "NAV" )
    {
        this.northPanel.showNavOnly();
    }
    else
    if( gToolBarVisible == "BASIC" )
    {
        this.northPanel.showNavOnly();
    }
   
    var pageToGoTo = 0;
    pageToGoTo = gPageCursor * 1;
    
     
     var tot = this.StoryController.CurrentStory.PageCount*1;

    
    if( pageToGoTo > (tot-1) )
    {
        this.StoryController.pageFirst();
    }
    else
    {
        this.StoryController.gotoPage( pageToGoTo );
    }

}



function loadPageElement(oStoryController,oPage,elementOrdinal) {


    


    var s = "var pem = oPage.PageElementMaps.pageElementMap_" + elementOrdinal;
    eval(s);

    if (pem != null && pem.visible) {

        var pageElement = null;

        //at this point only load at the attachpoing set in aspx ...
        pem.X = X_POINT;
        pem.Y = Y_POINT;

        s = " pageElement = oStoryController.CurrentStory.PageElements.pageElement_" + pem.PageElementID;
        eval(s);

        // alert("adding new page element " + pem.X + " x " + pem.Y);

       

        var pageElementValue = TheUte().decode64(pageElement.Value);
        var preJavaScript; var postJavaScript;

        if (pageElement.preJavaScript != null) {
            preJavaScript = TheUte().decode64(pageElement.preJavaScript);
         }
        if (pageElement.postJavaScript != null) {
            postJavaScript = TheUte().decode64(pageElement.postJavaScript);
        }

        var newWidget = addNewWidget(pem.X, pem.Y, pem.Z, pageElement.ID, pem.GUID, pageElement.BY, false, pageElement.DateAdded, pageElement.CanEdit);
        newWidget.updateSrcText(pageElementValue);

        if (preJavaScript != null) {
            newWidget.updatePreJavaScript(preJavaScript);
        }
        if (postJavaScript != null) {
            newWidget.updatePostJavaScript(postJavaScript);
        }
        
        newWidget.updateViewHTML(newWidget.id);

        oStoryController.setCurrentPECursor(elementOrdinal);
        oStoryController.setCurrentPECursorID(pageElement.ID);

        

        if (preJavaScript != null) {

            try {
                eval(preJavaScript);
            }
            catch (preX) {
                //alert("error evaluation of preJavaScript" + preX.description);
            }

        }
        

    }
    else {
        alert("PEM was null");
    }

}

//intialize page, in this case now only load first
function loadPageElements(oStoryController, oPage)
{
    var numPageElementsMapped = oPage.PageElementMapCount;

    if(numPageElementsMapped > 0) {
        loadPE_First(oStoryController, oPage);
    }

}

function loadPE_First(oStoryController, oPage) {
    loadPageElement(oStoryController, oPage, 0);
}



function getStoryMetaPanel()
{

    var sTitle = TheUte().decode64( this.StoryController.CurrentStory.Title );
    var sDesc = TheUte().decode64( this.StoryController.CurrentStory.Description ) ;
    
    var StoryMetaPanelValues = new Array();
    
    var txtName = document.createTextNode(  sTitle );
    var txtDescription = document.createTextNode( sDesc  );
    var dvName = document.createElement("DIV");
    var dvDesc = document.createElement("DIV");
    dvName.className = "clsStoryTitle";
    dvName.title = sDesc;
    

    dvName.appendChild(txtName);

    
    StoryMetaPanelValues.push(dvName);
    StoryMetaPanelValues.push(dvDesc);

    var StoryMetaPanelGrid  = newGrid2("storyMetaPanelGrid",1,2,StoryMetaPanelValues);
    StoryMetaPanelGrid.init( StoryMetaPanelGrid );

    return StoryMetaPanelGrid.gridTable;
}







    
function addNewWidget(x,y,z,aID,aGUID,BY,bShowEditor,DateAdded,bCanEdit) {



    var _bodyREF = document.getElementById("TheHook");


    if (TheUte().hasChildren(_bodyREF) == true) {
        //alert("Unloading previous element?");
        //fire previous elemets onUnloadJavaScript();

       
        try {
            var currStory = storyView.StoryController.CurrentStory;
            var currPage = storyView.StoryController.CurrentPage();
            var currPE_ID = storyView.StoryController.getCurrentPECursorID();
            var lastPE = storyView.StoryController.FindPageElement(currStory, currPage, currPE_ID);
            var postJS = TheUte().decode64(lastPE.postJavaScript);
        
            eval(postJS);
        }
        catch (postJSexp) {
            //do nothing
        }
        
        TheUte().removeChildren(_bodyREF);  //ensure old content is removed ...
    }
    
    if(DateAdded == null) DateAdded = "now";
//smell
    storyView.widgetCounter++;
    var _wc = storyView.widgetCounter;
    
    var tmpStoryElem =  new storyElemView(aID ,storyView.widgetCounter,x,y,z,aGUID,BY,bShowEditor,DateAdded,bCanEdit);
    tmpStoryElem.init();
    
    var apX = x*1;
    var apY = y*1;
    

    
    
    //create a FIXED attach point / hanger for story elements 
    //( attaching to content div had element resizing could interferene with other elems! 
    var _dvAttacher = null;
    
    var key = "divAttacher_" + _wc;
    
    _dvAttacher = document.getElementById(key);
    
    if(_dvAttacher == null) {
       // alert("new attacher created");
        _dvAttacher = document.createElement("DIV");
     }
     else
     {
       // alert("  using existing attacher div");
     }
     
        
    _dvAttacher.className = "clsAnchorPoint";
    _dvAttacher.id = key;
    _dvAttacher.style.left = apX;
    _dvAttacher.style.top = apY;
 

  

   
    _bodyREF.appendChild( _dvAttacher );

    _dvAttacher.appendChild( tmpStoryElem.getContainer() );
    return tmpStoryElem;
   
}


function getNewDraggableDiv(name,id)
{
    storyView.widgetCounter++;
    var _wc = storyView.widgetCounter;
    
    var _dv = document.createElement("DIV");
    _dv.id = "widget_" + _wc;
    
    var _dvHandle = document.createElement("DIV");
    _dvHandle.id = "handle_" + _wc;
    _dvHandle.className = "clsWidgetBoxHandle";
    
    var _dvProperties= document.createElement("DIV");
    _dvProperties.id = "widgetProperties_" + _wc;
    _dvProperties.className = "clsWidgetProperties";
    
    
    _dvProperties.appendChild( storyView.getPropertiesPanel("propPanel_" + _wc) );
    
     var _dvWidgetView= document.createElement("DIV");
    _dvWidgetView.id = "widgetView_" + _wc;
    _dvWidgetView.className = "clsWidgetView";  
     
    
    
    
    _dv.className = "clsWidgetBox";
    
    _dv.appendChild( _dvHandle );
    _dv.appendChild( _dvWidgetView );
    _dv.appendChild( _dvProperties );

    //
    //remove the drag/drop aspects
    //
    //var dd = new YAHOO.example.DDOnTop(_dv);
    //var dd = new YAHOO.util.DDProxy(_dv);
    //dd.setHandleElId(_dvHandle.id);
    //dd.setXConstraint(1000, 1000, 25);
    //dd.setYConstraint(1000, 1000, 25);
    
    return _dv;
}





















//object representing page navigation panel
function PageNavigatorPanel( aoStoryController )
{

    var _StoryController = aoStoryController;
   
    var _PageNavPanel = document.createElement("DIV");
    _PageNavPanel.className = "clsPageNameLabel";
    this.container = _PageNavPanel;
    
    var _divPageEditControls = document.createElement("DIV");
    _divPageEditControls.id = "dvPageEditControls";
    _divPageEditControls.style.display = "block";
    
    this.divPageEditControls = _divPageEditControls;
    
    var _lePageName = null;
    this.lePageName = _lePageName;
    
        
    this.savePageName = function (s)
    {
       
       var oPageName = TheUte().encode64(s);
       
       var oPage = storyView.StoryController.GetPage( storyView.StoryController.getCurrentPageCursor() );
       var macroUpdatePage = newMacro("UpdatePage");
       addParam( macroUpdatePage,"PageID",oPage.ID );
       addParam( macroUpdatePage,"GridX",oPage.gridCols );
       addParam( macroUpdatePage,"GridY",oPage.gridRows );
       addParam( macroUpdatePage,"StoryID",storyView.StoryController.CurrentStory.ID);
       addParam( macroUpdatePage,"PageName", oPageName);
       oPage.Name = oPageName;
      //alert( serializeMacroForRequest( macroUpdatePage) );
       processRequest( macroUpdatePage );  	   

    }

   this.lib_toggle = function ()
   {
       
   }
   
   


    this.init = function ( oPage, bReadOnly )
    {
        TheUte().removeChildren( _divPageEditControls );

        var PageNavValues = new Array();
        
        PageNavValues.push( TheUte().getSpacer(45,1) );
        PageNavValues.push( TheUte().getButton("cmdFirstPage","<<","go to first page",this.page_first,"clsButton"));
        PageNavValues.push( TheUte().getButton("cmdPreviousPage","<","go to previous page",this.page_previous,"clsButton"));
        PageNavValues.push( TheUte().getButton("cmdNextPage",">","go to next page",this.page_next,"clsButton"));
        PageNavValues.push( TheUte().getButton("cmdLastPage",">>","go to last page",this.page_last,"clsButton"));

        var grdPageEditControlsVals = new Array();
        grdPageEditControlsVals.push( TheUte().getButton("cmdAddPage","+","add new page",this.page_new,"clsButton"));
        grdPageEditControlsVals.push( TheUte().getButton("cmdRemovePage","-","remove page",this.page_remove,"clsButton"));
        grdPageEditControlsVals.push( TheUte().getButton("cmdRemovePage","order","re-order pages",this.page_reorder,"clsButton"));
        
      //  grdPageEditControlsVals.push( TheUte().getButton("cmdPageProperties","lib","toggle library",this.lib_toggle,"clsButton"));

        
        var aPageNames = storyView.StoryController.getPageNameArray();
        // add names to pulldown - set sel index for curr page.
        
        PageNavValues.push( TheUte().getSelect3("selPageNavList",aPageNames,storyView.StoryController.pagNavChanged,storyView.StoryController.getCurrentPageCursor() ,"clsPageNavPulldown" ));

        
        //alert("load sel" + TheUte().decode64(oPage.Name));
      
        var grdPageEditControls = newGrid2("grdPageEditControls",1,5,grdPageEditControlsVals);
        grdPageEditControls.init( grdPageEditControls );
        this.divPageEditControls.appendChild( grdPageEditControls.gridTable );
        
        if(bReadOnly)
            this.divPageEditControls.style.display = "none";
        
        var spacer = document.createElement("DIV");
        spacer.style.width = "10px";
        
        
        
        
        PageNavValues.push( this.divPageEditControls );

        _lePageName = new LabelEdit(TheUte().decode64( oPage.Name ),"lePageName",bReadOnly,"input",this.savePageName);
        _lePageName.init();
        
        PageNavValues.push( spacer );
        PageNavValues.push ( _lePageName.container );
        
        
        
        
        
         var _dvPageCursor = document.createElement("DIV");
         _dvPageCursor.className = "clsPageCounter";
        var _txtPageCursor = document.createTextNode("" + ( storyView.StoryController.getCurrentPageCursor() +1 ) + " of " + storyView.StoryController.CurrentStory.PageCount + "");
        _dvPageCursor.appendChild( _txtPageCursor );
       PageNavValues.push ( _dvPageCursor );
       
       
        


        var PageNavGrid  = newGrid2("PageNavGrid",1,PageNavValues.length,PageNavValues,0);
        PageNavGrid.init( PageNavGrid );

        TheUte().removeChildren( _PageNavPanel );

        _PageNavPanel.appendChild( PageNavGrid.gridTable );
        
    }
    
    this.page_remove = function ()
    {
        var p = _StoryController.CurrentPage();
        if(p != null)
        {
            var sName = TheUte().decode64( p.Name );

        
            var res = confirm("are you sure you want to delete page - " + sName);
            if(res)
            {
                var RemovePage = newMacro("RemovePage");
                addParam( RemovePage,"StoryID",_StoryController.CurrentStory.ID );
                addParam( RemovePage,"PageID",p.ID );
                processRequest( RemovePage );
            }
        }
    }
    
    this.page_reorder = function()
    {
        location.href="./StoryPageView2.aspx?StoryIDs=" + _StoryController.CurrentStory.ID;
    
    }
    
    this.page_manage = function()
    {
    //page_manage
    alert("managing");
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
  
    
    this.page_first = function ()
    {
        _StoryController.pageFirst();
    }
    
    
    this.page_last = function ()
    {
        _StoryController.pageLast();
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


















// object representing the North panel
function panel( aInputArea,aOutputArea )
{
    var _InputAreaREF = aInputArea;
    this.InputAreaREF = _InputAreaREF;
    
    var _OutputAreaREF = aOutputArea;
    this.OutputAreaREF = _OutputAreaREF;
    
    var _collapseExpandPanel = document.createElement("DIV");
    this.collapseExpandPanel = _collapseExpandPanel;
    
    var _collapseExpandBar = document.createElement("DIV");
    this.collapseExpandBar = _collapseExpandBar;
    
    var _collapseExpandBody = document.createElement("DIV");
    this.collapseExpandBody = _collapseExpandBody;
    
    var _dvHandleBarWidgets = document.createElement("DIV");
    this.dvHandleBarWidgets = _dvHandleBarWidgets;
    
    _dvHandleBarWidgets.onclick = function (ev)
    {
        ev = ev || window.event;
        ev.cancelBubble = true; 
      }
    
    


    var _dvLabel = document.createElement("DIV");
    this.dvLabel = _dvLabel;
    
   
    var txtLabel = document.createTextNode("view only");
    _dvLabel.appendChild(txtLabel);
    
    _dvLabel.title = "check here to hide edit aspects of page elements";
  
    
    this.hide = function()
    {
    
        _collapseExpandBar.style.display="none";
        _collapseExpandBody.style.display="none";
        _toggleDiv.style.display="none";
        _collapseExpandPanel.style.display="none";
        _dvHandleBarWidgets.style.display="none";
        storyView.pageNavPanel.container.style.display="none";
        
    }
    
    this.hideChat = function()
    {
        _collapseExpandBody.style.display="none";
    
    }
    
    this.showNavOnly = function()
    {
    
        _collapseExpandBar.style.display="none";
        _collapseExpandBody.style.display="none";
        _toggleDiv.style.display="none";
    }    
    
    this.minimize = function()
    {
    
        _collapseExpandBar.style.display="none";
        _collapseExpandBody.style.display="none";
    }

      
    var _toggleDiv = document.createElement("DIV");
    
    _toggleDiv.title = "click here to hide binder";
    this.toggleDiv = _toggleDiv;
    _toggleDiv.className = "clsToggleClose";
    
    _toggleDiv.onclick = function()
    {
        if( _collapseExpandBar.style.display=="none" )
        {
            _collapseExpandBar.style.display="block";
            _collapseExpandBody.style.display="block";
            _toggleDiv.title = "click here to hide binder";
          
            storyView.binderClose();

        }
        else
        {
            _collapseExpandBar.style.display="none";
            _collapseExpandBody.style.display="none";
            _toggleDiv.title = "click here to show binder";
          
            storyView.binderOpen();
            
        }
    }
    
    _toggleDiv.ondblclick = function(ev)
    {
            ev = ev || window.event;
            ev.cancelBubble = true;  
     }
    
    _toggleDiv.onmouseover = function()
    {
        if( _collapseExpandBar.style.display=="none" )
        {
           _toggleDiv.className = "clsToggleOpenMouseOver";
        }
        else
        {
         _toggleDiv.className = "clsToggleCloseMouseOver";
            
        }
    }
    
    _toggleDiv.onmouseout = function()
    {
        if( _collapseExpandBar.style.display=="none" )
        {
           _toggleDiv.className = "clsToggleOpen";
        }
        else
        {
          _toggleDiv.className = "clsToggleClose";
            
        }
    }
    
    

    this.getLockCheck = function getLockCheck(aoStoryController)
    {
        var dvLockCheckPanel = document.createElement("DIV");
        dvLockCheckPanel.className = "clsLockCheckPanel";
        
        var values = new Array();
        values.push(_dvLabel);
        
        if(aoStoryController.CurrentStory.CanEdit == 0)
        {

            dvLockCheckPanel.style.display = "none";
        }   
        
        var oGrid3 = newGrid2("lockViewGrid",1,2,values);
        oGrid3.init( oGrid3 );
        
        dvLockCheckPanel.appendChild(oGrid3.gridTable);
        return dvLockCheckPanel;

    }
    
    this.share_story = function ()
    {
        location.href="./ShareStory.aspx?StoryID=" + storyView.StoryController.CurrentStory.ID;
    }
    
    this.delete_story = function ()
    {
   
        var res = confirm("Are you sure you want to remove this story (" + storyView.StoryController.CurrentStory.ID + ")" );
        if(res == true)
        {
        
            var MarkStoryState = newMacro("MarkStoryState");
            addParam( MarkStoryState,"StoryID"        ,storyView.StoryController.CurrentStory.ID );
            addParam( MarkStoryState,"OperatorID"    ,storyView.StoryController.CurrentUser.ID     );
            addParam( MarkStoryState,"StoryState", 5);
            processRequest( MarkStoryState );
        }
       
       
    }
    
    this.clone_story = function ()
    {
       var res = prompt("Copy this story as: ","");
        if(res.trim().length > 0)
        {
            var CloneStory = newMacro("CloneStory");
            addParam( CloneStory,"StoryID"        ,storyView.StoryController.CurrentStory.ID );
            addParam( CloneStory,"OperatorID"    ,storyView.StoryController.CurrentUser.ID     );
            addParam( CloneStory,"NewStoryName64"    ,TheUte().encode64( res.trim() )    );
            processRequest( CloneStory );
        }
       
       
    }
    
    this.saveStoryTitle = function (s)
    {
        var storyTitle = TheUte().encode64(s);
    
        var updateStoryMeta = newMacro("UpdateStoryMeta");
        addParam( updateStoryMeta,"txtStoryName"    ,     storyTitle);
        addParam( updateStoryMeta,"txtStoryDescription",   TheUte().encode64( "bah" ));
        addParam( updateStoryMeta,"StoryID"        ,storyView.StoryController.CurrentStory.ID );
        
        processRequest( updateStoryMeta );
       
    }
    
    this.saveStoryDesc = function (s)
    {
    
        var updateStoryMeta = newMacro("UpdateStoryMeta");
        addParam( updateStoryMeta,"txtStoryName"    ,      TheUte().encode64( "bah" ));
        addParam( updateStoryMeta,"txtStoryDescription",   TheUte().encode64( s ));
        addParam( updateStoryMeta,"StoryID"        ,storyView.StoryController.CurrentStory.ID );
        
        processRequest( updateStoryMeta );
       
    }
    
    this.toggleBody = function ()
    {
            
        
        var dvToActOn = document.getElementById( "dvCollapseExpandBody" );
  
  
//        if( !dvToActOn.style.display )
//            TheLogger().log( "display mode not availible ","warn" );
//        else
//            TheLogger().log( "display mode " + dvToActOn.style.display , "warn" );
  
        if ( ! dvToActOn.style.display || dvToActOn.style.display == "block")
        {
            dvToActOn.style.display = "none";
            
            return;
        }
        
         
        if ( dvToActOn.style.display == "none")
        {
            dvToActOn.style.display = "block";
            
            return;
            
        }
        

    }
    
    this.init = initPanel;


}

function initPanel(title,author,desc,canEdit)
{
    TheUte().removeChildren(this.collapseExpandBar);
    TheUte().removeChildren(this.collapseExpandBody);
    TheUte().removeChildren(this.collapseExpandPanel);
    

    this.collapseExpandPanel.appendChild( this.toggleDiv );
    this.collapseExpandPanel.appendChild(this.collapseExpandBar);
    this.collapseExpandPanel.appendChild(this.collapseExpandBody);
     
    this.collapseExpandPanel.className = "clsCollapseExpandPanel";
    this.collapseExpandPanel.id = "dvCollapseExpandPanel"; 
    
    
    this.collapseExpandBar.className = "clsCollapseExpandBar";
    this.collapseExpandBar.onclick = this.toggleBody;
    this.collapseExpandBar.id = "dvCollapseExpandBar";
    
    var HandleBarValues = new Array();
    
    var klubIcon = document.createElement("DIV");
    klubIcon.className = "clsKlubIcon";
    var klubText = document.createTextNode("AI"); //$ todo: ouch pass in!!!!!
    klubIcon.appendChild(klubText);
    
    var sUser = "Visitor ";
    if(storyView.StoryController.CurrentUser.UserName)
      sUser = storyView.StoryController.CurrentUser.UserName.toUpperCase();
    
    klubIcon.title =  sUser + ", return to story index?";
    //klubIcon.onclick = function () { history.back(); }
    klubIcon.onclick = function () { location.href="./platform2.aspx"; }
    klubIcon.onmouseover = function () { klubIcon.className = "clsKlubIconMouseOver"; }
    klubIcon.onmouseout  = function () { klubIcon.className = "clsKlubIcon"; }

    var titleLabel = document.createTextNode( " by " + author );
    var dvTtitleLabel = document.createElement("DIV");
    dvTtitleLabel.className = "clsKlubStoryAuthor";
    dvTtitleLabel.appendChild( titleLabel );


    var bReadOnly = false;
    
    if(storyView.StoryController.CurrentStory.CanEdit == 0)  //$TO DO: REFACTOR UP
    {
        bReadOnly = true;
    }
    
    var leStoryTitle = new LabelEdit( title ,"leStoryTitle",bReadOnly,"input",this.saveStoryTitle, "clsKlubStoryTitle","clsKlubStoryTitleOnMouseOver");
    leStoryTitle.init();
    
 
    

    HandleBarValues.push( klubIcon );
    HandleBarValues.push( leStoryTitle.container);
    HandleBarValues.push( dvTtitleLabel );
    
    if( desc && desc != null)
    {

        var leStoryDesc = new LabelEdit( desc ,"leStoryDesc",bReadOnly,"textarea",this.saveStoryDesc, "clsKlubStoryDesc","clsKlubStoryDescOnMouseOver");
        leStoryDesc.init();   
        HandleBarValues.push(leStoryDesc.container);
   
    }
    
    if( canEdit == 1 )
    {
        //show sharing and story delete facilities
        var actionValues = new Array();
        var cmdShare = TheUte().getButton("cmdShareStory","share","set who can view and edit this story",this.share_story,"clsButton");
        var cmdDelete = TheUte().getButton("cmdDeleteStory","delete","delete this story",this.delete_story,"clsButton");
        var cmdClone  = TheUte().getButton("cmdCloneStory","copy","make a personal copy",this.clone_story,"clsButton");
        
       
        
        actionValues.push( cmdShare );
        actionValues.push( cmdClone );
        actionValues.push( cmdDelete );
        
        //actionValues.push( this.InputAreaREF );
        //actionValues.push( this.OutputAreaREF );
        actionValues.push( this.getLockCheck( storyView.StoryController ) );
        
        var actionButtonsGrid  = newGrid2("actionButtonsGrid",1,actionValues.length,actionValues,0);
        actionButtonsGrid.init( actionButtonsGrid );
        
         HandleBarValues.push( actionButtonsGrid.gridTable );
    }



    
    var HandleBarGrid  = newGrid2("HandleBarGrid",1,HandleBarValues.length,HandleBarValues);
    HandleBarGrid.init( HandleBarGrid );
    
    
    
      
    this.dvHandleBarWidgets.appendChild(HandleBarGrid.gridTable);
    
    this.collapseExpandBar.appendChild( this.dvHandleBarWidgets );
    this.collapseExpandBody.appendChild( this.OutputAreaREF );
    

    
   
   
    this.collapseExpandBody.className = "clsCollapseExpandBody"; 
    
    if( canEdit == 1 )
        this.collapseExpandBody.id = "dvCollapseExpandBody"; 
      
    this.collapseExpandBody.style.display = "block"; 
    

}





function  bufferedDisplay(bufLen)
{

    var _bufLen = bufLen;
    var _dvMain = document.createElement("DIV");
    
    var _outputArea = TheUte().getTextArea("","TheOutputArea",null,null,"clsTheOutput");
    _dvMain.appendChild(_outputArea);
    
    var _dataBuffer = new Array();
    
    this.addMessage = function(msg,sev)
    {
        _dataBuffer.push(msg);
        
        if(_dataBuffer.length > _bufLen)
        {
           
            //remove the first elem ( queue )
            _dataBuffer.reverse();
            _dataBuffer.pop();
            _dataBuffer.reverse();
            
        }
        
        //display buffer.
        _outputArea.value = "";
        var s = "";
        var i = 0;
        for(;i< _dataBuffer.length;i++)
        {
          s = _dataBuffer[i] + "\r\n" + s;
        }
        _outputArea.value = s;
    
    }
    
    this.container = _dvMain;



}



function setGate(storyGateState) {



    try {
        //updating gate state
        var macroSetPageGate = newMacro("SetPageGate");
        addParam(macroSetPageGate, "pageGate", storyGateState);
        addParam(macroSetPageGate, "StoryID", storyView.StoryController.CurrentStory.ID);
        processRequest(macroSetPageGate);
    }
    catch (e) {
        alert("gate set_state error " + e.description);
    }

}