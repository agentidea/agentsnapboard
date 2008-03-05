<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addUsers.aspx.cs" Inherits="addUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentIdea - bulk contact importer ( alpha )</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   
            
        <div id="divToolBarAttachPoint" runat="server" class="clsToolbar">
        </div>
         <div id="divMsgAttachPoint" runat="server" class="clsMsg">
        </div>
         <div>
         
    <h3>very alpha, this is a slippery when wet user entry tool, beware delimeters are commas and tildas.  Caveat emptor!</h3>
        <textarea id="txtUsers" runat="server" style="width: 468px; height: 188px"></textarea>
        <input id="cmdAddUsers" runat="server" onserverclick="cmdAddUsers_ServerClick" type="button"
            value="add users" /></div>
            
            
        <div id="divBodyAttachPoint" runat="server" class="clsBody">
        </div>
        <div id="divFooter" runat="server" class="clsFooter">
        </div>
        <div id="divLog" runat="server" class="clsLog">
        </div>
        
    </form>
</body>
</html>
