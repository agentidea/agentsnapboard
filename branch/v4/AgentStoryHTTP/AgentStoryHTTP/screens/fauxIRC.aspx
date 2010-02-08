<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fauxIRC.aspx.cs" Inherits="AgentStoryHTTP.screens.fauxIRC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory</title>
    
    <link href="./../style/main.css" rel="stylesheet" type="text/css" />
    
    <script src="../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/fauxIrc.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
 

    <script language="javascript" type="text/javascript">
    
    var oFauxIRC = null;
    
    function init()
    {
    
        oFauxIRC = new fauxIRC();
        oFauxIRC.init();
       
        var dv = TheUte().findElement("divBodyAttachPoint","div");
        dv.appendChild( oFauxIRC.container );

        oFauxIRC.tryFocus();

    }
    </script>
    
</head>
<body onload="init();" style="background-color:#FC7203;">
    <form id="form1" runat="server">
        <div id="divBodyAttachPoint" runat="server">
        </div>
     </form>   
</body>
</html>

