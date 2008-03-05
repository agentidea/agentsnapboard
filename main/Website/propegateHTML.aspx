<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propegateHTML.aspx.cs" Inherits="propegateHTML" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentStory - HTML Propegator</title>
      <link href="main.css" rel="stylesheet" type="text/css" />
   
   
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" src="includes/LoginWidget.js" type="text/javascript"></script>
    <script src="includes/StoryList.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 
   
       <script language="javascript" type="text/javascript">
    
    var oStoryList = null;
    
   
    
    window.onload = function init()
    {
            var stories = <%= StoriesJSON %>
            oStoryList = new StoryList( stories, document.getElementById("divBodyAttachPoint"));
            oStoryList.init();
    }
    
    </script>
    
</head>
<body>
 <form id="TheForm" method="post" runat="server">
    
    <div>

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
    </div>
    
    
 
    
    </form>
</body>
</html>
