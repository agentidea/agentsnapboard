<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendNewEmail.aspx.cs" Inherits="SendNewEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - create new message</title>
    
    <link href="main.css" rel="stylesheet" type="text/css" />
    
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="includes/selector.js" language="javascript" type="text/javascript"></script>
    <script src="includes/MessageNewEditor.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 

    <script language="javascript" type="text/javascript">
    
    var oMessageNewEditor = null;
    
    function init()
    {
       
        var operator = <%= CurrentUser.ID %>;
        var operatorName = '<%= CurrentUser.UserName %>';
        
        var groupToIDlist    = "<%= ugh.getPlistGroupsID() %>";
        var groupToNameList  = "<%= ugh.getPlistGroupsNames() %>";
        var userToIDlist  = "<%= ugh.getPlistUsersID() %>";
        var userToNameList  = "<%= ugh.getPlistUsersNames() %>";


        var emailData = <%= emailJSON %>;

        oMessageNewEditor = new MessageEditor();
        
        oMessageNewEditor.init(emailData,operator,operatorName,groupToIDlist,groupToNameList,userToIDlist,userToNameList);
        var dv = TheUte().findElement("divBodyAttachPoint","div");
        dv.appendChild( oMessageNewEditor.container );
    }
    </script>
    
</head>
<body onload="init();">
    <form id="form1" runat="server">
    <div>
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
    
    </div>
    </form>
</body>
</html>

