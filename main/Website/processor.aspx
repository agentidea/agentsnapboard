<%@ Page Language="C#" AutoEventWireup="true" CodeFile="processor.aspx.cs" Inherits="processor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    <link href="main.css" rel="stylesheet" type="text/css" />
    <script src="includes/timer.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    
        function init()
        {
            InitializeTimer();
            StartTheTimer();
        }
    </script>
 
</head>
<body onload="init();">
    <form id="form1" runat="server">
    <div id="procAttachPoint" runat="server"></div>
    </form>
</body>
</html>
