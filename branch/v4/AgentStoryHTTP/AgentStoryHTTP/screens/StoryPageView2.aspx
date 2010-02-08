<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoryPageView2.aspx.cs" Inherits="AgentStoryHTTP.screens.StoryPageView2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
<head id="Head1" runat="server">
    <title>AgentStory - Page Manager</title>
    <link rel="stylesheet" type="text/css" href="../style/list.css" />
    <script type="text/javascript" src="../includes/YUI/build/utilities/utilities.js"></script>
    <script  type="text/javascript" src="../includes/PageUtils.js"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script  type="text/javascript" src="../includes/listDrag.js"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
 

</head>
<body>

    <script language="javascript" type="text/javascript">
        var storiesMeta = <%= StoryMetaJSON %>;
    </script>
    
    <div id="user_actions">
      <input type="button" id="showButton" value="Save" class="clsActionButton" />
    </div>
  
    <div id="divBodyAttachPoint" class="clsBody"></div>
 
</body>
</html>
