<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>

<head>
     <title><%= PageTitle %> </title>
     <link href="main.css" rel="stylesheet" type="text/css" />
      
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" src="includes/LoginWidget.js" type="text/javascript"></script>
    <script src="includes/LoginScreen.js" language="javascript" type="text/javascript"></script>
    <script src="includes/corecommands.js" language="javascript" type="text/javascript"></script>
 
    <script language="javascript" type="text/javascript">
    
    var oLoginScreen = null;
    
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
           
            var clubName = "<%= ClubName %>";
            oLoginScreen = new LoginScreen( document.getElementById("divBodyAttachPoint"), clubName,loginCallback);
            oLoginScreen.init();
            
            try
            {
                var userNameTextBox = document.getElementById("txtUserName");
                userNameTextBox.focus();
                userNameTextBox.select();
            
            }
            catch(e)
            {
                //not much to do, let user obtain own focus
            }
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
            <asp:Label ID="lblPageMessage" runat="server" Height="40px" Width="488px"></asp:Label></div>
    </div>
    
    
        <input type="hidden" id="hUserName" name="hdnUserName" value="" />
        <input type="hidden" id="hPassword" name="hdnPassword" value="" />
    
    
    </form>
    
    


</body>

</html>
