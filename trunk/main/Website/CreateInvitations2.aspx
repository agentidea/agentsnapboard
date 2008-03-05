<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateInvitations2.aspx.cs" Inherits="CreateInvitations2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentIdea - Create Invitations</title>
    
    <link href="main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentIdea JS libs -->
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="includes/selector.js" language="javascript" type="text/javascript"></script>
    <script src="includes/InvitationManager.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 



<script language="javascript" type="text/javascript">

var oInvitationManager = null;

function initPage()
{


    
    var operatorID = <%= CurrentUser.ID %>;
    var operatorName = '<%= CurrentUser.UserName %>';
       
    var userFromIDlist     = "<%= FromUserIDList %>";
    var userFromNameList   = "<%= FromUserNameList %>";
    var groupToIDlist    = "<%= ugh.getPlistGroupsID() %>";
    var groupToNameList  = "<%= ugh.getPlistGroupsNames() %>";
    var userToIDlist  = "<%= ugh.getPlistUsersID() %>";
    var userToNameList  = "<%= ugh.getPlistUsersNames() %>";




   oInvitationManager = new InvitationManager();
  
   oInvitationManager.init(operatorID,operatorName, userFromIDlist,userFromNameList,groupToIDlist,groupToNameList,userToIDlist,userToNameList);
//alert("init" + oInvitationManager);
  
    var dv = TheUte().findElement("divBodyAttachPoint","div");
    dv.appendChild( oInvitationManager.container );
  
}


    </script>
</head>
<body onload="initPage();">
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
