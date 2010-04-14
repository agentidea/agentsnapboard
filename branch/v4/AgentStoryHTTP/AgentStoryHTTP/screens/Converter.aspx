<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Converter.aspx.cs" Inherits="AgentStoryHTTP.screens.Converter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
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

            //(val,id,focusHandler,blurHandler,className,title)
            var txtClear = TheUte().getInputBox("", "txtClear", null, changeTextTo64, "clsInputBox", "clear text here");
            var txt64 = TheUte().getInputBox("", "txt64", null, changeTextToClear, "clsInputBox", "base 64 text here");

            var ap = document.getElementById("dvAttachPoint");
            ap.appendChild(txtClear);
            ap.appendChild(txt64);

        }
        
        
    
    
    
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvAttachPoint">
    
    </div>
    </form>
</body>
</html>
