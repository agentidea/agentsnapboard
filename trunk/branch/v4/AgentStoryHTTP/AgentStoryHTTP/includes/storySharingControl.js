
function StorySharingControl(aoStoryController)
{

    var _storyController = aoStoryController;
    this.storyController = _storyController;

    this.init = StorySharingControlInit;

    var _container = null;
    this.container = _container;

    //this.getSPEStoryName             = getSPEStoryName;
    //this.getSPEStoryDesc             = getSPEStoryDesc;
    
    this.getPermissionsMatrix       = getPermissionsMatrix;
    
    this.getUsers                   = speGetUsers;
    this.getGroups                  = speGetGroups;
    
    
    this.getPanel = speGetPanel;
    
    this.getShiftInOutWidget        = getShiftInOutWidget;
    
    this.EditorUserCallback         = EditorUserCallback;
    
    var _userList = null;
    this.userList = _userList;
    var _groupList = null;
    this.groupList = _groupList;

 
    this.refreshPermissionMatirxData = refreshPermissionMatirxData;
    this.loadAllPerms = loadAllPerms;
    
}

function StorySharingControlInit (aoUserList, aoGroupList)
{
    this.groupList = aoGroupList;
    this.userList  = aoUserList;

    var divSpacer = document.createElement("DIV");
    divSpacer.className = "clsStorySharingMatrixHeader";

    var backToStoryHREF =  document.createElement("A");
    backToStoryHREF.href = "./SlideNavigator.aspx?StoryID=" + this.storyController.CurrentStory.ID;
    
    var txt = document.createTextNode("Story ["+ TheUte().decode64( this.storyController.CurrentStory.Title ) + "] access permissions");
    
   backToStoryHREF.appendChild(txt);
   divSpacer.appendChild(backToStoryHREF);
 
    
    var values = new Array();
    values[1] = divSpacer;
    values[2] = this.getPermissionsMatrix();

    var oGrid2 = newGrid2("storySharingEditorMainGrid",3,1,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;

}


function loadAllPerms()
{

    this.refreshPermissionMatirxData("Users","Editors");
    this.refreshPermissionMatirxData("Users","Viewers");
    this.refreshPermissionMatirxData("Groups","Editors");
    this.refreshPermissionMatirxData("Groups","Viewers");

}





function EditorUserCallback()
{
    var idBits = this.id.split('_');
    var direction = idBits[0];
    var operation = idBits[1];
    var role = idBits[2];
    
    var sDelims = "";
    
    //get selected items
    var selWidgetActingOn = TheUte().findElement("selWidget" + operation,"select");
    var numSelItems = 0;
    
    for(var i = 0;i < selWidgetActingOn.length;i++)
    {
        if(selWidgetActingOn.options[i].selected == true)
        {
           // alert( selWidgetActingOn.options[i].value);
            sDelims += selWidgetActingOn.options[i].value + "|";
            numSelItems++;
        }
     }
     
     //get 'other' selected items
     var sDelimsOther = "";
     var selWidgetOther = TheUte().findElement("selWidget_" + operation + "_" + role,"select");
    if(selWidgetOther != null)
    {
        for(var i = 0;i < selWidgetOther.length;i++)
        {
            if(selWidgetOther.options[i].selected == true)
            {
               // alert( selWidgetActingOn.options[i].value);
                sDelimsOther += selWidgetOther.options[i].value + "|";
                
            }
        }
    }
    
    //alert("role-" + role + "> operation-" +  operation + " direction-" + direction);
    //alert ( sDelimsOther );
    
    var ugMx = newMacro("UserGroupsStoryMx");
    addParam( ugMx,"userOrGroup"    ,operation );
    addParam( ugMx,"StoryID"        ,oStorySharingControl.storyController.CurrentStory.ID );
    addParam( ugMx,"delims"         ,sDelims );
    addParam( ugMx,"delimsOther"         ,sDelimsOther );
    addParam( ugMx,"editorOrViewer"      ,role );
    addParam( ugMx,"direction"      ,direction );
    //alert( serializeMacroForRequest( ugMx) );
    processRequest( ugMx );
  
}

function getPermissionsMatrix()
{
    var values = new Array();
    
values[0] = document.createTextNode("Editors");
values[4] = document.createTextNode("Viewers");
    
    
    values[5] =  this.getPanel("Users_Editors");
    values[6] =  this.getShiftInOutWidget("Users_Editors"   ,   EditorUserCallback);
    values[7] =  this.getUsers();
    values[8] =  this.getShiftInOutWidget("Users_Viewers"   ,   EditorUserCallback);
    values[9] =  this.getPanel("Users_Viewers");

    values[10] =  this.getPanel("Groups_Editors");
    values[11] =  this.getShiftInOutWidget("Groups_Editors"  ,   EditorUserCallback);
    values[12] =  this.getGroups();
    values[13] =  this.getShiftInOutWidget("Groups_Viewers"  ,   EditorUserCallback);
    values[14] =  this.getPanel("Groups_Viewers");

    var oGrid2 = newGrid2("storyPropertyEditorPermissionsMatrix",3,5,values,1);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}




function speGetPanel(name)
{
    var values = new Array();
    //values[0] =  document.createTextNode(name);
    
    var divPanel = document.createElement("div");
    divPanel.id = "div_" + name;
    divPanel.className = "clsSelAttachPoint";
    
    //alert("creating panel " + divPanel.id);
    values[0] = divPanel;
    

    var oGrid2 = newGrid2("storyPropertyEditorPanel_"+name ,1,1,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}

function getShiftInOutWidget(name,callback)
{
    var values = new Array();
    values[0] = TheUte().getButton("west_" + name,"<","",callback,"clsButtonAction");
    values[1] = TheUte().getButton("east_" + name,">","",callback,"clsButtonAction");

    var oGrid2 = newGrid2("ShiftInOut_" + name,2,1,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}


function speGetUsers()
{
    var values = new Array();
    values[0] =  document.createTextNode("Users");
    values[1] = this.userList.selWidget;
    var oGrid2 = newGrid2("storyPropertyEditorStoryPeople",2,1,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}

function speGetGroups()
{
    var values = new Array();
    values[0] =  document.createTextNode("Groups");
    values[1] =  this.groupList.selWidget;

    var oGrid2 = newGrid2("storyPropertyEditorStoryPeople",2,1,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}


function refreshPermissionMatirxData(userOrGroup,editorOrViewer)
{
    var getStoryPermissionData = newMacro("GetStoryPermissionData");
    addParam( getStoryPermissionData,"userOrGroup"    ,userOrGroup );
    addParam( getStoryPermissionData,"StoryID"        ,oStorySharingControl.storyController.CurrentStory.ID );
    addParam( getStoryPermissionData,"editorOrViewer"      ,editorOrViewer );
    processRequest( getStoryPermissionData );
}







