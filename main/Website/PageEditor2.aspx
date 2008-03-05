<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageEditor2.aspx.cs" Inherits="PageEditor2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WSYWIG editor</title>
    <script src="./includes/Mouse.js" language="javascript" type="text/javascript"></script>
</head>
<body onmouseup="direction='Released';" onmousemove="SetDivPosition(event);">

    <form id="form1" runat="server">
    
    <div id="firstdiv" 
style="BORDER-RIGHT:1px;BORDER-TOP:1px;OVERFLOW:auto;BORDER-LEFT:1px;WIDTH:100%;
BORDER-BOTTOM:1px;HEIGHT:300px;BACKGROUND-COLOR:green"></div>


<hr color="blue" width="100%" style="CURSOR:row-resize;height:3px;" 
 onmousedown="getDivPosition(event)">
 
 
 
    </form>
</body>
</html>
