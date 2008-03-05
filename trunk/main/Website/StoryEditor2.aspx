<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoryEditor2.aspx.cs" Inherits="StoryEditor2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - Story Editor Version 2</title>
     <link href="main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentStory JavaScript libraries -->
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="includes/json.js" language="javascript" type="text/javascript"></script>
    <script src="includes/StoryElements.js" language="javascript" type="text/javascript"></script>
    <script src="includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
    <script src="includes/dragDrop.js" language="javascript" type="text/javascript"></script>
    
    <script src="includes/contextMenu/mmenudom.js" language="javascript" type="text/javascript"></script>
    <script src="includes/contextMenu/mmenuns4.js" language="javascript" type="text/javascript"></script>
    <script src="includes/contextMenu/milonic_src.js" language="javascript" type="text/javascript"></script>
    <script src="includes/contextMenu/menu_data.js" language="javascript" type="text/javascript"></script>
    
    
    <script type="text/javascript" src="includes/contextMenu/milonic_src.js"></script>	
    <script type="text/javascript" src="includes/contextMenu/mmenudom.js"></script>
    
    <!-- The next file contains your menu data, links and menu structure etc -->
    <script type="text/javascript" src="includes/contextMenu/menu_data.js"></script>        <script type="text/javascript" src="includes/contextMenu/contextmenu.js"></script>	
    <!-- **** JavaScript Menu HTML Code -->

    
    
    
    
    
    
    
    <script src="includes/StoryEditor3.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    var oStoryEditor3 = null;

    function init()
    {


        oStoryEditor3 = newStoryEditor3();

        //bind to drag drop
       oStoryEditor3.container.onmousemove = mouseMove;
       oStoryEditor3.container.onmouseup = mouseUp;
       
         var dv = TheUte().findElement("divBodyAttachPoint","div");
         dv.appendChild( oStoryEditor3.container );
         

    }

    </script>
    
    


</head>
<body onload="init();" id="body">
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
