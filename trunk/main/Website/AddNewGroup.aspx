<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewGroup.aspx.cs" Inherits="AddNewGroup" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - Create New Group</title>
    
    <link href="main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentIdea JS libs -->
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">



function cmdAddNewGroup()
{

     var groupName = TheUte().findElement("m_txtGroupName","input");
     if(groupName.value == "")
     {
        alert("please supply a group name");
        groupName.focus();
        return;
     }

    var m = new macro();
    m.name = "AddNewGroup";
    
   var groupDescription = TheUte().findElement("m_txtGroupDescription","textarea");
   
   addParam(m,"groupName",TheUte().encode64( groupName.value ));
   addParam(m,"groupDescription",TheUte().encode64( groupDescription.value ));
   addParam(m,"UserID",<%= CurrentUser.ID %>);
   processRequest( m );
}

function init()
{

    var values = new Array();
    values[0] = document.createTextNode("Group Name");
    values[1] = TheUte().getInputBox("","m_txtGroupName",null,null,"clsInputField");
    values[2] = document.createTextNode("Group Description");
    values[3] = TheUte().getTextArea("","m_txtGroupDescription",null,null,"clsTextBox");
    
    values[5] = TheUte().getButton("cmd_AddNewGroup","add new group","",cmdAddNewGroup,"clsButtonAction");

    var oGrid2 = newGrid2("newStoryGrid",5,2,values);
    oGrid2.init( oGrid2 );
    
    var dv = TheUte().findElement("divBodyAttachPoint","div");
    dv.appendChild( oGrid2.gridTable );
    
    values[1].focus();

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
