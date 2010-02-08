<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendEmail.aspx.cs" Inherits="AgentStoryHTTP.screens.sendEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - send message</title>
    
    <link href="../style/main.css" rel="stylesheet" type="text/css" />
    
    <script src="../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/MessageEditor.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
 

    <script language="javascript" type="text/javascript">
    
    var oMessageEditor = null;
    
    function init()
    {
       
        var operator = <%= CurrentUser.ID %>;
        var operatorName = '<%= CurrentUser.UserName %>';
        
        var to = <%= ToID %>;
        var toName = '<%= ToUserName %>';
        
        var emailData = <%= emailJSON %>;

        oMessageEditor = new MessageEditor();
        
        oMessageEditor.init(emailData,operator,operatorName,to,toName);
        var dv = TheUte().findElement("divBodyAttachPoint","div");
        dv.appendChild( oMessageEditor.container );
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
