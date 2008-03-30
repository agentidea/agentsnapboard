<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewStory.aspx.cs" Inherits="AgentStoryHTTP.screens.CreateNewStory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>AgentIdea - Create New Story</title>
    
    <link href="../style/main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentIdea JS libs -->
    <script src="../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/corecommands.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="../includes/grid.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

var layoutToUse = 0;

function cmdAddNewTitle()
{

     var title = TheUte().findElement("m_txtStoryName","input");
     if(title.value == "")
     {
        alert("please supply a story title");
        title.focus();
        return;
     }
    var m = new macro();
    m.name = "AddNewStory";
    var storyName = TheUte().findElement("m_txtStoryName","input");
    var storyDescription = TheUte().findElement("m_txtStoryDescription","textarea");
   
   //var chatEnabled = TheUte().findElement("m_txtChatEnabled","input");
   
   addParam(m,"txtStoryName",TheUte().encode64( storyName.value ));
   addParam(m,"txtStoryDescription",TheUte().encode64( storyDescription.value ));
   addParam(m,"nLayoutToUse", 0 );
   addParam(m,"chatEnabled", "false" );
   
   addParam(m,"UserID",<%= CurrentUser.ID %>);
   processRequest( m );
}

function layoutPullDownChanged()
{
    layoutToUse = this.selectedIndex;
}

function init()
{

    var values = new Array();
    values[0] = document.createTextNode("Story Title");
    values[1] = TheUte().getInputBox("","m_txtStoryName",null,null,"clsInputField");
    values[2] = document.createTextNode("Story Description");
    values[3] = TheUte().getTextArea("","m_txtStoryDescription",null,null,"clsTextBox");
   // values[4] = document.createTextNode("Layout");
   // values[5] = TheUte().getSelect("Cartesian Layout|Table Layout", layoutPullDownChanged,"layOutSelectControl");
   // values[6] = document.createTextNode("chat enabled");
   // values[7] = TheUte().getInputBox("false","m_txtChatEnabled",null,null,"clsInputField");
    
    
    
    
    values[8] = TheUte().getButton("cmd_AddNewStory","add new story","",cmdAddNewTitle,"clsButtonAction");

    var oGrid2 = newGrid2("newStoryGrid",5,2,values,0);
    oGrid2.init( oGrid2 );
    
    var dv = TheUte().findElement("divBodyAttachPoint","div");
    dv.appendChild( oGrid2.gridTable );
    
    values[1].focus();

}

    </script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
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
     </form>   
</body>
</html>

