<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInvitation2.aspx.cs" Inherits="ViewInvitation2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >



<head runat="server">
    <title>AgentStory - View Invitation</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div id="divMsg" runat="server" class="clsMsg"></div>
        <div id="divInviteAttachPoint" runat="server" class="clsInvite" >
        </div>
        <br />
        <div id="divInviteCodeAttachPoint" runat="server" class="clsInviteCode">
        </div>
       
        <br />
        <br />
        <div id="divActionButtons" runat="server">
        
        <div>
        Please enter your Invite Code ( exactly as it appears in the black area above )
        &nbsp;<input id="txtInviteCode" runat="server" type="text" />
        </div>
        
        <input id="cmdContinue" runat="server" onserverclick="cmdContinue_ServerClick" type="button"
            value="Accept Invitation" class="clsButtonAction"/>
            
            <br />
            <br />
            <br />
            
            <input id="cmdDecline" runat="server" type="button"
            value="Decline Invitation" onserverclick="cmdDecline_ServerClick" class="clsButtonCancel"/>
            </div>
            </div>
    </form>
</body>
</html>
