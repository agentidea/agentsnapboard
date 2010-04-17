<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Converter.aspx.cs" Inherits="AgentStoryHTTP.screens.Converter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
   
    <script  type="text/javascript" src="../includes/grid.js"></script> 
    <script  type="text/javascript" src="../includes/PageUtils.js"></script>
    <script type="text/javascript" language="javascript">



        changeTextTo64 = function() {

            var txtClear = document.getElementById("txtClear");
            var txt64 = document.getElementById("txt64");

            txt64.value = TheUte().encode64(txtClear.value);


        }

    changeTextToClear = function() {

    var txtClear = document.getElementById("txtClear");
    var txt64 = document.getElementById("txt64");

    txtClear.value = TheUte().decode64(txt64.value);


    }



    window.onload = function() {


        var values = new Array();

        var clear = document.createTextNode("Clear Text");
        var txtClear = TheUte().getInputBox("", "txtClear", null, changeTextTo64, "clsInputBox", "clear text here");

        txtClear.style.width = 500;
        txtClear.style.backgroundColor = "#ffffcc";

        var enc = document.createTextNode("base 64 encoded");
        var txt64 = TheUte().getInputBox("", "txt64", null, changeTextToClear, "clsInputBox", "base 64 text here");
        txt64.style.width = 500;
        txt64.style.backgroundColor = "#ffffcc";
        
        values.push(clear);
        values.push(txtClear);
        values.push(enc);
        values.push(txt64);

        var g = newGrid2("grdInput", 2, 2, values, 1, "xyz");

        initializeGrid(g);


        var ap = document.getElementById("dvAttachPoint");
        ap.appendChild(g.gridTable);

    }
        
        
    
    
    
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
  <h3>  base 64 converter</h3>
    <div id="dvAttachPoint">
    
    </div>
    </form>
</body>
</html>
