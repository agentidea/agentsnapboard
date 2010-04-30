<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoryEditor4.aspx.cs" Inherits="AgentStoryHTTP.screens.StoryEditor4" %>


<html>
<head runat="server">
        <title><%= PageTitle %> </title>

    <link rel="stylesheet" type="text/css" href="../style/main.css" />
    <link rel="stylesheet" type="text/css" href="../style/StoryElements.css" />
      
    
    
    <!--

    AgentStory Story Viewer/Editor ( Cartesian ) 
    License: see http://www.agentidea.com/AgentStory/License.aspx

    -->
 




</head>
<body id="TheBod" class="clsBodyStoryEditor" ondblclick="passDblClick();" onunload="storyView.postExitMsg();">

   <script type="text/javascript" language="JavaScript">
 
    var storyView = null;
    var gViewMode = null;
    var gToolBarVisible = null;
    var gPageCursor = 0;
    var gUserCurrentTxID = null;
 
 
    window.onload = function()
    {
        var storyJSON = "<%= oStory.GetStoryJSON() %>";

        gUserCurrentTxID = "<%= currTx %>";
        var currUserJSON = "<%= currUserJSON %>";
        var currUserStoriesJSON = "<%= currUserStoriesJSON %>";
        
        gPageCursor = <%= PageCursor %>;
        gViewMode = "<%= ViewMode %>";
        gToolBarVisible = "<%= ToolBarVisible %>";
        eval(" var currUser = " + currUserJSON + ";");
        eval(" var currStoryToc = " + currUserStoriesJSON + ";");
        eval(" var story = " + storyJSON + ";");

        var storyController = newStoryController( story,currUser,currStoryToc );
        storyView = new storyView2(storyController, document.getElementById("TheBod"),story.LastSeq,story.ID,gToolBarVisible );
        storyView.init( document.getElementById("TheTempHook") , document.getElementById("FrontHook"));
        InitializeTimer(storyView.heartbeat,gDelay,true,gRefreshRate);
    }
    
    function passDblClick()
    {
               
        if( storyView == null )
        {
           alert("story view was null!");
        }
        else
        {
            storyView.storyViewDoublClick();
        }
    
    }
                
                
  
</script>

<script src="../includes/mousepos.js" type="text/javascript"></script>
    
<script  type="text/javascript" src="../includes/PageUtils.js"></script>
<script src="../includes/core.js" language="javascript" type="text/javascript"></script>
<script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
<script  type="text/javascript" src="../includes/LabelEdit.js"></script>

<!-- LIBRARY related includes -->
<script  type="text/javascript" src="../includes/LibraryViewer.js"></script>
<script  type="text/javascript" src="../includes/CoreLibraryPortals.js"></script>
<script  type="text/javascript" src="../includes/CustomLibraryPortals.js"></script>


<script  type="text/javascript" src="../includes/timer.js"></script>
<script src="../includes/StoryElements.js" language="javascript" type="text/javascript"></script>
<script src="../includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
    
<script type="text/javascript" src="../includes/YUI/build/yahoo/yahoo-min.js" ></script>
<script type="text/javascript" src="../includes/YUI/build/event/event-min.js" ></script>
<script type="text/javascript" src="../includes/YUI/build/dom/dom-min.js"></script>
<script type="text/javascript" src="../includes/YUI/build/logger/logger-min.js"></script>
<script type="text/javascript" src="../includes/YUI/build/dragdrop/dragdrop-debug.js" ></script>

<script type="text/javascript" src="../includes/tiny_mce/tiny_mce.js" ></script>


<script  type="text/javascript" src="../includes/PageUtils.js"></script>
<script  type="text/javascript" src="../includes/StoryElementEditor.js"></script>
<script  type="text/javascript" src="../includes/storyViewr2.js" ></script>
<script src="../includes/storyElemView.js" type="text/javascript"></script>
<script type="text/javascript" src="../includes/StoryElementDragOnTop.js" ></script>
<script type="text/javascript" src="../includes/simplePageNavPanel.js" ></script>


<!-- Game Specific includes -->
<%= GameSpecificIncludes %>


<script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>

    
    <div id ="TheHook" style="left:5;top:0;position:absolute;background-color:Green;width:0px;height:0px;display:block;"></div>
    <div id="MainContent"></div>
    <div id ="TheTempHook" style="left:1px;top:1px;position:absolute;background-color:Transparent;width:10px;height:10px;"></div>
    <div id ="FrontHook" style="left:1;top:1;position:absolute; background-image: url(../images/haze.png) ; background-repeat:repeat; width:1px;height:1px;"></div>
 
    <div id="LogDiv" style="left:1000px;position:absolute;" ></div>
    <div id="divLog" style="left:1000px;position:absolute;" ></div>

</body>
</html>
