<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SlideNavigator.aspx.cs" Inherits="AgentStoryHTTP.screens.SlideNavigator" %>

<html>
<head id="Head1" runat="server">
        <title><%= PageTitle %> </title>
    <link rel="stylesheet" type="text/css" href="../style/main.css" />
    <link rel="stylesheet" type="text/css" href="../style/StoryElements.css" />
</head>
<body id="TheBod" class="clsBodyStoryEditor"  onunload="storyView.postExitMsg();">

<!-- removed ondblclick='passDblClick();' -->


   <script type="text/javascript" language="JavaScript">
 
    var storyView = null;
    var gViewMode = null;
    var gToolBarVisible = null;
    var gPageCursor = 0;


    var gUserCurrentTxID = null;
    var gUserAlias = "not_set_yet";
    var gRowDataPK = -1;
    
    var X_POINT = 100;
    var Y_POINT = 125;
 
 
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




<script  type="text/javascript" src="../includes/timer.js"></script>
<script src="../includes/StoryElements.js" language="javascript" type="text/javascript"></script>
<script src="../includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
    



<script  type="text/javascript" src="../includes/PageUtils.js"></script>

<script  type="text/javascript" src="../includes/SlideNavigator.js" ></script>
<script src="../includes/storyElemViewSlideNav.js" type="text/javascript"></script>

<script type="text/javascript" src="../includes/AlertArea.js" ></script>
<script type="text/javascript" src="../includes/SlideNavigatorNavPanel.js" ></script>


<script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>


<!-- Game Specific includes -->

    <script src="../extras/SmartOrg/PortfolioGame/js/jquery.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/portfolio.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/game.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/gameController.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/dieImages.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/pickFive.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/animatedDice.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/dieRolls.js" type="text/javascript"></script>
    <script src="../extras/SmartOrg/PortfolioGame/js/projectSelection2.js" type="text/javascript"></script>
    
    




    
    <div id ="TheHook" style="left:5;top:0;position:absolute;background-color:Green;width:0px;height:0px;display:block;"></div>
    <div id="MainContent"></div>
    <div id ="TheTempHook" style="left:1px;top:1px;position:absolute;background-color:Transparent;width:10px;height:10px;"></div>
    <div id ="FrontHook" style="left:1;top:1;position:absolute; background-image: url(../images/haze.png) ; background-repeat:repeat; width:1px;height:1px;"></div>
 
    <div id="LogDiv" style="left:1000px;position:absolute;" ></div>
    <div id="divLog" style="left:1000px;position:absolute;" ></div>

</body>
</html>
