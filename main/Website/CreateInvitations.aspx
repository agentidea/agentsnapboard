<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateInvitations.aspx.cs" Inherits="CreateInvitations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentIdea - create invitations</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
      <div id="divToolBarAttachPoint" runat="server" class="clsToolbar">
        </div>
         <div id="divMsgAttachPoint" runat="server" class="clsMsg">
        </div>
    
    <div>
    <pre>
        INVITATION GENERATOR v1.1
        ==========================
    </pre>
        FROM:<asp:DropDownList ID="selUsrFrom" runat="server" Width="251px">
        </asp:DropDownList><br />
        <br />
        TO: Users on system:<br />
        &nbsp;<asp:ListBox ID="listBoxUsers" runat="server" Height="92px" SelectionMode="Multiple"
            Width="297px"></asp:ListBox>
        <br />
        <br />
        Invite title:
        <input id="txtTitle" runat="server" type="text" /><br />
        Greeting:
        <input id="txtGreeting" runat="server" style="width: 53px" type="text" />, &lt;&lt;username&gt;&gt;,<br />
        <textarea id="txtBody" runat="server" style="width: 454px; height: 159px"></textarea>
        <asp:Button ID="cmdCreateInvites" runat="server" OnClick="cmdCreateInvites_Click"
            Text="Create Invites" /></div>
    </form>
    
            <div id="divFooter" runat="server" class="clsFooter">
        </div>
        
</body>
</html>
