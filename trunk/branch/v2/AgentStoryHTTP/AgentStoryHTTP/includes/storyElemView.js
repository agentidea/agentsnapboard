/*

    AgentIdea - Story Story Element Viewer

*/
function storyElemView(aID,aCount,aX,aY,aZ,aGUID,aBY,abShowEditor,aDateAdded, abCanEdit)
{

    var _contextButtons = null;
    this.contextButtons = _contextButtons;
  
    var _dbID = aID;
    this.dbID = _dbID;
    
    var _GUID = aGUID;
    this.GUID = _GUID;
    
    var _DateAdded = aDateAdded;
    this.DateAdded = _DateAdded;
      
    var _id = "sev_" + aCount;  
    this.id = _id;
    
    var _bShowEditor = abShowEditor;
    this.bShowEditor = _bShowEditor;
    
    var _bCanEdit = abCanEdit;
    this.bCanEdit = _bCanEdit;
    
    var _count = aCount;
    this.count = _count;
    
    var _dvInfo = document.createElement("DIV");
    _dvInfo.className="clsSEV_Info";
    this.dvInfo = _dvInfo;

    var _x = aX;
    var _y = aY;
    var _z = aZ;

    this.getX = function(){ return _x};
    this.getY = function(){ return _y};
    this.getZ = function(){ return _z};

    
    var _srcText = TheUte().getTextArea("" ,"txtSource_" + aID,null,null,"clsSrcCode");
    this.srcText = _srcText;
    
    this.NewEdit_click = function()
    {
            storyView.hazeScreen(_GUID,_srcText);
     }
    
    this.showProps = function()
    {

        var widgetProp =  document.getElementById("widgetProperties_" + _count);
        if( widgetProp != null )
            widgetProp.style.display = "block";
            
        var widgetPropCB =  document.getElementById("widgetPropertiesContext_" + _count);
        if( widgetPropCB != null )
            widgetPropCB.style.display = "block";
            
    }
    
    this.hideProps = function()
    {
        var widgetProp =  document.getElementById("widgetProperties_" + _count);
        if( widgetProp != null )
            widgetProp.style.display = "none";
        var widgetPropCB =  document.getElementById("widgetPropertiesContext_" + _count);
        if( widgetPropCB != null )
            widgetPropCB.style.display = "none";
    
    }
    
    var _currMacro = null;
    
    //STACK FUNCTIONS
    var parseStack = new Array();
    
    var OFF = -1;
    var IDLE = 0;
    var COLLECTING_COMMAND = 1;
    var GOT_CMD_NAME = 2;
    var COLLECTING_PARAMS = 3;
    var END = 4;
    
    
    var _currStateSEV = OFF;
    
    
    //END STACK FUNCTIONS
    
    var prevChar = -1;
    var paramCount = 0;
    
    _srcText.onkeyup = function (ev)
    {
        ev = ev || window.event;
        
        var kc = ev.keyCode;
        
        //ignore command keys
        // shift || caps lock || left alt || windows 
       if( kc == 16 || kc ==20 || kc ==18 || kc == 91) return;
        
        //_dvInfo.innerHTML += kc;
        // return;
         
         if(kc == 8)
         {
            //back spacing
            parseStack.pop();
            return;
         }
         
        //beware flocking code ahead
        if(prevChar == OFF )
        {
             _currStateSEV = IDLE;  //running
             _dvInfo.innerHTML += "\r\nIDLING";
            
        }
        else
        {
            //RUNNING
            
            if( _currStateSEV == IDLE )
            {
                if(prevChar == 191 && kc == 13)
                {
                    _currStateSEV = COLLECTING_COMMAND;
                    _dvInfo.innerHTML += "\r\nCOLLECTING_COMMAND CAPTURE";
                    if(parseStack.length>0)
                    {
                        //alert("dirty stack");
                        
                        TheUte().cleanStack( parseStack );
                    }
                    
                    
                }
            }
            else
            if(_currStateSEV == COLLECTING_COMMAND)
            {
                if(kc == 13)
                {
                    if(prevChar == 191)
                    {
                        _dvInfo.innerHTML += "\r\nCANCEL";
                        _currStateSEV = IDLE;
                    }
                    else
                    {
                        _currStateSEV = GOT_CMD_NAME;
                        
                        var commandName = null;
                        commandName = TheUte().pullStringOffStack( parseStack );
                        
                        _dvInfo.innerHTML += "\r\nGOT COMMAND NAME " + commandName;
                        //create macro by name
                        commandName = commandName.toUpperCase();
                        _currMacro = newMacro(commandName.trim());
                        addParam( _currMacro,"guid",_GUID);
                        
                        _currStateSEV = COLLECTING_PARAMS;
                    }
                    
                }
                else
                {
                        parseStack.push(kc);
                }
            }
            else
            if(_currStateSEV = COLLECTING_PARAMS)
            {
                
                if(prevChar == 191 && kc == 13)
                {
                    _currStateSEV = END;
                     _dvInfo.innerHTML += "\r\nEND";
                     
                     // run command
                     _dvInfo.innerHTML += "\r\nRUN COMMAND " + _currMacro.name;
                     
                     executeLocalCommand( _currMacro );
                    // processRequest( _currMacro );  	
                     
                     _currStateSEV = IDLE;
                     _dvInfo.innerHTML += "\r\nRAN SUCCESSUFLLY!";
                     
                     _currMacro = null;
                     paramCount = 0;
                     
                    
                }
                else
                if(kc == 13)
                {
                
                    //add param to curr macro?
                    paramCount++;
                    var parameterVal = null;
                    parameterVal = TheUte().pullStringOffStack( parseStack );
                    _dvInfo.innerHTML += "\r\nADD PARAMETER ( " + paramCount + " ) - " + parameterVal;
                    
                    addParam( _currMacro,"p_" + paramCount,parameterVal);
                }
                else
                {
                    parseStack.push(kc);
                }
            }
        }

        prevChar = kc;
        
        //_dvInfo.innerText += ev.keyCode;
        
    }
    
    
    
    
    
    

    var _origVal = null;
    this.origVal = _origVal;
    
    var _by = aBY;
    this.by = _by;

    this.reportInfo = function()
    {
        var s = "";
        s += " ";
        s += "GUID";
        s += ":";
        s += _GUID;
        s += " <br> ";
        
        s += " ";
        s += "xyz";
        s += ":";
        s += _x + " X " + _y + " X " + _z;
        s += " <br> ";
        
        s += " ";
        s += "db PK id";
        s += ":";
        s += _dbID;
        s += " <br> ";
        
        
        _dvInfo.innerHTML = s;
        
        //": " +  + "<BR>dbID: " + _dbID;
    }


    var _container = null;
    var _gridContainerObj = null;
    
    this.getContainer = function()
    {
        return _container;
    }
    
    this.setContainer = function(rhs)
    {
        _container = rhs;
    }
    
    this.getGridContainer = function()
    {
        return _gridContainerObj;
    }
    
    this.setGridContainer = function(rhs)
    {
        _gridContainerObj = rhs;
    }
    
    this.setContID = function(aID)
    {
        _container.id = aID;
    }
    
     this.setContClassName = function(aCN)
    {
        _container.className = aCN;
    }
    

    
    var _dvHandle = document.createElement("DIV");
    this.handle = _dvHandle;
    
    var _dvWidgetView= document.createElement("DIV");
    this.dvWidgetView = _dvWidgetView;
    
    this.updateWidgetView = function(sVal)
    {
        _dvWidgetView.innerHTML = sVal;
    }
    
    var _cmdUpdate = null;
    this.cmdUpdate = _cmdUpdate;
    
    var _dvView = null;
    this.view = _dvView;      
    
    this.init = storyElemViewInit;
    this.getPropertiesPanel = getPropertiesPanel;

    this.setCoords = function (aX,aY)
    {
        _x = aX *1;
        _y = aY *1;
    }
    
    this.updateHandle = function ( s )
    {
        _dvHandle.innerHTML = s;
    }
    
    this.updateTitle = function ( title )
    {
        _dvHandle.title = title;
    }
    
    this.updateSrcText = function ( val )
    {
        if( _srcText != null)
        {
            _srcText.value = val;
        }
        _origVal = val;
    
    }
    
    this.updateGUID = function ( aGUID )
    {
        
        _GUID = aGUID;  
        _gridContainerObj.setGUID(aGUID);

    }
    
    this.getGUID = function ()
    {
       return _GUID;  
    }
    
    this.updateDBID = function ( aDBID )
    {
        _dbID = aDBID;
    }
    this.getDBID = function ()
    {
        return _dbID;
    }    
    this.hide = function(s)
    {
        _container.className = "clsStoryViewWidget_hidden";
    }
    
    this.show = function(s)
    {
        _container.className = "clsStoryViewWidget";
    }
    
    this.cmdDeleteElement = function ()
    {
    
        if(_dbID != -1)
        {
            var macroRemovePageElement = newMacro("RemovePageElement");
            addParam( macroRemovePageElement,"PageElementID",_dbID);
            addParam( macroRemovePageElement,"PageElementMapGUID", _GUID);
            addParam( macroRemovePageElement,"storyElementViewID", _count);
            addParam( macroRemovePageElement,"StoryID",storyView.StoryController.CurrentStory.ID);
            addParam( macroRemovePageElement,"PageID",storyView.StoryController.getCurrentPageCursor() );

            processRequest( macroRemovePageElement );  
        }
        else
        {
            //alert("not saved to db, hide");
            _container.className = "clsStoryViewWidget_hidden";

        }
        
        //$TODO: correct way to break state of editor open? 3
        //set busy false
        storyView.setBusy(0);

    }
    
    this.place = function()
    {
        //TheLogger().log( "place me " + _container.parentNode.id + "@ " + _x + " x " + _y, "warn");
        var attacherDIV = _container.parentNode;
        attacherDIV.style.left = _x;
        attacherDIV.style.top = _y;
    }
    
    this.mark = function()  //smell strips and pulls to the side? bug as feature?
    {
        var attacherDIV = _container.parentNode;
        attacherDIV.className = "clsStoryViewWidget_mark";
    
    }
    this.unmark = function()
    {
        var attacherDIV = _container.parentNode;
        attacherDIV.className = "clsStoryViewWidget";    
    }
    
    
    this.cmdCopyClip = function (ev)
    {
        //only works if visible
        
        var srcHTML = _srcText.value;
        _srcText.focus();
        _srcText.select();
        document.execCommand("Copy");
    }
    
    this.cmdMoveFront = function (ev)
    {
                
        var numLayers =  storyView.StoryController.CurrentPage().pemMaxZ;
        var newZ = numLayers + 1;
        storyView.StoryController.CurrentPage().pemMaxZ = newZ;
        
        
              
        if(_dbID == -1)
        {
            //new elem don't save
        }
        else
        {
            //persist z order change data in db
            var UpdatePageElementXYZ = newMacro("UpdatePageElementXYZ");
            addParam( UpdatePageElementXYZ,"pemGUID", _GUID );
            addParam( UpdatePageElementXYZ,"X", _x );
            addParam( UpdatePageElementXYZ,"Y", _y );
            addParam( UpdatePageElementXYZ,"Z", newZ );
            addParam( UpdatePageElementXYZ,"tsx",gUserCurrentTxID );
            addParam( UpdatePageElementXYZ,"CurrentPageCursor", storyView.StoryController.getCurrentPageCursor() );
            addParam( UpdatePageElementXYZ,"StoryID",storyView.StoryController.CurrentStory.ID);
            
            processRequest( UpdatePageElementXYZ );  
            
        }

        //move to front ( local user only )
        var indexNumber = _count;  
        var _attachDivToMove = document.getElementById("divAttacher_" + indexNumber);
        var _bodyREF = document.getElementById("TheHook");
        _bodyREF.appendChild( _attachDivToMove );

    }
    
    
    
    this.cmdMoveBack = function (ev)
    {
        //alert("move DOWN Not Yet Implemented" + this.id);
    
    }


    this.save = function()
    {
    
        var s = null;

        if( _srcText != null)
        {
            s =  _srcText.value;
        }
        else
        {
            s = _origVal;
        }

        if(s.trim().length == 0) return;

        s = applyHTMLai(s);
        var sContentVal64 = TheUte().encode64( s );
 
 
                if(_dbID == -1)
                {
                
                //new
                  var currPage = storyView.StoryController.GetPage(storyView.StoryController.getCurrentPageCursor());

                   var macroCreateNewPageElementAndMap = newMacro("CreateNewPageElementAndMap");
                   addParam( macroCreateNewPageElementAndMap,"currentPageCursor",storyView.StoryController.getCurrentPageCursor());
                   addParam( macroCreateNewPageElementAndMap,"PageID",currPage.ID);
                   addParam( macroCreateNewPageElementAndMap,"sevTmpGUID",_GUID);
                   addParam( macroCreateNewPageElementAndMap,"GridX",_x);
                   addParam( macroCreateNewPageElementAndMap,"GridY",_y);
                   addParam( macroCreateNewPageElementAndMap,"GridZ",_count);
                   addParam( macroCreateNewPageElementAndMap,"Value",sContentVal64);
                   addParam( macroCreateNewPageElementAndMap,"tags","xyz");
                   addParam( macroCreateNewPageElementAndMap,"TypeID",5 ); //storyView.ThePageElementEditor.TypeID  //"random"
                   addParam( macroCreateNewPageElementAndMap,"StoryID",storyView.StoryController.CurrentStory.ID );
                   addParam( macroCreateNewPageElementAndMap,"StoryOpenedBy", storyView.StoryController.CurrentStory.StoryOpenedBy);
                  
                   processRequest( macroCreateNewPageElementAndMap ); 
                   
                
                }
                else
                {
                    //
                    //update
                    //
                    var macroUpdatePageElementAndMap = newMacro("UpdatePageElement");
                    addParam( macroUpdatePageElementAndMap,"PageElementID",_dbID);
                    addParam( macroUpdatePageElementAndMap,"PageElementMapGUID", _GUID);
                    addParam( macroUpdatePageElementAndMap,"currentPageCursor",storyView.StoryController.getCurrentPageCursor() );
                    addParam( macroUpdatePageElementAndMap,"GridX",_x);
                    addParam( macroUpdatePageElementAndMap,"GridY",_y);
                    addParam( macroUpdatePageElementAndMap,"GridZ",_count);
                    addParam( macroUpdatePageElementAndMap,"StoryID",storyView.StoryController.CurrentStory.ID);
                    addParam( macroUpdatePageElementAndMap,"Value",sContentVal64);
                    addParam( macroUpdatePageElementAndMap,"tags","xyzR");
                    addParam( macroUpdatePageElementAndMap,"TypeID",5);
                    addParam( macroUpdatePageElementAndMap,"StoryOpenedBy", storyView.StoryController.CurrentStory.StoryOpenedBy);
                    
                    processRequest( macroUpdatePageElementAndMap );  
                }
        
    }
    
    this.updateViewHTML =  function (bSaveIfInDB)
    {
           
            
           // alert( "updateViewHTML[[" + bSaveIfInDB + "]]");
            if( bSaveIfInDB == null) bSaveIfInDB = false;
            
            
           // alert(" updateViewHTML :: " + bSaveIfInDB );
            
            var idBits = _id.split('_');
            var indexNumber = idBits[1];
            
            //alert("indexNumber " + indexNumber);
            
           
            var key = "txtSource_propPanel_" + indexNumber;
            var s = null;

            if( _srcText != null)
            {
                s =  _srcText.value;
            }
            else
            {
                s = _origVal;
            }

            if(s.trim().length == 0) return;
            
            s = applyHTMLai(s);
           
            var srcEventID = this.id;
            
            var srcEventBits = srcEventID.split("_");
            // TheLogger().log("src :: " + srcEventBits[0],"warn");
            
            var sContentVal64 = TheUte().encode64( s );
             
            if( srcEventBits[0] == "Update" || bSaveIfInDB == true )
            {
                if(_dbID == -1)
                {
                
                    if(bSaveIfInDB==true) return;  // do not save positions of new elements!
                    
                    
                    
                   //INSERT NEW story element
                   TheLogger().log( "NEW story element@" + _x + "x" + _y,"warn");
                  
                   var currPage = storyView.StoryController.GetPage(storyView.StoryController.getCurrentPageCursor());

                   var macroCreateNewPageElementAndMap = newMacro("CreateNewPageElementAndMap");
                   addParam( macroCreateNewPageElementAndMap,"currentPageCursor",storyView.StoryController.getCurrentPageCursor());
                   addParam( macroCreateNewPageElementAndMap,"PageID",currPage.ID);
                   addParam( macroCreateNewPageElementAndMap,"sevTmpGUID",_GUID);
                   addParam( macroCreateNewPageElementAndMap,"GridX",_x);
                   addParam( macroCreateNewPageElementAndMap,"GridY",_y);
                   addParam( macroCreateNewPageElementAndMap,"GridZ",_count);
                   addParam( macroCreateNewPageElementAndMap,"Value",sContentVal64);
                   addParam( macroCreateNewPageElementAndMap,"tags","xyz");
                   addParam( macroCreateNewPageElementAndMap,"TypeID",5 ); //storyView.ThePageElementEditor.TypeID  //"random"
                   addParam( macroCreateNewPageElementAndMap,"StoryID",storyView.StoryController.CurrentStory.ID );
                   addParam( macroCreateNewPageElementAndMap,"StoryOpenedBy", storyView.StoryController.CurrentStory.StoryOpenedBy);
                   
                   //alert( serializeMacroForRequest( macroCreateNewPageElementAndMap) );
                   processRequest( macroCreateNewPageElementAndMap ); 
                   
                    
                    
                   
                }
                else
                {
                   //UPDATE EXISTING page element
                   TheLogger().log( "EXISTING story element","warn");

                   var buttonClicked = document.getElementById(srcEventID);
                   buttonClicked.className = "clsButtonActionClicked";
                   buttonClicked.value = " saving ... ";
                   buttonClicked.disabled = true;

                   var macroUpdatePageElementAndMap = newMacro("UpdatePageElement");
                   addParam( macroUpdatePageElementAndMap,"PageElementID",_dbID);
                   addParam( macroUpdatePageElementAndMap,"PageElementMapGUID", _GUID);
                   addParam( macroUpdatePageElementAndMap,"currentPageCursor",storyView.StoryController.getCurrentPageCursor() );
                   addParam( macroUpdatePageElementAndMap,"GridX",_x);
                   addParam( macroUpdatePageElementAndMap,"GridY",_y);
                   addParam( macroUpdatePageElementAndMap,"GridZ",_count);
                   addParam( macroUpdatePageElementAndMap,"StoryID",storyView.StoryController.CurrentStory.ID);
                   addParam( macroUpdatePageElementAndMap,"Value",sContentVal64);
                   addParam( macroUpdatePageElementAndMap,"tags","xyzR");
                   addParam( macroUpdatePageElementAndMap,"TypeID",5);
                   addParam( macroUpdatePageElementAndMap,"StoryOpenedBy", storyView.StoryController.CurrentStory.StoryOpenedBy);
                  
                   //alert( serializeMacroForRequest( macroUpdatePageElementAndMap) );
                   processRequest( macroUpdatePageElementAndMap );  
                   
                   buttonClicked.value = "Save";
                   buttonClicked.className = "clsButtonAction";
                   buttonClicked.disabled = false;
                   
                   
                }

                
                //hide prop panel
                var widgetProp =  document.getElementById("widgetProperties_" + indexNumber);
                if( widgetProp != null )
                    widgetProp.style.display = "none";
                    
                //hide prop cintext buttons
                
                              
                var widgetPropCB =  document.getElementById("widgetPropertiesContext_" + indexNumber);
                if( widgetPropCB != null )
                    widgetPropCB.style.display = "none";
                
                
                //$TODO: correct way to break state of editor open? 2
                //set busy false
                storyView.setBusy(0);

             } //~update db
             
             


            var viewToUpdate = document.getElementById("widgetView_" + indexNumber);
            if( _srcText != null)
            {
                _srcText.value = s;
            }

            viewToUpdate.innerHTML = s;
            viewToUpdate.style.display = "block";

    }

    
   

    
    this.getHandleBar = getHandleBar;
    this.HandleDoubleClick = HandleDoubleClick;
    
    

}


function storyElemViewInit()
{

    
    //TheLogger().log( "init SEV " + this.id + " @ " + this.getX() + " x " + this.getY() ,"warn");
    
    
    var values = new Array();

    this.container = document.createElement("DIV");
    this.container.id = "widget_" + this.count; 

    var _dvProperties = null;
    var _dvMidPane = null;
    var _dvPropertiesContextButtons = null;
    var _cmdToggleSrc = null;
    var _NewEditor = null;
    
    var bShowEditStuff = false;
    
    //if( storyView.StoryController.CurrentStory.CanEdit == 1 )
    //     bShowEditStuff = true;
    
    if ( this.bCanEdit == 1 )
          bShowEditStuff = true;
         
    if ( storyView.getViewLocked() == true )
        bShowEditStuff = false;
        
    
    
   
    
    if( bShowEditStuff == true )
    {
       
        var _dvHandleBar = this.getHandleBar(this.count ,  this.by );

       if(this.DateAdded != null)
            _dvHandleBar.title = "Added @ " + this.DateAdded;    
       else
            _dvHandleBar.title = "Unsaved Element (" + this.id + ")";
         

        _dvPropertiesContextButtons = document.createElement("DIV");
        _dvPropertiesContextButtons.id ="widgetPropertiesContext_" + this.count;
        this.contextButtons = _dvPropertiesContextButtons;
        
        _dvPropertiesContextButtons.style.display = "none";
        
        //buttons for the properties panel
        var cmdPreview = TheUte().getButton("Preview_" + this.id,"Preview","Preview any changes" ,this.updateViewHTML,"clsButtonAction")
        this.cmdUpdate = TheUte().getButton("Update_" + this.id,"Save","Save any changes",this.updateViewHTML,"clsButtonAction")
        // this.cmdUpdate.disabled = true;
         var _cmdCopyClip = TheUte().getButton("cmdCopy_" + this.count," Copy","Copy the source of this element to clipboard",this.cmdCopyClip,"clsButtonAction");


    
        var buttons = new Array();
        buttons.push ( cmdPreview );
        buttons.push ( this.cmdUpdate );
        buttons.push ( _cmdCopyClip );
        var buttonGrid = newGrid2("gridButtons",1,buttons.length,buttons,1);
        buttonGrid.init( buttonGrid );
        
        _dvPropertiesContextButtons.appendChild ( buttonGrid.gridTable );

        var _dvToggleSrc = document.createElement("DIV");
        _dvToggleSrc.id = "dvToggleSrc_" + this.count;
        _dvToggleSrc.className = "clsToggleSrc";
        
        _cmdToggleSrc = TheUte().getButton("cmdToggleSrc_" + this.count,"edit","edit source HTML",null,"clsButtonSEV")
        _dvToggleSrc.appendChild( _cmdToggleSrc );
        
        var _NewEditor = TheUte().getButton("cmdToggleSrc2_" + this.count,"ne","edit via rich text box",null,"clsButtonSEV")
        _dvToggleSrc.appendChild( _NewEditor );
        
         storyView.setCurrElement( this );
        
        var _cmdMoveBack = TheUte().getButton("cmdMoveDown_" + this.count," Back","Send this Element to the Back",this.cmdMoveBack,"clsButtonSEV");
        var _cmdMoveFront = TheUte().getButton("cmdMoveUp_" + this.count,"front","Bring this Element to the Front",this.cmdMoveFront,"clsButtonSEV");
        var _cmdDelete = TheUte().getButton("cmdDelete_" + this.count,"delete","Delete this element",this.cmdDeleteElement,"clsButtonSEV");
       
        //touchy buttons
        _cmdToggleSrc.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
        }
         _NewEditor.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
        }
        _cmdMoveFront.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
        }
        _cmdDelete.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
        }
 
        var midPanelValues = new Array();
        midPanelValues.push( _dvToggleSrc );
        midPanelValues.push(_cmdMoveFront );
        
        midPanelValues.push(_cmdDelete );    
        //midPanelValues.push( _dvPropertiesContextButtons );

        _dvMidPanel = document.createElement("DIV");
        _dvMidPanel.className = "clsMidPanel";
        _dvMidPanel.id = "dvMidPanel_" + this.count;
        
        
        _dvMidPanel.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true;  
          }
        
        var midPanelGrid = newGrid2("midPanelGrid",1,5,midPanelValues);
        midPanelGrid.init( midPanelGrid );

        _dvMidPanel.appendChild( midPanelGrid.gridTable );
        
        _dvProperties = document.createElement("DIV");
        _dvProperties.id = "widgetProperties_" + this.count;
        _dvProperties.className = "clsWidgetProperties";
        
        
       
        _dvProperties.appendChild( this.getPropertiesPanel("propPanel_" + this.count) );
         _dvProperties.appendChild( _dvPropertiesContextButtons );
        
        
        _dvProperties.appendChild(this.dvInfo);
        
        //hide on init
        _dvProperties.style.display = "none";
         if( this.bShowEditor == true )
         _dvProperties.style.display = "block";
        
        _dvProperties.ondblclick = function (ev) {
            ev = ev || window.event;
            ev.cancelBubble = true; 
          }
          
          values.push( _dvHandleBar );
          
     } //~ can edit
    
    
    
    
    this.dvWidgetView.id = "widgetView_" + this.count;
    this.dvWidgetView.className = "clsWidgetView"; 
    this.dvWidgetView.title = " added by " + this.by + " @ " + this.DateAdded; 

    
    values.push( this.dvWidgetView );
    
    
    
    if( bShowEditStuff == true )
    {
        //editor only
        
        values.push( _dvMidPanel );
        values.push( _dvProperties );
        
        var toggleCount = 0;
        var dvView = this.dvWidgetView;
    
        

        _NewEditor.onclick = this.NewEdit_click;
        
        _cmdToggleSrc.onclick = function ()
        {


            if(toggleCount == 0)
            {
                //1
                toggleCount = 1;
               //show properties
                _dvProperties.style.display = "block";
                dvView.style.display = "block";
                _dvPropertiesContextButtons.style.display = "block";
                
            }
            else
            if( toggleCount == 1)
            {
                //2
                toggleCount = 2;
                //hide properties
                _dvProperties.style.display = "none";
                _dvPropertiesContextButtons.style.display = "none";
                
            }
            else
            if(toggleCount == 2)
            {
                //??? this mode useful?
                //must be 3
                toggleCount = 0;
                dvView.style.display = "none";
                _dvProperties.style.display = "block";
                _dvPropertiesContextButtons.style.display = "block";
                
            }
            
            
            
           
        }
    }


    var oGrid3 = newGrid2("containerGrid_" + this.count ,values.length,1,values,0,this.getGUID() );
    oGrid3.init( oGrid3 );


    this.setContainer(oGrid3.gridTable);
    this.setGridContainer(oGrid3);
    this.setContID( "widget_" + this.count);
    this.setContClassName("clsStoryViewWidget_none");

  
    
    if( bShowEditStuff == true )
    {
        //editor only
        //add drag drop capabilities to the story element view
        var c = this.getContainer();
        var dd = new YAHOO.example.DDOnTop( c );
        dd.setHandleElId(this.handle.id);
        dd.setXConstraint(1000, 1000, 25);
        dd.setYConstraint(1000, 1000, 25);
        
    }
    
    this.reportInfo();
    
    //all new SEV's must be registered!!!!
    storyView.registerSEV( this );
    
}



function applyHTMLai( s )
{
    var tmp = "";
    
    //filter out disallowed chars
    s = TheUte().filterText(s);

    if(s.indexOf("<") != -1)
    {
        //most likely markup LEAVE alone
        // $to do: better to use-- regex to do this
        //http://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx
        //</?\w+((\s+\w+(\s*=\s*(?:".*?"|'.*?'|[^'">\s]+))?)+\s*|\s*)/?>
     }
     else
     {
        //NON MARKUP
        
        if(s.indexOf("http") != -1 || s.indexOf("HTTP") != -1 )
        {
            //url
            if(s.indexOf(".jpg") != -1 || s.indexOf(".gif") != -1 || s.indexOf(".png") != -1
              || s.indexOf(".JPG") != -1 || s.indexOf(".jpeg") != -1 
              || s.indexOf(".JPEG") != -1 || s.indexOf(".GIF") != -1 || s.indexOf(".PNG") != -1)
            {
                //adorn with IMG tags
                tmp = "<IMG src='" + s + "' />";
                return tmp;
            }
            else
            {
                //could be a link tag
                 tmp = "<A HREF='" + s + "' target='_TOP'>" + s + "</A>";
                return tmp;
            
            }

        
        }
        else
        {
            //adorn with PRE tags
             tmp = "<PRE>\r\n" + s + "\r\n</PRE>";
            return tmp;
        }
     }
   return s;
}




function getHandleBar(count, title)
{
    var _dv = document.createElement("DIV");
    
    this.handle.id = "handle_" + count;
    this.handle.className = "clsWidgetBoxHandle";
    this.handle.ondblclick = function (ev)
    {
        ev = ev || window.event;
        ev.cancelBubble = true;
    }
    

    if(title != null)
    {
        var txtTitle = document.createTextNode( title );
        this.handle.appendChild( txtTitle );
    
    }
    
    var values = new Array();
    values.push( this.handle );
    
   
    var oGrid3 = newGrid2("handleGrid_" + count ,1,1,values);
    oGrid3.init( oGrid3 );
    _dv.appendChild( oGrid3.gridTable );
    return _dv;
}


function getPropertiesPanel(id)
{
    var values = new Array();

    values.push( this.srcText );
    
    
    var oGrid3 = newGrid2(id,1,1,values);
    oGrid3.init( oGrid3 );
    
    return oGrid3.gridTable;

}

function HandleDoubleClick(ev)
{


//not used as handle double click seemed unintuitive
   alert("deprecated method:: HandleDoubleClick() ");

}


