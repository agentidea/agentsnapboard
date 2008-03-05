<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInvitation.aspx.cs" Inherits="ViewInvitation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        Please enter invitation GUID:
        <input id="txtGUID" type="text" runat="server" /><br />
        <br />
        <input id="cmdEnterGUID" type="button" value="button" onserverclick="cmdEnterGUID_ServerClick" runat="server" /></div>
    </form>
</body>
</html>
