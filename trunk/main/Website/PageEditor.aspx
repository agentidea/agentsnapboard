<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageEditor.aspx.cs" Inherits="PageEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AgentStory - Page Editor</title>
    <link href="main.css" rel="stylesheet" type="text/css" />

    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="includes/ContextMenu.js" language="javascript" type="text/javascript"></script>
    <script src="includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="includes/PageBuilder.js" language="javascript" type="text/javascript"></script>
     
    <style type="text/css">

#dropmenudiv{
position:absolute;
border:1px solid black;
border-bottom-width: 0;
font:normal 12px Verdana;
line-height:18px;
z-index:100;
}

#dropmenudiv a{
width: 100%;
display: block;
text-indent: 3px;
border-bottom: 1px solid black;
padding: 1px 0;
text-decoration: none;
font-weight: bold;
}

#dropmenudiv a:hover{ /*hover background color*/
background-color: yellow;
}

</style>





    
<script language="javascript" type="text/javascript">
function init()
{
    
    var pageGrid = new flexGrid();
    var divAttachPoint = pageGrid.utils.findElement("divPageGridAttachPoint","div");
    pageGrid.Render(3,5,divAttachPoint);
    
    
    var values = new Array();
    values[0] = document.createTextNode("zero");
    values[1] = document.createTextNode("one");
    values[2] = document.createTextNode("two");
    values[3] = document.createTextNode("three");
    values[9] = document.createTextNode("nine");
    values[12] = document.createTextNode("twelve");
    
    
    
    var oGrid2 = newGrid2("tst",5,4,values);
    //oGrid2.border =1;
    oGrid2.initializeGrid( oGrid2 );
    divAttachPoint.appendChild( oGrid2.gridTable );
    
    //can we bind to this other grid?
    
    //pageGrid.utils.setGridCell("tst",1,2,)
    divAttachPoint.appendChild( pageGrid.StoryCellWidgetGrid("bbb").gridTable );
    divAttachPoint.appendChild( pageGrid.StoryCellWidgetGrid("bbb").gridTable );
    divAttachPoint.appendChild( pageGrid.StoryCellWidgetGrid("bbb").gridTable );
    divAttachPoint.appendChild( pageGrid.StoryCellWidgetGrid("bbb").gridTable );
    
}


</script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
    <div>
    
    <a href="default2.htm" onClick="return dropdownmenu(this, event, menu2, '200px')" onMouseout="delayhidemenu()">News Sites</a> 

    </div>
    
    <div id="divPageGridAttachPoint">
    </div>
    </form>
</body>
</html>
