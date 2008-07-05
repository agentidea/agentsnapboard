    /*

    AgentIdea - Story corecommands.js
    
*/

function trace(msg)
{
    //alert(msg);
}

function cmdHeartBeat (macro)
{
    var timestamp = TheUte().decode64( getParameterVal("timestamp",macro));
    var msg = TheUte().decode64( getParameterVal("msg",macro));
    var seq = getParameterVal("seq",macro) *1;
    var lastStorySeq = getParameterVal("lastStorySeq",macro) *1;
    
    if( seq > maxHeartbeatsBeforeRefresh )
    {
       // alert("story timed out, refreshing ... ");
        location.href = location.href;
        return;
    
    }
    
   
    
    var UserCurrentTxID = TheUte().decode64( macro.UserCurrentTxID );
    
    var bChanged = getParameterVal("bChanged",macro) *1;
    
    if(bChanged == 1)
    {
        var cmdJSON64 = getParameterVal("cmd64",macro);
       // alert(cmdJSON64);
        var cmdJSON = TheUte().decode64( cmdJSON64 );
        
         var resMacros = null;
         try
         {
            
            resMacros = eval('(' + cmdJSON + ')');
            
            var lenGroup = resMacros.cmds.length;
            var numCmds = resMacros.numMacroEnvelopes;
            lastStorySeq = resMacros.lastUpdateSeq;
            
           
            var k = 0;
            for(;k<lenGroup;k++)
            {
            
                //run macros
                var curMacros = resMacros.cmds[k];

                var i = 0;
                for ( i = 0 ;i<curMacros.macros.length;i++)
                {
                    var UserRunningTxID = TheUte().decode64( curMacros.macros[i].UserCurrentTxID );
                    //alert(UserCurrentTxID + " :: " + UserRunningTxID );
                    if(UserCurrentTxID == UserRunningTxID)
                    {
                        msg += " MINE IGNORE ";
                    }
                    else
                    {
                        try
                        {
                            var sm = curMacros.macros[i];
                            executeLocalCommand( sm );
                        }
                        catch(ee)
                        {
                            //$to do: understand what the error was.
                            location.href = location.href;
                            
                            alert("TX2 error " + ee.description + "\r\n" + cmdJSON );
                        }
                    }
                    
                } //run macros
              } //~run groups

         }
         catch(e)
         {
         
            //$to do: understand what the error was.
         
            
            alert("TX eval error :: " + e.description );
            
            location.href = location.href;
            
         }
        
        storyView.setStorySeq(lastStorySeq);
        
    }
    
    storyView.setSeq(seq);
    //storyView.displayMessage("session:" + UserCurrentTxID + "\r\nmsg: " + msg +  "\r\nseq: " + seq ); //"\r\nt: " + timestamp +
    //storyView.displayMessage( timestamp +  "\r\n[" + seq + "]"); //"\r\nt: " + timestamp +
    //storyView.displayMessage( timestamp ); //"\r\nt: " + timestamp +
}


function cmdRenderChatMsg(macro)
{
        var msg64 = getParameterVal("msg64",macro);
        var severity = getParameterVal("severity",macro);
        msg = TheUte().decode64(msg64);
        storyView.displayMessage( msg );

}

function cmdRemovePageElement (macro)
{
    var storyElementViewID = getParameterVal("storyElementViewID",macro);
    var PageID = getParameterVal("PageID",macro);
    var pemGUID = getParameterVal("pemGUID", macro);

   //alert(" item @ sev_" +storyElementViewID+ " removed from page " +  PageID);

    //update underlying model
   //var currPage = storyView.StoryController.CurrentPage();
   var currPage = storyView.StoryController.GetPage( PageID );
   var pem = storyView.StoryController.FindPageElementMapByGUID( currPage, pemGUID );
   if(pem != null)
   {
        pem.visible = false;
   }
    
    if(PageID == storyView.StoryController.getCurrentPageCursor())
    {
       
         var sev = storyView.sevByGuid( pemGUID );
        if(sev != null)
        {
            sev.hide();
        }
        else
        {
            alert(pemGUID + " no sev found");
        }
    }
    else
    {
        //alert("removed on another page " + PageID);
    }
    
   
        

}

function cmdDisplayAlert(macro)
{
    var msg = getParameterVal("msg",macro);
    alert(TheUte().decode64(msg));
}

function cmdNullMacro(macro)
{
   //do nothing
}


function cmdProcessStoryPageOrder(macro)
{

    var storyID = getParameterVal("StoryID",macro) *1;
    //alert("page ordering have been changed for story - " + storyID);
    location.href = "./StoryEditor4.aspx?StoryID=" + storyID;

}

function cmdReloadLog(macro)
{
    oFauxIRC.refresh_msg();
}


function cmdLoadMediaItems(macro)
{
    
    var mediaItemsJSON = TheUte().decode64(getParameterVal("mediaItemsJSON64",macro));
    var viewPortID = getParameterVal("viewPortID",macro);
    var mi = eval("(" + mediaItemsJSON + ")");
    
    if( mi.envelope.length > 0)
    {
//        if(oStoryEditor != null)
//        {
//            oStoryEditor.MainPalette.displayMediaItems( mi );
//        }
        

           //alert(" media items " + mi.envelope.length + " " + viewPortID );
            var libPort = storyView.getLibViewer().getLibPort(viewPortID);
            libPort.updateMedia ( mi );
 
    }
}

function cmdProcessNewInvites(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    location.href = "MessageManager.aspx";
}
function cmdProcessNewMails(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    location.href = "MessageManager.aspx";
}


function cmdRefreshMessages(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    location.href = "MessageManager.aspx";
}


function cmdAddNewUser(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    location.href="Platform2.aspx";
    
}
function cmdSaveExistingUser(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    //alert("about to signout ... ");
    location.href="ListMembers.aspx";
}
function cmdProcessStoryClone(macro)
{
    var NewStoryID = getParameterVal("NewStoryID",macro);
    location.href = "./StoryEditor4.aspx?StoryID=" + NewStoryID;
    
}
function cmdProcessNewEmail(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    var guid = getParameterVal("guid",macro);
    alert(msg + " with UniqueID of " + guid);
    
    location.href = "SendEmail.aspx?emailGuid=" + guid;
    
}
function cmdProcessTerminateProfile(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    location.href = "./../default.aspx?msg=Goodbye";
}
function cmdProcessChangeOfProfile(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    alert(msg);
    location.href = "ListMembers.aspx";
}

function cmdProcessEmailSent(macro)
{
    var msg = TheUte().decode64(getParameterVal("msg",macro));
    var guid = getParameterVal("guid",macro);
    alert(msg + " with UniqueID of " + guid);
    
    //location.href = "ListMembers.aspx";
    location.href = "MessageManager.aspx";
}

function cmdProcessEmailsSent(macro)
{

   var msg = TheUte().decode64(getParameterVal("msg",macro));
   alert("email status " + msg);
   
   location.href = location.href;  //refresh!

}


function cmdRefreshView(macro)
{
     var ViewID = getParameterVal("ViewID",macro);
     var userOrGroup = getParameterVal("userOrGroup",macro);
     
     var ids = getParameterVal("ids",macro);
     var names = getParameterVal("names",macro);
     var count = getParameterVal("count",macro) *1;
     var divToRefresh = TheUte().findElement("div_" + ViewID,"div");
    
     //alert(" found div " + divToRefresh.id );
     
     var decode = false;
    if( userOrGroup == "Groups" )
    {
        decode = true;
    }
     
    TheUte().removeChildren( divToRefresh );

    var values = new Array();
   
    if(count > 0 )
    {
        var _selItems = buildSelItems(ids,names,decode,null);
        var  _selWidget = new selWidget("selWidget_" + ViewID);
        _selWidget.init(_selItems,null,8,"clsMultiSelBox");    
        values[1] = _selWidget.selWidget;
    }

    var oGrid2 = newGrid2("panel_" + ViewID,2,1,values,3);
    oGrid2.init( oGrid2 );

    divToRefresh.appendChild( oGrid2.gridTable );
    
}

function cmdRefreshPermissionsView(macro)
{
     var ViewID = getParameterVal("ViewID",macro);
     var userOrGroup = getParameterVal("userOrGroup",macro);
     var editorOrViewer = getParameterVal("editorOrViewer",macro);
     
     oStorySharingControl.refreshPermissionMatirxData(userOrGroup,editorOrViewer);
}

function cmdProcessNewGroup(macro)
{
    var newGroupID = getParameterVal("newGroupID",macro);
    var groupName = getParameterVal("groupName",macro);
    groupName = TheUte().decode64( groupName );
    var groupGUID = getParameterVal("groupGUID",macro);
    
    alert( " your new group " + groupName + " added");
    
    location.href = "ManageUsersGroups.aspx";
    
    
}

function cmdAddUsersToGroup(macro)
{
    var msg = TheUte().decode64( getParameterVal("msg",macro));
    //alert( msg );
    
    //update the list box
    loadUsersIntoGroup( getCurrSelectedGroupID() );
}

function cmdRemoveUsersFromGroup(macro)
{
    var msg = TheUte().decode64( getParameterVal("msg",macro));
    //alert( msg );
    
    //update the list box
    loadUsersIntoGroup( getCurrSelectedGroupID() );
}


function cmdLoadUsersGroups(macro)
{

    var userIDs = getParameterVal("userIDs",macro);
    var userNames = getParameterVal("userNames",macro);
    var count = getParameterVal("count",macro)*1;

    TheUte().removeChildren( divUsersInGroup );

    var values = new Array();
    values[0] = document.createTextNode("group users (" + count + ")");
    if(count > 0 )
    {
        var selItemsUsersInGroups = buildSelItems(userIDs,userNames,false,null);
        var  selWidgetUsersInGroup = new selWidget("selWidgetUsersInGroup");
        selWidgetUsersInGroup.init(selItemsUsersInGroups,selUsersGroupsChanged,8,"clsMultiSelBox");    
        values[1] = selWidgetUsersInGroup.selWidget;
    }

    var oGrid2 = newGrid2("groupsusers",2,1,values,1);
    oGrid2.init( oGrid2 );

    divUsersInGroup.appendChild( oGrid2.gridTable );
    
    
}




function cmdRedirect(macro)
{
    var url = getParameterVal("url",macro);
    location.href = url;
}

function cmdProcessNewPage(macro)
{
    var newPageID = getParameterVal("newPageID",macro);
    var pageName = getParameterVal("pageName",macro);
    var pageGUID = getParameterVal("pageGUID",macro);
    var y = getParameterVal("gridCols",macro);
    var x = getParameterVal("gridRows",macro);
    var z = getParameterVal("GridZ",macro) *1;
    
    var neuPagina = newPage(pageName,x,y);   
    neuPagina.ID = newPageID;
    neuPagina.GUID = pageGUID;
    
    if( z == -1 )
    {
        oStoryEditor.StoryController.AddPage( neuPagina );
        oStoryEditor.refreshPageNavList();
        oStoryEditor.page_last();
    }
    else
    {
        neuPagina.gridZ = z;
        storyView.StoryController.AddPage ( neuPagina );
        storyView.StoryController.pageLast();
        
    }
}

function cmdProcessNewPageElement(macro)
{
    var newPageElementID = getParameterVal("newPageElementID",macro) *1;
    var newPageElementMapID = getParameterVal("newPageElementMapID",macro) *1;
    var CurrentPageCursor = getParameterVal("CurrentPageCursor",macro) *1;
    var x = getParameterVal("GridX",macro) *1;
    var y = getParameterVal("GridY",macro) *1;
    var z = getParameterVal("GridZ",macro) *1;
    var val64 = getParameterVal("val64",macro);
    var GUID = getParameterVal("GUID",macro);
    var DateAdded =   TheUte().decode64(getParameterVal("DateAdded",macro));
    
    
    var originUser = TheUte().decode64(getParameterVal("originUser",macro));
    
    if( z == -1)
    {
        //old grid method of adding page elements
        var newPageElem = newPageElement(newPageElementID, oStoryEditor.ThePageElementEditor.Type,oStoryEditor.ThePageElementEditor.TypeID, TheUte().encode64( oStoryEditor.ThePageElementEditor.msgBox.value),null );
        newPageElem.Tags = oStoryEditor.ThePageElementEditor.tagBox.value;

        oStoryEditor.StoryController.AddPageElement( oStoryEditor.StoryController.CurrentStory , newPageElem );
        
        var page = oStoryEditor.StoryController.GetPage( CurrentPageCursor );
        oStoryEditor.ThePageElementEditor.CurrentPageElement = newPageElem;

        var oPageElementMap = newPageElementMap(newPageElementID,x,y,null);
        oStoryEditor.StoryController.AddPageElementMap( page, oPageElementMap );
            
        oStoryEditor.decorateGrid();
        oStoryEditor.refreshPageNavList();
        
        oStoryEditor.ThePageElementEditor.bCommited = true;

    }
    else
    {
        //cartesian method.
        var newPageElem = newPageElement(newPageElementID,"random",5, val64, originUser);
        storyView.StoryController.AddPageElement( storyView.StoryController.CurrentStory , newPageElem );
        
        //upate the structure.
        var page = storyView.StoryController.GetPage( CurrentPageCursor );
        var oPageElementMap = newPageElementMap(newPageElementID,x,y,z,GUID);
        storyView.StoryController.AddPageElementMap2( page, oPageElementMap );

        //update the storyelementview
        
        if(CurrentPageCursor == storyView.StoryController.getCurrentPageCursor())
        {
            //current page is visible, update view
        
        
            var sevActual = null;
            
            var sevTmpGUID = getParameterVal("sevTmpGUID",macro);
            var sevTmp = storyView.sevByGuid( sevTmpGUID );
            
            if(sevTmp != null)
            {
                //has a sev already
                sevActual = sevTmp;
            }
            else
            {
                //no sevTmp see if there is a real GUID one
                var sev = storyView.sevByGuid( GUID );
            
                if(sev == null)
                {
                    //no entry in SEV[]
                    //can we add and update a view that is not yet created?
                    var pageElement = null;
                    var s = " pageElement = storyView.StoryController.CurrentStory.PageElements.pageElement_" + oPageElementMap.PageElementID;
                    eval(s);
                    var pageElementValue =  TheUte().decode64( newPageElem.Value );
                    var newWidget = addNewWidget( oPageElementMap.X,oPageElementMap.Y,oPageElementMap.Z,newPageElem.ID,oPageElementMap.GUID,newPageElem.BY,false,null,0 );
                    sevActual = newWidget;
                    
                    newWidget.updateSrcText( pageElementValue );
                    newWidget.updateViewHTML(newWidget.id);
                    

                      
                }
                else
                {
                    alert(" SEV ALWAWS NULL??????? im cozy in sev["+sev.id+"] --  @guid - " + GUID);
                }
            
            }
            

                //sevActual.updateTitle( "sev_" + z + " " + originUser);
                sevActual.updateTitle( "Added @ " + DateAdded);
                sevActual.updateGUID( GUID );
                sevActual.updateHandle( originUser );
                sevActual.updateDBID( newPageElementID );
                sevActual.reportInfo();
            
            
            
          }//~view visible
          else
          {
            //alert("element changed on page :: " + CurrentPageCursor );
          }
           
     }//~cartesian method
}

function cmdUpdatePageElementXYZ(macro)
{
    var pemGUID = getParameterVal("pemGUID",macro);
    var x = getParameterVal("GridX",macro) *1;
    var y = getParameterVal("GridY",macro) *1;
    var z = getParameterVal("GridZ",macro) *1;
    var tsx = getParameterVal("tsx",macro);
    var CurrentPageCursor = getParameterVal("CurrentPageCursor",macro)*1;
    var changeZ = getParameterVal("changeZ",macro)*1;
    
    var currPage = storyView.StoryController.GetPage( CurrentPageCursor );
    var pem = storyView.StoryController.FindPageElementMapByGUID( currPage, pemGUID );
    if(pem != null)
    {
        pem.X = x;
        pem.Y = y;
        pem.Z = z;
    }
    

    if( tsx == gUserCurrentTxID )
    {
        //ignore as element is already moved. x y and z
    }
    else
    {
        if( CurrentPageCursor == storyView.StoryController.getCurrentPageCursor() )
        {
            //on the same page on the other client update!  
            var sev = storyView.sevByGuid( pemGUID );
            if(sev != null)
            {
                sev.setCoords(x,y);
                sev.reportInfo();
                sev.place();      //move x,y
                
                
                if(changeZ == 1)      
                {
                       
                    var indexNumber = sev.count;  
                    var _attachDivToMove = document.getElementById("divAttacher_" + indexNumber);
                    
                    var _bodyREF = document.getElementById("TheHook");
                    _bodyREF.appendChild( _attachDivToMove ); //move z
                }
            }
        }
    }
    
    

}
   


function cmdUpdatePage(macro)
{
    var dateUpdated = getParameterVal("dateUpdated",macro);
    
    
    try
    {
        oStoryEditor.decorateGrid();
    }
    catch(e)
    {
        //was most likely the cartesian grid editor!
        //do nothing.
    }
}


function cmdRemovePage(macro)
{
    var pageID = getParameterVal("PageID",macro) *1;
    var msg64 =  getParameterVal("msg64",macro);
    
    
    
    try
    {
        alert(TheUte().decode64(msg64));
        oStoryEditor.page_first();
    } 
    catch(e)
    {
        //need to remove page from model?
        // hard to remove pages from model
        // easier to refresh
        location.href = location.href;
       // storyView.StoryController.gotoPage(0);
    }
        
        

}

function cmdUpdatePageElement(macro)
{
    var pageElementID = getParameterVal("pageElementID",macro) *1;

    var x = getParameterVal("GridX",macro) *1;
    var y = getParameterVal("GridY",macro) *1;
    var z = getParameterVal("GridZ",macro) *1;
    var pemGUID = getParameterVal("pemGUID",macro);
    var CurrentPageCursor = getParameterVal("CurrentPageCursor",macro) *1;

    if( z == -1)
    {
        //old table method
        var _currStory = oStoryEditor.StoryController.CurrentStory;

        if( _currStory == null)
        {
            alert("current Story is null ");
        }
        var _cursor = oStoryEditor.StoryController.CurrentPageCursor;

        if(_cursor == null)
        {
            alert("page cursor was null");
        }

        var _page = oStoryEditor.StoryController.GetPage( _cursor);

        if(_page == null)
        {
            alert("page was null");
        }

        var currPageElem = oStoryEditor.StoryController.FindPageElementByCoord( _currStory, _page,x,y);

        if( currPageElem == null)
        {
            var key = pageElementID + " , " + x + " ," + y;
            alert ("currPageElem was NULL.  The KEY " + key + " COULD NOT BE not found?");
        }
        else
        {
            currPageElem.Value = TheUte().encode64( oStoryEditor.ThePageElementEditor.msgBox.value );
            currPageElem.Type  = oStoryEditor.ThePageElementEditor.Type;
            currPageElem.Tags = oStoryEditor.ThePageElementEditor.tagBox.value;
        }

        oStoryEditor.decorateGrid();
        oStoryEditor.refreshPageNavList();
    
    }
    else
    {

       //update underlying model
       var val =  getParameterVal("val64",macro);
       
       var currPage = storyView.StoryController.GetPage( CurrentPageCursor );
       var pem = storyView.StoryController.FindPageElementMapByGUID( currPage, pemGUID );
       if(pem != null)
       {
           pem.X = x;
           pem.Y = y;
           pem.Z = z;
       }
       
       var pe = storyView.StoryController.FindPageElement(storyView.StoryController.CurrentStory,currPage,pageElementID);
       pe.Value = val;

        if( CurrentPageCursor == storyView.StoryController.getCurrentPageCursor() )
        {
            //on the same page so update!  
            var sev = storyView.sevByGuid( pemGUID );
            if(sev != null)
            {
                val = TheUte().decode64(val);
                sev.updateSrcText(val);
                sev.updateWidgetView(val);
                sev.setCoords(x,y);
                sev.place();
                sev.reportInfo();
            }
        }
    }
}


function cmdProcessStoryState(macro)
{
    var storyID = getParameterVal("StoryID",macro) *1;
    var storyState = getParameterVal("StoryState",macro) *1;
    var stateName = getParameterVal("StateName",macro);
    var OperatorUserName = getParameterVal("OperatorUserName",macro);
    
    
   // trace(" Story(" + storyID + ") marked as " + stateName + " by operator (" + OperatorUserName + ").");
    
    if( storyState == 5 )
        window.location.href = "./Platform2.aspx?msg=Story%20id%20" + storyID + "%20deleted";
    else
        window.location.href = window.location.href;

}

// local functions called locally?

//
// clear
//
function cmdCLEAR(macro)
{
    storyView.hideAllSEV();
}

//
// cls [clear alias]
//
function cmdCLS(macro) { cmdCLEAR(macro); }

//
// show
//
function cmdSHOW(macro)
{
    storyView.showAllSEV();
}

//
// mark
//
function cmdMARK(macro)
{
    storyView.markAllSEV();
}

//
// unmark
//
function cmdUNMARK(macro)
{
    storyView.unmarkAllSEV();
}


//
// strat
//

function cmdSTRAT(macro)
{
    cmdADDSTRATEGY(macro);
}

//
// AddStrategy
//
function cmdADDSTRATEGY(macro)
{
 
    if( macro.parameters.length != 2 )
    {
        var help = "incorrect number of parameters expected 1 :";
        help += "\r\n\r\n";
        help += "\t STRATEGY NAME";
        alert( help );
        return;
    }
    var _guid = macro.parameters[0].value;
    var _name = macro.parameters[1].value;
    _name = TheUte().encode64( _name );
    
    
    var AddStrategy = newMacro("AddStrategy");
    addParam( AddStrategy,"name64", _name);
    addParam( AddStrategy,"sevGUID", _guid);
    
    processRequest( AddStrategy );
    
    
    
    
}

function cmdStrategyAdded(macro)
{
    //remote callback that strat added
    var _dbID = macro.parameters[0].value;
    var stratGUID = macro.parameters[1].value;
    var sevGUID = macro.parameters[2].value;

    var sev = storyView.sevByGuid(sevGUID);
    
    if( sev != null)
    {
    
        var url = "./StrategyPortal.aspx";
        url += "?stratGUID=";
        url += stratGUID;

    
        var txt = "";
        txt += "<IFRAME ";
        txt += " src='";
        txt += url.trim();
        txt += "'";
        txt += " style='";
        txt += "width:" + 900;
        txt += ";height:" + 500;
        txt += "'";
        txt += ">";
        txt += "</IFRAME>";
        
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }

}


//
// iframe
//
function cmdIFRAME(macro)
{
    
    if( macro.parameters.length != 4 )
    {
        var help = "incorrect number of parameters expected 3 :";
        help += "\r\n\r\n";
        help += "\t URL";
        help += "\r\n";
        help += "\t WIDTH";
        help += "\r\n";
        help += "\t HEIGHT";
        help += "\r\n";
        help += "";
        alert( help );
        return;
    }
    
    var _guid = macro.parameters[0].value;
    var _url = macro.parameters[1].value;
    var w = macro.parameters[2].value;
    var h = macro.parameters[3].value;

    
    var sev = storyView.sevByGuid(_guid);
    
    if( sev != null)
    {
        var txt = "";
        txt += "<IFRAME ";
        txt += " src='http://";
        txt += _url.trim();
        txt += "'";
        txt += " style='";
        txt += "width:" + w;
        txt += ";height:" + h;
        txt += "'";
        txt += ">";
        txt += "</IFRAME>";
        
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }
}

//
// link
//
function cmdLINK(macro)
{
    
     if( macro.parameters.length != 4 )
    {
        var help = "incorrect number of parameters expected 3 :";
        help += "\r\n\r\n";
        help += "\t LINK NAME";
        help += "\r\n";
        help += "\t URL";
        help += "\r\n";
        help += "\t TARGET [ SELF | TOP | NEW ]";
        help += "\r\n";
        help += "";
        alert( help );
        return;
    }
    
    var _guid = macro.parameters[0].value;
    var _linkName = macro.parameters[1].value;
    var _url = macro.parameters[2].value;
    var _target = macro.parameters[3].value;

    
    var sev = storyView.sevByGuid(_guid);
    
    if( sev != null)
    {
        var txt = "";
        txt += "<A ";
        
        txt += " href='http://";
        txt += _url.trim();
        txt += "'";
        
        txt += " target='_";
        txt += _target.trim();
        txt += "'"; 
               
        txt += ">";
        txt += _linkName;
        txt += "</a>";
        
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }
}

//
// extdoor
//
function cmdEXTDOOR(macro)
{
    
   
   if( macro.parameters.length != 4 )
    {
        var help = "incorrect number of parameters expected 3 :";
        help += "\r\n\r\n";
        help += "\t LINK NAME";
        help += "\r\n";
        help += "\t STORY ID";
        help += "\r\n";
        help += "\t PAGE INDEX";
        help += "\r\n";
        help += "";
        alert( help );
        return;
    }
    
    var _guid = macro.parameters[0].value;
    var _linkName = macro.parameters[1].value;
    var _storyID = macro.parameters[2].value;
    var _pageID = macro.parameters[3].value;
    
    
    var sev = storyView.sevByGuid(_guid);
    
    if( sev != null)
    {
        var txt = "";
        txt += "<A ";
        
        txt += " href='./StoryEditor4.aspx?StoryID=";
        txt += _storyID;
        txt += "&PageCursor=";
        txt += _pageID;
        txt += "'"; 
        txt += ">";
        txt += _linkName;
        txt += "</a>";
        
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }
}

//
// door
//
function cmdDOOR(macro)
{
    
   
      if( macro.parameters.length != 3 )
    {
        var help = "incorrect number of parameters expected 2 :";
        help += "\r\n\r\n";
        help += "\t LINK NAME";
        help += "\r\n";
        help += "\t PAGE INDEX";
        help += "\r\n";
        help += "";
        alert( help );
        return;
    }
    
    var _guid = macro.parameters[0].value;
    var _linkName = macro.parameters[1].value;
    var _pageID = macro.parameters[2].value;
    
    
    var sev = storyView.sevByGuid(_guid);
    
    if( sev != null)
    {
        var txt = "";
        txt += "<A ";
        
        txt += " href='JavaScript:storyView.StoryController.gotoPage(";
        txt += _pageID;
        txt += ");'"; 
        txt += ">";
        txt += _linkName;
        txt += "</a>";
        
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }
}


//
// box
//
function cmdBOX(macro)
{

    var _guid = macro.parameters[0].value;
    var sev = storyView.sevByGuid(_guid);
    var _x = "250px";
    var _y = "250px";
    var _color = "Transparent";
    
    try
    {
    
       _color = macro.parameters[1].value;
       _x = macro.parameters[3].value;
       _y = macro.parameters[2].value;
       
    }
    catch(exp)
    {
        //ignore
    }

    if( sev != null)
    {
        var txt = "";
        txt += "<DIV ";
        
        txt += " style='";
        
        txt += "width:";
        txt += _x;
        txt += ";";
        
        txt += "height:";
        txt += _y;
        txt += ";";
        
        txt += "background-color:";
        txt += _color;
        txt += ";";
        
        
        txt += "'";
        txt += " > ";
        txt += "</DIV>";
        
                
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }
    
    
}




/*


 color:#;
 
*/


//
// gauze
//
function cmdGAUZE(macro)
{

    var _guid = macro.parameters[0].value;
    var sev = storyView.sevByGuid(_guid);
    var _x = "250px";
    var _y = "250px";
    var _color = "#F85816";
    
    try
    {
    
       _color = macro.parameters[1].value;
       _x = macro.parameters[3].value;
       _y = macro.parameters[2].value;
       
    }
    catch(exp)
    {
        //ignore
    }

    if( sev != null)
    {
        var txt = "";
        txt += "<DIV ";
        
        txt += " style='";
        
        txt += "width:";
        txt += _x;
        txt += ";";
        
        txt += "height:";
        txt += _y;
        txt += ";";
        
        txt += "color:";
        txt += _color;
        txt += ";";
        
        txt += "background-image:url(http://c.myspace.com/Groups/00007/99/15/7775199_m.gif);";
        txt += "background-position:Center Center;";
        txt += "background-attachment:scroll;";
        txt += "background-repeat:repeat;";
        txt += "border-color:333333;";
        txt += "border-style:solid;";
        txt += "border-width:2px;";
        txt += "'";
        txt += " > ";
        txt += "</DIV>";
        
                
        sev.updateSrcText(txt);
    }
    else
    {
        alert("no sev for guid [" + _guid + "]");
    }
    
    
}
