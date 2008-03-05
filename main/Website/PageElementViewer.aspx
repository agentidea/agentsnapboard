<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageElementViewer.aspx.cs" Inherits="PageElementViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - PageElement viewer</title>
    
    <link href="main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentStory JS libs -->
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="includes/selector.js" language="javascript" type="text/javascript"></script>
    <script src="includes/PageElementViewer.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 



<script language="javascript" type="text/javascript">

var oPageElementViewer = null;

function initPage()
{


    
   var operatorID = <%= CurrentUser.ID %>;
   var pageElementJSON = <%= PageElementJSON %>;

   oPageElementViewer = new PageElementViewer();
   oPageElementViewer.init(operatorID,pageElementJSON);

  
   var dv = TheUte().findElement("divBodyAttachPoint","div");
   dv.appendChild( oPageElementViewer.container );
  
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
