    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="StrategyPortal.aspx.cs" Inherits="StrategyPortal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!-- testing <EMBED>'ing 'SmartOrgs' strat table - non-propriety technology ( strategy tables are open source ) -->
<html>
  <head>
    <title>AJAX - Strategy Editor</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">

    <LINK rel="stylesheet" type="text/css" href="./includes/strategy/strat.css" media="screen">
    <LINK rel="stylesheet" type="text/css" href="./main.css" media="screen">

    <script src="./includes/pageUtils.js" language="javascript"></script>

	<script src="./includes/strategy/strategyUtils.js" language="javascript"></script>
	<script src="./includes/strategy/currentEditor.js" language="javascript"></script>
	<script src="./includes/strategy/decisionAreaElements.js" language="javascript"></script>
	<script src="./includes/strategy/strategyElements.js" language="javascript"></script>
	<script src="./includes/strategy/strategyTableElements.js" language="javascript"></script>
	<script src="./includes/strategy/json.js" language="javascript"></script>



  </head>
  <body onload="loadStrategyShell();" onkeyup="Capture_Keys();" style="background-color:#e0e0e0;">

	<script language="javascript">

	var st = null;
 
	    
	var sname = "<%= stratTable.getSN() %>";
	var sc = "<%= stratTable.getSC() %>";
    var dc = "<%= stratTable.getDC() %>";
    var log = "<%= stratTable.getLOG() %>";
	var ownerName = "babu";

	function loadStrategyShell()
	{
		var DEBUG = false;
		var divStAttachPoint = document.getElementById("divStAttachPoint");

		st = new StrategyTable("<%= stratTable.GUID %>");
		st.Init(DEBUG,divStAttachPoint,ownerName,sname);
		st.strategyList.setStrategyJSON(sc);
		st.decisionAreaContainer.setDecisionAreaJSON(dc);
		st.setLog(log);
		st.setSaveCallback(saveCallback);
		st.Display();
		
		//determine if there is any strategy table 'data'
		if( TheCurrentEditor().CountRows() > 0 )  //count # viz strategies 
		{
			// switch to print view (aka Matrix view)
			TheCurrentEditor().ChangeCurrEditorMode( TheCurrentEditor().PRINT );
			//adjust the pulldown to read correctly.
			TheCurrentEditor().modeSel.selectedIndex = TheCurrentEditor().PRINT;
		}
	}


    function saveCallback(ast)
    {
        Form1.hdnAction.value = "save";
        Form1.hdnGUID.value = ast.getGUID();
        
        var out = ast.strategyList.getStrategyJSON();
		out += "^";
		out += ast.decisionAreaContainer.getDecisionAreaJSON();
		out += "^";
		out += ast.strategyList.getLogJSON();
		
		Form1.hdnST.value = TheUte().encode64(out);
        Form1.submit();
    
    }

	function setSer()
	{
		alert("WARNING newly loaded table will not function properly and only test once per page load. \r\n\r\n only a test load of " + Form1.txtIn.value );
		var bits =  Form1.txtIn.value.split("^");

		scJSON = bits[0];
		decisionJSON = bits[1];

		loadStrategyShell();


	}

	function getSer()
	{
		var out = st.strategyList.getStrategyJSON();
		out += "^";
		out += st.decisionAreaContainer.getDecisionAreaJSON();


		Form1.txtOut.value = out;

	}

	function Capture_Keys()
	{
		//send key events to the strategy table.

		if(st != null)
		{
			 st.CaptureKeys();
		}


	}

	</script>
    <form id="Form1" method="post" runat="server">



        <div id="divToolBarAttachPoint" runat="server" style="display:none;">
        </div>
        
         <div id="divMsgAttachPoint" runat="server" class="clsMsg">
        </div>
        
        <div id='divStAttachPoint' class="clsStage"></div>
        <div id='oPalette' class="clsStageUpper"></div>
        
        <div id="divBodyAttachPoint" runat="server" class="clsBody">
        </div>
        <div id="divFooter" runat="server" style="display:none;">
        </div>
        

		
		<input type="hidden" name="hdnST" value="NOT_SET" id="hdnST"/>
		<input type=hidden name="hdnGUID" value="NOT_SET" id="hdnGUID" />
		<input type=hidden name="hdnAction" value="NOT_SET" id="hdnAction" />
		
     </form>

  </body>
</html>
