<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoryOrig.aspx.cs" Inherits="AgentStoryHTTP.screens.StoryOrig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - story</title>
    <link href="./../style/main.css" rel="stylesheet" type="text/css" />
    
    <script src="./../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/json.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/StoryElements.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/StoryViewer.js" language="javascript" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
   
    var oStoryViewer = null;
    function init()
    {

       var storyJSON = "<%= oStory.GetStoryJSON() %>";
       eval(" var story = " + storyJSON + ";");
       var storyController = newStoryController(story);
        
       oStoryViewer = new StoryViewer();
       oStoryViewer.init(storyController,<%= oStory.canEditInt %>);
       
       if( oStoryViewer.container != null)
       {
           var dv = TheUte().findElement("divBodyAttachPoint","div");
           dv.appendChild( oStoryViewer.container );
       }
       
    }
    </script>
    
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="divToolBarAttachPoint" runat="server" class="clsToolbar"></div>
         <div id="divMsgAttachPoint" runat="server" class="clsMsg"></div>
        <div id="divBodyAttachPoint" runat="server" class="clsBody"></div>
        <div id="divFooter" runat="server" class="clsFooter"></div>
        <div id="divLog" runat="server" class="clsLog">
        </div>
     </form>   
</body>
</html>
