<%@ Page Language="C#" AutoEventWireup="true" CodeFile="storyEditor3.aspx.cs" Inherits="StoryEditor3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentStory - Story Composer</title>
    
    <link href="main.css" rel="stylesheet" type="text/css" />
    
    <script src="./includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="./includes/core.js" language="javascript" type="text/javascript"></script>
    
    
    <!-- ***** This is the section of code you need to paste into your web pages ***** -->
<script type="text/javascript" src="./pages/StoryEditor/menu/modules/dragdrop.js"></script>	
<script type="text/javascript" src="./pages/StoryEditor/menu/milonic_src.js"></script>	
<script type="text/javascript" src="./pages/StoryEditor/menu/mmenudom.js"></script>
<!-- The next file contains your menu data, links and menu structure etc -->
<script type="text/javascript" src="./pages/StoryEditor/menu/menu_data.js"></script>
<script type="text/javascript" src="./pages/StoryEditor/menu/contextmenu.js"></script>	
<!-- **** JavaScript Menu HTML Code -->


<script language="javascript" type="text/javascript">

    var group = null;
    var coordinates = null;
    var drag = null;
    
    var mousePosREF = null;

</script>


 <!-- AgentStory JavaScript libraries -->

    <script src="./includes/grid.js" language="javascript" type="text/javascript"></script>
    <!-- removed JSON conflicts with toolman -->
    <script src="./includes/StoryElements.js" language="javascript" type="text/javascript"></script>
    <script src="./includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
   
    <!-- <script src="./includes/dragDrop.js" language="javascript" type="text/javascript"></script> -->
    <!-- toolman\source\org\tool-man -->
    <script language="JavaScript" type="text/javascript" src="./includes/toolman/source/org/tool-man/core.js"></script>
    <script language="JavaScript" type="text/javascript" src="./includes/toolman/source/org/tool-man/events.js"></script>
    <script language="JavaScript" type="text/javascript" src="./includes/toolman/source/org/tool-man/css.js"></script>
    <script language="JavaScript" type="text/javascript" src="./includes/toolman/source/org/tool-man/coordinates.js"></script>
    <script language="JavaScript" type="text/javascript" src="./includes/toolman/source/org/tool-man/drag.js"></script>




   
    <script src="./includes/StoryEditor3.js" language="javascript" type="text/javascript"></script>
    <script src="./includes/corecommands.js" language="javascript" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    var oStoryEditor3 = null;

    function init()
    {

       
        coordinates = ToolMan.coordinates();
        drag = ToolMan.drag();
        
        var dvBoxTest = document.getElementById("dvBoxTest");
        drag.createSimpleGroup(dvBoxTest);

        var storyJSON = "<%= oStory.GetStoryJSON() %>";
        //load story by way of JSON
        eval(" var story = " + storyJSON + ";");

    
        var storyController = newStoryController(story);
      
       var dvAttachPoint = TheUte().findElement("divBodyAttachPoint","div");  //get attach point for early bind.
       oStoryEditor3 = newStoryEditor3( storyController,dvAttachPoint );

        //bind to drag drop
        document.onmousemove = mouseWhere;
        
     //oStoryEditor3.container.onmousemove = mouseMove;
     //  oStoryEditor3.container.onmouseup = mouseUp;

    }
    
    

    function mouseWhere(ev)
    {
    
        ev = ev || window.event;
        
	    var mousePos = mouseCoords(ev);
	    mousePosREF = mousePos;
	    xy( mousePos );
       
    }
    
    function mouseCoords(ev){
	    if(ev.pageX || ev.pageY)
	    {
	        //Mozilla
		    return {x:ev.pageX, y:ev.pageY};
	    }
	    
	    //Explorer
	    return {
	    
		    x:ev.clientX + document.body.scrollLeft - document.body.clientLeft,
		    y:ev.clientY + document.body.scrollTop  - document.body.clientTop
	    };
}

    </script>
    
    


</head>
<body onload="init();" id="TheBody">

    
    
        <div id="dvBoxTest" class="box">boxer</div>
        
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
        <div id="historyDiv"  class="clsHistory">
        </div>
        
        

</body>
</html>

