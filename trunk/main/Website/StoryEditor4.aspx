<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoryEditor4.aspx.cs" Inherits="StoryEditor4" %>


<html>  

<head>
    <title><%= PageTitle %> </title>

    <link rel="stylesheet" type="text/css" href="./main.css">
    
    <!--
    
    
    /*

    AgentStory Story Viewer/Editor ( Cartesian ) 
    Copyright AgentIdea 2007
    
    please note this is private and a copyrighted original work by Grant Steinfeld.
    
    
    
1. Registration Number:    TXu-959-906  
Title:    AgentIdea application studio for IE : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    text of computer program: Grant Steinfeld , 1967- & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
2. Registration Number:    TXu-960-165  
Title:    AgentIdea application studio for Java : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    program text: Grant Steinfeld , 1967-, & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
3. Registration Number:    TXu-961-904  
Title:    AgentIdea application studio for IIS : version 1.0 / authors, Grant Steinfeld, Oren Kredo. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    cAgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Special Codes:   1/C 
--------------------------------------------------------------------------------
4. Registration Number:    ( awaiting filing ) ( expected 27Sept07 )
Title:    AgentIdea - AgentStory
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2006 

Filed:    27Aug07

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

*/
 
    -->
    
    
    
    
</head>

<body id="TheBod" class="clsBodyStoryEditor" ondblclick="storyView.storyViewDoublClick();" onunload="storyView.postExitMsg();">
<!-- some issues with the dblclick event in mozilla only -->
<!-- onresize="alignFloatLayers()" onscroll="alignFloatLayers()" -->



<script src="./includes/mousepos.js" type="text/javascript"></script>
    
<script  type="text/javascript" src="./includes/PageUtils.js"></script>
<script src="includes/core.js" language="javascript" type="text/javascript"></script>
<script src="includes/grid.js" language="javascript" type="text/javascript"></script>
<script  type="text/javascript" src="./includes/LabelEdit.js"></script>

<!-- LIBRARY related includes -->
<!--  type="text/javascript" src="./includes/LibElemDrag.js" -->
<script  type="text/javascript" src="./includes/LibraryViewer.js"></script>
<script  type="text/javascript" src="./includes/CoreLibraryPortals.js"></script>
<script  type="text/javascript" src="./includes/CustomLibraryPortals.js"></script>


<script  type="text/javascript" src="./includes/timer.js"></script>


<script src="includes/StoryElements.js" language="javascript" type="text/javascript"></script>
<script src="includes/StoryControllers.js" language="javascript" type="text/javascript"></script>

    
<script type="text/javascript" src="<%= IncludeMaster %>/build/yahoo/yahoo-min.js" ></script>
<script type="text/javascript" src="<%= IncludeMaster %>/build/event/event-min.js" ></script>
<script type="text/javascript" src="<%= IncludeMaster %>/build/dom/dom-min.js"></script>
<script type="text/javascript" src="<%= IncludeMaster %>/build/logger/logger-min.js"></script>
<script type="text/javascript" src="<%= IncludeMaster %>/build/dragdrop/dragdrop-debug.js" ></script>

<script  type="text/javascript" src="./includes/storyViewr2.js" ></script>
<script src="./includes/storyElemView.js" type="text/javascript"></script>
<script type="text/javascript" src="./includes/StoryElementDragOnTop.js" ></script>
<script type="text/javascript" src="./includes/simplePageNavPanel.js" ></script>
<script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>

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
                    gPageCursor = <%= PageCursor %>;
                    gViewMode = "<%= ViewMode %>";
                    gToolBarVisible = "<%= ToolBarVisible %>";
                    
                    
                    eval(" var currUser = " + currUserJSON + ";");
                    eval(" var story = " + storyJSON + ";");

                    var storyController = newStoryController( story,currUser );
                    storyView = new storyView2(storyController, document.getElementById("TheBod"),story.LastSeq,story.ID,gToolBarVisible );
                    storyView.init( document.getElementById("TheTempHook") );

                    InitializeTimer(storyView.heartbeat,gDelay,true,gRefreshRate);
                    
                   //storyView.displayMessage("contacting host ... ");
                   
                   //new FloatLayer('floatlayer',15,15,10);


                }
                
                
  
</script>

<% if ( DebugMode == true)
   {
       %>
<script type="text/javascript">
//<![CDATA[
    YAHOO.example.logApp =  {

       reader: null,

       init:  function() {
            if (YAHOO.widget.Logger) {
                this.reader = new YAHOO.widget.LogReader( "LogDiv", 
                        { newestOnTop: true, height: "400px" } );

                this.reader._onClickPauseBtn(null, this.reader);
            }
        }
    };

    YAHOO.util.Event.on(window, "load", YAHOO.example.logApp.init);

//]]>
</script>
<% }
    %>

    <div id ="TheHook" style="left:5;top:0;position:absolute;background-color:Green;width:0px;height:0px;display:block;"></div>
    <div id="MainContent"></div>
    <div id ="TheTempHook" style="left:1px;top:1px;position:absolute;background-color:Transparent;width:10px;height:10px;"></div>
 
    <div id="LogDiv" style="left:1000px;position:absolute;" ></div>
    <div id="divLog" style="left:1000px;position:absolute;" ></div>
    

   
 
</body>

</html>