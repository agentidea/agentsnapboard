<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsersGroups.aspx.cs" Inherits="AgentStoryHTTP.screens.ManageUsersGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentIdea - Story - Manage users/groups</title>
    
    <link href="../style/main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentIdea - Story JS libs -->
    <script src="../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/selector.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
var selWidgetGroups = null;
var selWidgetUsers = null;
var divUsersInGroup = null;
var currentGroupID = -1;



function goToAddNewGroupPage()
{
    location.href = "AddNewGroup.aspx";
}

function addUserToGroup()
{
    
    var userSel = TheUte().findElement("selWidgetUsers","select");
    //alert("add to group!" + userSel);
    var UserIDs = "";
    
    var numSelItems = 0;
    
    for(var i = 0;i < userSel.length;i++)
    {
        if(userSel.options[i].selected == true)
        {
            UserIDs += userSel.options[i].value + "|";
            numSelItems++;
        }
     }
     
     if( numSelItems > 0 && currentGroupID != -1)
     {
            //add users to group
            var m = new macro();
            m.name = "AddUserToGroup";
            addParam(m,"GroupID",currentGroupID);
            addParam(m,"UserIDs",UserIDs);
            processRequest( m );
      }
  
}


function removeUserFromGroup()
{
    
    var userSel = TheUte().findElement("selWidgetUsersInGroup","select");
    var UserIDs = "";
    
    var numSelItems = 0;
    
    for(var i = 0;i < userSel.length;i++)
    {
        if(userSel.options[i].selected == true)
        {
           // alert( userSel.options[i].value);
            UserIDs += userSel.options[i].value + "|";
            numSelItems++;
        }
     }
     
     if( numSelItems > 0 && currentGroupID != -1)
     {
            //add users to group
            var m = new macro();
            m.name = "RemoveUsersFromGroup";
            addParam(m,"GroupID",currentGroupID);
            addParam(m,"UserIDs",UserIDs);
            processRequest( m );
      }
  
}

function getCurrSelectedGroupID()
{
    
     var groupSel = TheUte().findElement("selWidgetGroups","select");
     if(groupSel==null) alert("nuller");
    return groupSel.options[ groupSel.selectedIndex ].value;
}
function selGroupsChanged()
{
    loadUsersIntoGroup( getCurrSelectedGroupID() );
}

function selUsersChanged()
{
  //  alert("sel changed" + this.id);
}
function selUsersGroupsChanged()
{
  //  alert("sel changed" + this.id);
}
function getSelWidgetGroupsText()
{
    var selIndex = selWidgetGroups.selectedIndex;
    if( selIndex == null)
    {
        //nothing selected yet
        selIndex = 0;
    }
    
    return selWidgetGroups.options[selIndex].text;
}



function init()
{
    var userIDs     = "<%= ugh.getPlistUsersID() %>";
    var groupIDs    = "<%= ugh.getPlistGroupsID() %>";
    var userNames   = "<%= ugh.getPlistUsersNames() %>";
    var groupNames  = "<%= ugh.getPlistGroupsNames() %>";
    currentGroupID  = <%= ugh.getPlistCurrentGroupID() %>;
    
    //load these into sel boxes.
    var selItemsGroups = buildSelItems(groupIDs,groupNames,true,null);
    var selItemsUsers = buildSelItems(userIDs,userNames,false,null);
    
    //pass the items into views.
    selWidgetGroups = new selWidget("selWidgetGroups");
    //alert(selWidgetGroups);
    
    selWidgetGroups.init(selItemsGroups,selGroupsChanged,1,"clsSingleSelBox");
    selWidgetUsers = new selWidget("selWidgetUsers");
    selWidgetUsers.init(selItemsUsers,selUsersChanged,8,"clsMultiSelBox");    
    
     var values = new Array();
     values[0] = getGroupHeader( selWidgetGroups.selWidget );
     values[1] = getBodyMain( selWidgetUsers.selWidget );
     values[3] = document.createTextNode("Note: You need to be group owner to add users to a group.");    
     values[4] = document.createTextNode("Note: You can remove yourself from any group.  To remove other users you need to be the group owner.");    
    var oGrid2 = newGrid2("outerGrid",5,1,values);
    oGrid2.init( oGrid2 );
    
    var dv = TheUte().findElement("divBodyAttachPoint","div");
    dv.appendChild( oGrid2.gridTable );

    loadUsersIntoGroup(currentGroupID);
  
}

function getGroupHeader(w)
{
     var values = new Array();
     values[0] = document.createTextNode("groups");
     values[1] = w;
     values[2] = TheUte().getButton("cmdAddNewGroup","create your own new group","add a new group you are responsible for",goToAddNewGroupPage,"clsButtonAction");

    var oGrid2 = newGrid2("group header",1,3,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}

function getBodyMain(w)
{
     var values = new Array();
     values[0] = getGroupsUsers();
     values[1] = getInOutWidget();
     values[2] = getUsers(w);
    
    
    var oGrid2 = newGrid2("body",1,3,values,2);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}

function loadUsersIntoGroup(groupID)
{
    //request list users for this group id
    var m = new macro();
    m.name = "GetGroupUsers";
    addParam(m,"GroupID",groupID);
    processRequest( m );
    
    currentGroupID = groupID;
}


function getGroupsUsers()
{
    
    
     var values = new Array();
     
     divUsersInGroup = document.createElement("div");
     divUsersInGroup.id = "divUsersInGroup";
     
     values[0] = divUsersInGroup;
    
    
    var oGrid2 = newGrid2("group header",1,1,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}

function getUsers(w)
{
     var values = new Array();
     values[0] = document.createTextNode("users");
     values[1] = w;
    
    
    var oGrid2 = newGrid2("group header",2,1,values,1);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}


function getInOutWidget()
{
    var values = new Array();
     values[0] = TheUte().getButton("cmdAddUserToGroup","<","add user to group",addUserToGroup,"clsButtonAction");
     values[1] = TheUte().getButton("cmdRemoveUserFromGroup",">","remove user to group",removeUserFromGroup,"clsButtonAction");

    var oGrid2 = newGrid2("group header",2,1,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}


    </script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="divToolBarAttachPoint" runat="server" class="clsToolbar">
        </div>
         <div id="divMsgAttachPoint" runat="server" class="clsMsg">
        </div>
        <div id="divBodyAttachPoint" runat="server" class="clsBody">
        </div>
        <div id="divFooter" runat="server" class="clsFooter">
        </div>
        <div id="divLog" runat="server" class="clsLog">
        </div>
        
     </form>   
</body>
</html>
