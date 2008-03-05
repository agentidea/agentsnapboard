<%@ Page Language="C#" AutoEventWireup="true" CodeFile="platform2.aspx.cs" Inherits="platform2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%= PageTitle %> </title>
    
      <link href="main.css" rel="stylesheet" type="text/css" />
      
      
      <script language="javascript">
      var defaultToolBR = "<%= ToolBRdefault %>";
      </script>
      
      
       <!--
    
    
    /*
    
     AgentIdea Story Table of Contents Page
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

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

*/
    
    
    
    
    
    
    
    -->

    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" src="includes/LoginWidget.js" type="text/javascript"></script>
    <script src="includes/StoryToc2.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 
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
