<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoryPageView2.aspx.cs" Inherits="StoryPageView2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentIdea - Page Manager</title>
    <link rel="stylesheet" type="text/css" href="./list.css">
    <script type="text/javascript" src="<%= IncludeMaster %>/build/utilities/utilities.js?_yuiversion=2.3.1"></script>
    <script  type="text/javascript" src="./includes/PageUtils.js"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script  type="text/javascript" src="./includes/listDrag.js"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 

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
