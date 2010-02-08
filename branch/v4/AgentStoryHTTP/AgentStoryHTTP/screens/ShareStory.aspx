<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareStory.aspx.cs" Inherits="AgentStoryHTTP.screens.ShareStory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - share story</title>
    <link href="../style/main.css" rel="stylesheet" type="text/css" />
    
    <script src="../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/selector.js" language="javascript" type="text/javascript"></script>

    <script src="../includes/json.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/StoryElements.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/storySharingControl.js" language="javascript" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
   
    var oStorySharingControl = null;
    function init()
    {

       var storyJSON = "<%= oStory.GetStoryJSON() %>";
       eval(" var story = " + storyJSON + ";");
       var storyController = newStoryController(story);
        
       oStorySharingControl = new StorySharingControl(storyController);
       
       var userList = getUsers();
       var groupList = getGroups();
       
       oStorySharingControl.init( userList, groupList );
       var dv = TheUte().findElement("divBodyAttachPoint","div");
       dv.appendChild( oStorySharingControl.container );
       
       oStorySharingControl.loadAllPerms();
       
    }
    
    
    
    function getUsers()
    {
        //get users and groups
        var userIDs     = "<%= ugh.getPlistUsersID() %>";
        var userNames   = "<%= ugh.getPlistUsersNames() %>";
        var selItemsUsers = buildSelItems(userIDs,userNames,false,null);
        selWidgetUsers = new selWidget("selWidgetUsers");
        selWidgetUsers.init(selItemsUsers,null,8,"clsMultiSelBox");    
        return selWidgetUsers;
    }
    
    function getGroups()
    {
        var groupIDs    = "<%= ugh.getPlistGroupsID() %>";
        var groupNames  = "<%= ugh.getPlistGroupsNames() %>";    
        var selItemsGroups = buildSelItems(groupIDs,groupNames,true,null);
        selWidgetGroups = new selWidget("selWidgetGroups");
        selWidgetGroups.init(selItemsGroups,null,8,"clsMultiSelBox");
        return selWidgetGroups;    
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

