<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="platform2.aspx.cs" Inherits="AgentStoryHTTP.screens.platform2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title><%= PageTitle %> </title>
    
      <link href="../style/main.css" rel="stylesheet" type="text/css" />

      
      <script language="JavaScript" type="text/javascript">
      var defaultToolBR = "<%= ToolBRdefault %>";
      </script>
      
    <script src="../includes/LoginWidget.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/StoryToc2.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
 
    <script language="javascript" type="text/javascript">
    
    var oStoryToc = null;
    
    function loginCallback(u,p)
    {
        
        var username = TheUte().decode64( u );
        var pwd = TheUte().decode64( p );
        
        //alert(username.trim().length + " " + pwd.trim().length);
        // ??? len 1?  why not 0 ???
        if(username.trim().length == 1 || pwd.trim().length == 1)
        {
            alert("please supply valid email address and password to login");
            return;
        }
        var theForm = document.getElementById("TheForm");
        
        theForm.hUserName.value = u;
        theForm.hPassword.value = p;
        theForm.submit();
    }
    
    window.onload = function init()
    {
            var stories = <%= StoriesJSON %>
            var clubName = "<%= ClubName %>";
            oStoryToc = new StoryToc( stories, document.getElementById("divBodyAttachPoint"), clubName,loginCallback);
            oStoryToc.init();
    }
    
    </script>
    
</head>
<body >
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
    
    
        <input type="hidden" id="hUserName" name="hdnUserName" value="" />
        <input type="hidden" id="hPassword" name="hdnPassword" value="" />
    
    
    </form>
    
    
</body>
</html>
