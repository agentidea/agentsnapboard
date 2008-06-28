<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageElementEditor.aspx.cs" Inherits="AgentStoryHTTP.screens.PageElementEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentStory - Page Element Editor</title>

    <style>
    body
{
	margin:0;
	padding:0;
}
    
    </style>


    <link rel="stylesheet" type="text/css" href="../style/StoryElements.css" />

    <!-- YUI -->
    <!-- Skin CSS file -->  
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.5.1/build/assets/skins/sam/skin.css">   
    <!-- Utility Dependencies -->  
    <script type="text/javascript" src="http://yui.yahooapis.com/2.5.1/build/yahoo-dom-event/yahoo-dom-event.js"></script>    
    <script type="text/javascript" src="http://yui.yahooapis.com/2.5.1/build/element/element-beta-min.js"></script>    
    <!-- Needed for Menus, Buttons and Overlays used in the Toolbar -->  
    <script src="http://yui.yahooapis.com/2.5.1/build/container/container_core-min.js"></script>  
    <script src="http://yui.yahooapis.com/2.5.1/build/menu/menu-min.js"></script>  
    <script src="http://yui.yahooapis.com/2.5.1/build/button/button-min.js"></script>  
     <!-- Source file for Rich Text Editor-->  
     <script src="http://yui.yahooapis.com/2.5.1/build/editor/editor-beta-min.js"></script>  
    <!-- YUI -->

    <script  type="text/javascript" src="../includes/PageUtils.js"></script>
    <script  type="text/javascript" src="../includes/StoryElementEditor.js"></script>

    <script language="javascript" type="text/javascript">
    window.onload = function()
    {
        var _StoryElementEditor = new StoryElementEditor(44,"test editor");
        var attachPoint = document.getElementById("dvAttachPoint");
        _StoryElementEditor.init(attachPoint);
    }
    </script>
    
    


</head>
<body class="yui-skin-sam">

    <div id="dvAttachPoint">
                
    </div>
    
   



</body>
</html>
